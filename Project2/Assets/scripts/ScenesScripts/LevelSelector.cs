using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    private int maxLevel;
    public GridLayoutGroup levelsGrid;
    private void Awake() // before other's start
    {
        PlayerData data = SaveSystem.LoadPlayer();
        if (data == null)
        {
            maxLevel = 1;
        }
        else
        {
            maxLevel = data.level;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
       
        // set level backgroud: lock or unlocked
        Transform[] levelsBG = levelsGrid.GetComponentsInChildren<Transform>(); // get array of all the children of the grid
        int levelsPassed = 0;
        for (int i = 0; i < levelsBG.Length; i++) // go through all possible childers of grid
        {
            Transform levelLocked = levelsBG[i].Find("LevelLocked");
            if (levelLocked != null) // found level locked background
            {
                levelLocked.gameObject.SetActive(false); // remove the lock
                levelsPassed++;
                if (levelsPassed >= maxLevel) break; // stop when opened maxLevel levels
            }
        }
    
    }
    
    
    public void StartLevel(int level)
    {
        if (level <= maxLevel)
        {
            Loader.Load("Level " + level);
        }
        else
        {
            Debug.Log("pass prev level to enter");
        }
    }
    

}
