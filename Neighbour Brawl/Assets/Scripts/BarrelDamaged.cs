using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelDamaged : MonoBehaviour
{

    private Animator anim;
    public int barrelLife = 100;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim = GetComponent<Animator>();
        Debug.Log(barrelLife);
        if(barrelLife <= 0){
            anim.SetBool("dead" , true);
        }
    }

    //Cuando el barril es golpeado trigerea la animación
    public void Damaged(){
        anim.SetTrigger("hit");
        barrelLife -= 10; 
    }

}
