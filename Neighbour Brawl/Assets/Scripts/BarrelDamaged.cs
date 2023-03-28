using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelDamaged : MonoBehaviour
{

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        anim = GetComponent<Animator>();
    }

    //Cuando el barril es golpeado trigerea la animación
    public void Damaged(){
        anim.SetTrigger("hit");
    }

}
