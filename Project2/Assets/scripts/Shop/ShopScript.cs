using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public Text moneyText;
    public GameObject PlayerUp;
    public GameObject PowerupUp;
    public Button PlayerBtn;
    public Button PowerupBtn;
   
    // Start is called before the first frame update
    void Start()
    {
        //load money
        PlayerData data = SaveSystem.LoadPlayer();
        if (data == null)
        {
            moneyText.text = "0"; // no saves
        }
        else
        {
          
            moneyText.text = data.money.ToString();
        }

        //set as player category selected
        ChangeCategory("Player");


    }

   
    public void UpdateMoneyText(int money) 
    {
        moneyText.text = money.ToString();
    }
    public void ChangeCategory(string category)
    {
        if (category.Equals("Player"))
        {
            PlayerUp.SetActive(true);
            PowerupUp.SetActive(false);
            PlayerBtn.GetComponent<Image>().color =Color.gray;
            PowerupBtn.GetComponent<Image>().color =Color.white;

        }
        else if (category.Equals("Powerup"))
        {
            PlayerUp.SetActive(false);
            PowerupUp.SetActive(true);
            PlayerBtn.GetComponent<Image>().color = Color.white;
            PowerupBtn.GetComponent<Image>().color = Color.gray;
        }
    }
   
}
