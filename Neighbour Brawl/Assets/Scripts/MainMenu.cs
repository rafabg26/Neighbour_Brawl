using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject creditsMenu;
    void Start(){
        menu.SetActive(true);
        creditsMenu.SetActive(false);
    }
    public void Salir()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    public void CargarLevel0()
    {
        SceneManager.LoadScene("Level0");
    }
    public void CargarLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void CargarCreditos(){
        menu.SetActive(false);
        creditsMenu.SetActive(true);
    }
    public void CreditosAMenu(){
        menu.SetActive(true);
        creditsMenu.SetActive(false);
    }
}
