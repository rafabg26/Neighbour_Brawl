using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDamaged : MonoBehaviour
{

    private Animator anim;
    private SceneController controller;
    public Transform player;
    public HealthBar healthBar;
    public PlayerDamage playerDamage;
    public int maxHealth = 100;
    public float moveSpeed = 5f;
    public float attackRange = 2f;
    public float attackDamage = 10f;
    public float attackCooldown = 1f;
    public float knockbackDistance = 2f;
    public float knockbackDuration = 1f;
    public int maxRegenerationSpan = 5;
    public bool canRegenerate = false;

    private int currentHealth;
    private int regenerationSpan;
    private bool isAttacking = false;
    private float nextAttackTime = 0f;
    private bool isKnockedBack = false;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 knockbackDirection = Vector3.zero;
    private float knockbackEndTime = 0f;




    // Start is called before the first frame update
    public void Start()
    {
        regenerationSpan = maxRegenerationSpan;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        moveDirection = new Vector3(Mathf.Sign(player.position.x - transform.position.x), 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        anim = GetComponent<Animator>();
        if(canRegenerate) Regenerate();
        if (SceneManager.GetActiveScene().name != "Level0")
        {
            float distance = Vector3.Distance(transform.position, player.position);

            if (!isKnockedBack && distance > attackRange)
            {
                moveDirection = new Vector3(Mathf.Sign(player.position.x - transform.position.x), 0, 0);
                transform.position += moveDirection * moveSpeed * Time.deltaTime;
            }

            // Ataque del enemigo al jugador           
            if (distance < attackRange && Time.time > nextAttackTime && !isAttacking)
            {
                nextAttackTime = Time.time + attackCooldown;
                isAttacking = true;
                anim.SetBool("attacking", true);
                Invoke(nameof(ResetAttack), 0.5f);
                //HAY QUE REFACTORIZAR
                playerDamage.TakeDamage(10);

                isKnockedBack = true;
                knockbackEndTime = Time.time + knockbackDuration;
                knockbackDirection = new Vector3(-Mathf.Sign(moveDirection.x), 0, 0);


                // Invertir la escala del sprite del enemigo para que mire hacia la dirección en la que retrocede
                Vector3 scale = transform.localScale;
                scale.x *= -1;
                transform.localScale = scale;
            }

            if (isKnockedBack)
            {
                if (Time.time < knockbackEndTime)
                {
                    transform.position += knockbackDirection * moveSpeed * Time.deltaTime;
                }
                else
                {
                    isKnockedBack = false;
                    moveDirection = new Vector3(Mathf.Sign(player.position.x - transform.position.x), 0, 0);

                    // Invertir la escala del sprite del enemigo para que mire hacia la dirección en la que avanza
                    Vector3 scale = transform.localScale;
                    scale.x *= -1;
                    transform.localScale = scale;
                }
            }
        }
    }

    private void ResetAttack()
    {
        isAttacking = false;
        anim.SetBool("attacking", false);
    }

    public void Damaged(int damage){
        anim.SetTrigger("hit");
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if((int)currentHealth <= 0) {
            GameController.Instance.EndLevel("YOU WIN");
        };
    }

//NO FUNCIONA CORRECTAMENTE
    private void  Regenerate(){
        float aux = regenerationSpan -  Time.deltaTime;
        regenerationSpan = (int)aux;
        Debug.Log(regenerationSpan);
        if(regenerationSpan <= 0){
            regenerationSpan = maxRegenerationSpan;
            Heal(maxHealth);
        }
    }
    private void Heal(int life){
        if(currentHealth + life >= maxHealth){
            currentHealth = maxHealth;
        }else{
            currentHealth+= life;
        }
        healthBar.SetHealth(currentHealth);
    }
}


