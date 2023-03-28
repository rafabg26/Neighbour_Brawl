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
        enemyLife.transform.localScale = new Vector3(hit, 1.0f);
    }
    public void DecreaseMCLife(int hit){
        mainCharacterLife.transform.localScale = new Vector3(hit, 1.0f);
    }

    public void SetTimer(float time){
        Debug.Log("CULO");
        timer.text = ""+ time.ToString("f0");
    }
}
