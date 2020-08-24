using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyingScript : MonoBehaviour
{
    public Text healthText;
    public Text healthTextBtn;
    public Text attackTextBtn;
    public Text playerInfo;
    public Text missileCount;
    public Text missileDmgTextBtn;
    public Text missileCountTextBtn;

    private PlayerData data;
    public Player player;
    public ShopScript shopScript;

    // Start is called before the first frame update
    void Start()
    {
        data = SaveSystem.LoadPlayer();
        
        // for the next save with player, other fields will not change
        if (data != null)
        {
            player.bulletDamage = data.attack;
            player.maxHP = data.health;
            player.poweruplevels = data.powerupLevels;
            Player.money = data.money;
        }

        UpdateTexts();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void UpdateTexts()
    {
        if (data != null) //if data is null, first texts already set (money should be 0 and cant buy anyway)
        {
            healthText.text = data.health + "/" + data.health;
            playerInfo.text = "health: " + data.health + "\n" + "attack: " + data.attack + "\n" + "missile damage: " + data.MissileDamage;
            missileCount.text = "x " + data.startMissiles;
            
            healthTextBtn.text = 100 + 50 * ((data.health - 100) / 20) + "$"; // calc current hp level and the cost is 100+50*lvl
            attackTextBtn.text = 100 + 50 * ((data.attack - 10) / 5) + "$"; // calc current attack level and the cost is 100+50*lvl
            missileDmgTextBtn.text = 100 + 50 * ((data.MissileDamage - 20) / 10) + "$";// calc current missile damage level and the cost is 100+50*lvl
            missileCountTextBtn.text = 100 + 50 * (data.startMissiles)+"$"; // cost is 100+50*lvl
            shopScript.UpdateMoneyText(Player.money); 
        }
    }

    public void onAttackBuy()
    {
        
        if (data!=null && Player.money >= 100 + 50 * ((data.attack - 10) / 5))
        {
            player.bulletDamage = data.attack + 5; // update player attack
            Player.money -= (int)(100 + 50 * ((data.attack - 10) / 5));
            SaveSystem.SavePlayer(player, Player.money);
            data = SaveSystem.LoadPlayer();
            UpdateTexts();
        }
        else
        {
            // inform message
            Debug.Log("not enough money for attack upgrade");
        }
    }
    public void onHealthBuy()
    {
        if (data != null && Player.money >= 100 + 50 * ((data.health - 100) / 20))
        {
            player.maxHP = data.health + 20; // update player health
            Player.money -= (int)(100 + 50 * ((data.health - 100) / 20));
            SaveSystem.SavePlayer(player, Player.money);
            data = SaveSystem.LoadPlayer();
            UpdateTexts();
        }
        else
        {
            // inform message
            Debug.Log("not enough money for health upgrade");

        }
    }

    public void onMissileDamageBuy()
    {
        if (data != null && Player.money >= 100 + 50 * ((data.MissileDamage - 20) / 10))
        {
            player.MissileDamage = data.MissileDamage + 10; // update missile damage
            Player.money -= (int)(100 + 50 * ((data.MissileDamage - 20) / 10));
            SaveSystem.SavePlayer(player, Player.money);
            data = SaveSystem.LoadPlayer();
            UpdateTexts();
        }
        else
        {
            // inform message
            Debug.Log("not enough money for missile damage upgrade");

        }
    }

    public void onMissileCountBuy()
    {
        if (data != null && Player.money >= 100 + 50 * (data.startMissiles))
        {
            player.startMissiles++; // update missile starting count
            Player.money -= (int)(100 + 50 * (data.startMissiles));
            SaveSystem.SavePlayer(player, Player.money);
            data = SaveSystem.LoadPlayer();
            UpdateTexts();
        }
        else
        {
            // inform message
            Debug.Log("not enough money for missile count upgrade");

        }
    }

}
