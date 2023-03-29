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

    public void DecreaseEnemyLife(int hit){
        enemyLife.rectTransform.offsetMax = new Vector2(enemyLife.rectTransform.offsetMax.x - hit*10, enemyLife.rectTransform.offsetMax.y);
    }
    public void DecreaseMCLife(int hit){
        mainCharacterLife.transform.localScale = new Vector3(hit, 1.0f);
    }

    public void SetTimer(string text){
        timer.text = text;
    }
}
