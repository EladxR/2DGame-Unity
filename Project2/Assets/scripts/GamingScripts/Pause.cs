using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pause : MonoBehaviour
{
    public static bool gameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject OptionsMenu;
    private void Start()
    {
        pauseMenu.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                onResume();
            }
            else
            {
                onPause();
            }
        }
    }
    public void onPause() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        gameIsPaused = true;

    }
    public void onResume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    public void onQuit()
    {
        onResume();
        Loader.Load("Level Select");
    }
    public void openOptions()
    {
        OptionsMenu.SetActive(true);
    }
    public void closeOptions()
    {
        OptionsMenu.GetComponent<SettingsScript>().Save();
        OptionsMenu.SetActive(false);

    }
}
