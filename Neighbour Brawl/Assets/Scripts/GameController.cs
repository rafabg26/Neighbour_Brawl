using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    

    private UIController ui;
    
    private void Start(){
        ui = GetComponent<UIController>();
    }

    public void SetTimer( float v){
        ui.SetTimer(v);
    }

    public void Quit(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
