using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Text timer;

    public float time = 240;
    void Start(){
        SetTimer(time);
    }

    void Update()
    {
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

    public void SetEndMessage(string message){
        timer.text = message;
    }

    private void SetTimer(float time){
        timer.text = time.ToString("f0");
    }

}
