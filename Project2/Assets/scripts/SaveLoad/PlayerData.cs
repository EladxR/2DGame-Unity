using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PlayerData 
{
    public float health; //start: 100
    public float attack; // start: 10
    public int[] powerupLevels; //start: {0,0} $rep {heal level, shield level, attackBoost level}
    public int money; // start:0
    public int startMissiles; // start: 0
    public float MissileDamage; //start: 20
    public int level; // the highest level reached, start: 1
    

    public PlayerData(Player player,int money,int level)
    {
        this.health = player.maxHP;
        this.attack = player.bulletDamage;
        this.powerupLevels = player.poweruplevels;
        this.money = money;
        this.startMissiles = player.startMissiles;
        this.MissileDamage = player.MissileDamage;
        this.level = level;

    }
    
}
