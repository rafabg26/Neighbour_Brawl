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

    private bool _canBeHit = true;
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }
    public void TakeDamage(int damage){
        if (_canBeHit) {
            _body.velocity = new Vector2(0,0);
            _anim.SetTrigger("Damaged");
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            if(currentHealth <= 0){
                GameController.Instance.EndLevel("YOU LOSE");
            }
            StopCoroutine("HitCoroutine"); // Detiene cualquier instancia anterior de la corrutina
            StartCoroutine("HitCoroutine");
        }
    }

    private IEnumerator HitCoroutine() {
        _canBeHit = false;  // Desactiva la capacidad de ser golpeado
        _body.velocity = Vector2.zero;  // Detiene cualquier movimiento del personaje
        _body.isKinematic = true;  // Desactiva temporalmente el Rigidbody
        float originalGravityScale = _body.gravityScale;  // Guarda el valor original de la gravedad
        _body.gravityScale = 0f;  // Desactiva temporalmente la gravedad

        float originalX = transform.position.x;
        float targetX = transform.position.x - 0.5f;
        float elapsedTime = 0f;
        float moveTime = 0.10f;  // Tiempo total de movimiento hacia atrás
        float speed = (targetX - originalX) / moveTime;  // Velocidad de movimiento hacia atrás

        while (elapsedTime < moveTime) {
            float newX = transform.position.x - (speed * Time.deltaTime * -1.5f);
            transform.position = new Vector3(newX, transform.position.y, transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _body.isKinematic = false;  // Reactiva el Rigidbody
        _body.gravityScale = originalGravityScale;  // Reactiva la gravedad
        _canBeHit = true;  // Activa la capacidad de ser golpeado nuevamente
    }
    
}
