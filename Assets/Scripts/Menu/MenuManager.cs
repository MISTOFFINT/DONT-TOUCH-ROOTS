using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject MainMenu;
    public GameObject SettingsMenu;

    public GameObject ManagementPanel;
    public GameObject GraphicPanel;
    public GameObject AudioPanel;

    public void Settings_btn()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
        ManagementPanel.SetActive(true);
        return;
    }

    public void BackMenu_btn()
    {
        SettingsMenu.SetActive(false);
        ManagementPanel.SetActive(false);
        GraphicPanel.SetActive(false);
        AudioPanel.SetActive(false);
        MainMenu.SetActive(true);
        return;
    }

    public void ManagementPanel_btn()
    {
        GraphicPanel.SetActive(false);
        AudioPanel.SetActive(false);
        ManagementPanel.SetActive(true);
        return;
    }

    public void GraphicPanel_btn()
    {
        ManagementPanel.SetActive(false);
        AudioPanel.SetActive(false);
        GraphicPanel.SetActive(true);
        return;
    }

    public void AudioPanel_btn()
    {
        ManagementPanel.SetActive(false);
        GraphicPanel.SetActive(false);
        AudioPanel.SetActive(true);
        return;
    }

    public void PlayGame()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
