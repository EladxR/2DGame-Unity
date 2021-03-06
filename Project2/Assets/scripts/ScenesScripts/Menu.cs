﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject howToPlayPanel;
   

    public void ToLevelSelect()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Back()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void toShop()
    {
        SceneManager.LoadScene("Shop");
    }
    public void ToMainMenu()
    {
        SceneManager.LoadScene("Main");
    }
    public void Exit()
    {
        Application.Quit(); // quit game (ignore in Edit)
    }
    public void toSettings()
    {
        SceneManager.LoadScene("Settings");
    }
    public void onHowToPlay()
    {
        howToPlayPanel.SetActive(true);
    }
    public void onCloseHowToPlay()
    {
        howToPlayPanel.SetActive(false);
    }
}
