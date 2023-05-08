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
    public float retreatRange = 3.5f;


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
        if (SceneManager.GetActiveScene().name != "Level0" && SceneManager.GetActiveScene().name != "Level0Refact")
        {
            float distance = Vector3.Distance(transform.position, player.position);
            if (currentHealth > maxHealth * 0.6f)
            {
                // Comportamiento del enemigo cuando su salud es mayor al 60%
                if (!isKnockedBack && distance > attackRange)
                {
                    moveDirection = new Vector3(Mathf.Sign(player.position.x - transform.position.x), 0, 0);
                    transform.position += moveDirection * moveSpeed * Time.deltaTime;
                }

                if (distance < attackRange && Time.time > nextAttackTime && !isAttacking)
                {
                    nextAttackTime = Time.time + attackCooldown;
                    isAttacking = true;
                    anim.SetBool("attacking", true);
                    Invoke(nameof(ResetAttack), 0.5f);
                    playerDamage.TakeDamage(5);

                    isKnockedBack = true;
                    knockbackEndTime = Time.time + knockbackDuration;
                    knockbackDirection = new Vector3(-Mathf.Sign(moveDirection.x), 0, 0);

                    //Vector3 scale = transform.localScale;
                    //scale.x *= -1;
                    //transform.localScale = scale;
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

                        Vector3 scale = transform.localScale;
                        scale.x *= -1;
                        transform.localScale = scale;
                    }
                }   
                else
                {
                    // Comportamiento del enemigo después de atacar al jugador
                    if (Time.time > nextAttackTime && !isAttacking)
                    {
                        moveDirection = new Vector3(Mathf.Sign(player.position.x - transform.position.x), 0, 0);
                        transform.position += moveDirection * moveSpeed * Time.deltaTime;
                    }
                }
            }
            else if (currentHealth > maxHealth * 0.3f) 
            {
                // Comportamiento del enemigo cuando su salud está entre el 30% y el 60%
                if (distance < attackRange && Time.time > nextAttackTime && !isAttacking)
                {
                    nextAttackTime = Time.time + attackCooldown;
                    isAttacking = true;
                    anim.SetBool("attacking", true);
                    Invoke(nameof(ResetAttack), 0.5f);
                    playerDamage.TakeDamage(10);

                    isKnockedBack = true;
                    knockbackEndTime = Time.time + knockbackDuration;
                    knockbackDirection = new Vector3(-Mathf.Sign(moveDirection.x), 0, 0);

                    //Vector3 scale = transform.localScale;
                    //scale.x *= -1;
                    //transform.localScale = scale;
                }
                else
                {
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

                            Vector3 scale = transform.localScale;
                            scale.x *= -1;
                            transform.localScale = scale;
                        }
                    }
                    else if (distance < retreatRange)
                    {
                        moveDirection = new Vector3(-Mathf.Sign(player.position.x - transform.position.x), 0, 0);
                        transform.position += moveDirection * moveSpeed * Time.deltaTime;
                    }
                    else if (distance > attackRange)
                    {
                        moveDirection = new Vector3(Mathf.Sign(player.position.x - transform.position.x), 0, 0);
                        transform.position += moveDirection * moveSpeed * Time.deltaTime;
                    }
                }
            }
            else
            {
                // Comportamiento del enemigo cuando su salud es menor al 30%
                if (distance < retreatRange)
                {
                    moveDirection = new Vector3(-Mathf.Sign(player.position.x - transform.position.x), 0, 0);
                    transform.position += moveDirection * moveSpeed * Time.deltaTime;
                }
                else if (distance > attackRange)
                {
                    moveDirection = new Vector3(Mathf.Sign(player.position.x - transform.position.x), 0, 0);
                    transform.position += moveDirection * moveSpeed * Time.deltaTime;
                }
            }
        }
    }
    private void ResetAttack()
    {
        isAttacking = false;
        if(anim != null) anim.SetBool("attacking", false);
         Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    public void Damaged(int damage){
        if(anim != null) anim.SetTrigger("hit");
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


