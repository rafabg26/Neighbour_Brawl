using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject menu;
    public GameObject creditsMenu;
    public GameObject splash;
    void Start(){
        splash.SetActive(true);
        menu.SetActive(false);
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
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Level0", LoadSceneMode.Single);
    }
    public void CargarLevel1()
    {
        SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("Level0Refact", LoadSceneMode.Single);
    }
    public void CargarCreditos(){
        menu.SetActive(false);
        creditsMenu.SetActive(true);
    }
    public void CreditosAMenu(){
        menu.SetActive(true);
        creditsMenu.SetActive(false);
    }
    public void SplashAMenu(){
        menu.SetActive(true);
        splash.SetActive(false);
    }
}
