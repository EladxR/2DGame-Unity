using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Ship ship;
    public Text healthText;
    public bool showNumber=false;

    private void Start()
    {
        slider.maxValue = ship.maxHP;
        setHealth((int)slider.maxValue);
    }

    public void setMaxHealth(int max)
    {
        slider.maxValue = max;
    }
    public void setHealth(int health)
    {
        slider.value = health;
        if (showNumber)
        {
            healthText.text = health + "/" + ship.maxHP;
        }
    }
}
