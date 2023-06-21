using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuController4 : MonoBehaviour
{
    public GameObject menuLevel;
    public GameObject gameOptionsMenuPanel;
    public GameObject optionsGamePanel;
    public GameObject settingsGamePanel;
    public GameObject exitPanel;

    private enum MenuState { Game, Options, Settings, Exit }

    private MenuState currentMenuState;

    private void Start()
    {
        OpenGameMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            HandleEscKey();
        }
    }

    private void HandleEscKey()
    {
        switch (currentMenuState)
        {
            case MenuState.Game:
                OpenOptionsGamePanel();
                break;
            case MenuState.Options:
                ReturnToGameMenu();
                break;
            case MenuState.Settings:
                ReturnToOptionsGamePanel();
                break;
            case MenuState.Exit:
                ReturnToGameMenu();
                break;
        }
    }

    public void OnGameOptionsMenuButtonClick()
    {
        OpenOptionsGamePanel();        
    }

    public void OnSettingsGameButtonClick()
    {
        OpenSettingsGamePanel();
    }

    public void OnInMainMenuButtonClick()
    {
        ReturnToMainMenu();
    }

    public void OnReturnGameButtonClick()
    {
        ReturnToGameMenu();
    }

    public void OnVolumeButtonClick()
    {
        // Handle volume button click
    }

    public void OnGraphicsButtonClick()
    {
        // Handle graphics button click
    }

    public void OnExitButtonClick()
    {
        OpenExitPanel();
    }

    public void OnYesButtonClick()
    {
        QuitGame();
    }

    public void OnNoButtonClick()
    {
        ReturnToGameMenu();
    }

    private void OpenGameMenu()
    {
        menuLevel.SetActive(true);
        gameOptionsMenuPanel.SetActive(true);
        optionsGamePanel.SetActive(false);
        settingsGamePanel.SetActive(false);
        exitPanel.SetActive(false);
        currentMenuState = MenuState.Game;
    }

    private void OpenOptionsGamePanel()
    {
        optionsGamePanel.SetActive(true);
        settingsGamePanel.SetActive(false);
        exitPanel.SetActive(false);
        currentMenuState = MenuState.Options;
    }

    private void OpenSettingsGamePanel()
    {
        settingsGamePanel.SetActive(true);
        optionsGamePanel.SetActive(false);
        exitPanel.SetActive(false);
        currentMenuState = MenuState.Settings;
    }

    private void ReturnToOptionsGamePanel()
    {
        settingsGamePanel.SetActive(false);
        optionsGamePanel.SetActive(true);
        exitPanel.SetActive(false);
        currentMenuState = MenuState.Options;
    }

    private void ReturnToGameMenu()
    {
        optionsGamePanel.SetActive(false);
        settingsGamePanel.SetActive(false);
        exitPanel.SetActive(false);
        currentMenuState = MenuState.Game;
    }

    private void OpenExitPanel()
    {
        optionsGamePanel.SetActive(false);
        settingsGamePanel.SetActive(false);
        exitPanel.SetActive(true);
        currentMenuState = MenuState.Exit;
    }

    private void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void QuitGame()
    {
        Debug.Log("Выход из игры");
        Application.Quit();
    }
}
