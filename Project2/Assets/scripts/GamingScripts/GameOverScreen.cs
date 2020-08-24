using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
   



    public void onRestart()
    {
        Time.timeScale = 1f;
        Loader.Load(SceneManager.GetActiveScene().name);
        
    }


    public void onBack()
    {
        Debug.Log("Sacle time to 1");
        Time.timeScale = 1f;

        Loader.Load("Level Select");
    }
}
