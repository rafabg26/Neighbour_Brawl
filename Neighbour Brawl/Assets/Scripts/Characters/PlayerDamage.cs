using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    
    public HealthBar healthBar;
    private Rigidbody2D _body;
    public int maxHealth = 100;
    private int currentHealth;
    private Animator _anim;
    private Timer label;

    public bool _canBeHit;

    private Color colorInicial;
    public Color colorGolpe = new Color(1f, 0.5f, 0.5f); // Define el color de golpe
    

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        colorInicial = GetComponent<Renderer>().material.color;
        healthBar.SetMaxHealth(maxHealth);

    }
    public void TakeDamage(int damage){

        if(IsTouchingGround()){

        _body.velocity = new Vector2(0,0);
        _anim.ResetTrigger("Punch");
        _anim.SetTrigger("Damaged");
        StopCoroutine("HitCoroutine"); 
        StartCoroutine("HitCoroutine");

        } else {

            GetComponent<Renderer>().material.color = colorGolpe; 
            StartCoroutine(Espera());  // Agrega una corutina para devolver el color a su valor inicial después de un tiempo

        }

        GetComponent<Renderer>().material.color = colorInicial;

        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0){
            GameController.Instance.EndLevel("YOU LOSE");
        }
    }

    private IEnumerator HitCoroutine() {

        _canBeHit = false;
        _body.velocity = Vector2.zero;
        _body.isKinematic = true;
        float originalGravityScale = _body.gravityScale;
        _body.gravityScale = 0f;
        
        // Mover hacia atrás en la dirección opuesta a la que está mirando
        float moveDistance = 0.5f;
        float moveTime = 0.1f;
        float originalX = transform.position.x;
        float moveDirection = transform.localScale.x < 0 ? -1 : 1;
        float targetX = originalX + (moveDirection * moveDistance);

        float elapsedTime = 0f;
        while (elapsedTime < moveTime) {
            float newX = Mathf.Lerp(originalX, targetX, elapsedTime / moveTime);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        _body.isKinematic = false;
        _body.gravityScale = originalGravityScale;
        _canBeHit = true;

        GetComponent<Renderer>().material.color = colorInicial;
            
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            // Código que se ejecutará cuando haya una colisión con el personaje
            TakeDamage(10);

            colorInicial = GetComponent<Renderer>().material.color;
            GetComponent<Renderer>().material.color = colorGolpe;

            StartCoroutine("HitCoroutine");
            
        }
    }

    private bool IsTouchingGround() {
        int layerMask = LayerMask.GetMask("Ground");
        Collider2D collider = GetComponent<Collider2D>();
        return collider.IsTouchingLayers(layerMask);
    }

    private IEnumerator Espera() {
        yield return new WaitForSeconds(0.4f);
    }
}
