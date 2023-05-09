using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{

    public Timer label;
    public LevelLoaderScript levelLoader;
    private bool _paused;
    public bool paused {
        get { return _paused; }
        set {
            _paused = value;
            Time.timeScale = value ? 0.0f : 1.0f;

        }
    }

    public void EndLevel(bool didWin){
        if(didWin){
            StartCoroutine(pauseGame("You win", 1.0f));

        }else{
            StartCoroutine(pauseGame("You lose", 1.0f));
        }
    }


    // public void ReloadEnemyLife(){
    //     if(SceneManager.GetActiveScene().name == "Level0"){
    //         ui.ReloadEnemyLife();
    //     }
    // }

    private IEnumerator pauseGame(string message, float waitSeconds ){
        label.SetEndMessage(message);
        yield return new WaitForSeconds(waitSeconds);
        
        if(message == "You win"){
            levelLoader.LoadSceneByName("YouWin");
        }else if(message == "You lose"){
            levelLoader.LoadSceneByName("YouLose");
        }
        label.SetEndMessage("");
    }
    private void GoToNextScene(){
        Debug.Log("ME TRIGUEREÉ");
        // if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCountInBuildSettings - 1) {
        //     Quit();
        // } else {
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        // }
    }
   

    public void Quit(){
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
