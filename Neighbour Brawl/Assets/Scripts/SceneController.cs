using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    private float timer = 120;
    public int enemyLife = 100;

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
        }else if(enemyLife > 0){
            GameController.Instance.SetTimer(timer);
        }
    }

    public void DecreaseEnemyLife(float damage)
    {
        enemyLife -= (int)damage;
        GameController.Instance.DecreaseEnemyLife(damage);
        if(enemyLife <= 0)
            GameController.Instance.EndLevel("YOU WIN");
      
    }
}
