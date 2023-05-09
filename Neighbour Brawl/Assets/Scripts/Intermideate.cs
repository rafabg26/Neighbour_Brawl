using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intermideate : MonoBehaviour
{
    public LevelLoaderScript levelLoader;
    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "YouWin" || SceneManager.GetActiveScene().name == "YouLose"){
            if(Input.anyKeyDown)
            {
                //Now check if your key is being pressed
                if (!Input.GetKeyDown(KeyCode.Escape))
                {
                    levelLoader.NextOrRestart();
                }
            }
        }
    }
}
