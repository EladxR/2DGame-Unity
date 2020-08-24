using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyingPowerupScript : MonoBehaviour
{
    public Text healText;
    public Text healTextBtn;
    public Text shieldText;
    public Text shieldTextBtn;
    public Text atkBoostText;
    public Text atkBoostTextBtn;

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

    private void UpdateTexts()
    {
        if (data != null) //if data is null, first texts already set (money should be 0 and cant buy anyway)
        {
            healText.text = $"increases heal from {50+10*data.powerupLevels[0]} to {10+ 50 + 10 * data.powerupLevels[0]}"; // heal: 50+10*lvl
            shieldText.text = $"increases shield duration from {6+data.powerupLevels[1]} seconds to {1+ 6 + data.powerupLevels[1]} seconds"; // shield duration: 6+lvl
            atkBoostText.text = $"increases attack boost from {5 + 5 * data.powerupLevels[2]} to {5 + 5 + 5 * data.powerupLevels[2]}"; // attack boost: 5+5*lvl
            healTextBtn.text = 100 + 50 * data.powerupLevels[0] + "$"; //  the cost is 100+50*lvl
            shieldTextBtn.text = 100 + 50 * data.powerupLevels[1] + "$"; // the cost is 100+50*lvl
            atkBoostTextBtn.text = 100 + 50 * data.powerupLevels[2] + "$"; //  the cost is 100+50*lvl
            shopScript.UpdateMoneyText(Player.money);
        }
    }

  /*  public void onShieldBuy() ////////////// no need , have normal buy
    {

        if (data != null && Player.money >= 100 + 50 * (player.poweruplevels[1]))
        {
            player.poweruplevels[1]++; // update level
            Player.money -= (int)(100 + 50 * (player.poweruplevels[1]-1));
            SaveSystem.SavePlayer(player, Player.money);
            data = SaveSystem.LoadPlayer();
            UpdateTexts();
        }
        else
        {
            // inform message
            Debug.Log("not enough money for Shield upgrade");
        }
    }
    public void onRepairBuy()
    {
        if (data != null && Player.money >= 100 + 50 * (player.poweruplevels[0]))
        {
            player.poweruplevels[0]++; // update level
            Player.money -= (int)(100 + 50 * (player.poweruplevels[0] - 1));
            SaveSystem.SavePlayer(player, Player.money);
            data = SaveSystem.LoadPlayer();
            UpdateTexts();
        }
        else
        {
            // inform message
            Debug.Log("not enough money for Repair upgrade");

        }
    }

    public void OnAtkBoostBuy()
    {
        if (data != null && Player.money >= 100 + 50 * (player.poweruplevels[2]))
        {
            player.poweruplevels[2]++; // update level
            Player.money -= (int)(100 + 50 * (player.poweruplevels[2] - 1));
            SaveSystem.SavePlayer(player, Player.money);
            data = SaveSystem.LoadPlayer();
            UpdateTexts();
        }
        else
        {
            // inform message
            Debug.Log("not enough money for Repair upgrade");

        }
    }*/

    public void onNormalBuy(int type) //normal buy cost is 100,150,200... // type 0,1,2 as above
    {
        if (data != null && Player.money >= 100 + 50 * (player.poweruplevels[type]))
        {
            player.poweruplevels[type]++; // update level
            Player.money -= (int)(100 + 50 * (player.poweruplevels[type] - 1));
            SaveSystem.SavePlayer(player, Player.money);
            data = SaveSystem.LoadPlayer();
            UpdateTexts();
        }
        else
        {
            // inform message
            Debug.Log("not enough money for " + type + " upgrade");

        }
    }
}
