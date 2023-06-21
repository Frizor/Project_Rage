using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuController4 : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject exitMenu;

    private void Start()
    {
        OpenMainMenu();
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

        if (mainMenu.activeSelf)
        {
            OpenExitMenu();
            return;
        }

        if (settingsMenu.activeSelf)
        {
            ReturnToMainMenu();
            return;
        }

        if (exitMenu.activeSelf)
        {
            ReturnToMainMenu();
            return;
        }
    }

    public void OnButtonClick(string buttonName)
    {
        switch (buttonName)
        {
            case "SettingsButton":
                OpenSettingsMenu();
                break;
            case "ExitButton":
                OpenExitMenu();
                break;
            case "BackButton":
                if (settingsMenu.activeSelf)
                {
                    ReturnToMainMenu();
                }
                break;
            case "NoButton":
                if (exitMenu.activeSelf)
                {
                    ReturnToMainMenu();
                }
                break;

            case "ReturnButton":
                if (settingsMenu.activeSelf)
                {
                    ReturnToMainMenu();
                }
                break;
        }
    }

    private void OpenMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        exitMenu.SetActive(false);
    }

    private void OpenSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        exitMenu.SetActive(false);
    }

    private void OpenExitMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(false);
        exitMenu.SetActive(true);
    }

    private void ReturnToMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        exitMenu.SetActive(false);
    }

    public void ButtonScene(int whatScene)
    {
        SceneManager.LoadScene(whatScene);
    }

    public void Quit()
    {
        Debug.Log("Выход из игры");
        Application.Quit();
    }
}