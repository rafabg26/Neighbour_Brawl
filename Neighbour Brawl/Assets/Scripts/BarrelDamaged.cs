using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelDamaged : MonoBehaviour
{

    private Animator anim;
    private SceneController controller;

    // Start is called before the first frame update
    public void Start() {
        controller = GameObject.Find("SceneController").GetComponent<SceneController>();
    }

    // Update is called once per frame
    void Update()
    {
        anim = GetComponent<Animator>();
        // if(controller.getEnemyLife() <= 0){
        //     anim.SetBool("dead" , true);
        // }
    }

    //Cuando el barril es golpeado trigerea la animación
    public void Damaged(){
        anim.SetTrigger("hit");
        if(controller.getEnemyLife()>0)  controller.DecreaseEnemyLife(10);
    }

}
