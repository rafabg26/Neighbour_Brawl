using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private RawImage enemyLife;
    [SerializeField] private RawImage mainCharacterLife;
    [SerializeField] private TextMeshProUGUI timer;
    private float maxLife;

    public void DecreaseEnemyLife(int hit){
        enemyLife.rectTransform.offsetMax = new Vector2(enemyLife.rectTransform.offsetMax.x - hit*10, enemyLife.rectTransform.offsetMax.y);
    }
    public void DecreaseMCLife(int hit){
        mainCharacterLife.transform.localScale = new Vector3(hit, 1.0f);
    }
    public void ReloadEnemyLife(){
        enemyLife.rectTransform.offsetMax = new Vector2(maxLife, enemyLife.rectTransform.offsetMax.y);
    }

    public void SetTimer(string text){
        timer.text = text;
    }
    
    void Start(){
        maxLife = enemyLife.rectTransform.offsetMax.x;
    }
}
