using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelScript : MonoBehaviour
{
    //variables
    public GameObject MainMenu;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameObject PauseMenu;
    public GameObject PauseMenuOptions;
    public GameObject PauseMenuHTP;
    public GameObject PauseMenuCredits;

    public bool isGamePaused = false;
    public bool options = false;
    public bool credits = false;
    public bool htp = false;

    // put in those functions
    public void GoToScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        Debug.Log("Application has Quit!");
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void ShowMainMenu() // start on main menu
    {
        Time.timeScale = 0f;
        MainMenu.SetActive(true);
        PauseMenu.SetActive(false);
        PauseMenuOptions.SetActive(false);
        PauseMenuHTP.SetActive(false);
        PauseMenuCredits.SetActive(false);

    }

    public void StartGame() // button to play game on main menu
    {
        MainMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame() // pause the game
    {
        Time.timeScale = 0f; // stop timer
        MainMenu.SetActive(false);
        PauseMenu.SetActive(true); // pause panel on
        PauseMenuOptions.SetActive(false);
        PauseMenuHTP.SetActive(false);
        PauseMenuCredits.SetActive(false);
    }

    public void OpenCreditsMenu() // open credits 
    {
        MainMenu.SetActive(false);
        PauseMenu.SetActive(false);
        PauseMenuOptions.SetActive(false);
        PauseMenuHTP.SetActive(false);
        PauseMenuCredits.SetActive(true); // credits panel on
    }

    public void OpenOptionsMenu() // open options
    {
        MainMenu.SetActive(false);
        PauseMenu.SetActive(false);
        PauseMenuOptions.SetActive(true); // options panel on
        PauseMenuHTP.SetActive(false);
        PauseMenuCredits.SetActive(false);
    }

    public void OpenHTPMenu() // how to play (paused)
    {
        MainMenu.SetActive(false);
        PauseMenu.SetActive(false);
        PauseMenuOptions.SetActive(false);
        PauseMenuHTP.SetActive(true); // HTP panel on
        PauseMenuCredits.SetActive(false);
    }
    public void ResumeGame() // turn everything off
    {
        isGamePaused = false;
        Time.timeScale = 1f;
        MainMenu.SetActive(false);
        PauseMenu.SetActive(false);
        PauseMenuOptions.SetActive(false);
        PauseMenuHTP.SetActive(false);
        PauseMenuCredits.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 0f;
        ShowMainMenu();
    }
}
