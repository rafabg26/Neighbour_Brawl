using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderScript : MonoBehaviour
{
    public Animator transition;
    private int lastSceneIndex;
    
    public void LoadSceneByName(string name){
        lastSceneIndex = SceneManager.GetActiveScene().buildIndex;
        StartCoroutine(LoadLevelByName(name));
    }
    IEnumerator LoadLevelByName(string name){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2);
        transition.SetTrigger("End");
        SceneManager.LoadScene(name);
    }
    IEnumerator LoadLevelByIndex(int index){
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(2);
        transition.SetTrigger("End");
        SceneManager.LoadScene(index);
    }
    private void ReloadStage(){
        StartCoroutine(LoadLevelByIndex(lastSceneIndex));
    }
    public void NextOrRestart(){
        //Si estamos en la escena de "YouLose" significa que hemos perdido
        //Por lo que querremos reintentar el nivel
        
        if(SceneManager.GetActiveScene().name == "YouLose"){
            ReloadStage();
        }
        //Si hemos ganado
        else if (SceneManager.GetActiveScene().name == "YouWin"){
            //Y venimos del nivel de la puerta
            Debug.Log("Estamos en you win");
            if(lastSceneIndex==2){
                Debug.Log("El último índice es igual a 2");
                //Cargamos el nivel de la abuela
                LoadSceneByName("Level1");
            //Esto significaría que hemos vencido a la abuela
            }else{
                Debug.Log("Estamos en main menu");
                //Y podriamos volver al principio
                LoadSceneByName("MainMenu");
            }
        }
    }
    
}
