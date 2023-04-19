using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyDamaged : MonoBehaviour
{

    private Animator anim;
    private SceneController controller;
    public Transform player;
    public float moveSpeed = 5f;
    public float attackRange = 2f;
    public float attackDamage = 10f;
    public float attackCooldown = 1f;
    public float knockbackDistance = 2f;
    public float knockbackDuration = 1f;

    private bool isAttacking = false;
    private float nextAttackTime = 0f;
    private bool isKnockedBack = false;
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 knockbackDirection = Vector3.zero;
    private float knockbackEndTime = 0f;

    // Start is called before the first frame update
    public void Start()
    {
        controller = GameObject.Find("SceneController").GetComponent<SceneController>();
        moveDirection = new Vector3(Mathf.Sign(player.position.x - transform.position.x), 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        anim = GetComponent<Animator>();

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
                if (controller.getMCLife() > 0) { controller.DecreaseMCLife(attackDamage); }

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

    public void Damaged(){
        anim.SetTrigger("hit");
        if(controller.getEnemyLife()>0)  controller.DecreaseEnemyLife(10);
    }
}


