using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int totalEnemies;
    public int startingTotalEnemies;
    public GameObject winningScreen;
    public GameObject losingScreen;
    public static bool isWin;
    public Player player;
    private bool entered = false;

    private void Awake()
    {
        totalEnemies = startingTotalEnemies;
        isWin = false;
        
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
      //  winningScreen.SetActive(false);
        losingScreen.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        //check if no enemies left -> win
        if (!entered && totalEnemies <= 0)
        {
            entered = true;
            GameWin();
        }
       
    }

    private void GameWin()
    {
        isWin = true;
        // Time.timeScale = 0f; -on animation
        //save:
        int afterMoney = 0;
        int level = 1;
        PlayerData data = SaveSystem.LoadPlayer();
        if (data == null)
        {
            afterMoney = Player.money; //was 0 now this
            player.bulletDamage = 10; // player attack wont increase from attack boost after a level
        }
        else
        {
            afterMoney = data.money + Player.money;
            player.bulletDamage = data.attack; // player attack wont increase from attack boost after a level
            level = data.level;
        }
        int currlevel = int.Parse(SceneManager.GetActiveScene().name.Remove(0, 6)); // getting current level from the scene name (maintain name: "Level X")
        if (currlevel+1 > level) // the current level won bigger than highest level reached before
        {
            level = currlevel + 1; //open the next level
        }

        SaveSystem.SavePlayer(player, afterMoney, level); //saves only the money and the level
        winningScreen.SetActive(true);
        Debug.Log("Win");
    }

    internal void GameLose()
    {
        Time.timeScale = 0f;
        losingScreen.SetActive(true);
    }
}
