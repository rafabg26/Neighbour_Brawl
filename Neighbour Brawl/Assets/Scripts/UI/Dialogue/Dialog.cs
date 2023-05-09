using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;
    public float typingSpeed;
    public GameObject dialogueObject;
    public GameObject continueButton;
    public GameObject omitirButton;
    public Animator textDisplayAnim;

    public GameObject protagonist;
    public GameObject granny;

    private GameObject timer;
    private Timer _timer;

    void Start(){
        timer = GameObject.Find("Timer");
        _timer = timer.GetComponent<Timer>();
        _timer.SetActiveTimer(false);
        _timer.ResetTime(240);
        StartCoroutine(Type());
        
        continueButton.SetActive(false);
    }

    void Update() {

        protagonist = GameObject.Find("MainCharacter");
        if (SceneManager.GetActiveScene().name != "Level0Refact")
        {
        granny = GameObject.Find("Granny");
        granny.GetComponent<EnemyDamaged>().enabled = false;
        }

        protagonist.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        if(textDisplay.text == sentences[index]){
            continueButton.SetActive(true);
        }   

    }


    IEnumerator Type(){
        foreach (char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSentence(){
        textDisplayAnim.SetTrigger("NextLine");
        continueButton.SetActive(false);
        if(index < sentences.Length - 1){
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }else{
            textDisplay.text = "";
            _timer.SetActiveTimer(true);
            Destroy(dialogueObject);

            ActivarMovimiento();

        }
    }

    public void OmitirText(){
       Destroy(dialogueObject); 
       _timer.SetActiveTimer(true);
       ActivarMovimiento();
    }

    public void ActivarMovimiento(){
        protagonist.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        granny.GetComponent<EnemyDamaged>().enabled = true;
    }
}
