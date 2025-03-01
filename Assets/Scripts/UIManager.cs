using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject MainMenuHTP;
    public GameObject MainMenuCredits;
    public GameObject WinScreen;
    public GameObject LoseScreen;
    public GameObject PauseMenu;
    public GameObject PauseMenuOptions;
    public GameObject PauseMenuHTP;
    public GameObject PauseMenuCredits;

    public AudioSource buttonSource;


    public bool isGamePaused = false;

    void Start()
    {
        MainMenu.SetActive(false);
        MainMenuHTP.SetActive(false);
        MainMenuCredits.SetActive(false);
        PauseMenu.SetActive(false);
        PauseMenuOptions.SetActive(false);
        PauseMenuHTP.SetActive(false);
        PauseMenuCredits.SetActive(false);
        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);
        ShowMainMenu();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) // go back
        {
            if (isGamePaused)
                ResumeGame();
            else
                PauseGame();
        }

        PauseMenu.SetActive(isGamePaused); // if value is true/false it will activate said panel
        
    }
    public void GoToScene(string sceneName) // switch scenes (USE FOR RESTARTING GAME)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ShowMainMenu() // start on main menu
    {
        Time.timeScale = 0f;
        MainMenu.SetActive(true);
        MainMenuHTP.SetActive(false);
        MainMenuCredits.SetActive(false);
        PauseMenu.SetActive(false);
        PauseMenuOptions.SetActive(false);
        PauseMenuHTP.SetActive(false);
        PauseMenuCredits.SetActive(false);

        WinScreen.SetActive(false);
        LoseScreen.SetActive(false);

    }

    public void StartGame() // button to play game on main menu
    {
        MainMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void PauseGame() // pause the game
    {
        buttonSource.Play();
        Time.timeScale = 0f; // stop timer
        isGamePaused = true;
        MainMenu.SetActive(false);
        MainMenuHTP.SetActive(false);
        MainMenuCredits.SetActive(false);
        PauseMenu.SetActive(true); // pause panel on
        PauseMenuOptions.SetActive(false);
        PauseMenuHTP.SetActive(false);
        PauseMenuCredits.SetActive(false);
    }

    public void OpenCreditsMenu() // open credits  pause
    {
        buttonSource.Play();
        isGamePaused = true;
        MainMenu.SetActive(false);
        MainMenuHTP.SetActive(false);
        MainMenuCredits.SetActive(false);
        PauseMenu.SetActive(false);
        PauseMenuOptions.SetActive(false);
        PauseMenuHTP.SetActive(false);
        PauseMenuCredits.SetActive(true); // credits panel on
    }

    public void OpenOptionsMenu() // open options
    {
        buttonSource.Play();
        isGamePaused = true;
        MainMenu.SetActive(false);
        MainMenuHTP.SetActive(false);
        MainMenuCredits.SetActive(false);
        PauseMenu.SetActive(false);
        PauseMenuOptions.SetActive(true); // options panel on
        PauseMenuHTP.SetActive(false);
        PauseMenuCredits.SetActive(false);
    }

    public void OpenHTPMenu() // how to play (paused)
    {
        buttonSource.Play();
        isGamePaused = true;
        MainMenu.SetActive(false);
        MainMenuHTP.SetActive(false);
        MainMenuCredits.SetActive(false);
        PauseMenu.SetActive(false);
        PauseMenuOptions.SetActive(false);
        PauseMenuHTP.SetActive(true); // HTP panel on
        PauseMenuCredits.SetActive(false);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void ResumeGame() // turn everything off
    {
        buttonSource.Play();
        isGamePaused = false;
        Time.timeScale = 1f;
        MainMenu.SetActive(false);
        MainMenuHTP.SetActive(false);
        MainMenuCredits.SetActive(false);
        PauseMenu.SetActive(false);
        PauseMenuOptions.SetActive(false);
        PauseMenuHTP.SetActive(false);
        PauseMenuCredits.SetActive(false);
    }

    public void ReturnToMainMenu() // for main menu return NOT for restarting the game
    {
        buttonSource.Play();
        Time.timeScale = 0f;
        isGamePaused = false;
        ShowMainMenu();
    }
    // if you want to restart game, link the samplescene using the load scene function

    public void OpenHTPMainMenu()
    {
        buttonSource.Play();
        isGamePaused = false;

        MainMenu.SetActive(false);
        MainMenuHTP.SetActive(true);
        MainMenuCredits.SetActive(false);
        PauseMenu.SetActive(false);
        PauseMenuOptions.SetActive(false);
        PauseMenuHTP.SetActive(false);
        PauseMenuCredits.SetActive(false);
    }

    public void OpenCreditsMainMenu()
    {
        buttonSource.Play();
        isGamePaused = false;

        MainMenu.SetActive(false);
        MainMenuHTP.SetActive(false);
        MainMenuCredits.SetActive(true);
        PauseMenu.SetActive(false);
        PauseMenuOptions.SetActive(false);
        PauseMenuHTP.SetActive(false);
        PauseMenuCredits.SetActive(false);
    }
}
