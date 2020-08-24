using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerUp : MonoBehaviour
{
    public SpriteRenderer sp;
    public float fallingSpeed;
    public int type; // 0-hp , 1-speed, 2-BulletSpeed, 3-iconShield, 4-bulletUpgrade, 5- missile, 6-attack boost
    // images:
    public Sprite iconHp; //0
    public Sprite iconSpeed; //1
    public Sprite iconBulletSpeed; //2
    public Sprite iconShield; //3
    public Sprite iconBulletUpgrade; //4
    public Sprite iconMissile; //5
    public Sprite iconAttackBoost; //6

    public AudioSource soundPowerUp; // sound of picking up


    // Start is called before the first frame update
    void Start()
    {
        //choose image on create
        switch (type){
            case 0:
                sp.sprite = iconHp;
                break;
            case 1:
                sp.sprite = iconSpeed;
                break;
            case 2:
                sp.sprite = iconBulletSpeed;
                break;
            case 3:
                sp.sprite = iconShield;
                break;
            case 4:
                sp.sprite = iconBulletUpgrade;
                break;
            case 5:
                sp.sprite = iconMissile;
                break;
            case 6:
                sp.sprite = iconAttackBoost;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // always moving down
        this.transform.position = this.transform.position + new Vector3(0, (-fallingSpeed) * Time.deltaTime, 0);

    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Wall")
        {
            // destroy objects outside the frame
            Destroy(this.gameObject);
        }
        if (other.tag == "Player")
        {
            // sound effect
            PlaySound();
          
            // disapear of powerUp
            sp.enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            //destroy with delay of 3 secound for sound effect
            Invoke("DestroyObject", 3f);
            
            //get player and use power up
            Player p = other.GetComponent<Player>();
            switch (type)
            {
                case 0:
                    p.IncreaseHP(50+10*p.poweruplevels[0]); //heal formula: 50+10*healLevel
                    break;
                case 1:
                    p.IncreaseSpeed(1.3f);
                    break;
                case 2:
                    p.IncreaseBulletSpeed(1.4f);
                    break;
                case 3:
                    p.startShield();
                    break;
                case 4:
                    p.bulletLevel++;
                    break;
                case 5:
                    p.IncreaceMissilecount(1);
                    break;
                case 6:
                    p.AttackBoost();
                    break;
            }
            
        }
    }

    private void PlaySound()
    {
        SettingsData data = SaveSystem.LoadSettings();
        if (data != null)
        {
            if (data.soundEffectMute)
            {
                soundPowerUp.volume = 0;
            }
            else
            {
                soundPowerUp.volume = 0.28f * data.soundEffectVol; // normalize by 0.28
            }
        }
        soundPowerUp.Play();
    }

    private void DestroyObject() // destroy the the current object
    {
        Destroy(this.gameObject);
    }
}
