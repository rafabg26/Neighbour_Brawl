using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    private float timer = 120;
    public int enemyLife = 100;
    public float countDownReloadLife = 5;
    public float timeSinceLastHit = 0f;

    public int getEnemyLife(){
        return enemyLife;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if((int) timer <= 0 )
        {
            GameController.Instance.EndLevel("YOU LOSE");
        }
        else
        {
            ReloadEnemyLife();
            GameController.Instance.SetTimer(timer);
        }
    }

    public void DecreaseEnemyLife(float damage)
    {
        countDownReloadLife = 5;
        enemyLife -= (int)damage;
        GameController.Instance.DecreaseEnemyLife(damage);
        timeSinceLastHit = 0f;

        if(enemyLife <= 0)
        {
            ReloadEnemyLife();
        }
    }

    public void ReloadEnemyLife()
    {
        timeSinceLastHit += Time.deltaTime;

        // If enough time has passed since last hit, regenerate enemy's health
        if (timeSinceLastHit >= 5f)
        {
            enemyLife = 100;
            GameController.Instance.ReloadEnemyLife();
            timeSinceLastHit = 0f;
        }
    }
}
