using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : Singleton<GameController>
{
    

    private UIController ui;

    private bool _paused;
    public bool paused {
        get { return _paused; }
        set {
            _paused = value;
            Time.timeScale = value ? 0.0f : 1.0f;

        }
    }

    private void Start(){
        ui = GetComponent<UIController>();
    }

    public void SetTimer( float v){

        ui.SetTimer(v.ToString("f0"));
        
    }

    public void EndLevel(string message){
            ui.SetTimer(message);
            StartCoroutine(pauseGame(message, 3.0f));
    }

     public void DecreaseEnemyLife(float v){
        ui.DecreaseEnemyLife((int) v);
    }

    private IEnumerator pauseGame(string message, float waitSeconds ){
        ui.SetTimer(message);
        yield return new WaitForSeconds(waitSeconds);
        Quit();
    }

    public void Quit(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
