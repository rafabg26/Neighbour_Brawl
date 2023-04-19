using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public int enemyLife = 100;
    public int mCLife = 100;
    public float countDownReloadLife = 5;
    public float timeSinceLastHit = 0f;
    private bool sceneChanged = false;

    public int getEnemyLife(){
        return enemyLife;
    }

    public int getMCLife(){
        return mCLife;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // timer -= Time.deltaTime;
        // if((int) timer <= 0 )
        // {
        //     GameController.Instance.EndLevel("YOU LOSE");
        // }
        // else
        // {
        //     ReloadEnemyLife();
        //     GameController.Instance.SetTimer(timer);
        // }

        if(sceneChanged){
            enemyLife = 100;
        }
    }

    // public void DecreaseEnemyLife(float damage)
    // {
    //     countDownReloadLife = 5;
    //     enemyLife -= (int)damage;
    //     GameController.Instance.DecreaseEnemyLife(damage);
    //     timeSinceLastHit = 0f;

    //     if(enemyLife <= 0 && SceneManager.GetActiveScene().name == "Level0")
    //     {
    //         ReloadEnemyLife();
    //     }
    // }

    // public void DecreaseMCLife(float damage)
    // {
    //     countDownReloadLife = 5;
    //     mCLife -= (int)damage;
    //     GameController.Instance.DecreaseMCLife(damage);
    //     timeSinceLastHit = 0f;

    //     if(mCLife <= 0)
    //     {
    //         //derrota
    //     }
    // }

    public void ReloadEnemyLife()
    {
        timeSinceLastHit += Time.deltaTime;

        // If enough time has passed since last hit, regenerate enemy's health
        if (timeSinceLastHit >= 5f)
        {
            enemyLife = 100;
            //GameController.Instance.ReloadEnemyLife();
            timeSinceLastHit = 0f;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        sceneChanged = true;
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
