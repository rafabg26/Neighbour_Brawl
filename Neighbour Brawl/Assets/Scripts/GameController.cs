using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{

    public Timer label;
    private bool _paused;
    public bool paused {
        get { return _paused; }
        set {
            _paused = value;
            Time.timeScale = value ? 0.0f : 1.0f;

        }
    }

    public void EndLevel(string message){
        Debug.Log("LO HAS MATAO");
        StartCoroutine(pauseGame(message, 3.0f));
    }


    // public void ReloadEnemyLife(){
    //     if(SceneManager.GetActiveScene().name == "Level0"){
    //         ui.ReloadEnemyLife();
    //     }
    // }

    private IEnumerator pauseGame(string message, float waitSeconds ){
        label.SetEndMessage(message);
        yield return new WaitForSeconds(waitSeconds);
        GoToNextScene();
    }
    private void GoToNextScene(){
        if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1) {
            Quit();
        } else {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void Quit(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
