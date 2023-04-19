using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timer;
    public float time = 240;
    private bool gameEnded;
    void Start(){
        SetTimer(time);
        gameEnded = false;
    }

    void Update()
    {
        if(!gameEnded)DecreaseTime();
    }

    public void SetEndMessage(string message){
        gameEnded = true;
        timer.text = message;
    }

    private void SetTimer(float time){
        timer.text = time.ToString("f0");
    }
    private void DecreaseTime(){
        time -= Time.deltaTime;
        if( time > 0 )
        {
            SetTimer(time);
        }
        else
        {
            SetEndMessage("YOU LOSE");
        }
    }

}
