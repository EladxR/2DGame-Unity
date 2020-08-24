using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public static class Loader
{
    private static Action onLoaderCallBack;
    public enum Scene
    {
        Level1,
        Loading,
        
    }
    public static void Load(Scene scene)
    {
        Load(scene.ToString());
    }

    public static void LoaderCallBack()
    {
        // refresh screen - loading
        if (onLoaderCallBack != null)
        {
            onLoaderCallBack();
            onLoaderCallBack = null;
        }
    }

    public static void Load(string sceneName)
    {
        //set Loader to load scene
        onLoaderCallBack = () =>
         {
             SceneManager.LoadScene(sceneName);
         };

        //load Loading scene
        SceneManager.LoadScene(Loader.Scene.Loading.ToString());
    }

}
    
