using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorDamage : MonoBehaviour
{
    
    private Animator anim;
    private SceneController controller;
    public HealthBar healthBar;
    private int currentHealth;
    public int maxHealth = 100;
   




    // Start is called before the first frame update
    public void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        anim = GetComponent<Animator>();
    }

    public void Damaged(int damage){
        if(anim != null) anim.SetTrigger("Hit");
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if((int)currentHealth <= 0) {
            GameController.Instance.EndLevel("YOU WIN");
        };
    }

}
