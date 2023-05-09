using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timer;
    public float time = 240;
    private bool gameEnded;
    private bool activeTimer = true;
    void Start(){
        SetTimer(time);
        gameEnded = false;
    }

    void Update()
    {
        if(!gameEnded && activeTimer)DecreaseTime();
    }

    public void SetEndMessage(string message){
        gameEnded = true;
        timer.text = message;
    }
    public void SetActiveTimer(bool active){
        activeTimer = active;
    }
    public void ResetTime(float time){
        gameEnded = false;
        this.time = time;
        timer.text = time.ToString("f0");
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
