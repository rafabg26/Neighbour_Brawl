using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{

    private bool _paused;
    public bool paused {
        get { return _paused; }
        set {
            _paused = value;
            Time.timeScale = value ? 0.0f : 1.0f;

        }
    }

    public void EndLevel(string message){
            StartCoroutine(pauseGame(message, 3.0f));
    }


    // public void ReloadEnemyLife(){
    //     if(SceneManager.GetActiveScene().name == "Level0"){
    //         ui.ReloadEnemyLife();
    //     }
    // }

    private IEnumerator pauseGame(string message, float waitSeconds ){
        //ui.SetTimer(message);
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
