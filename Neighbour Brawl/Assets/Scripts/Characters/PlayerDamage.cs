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
    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _body = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

    }
    public void TakeDamage(int damage){
        _body.velocity = new Vector2(0,0);;
        _anim.ResetTrigger("Punch");
        _anim.SetTrigger("Damaged");
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0){
            GameController.Instance.EndLevel("YOU LOSE");
        }
    }
}
