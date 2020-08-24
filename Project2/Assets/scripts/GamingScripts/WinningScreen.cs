using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinningScreen : MonoBehaviour
{
    public int level;
    public const int maxLevel= 3;
    public Text moneyEarned;
    // Start is called before the first frame update
    void Start()
    {
        moneyEarned.text = "+" + Player.money.ToString();
    }

    public void onNextLevel()
    {
        Time.timeScale = 1f;
        if (level < maxLevel)
        {
            Loader.Load("Level " + (level + 1));
        }
    }
    public void onBack()
    {
        Time.timeScale = 1f;

        Loader.Load("Level Select");
    }
    
}
