using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    //Clase que hace referencia a la hitbox del puño. Esta hitbox solamente está activa cuando se realiza la animación del puñetazo

    // Start is called before the first frame update

    private Color colorInicial;
    public Color colorGolpe = new Color(1f, 0.5f, 0.5f); // Define el color de golpe

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Cuando el puño entre en contacto con otro objeto comprobamos que se trata del barril por la tag, si es así se llama
    // a la animación de la clase del barril
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemies")){
            other.GetComponent<EnemyDamaged>().Damaged(10);
            

            //Obtener color inicial del objeto golpeado y cambiar a un color rojizo
            colorInicial = other.GetComponent<Renderer>().material.color;
            other.GetComponent<Renderer>().material.color = colorGolpe; 
                       
        }
    }

     private void OnTriggerExit2D(Collider2D collision)
    {
        //Devolver el objeto golpeado a su color inicial
        collision.GetComponent<Renderer>().material.color = colorInicial;
    }

}
