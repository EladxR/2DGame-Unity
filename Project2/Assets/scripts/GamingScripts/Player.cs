using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Ship
{
    public float speed;
    public Bullet bullet;
    public MissleExplosion missile;
    private bool canCreateBullet = true;
    private float timeForBullet;
    public float bulletSpeed;
    private float stopperForBullet = 0;
    public GameObject RightWall;
    public GameObject LeftWall;
    public Shield shield;
    public AudioSource soundHit;
    public GameObject explosion;
    private bool dead = false; // die only once
    internal int bulletLevel=0;
    public int[] poweruplevels;
    private const int powerupsUpgradeableCount=3;
    public static int money = 0; //defalute money if cant load
    public Text moneyText;
    public int startMissiles;
    private int currMissiles;
    public Text missileCount;
    public float MissileDamage;

    private const float maxSpeed=400;

    public GameManager gm;
    public bool playing = true;

    private void Awake()
    {
        money = 0;
        //loading data in awake so it happans before ant other start
        PlayerData data = SaveSystem.LoadPlayer();
        if (data != null)
        {
            this.maxHP = data.health;
            this.bulletDamage = data.attack;
            // money = data.money; // no need to load here. its just the money in this level
            poweruplevels = data.powerupLevels;
            startMissiles = data.startMissiles;
            MissileDamage = data.MissileDamage;
            if (playing)
            {
                shield.ShieldTime = 6 + poweruplevels[1]; // shield time formula: 6+level, (6,7,8..)
            }
        }
        else
        {
            poweruplevels = new int[powerupsUpgradeableCount];
            startMissiles = 0;
            MissileDamage = 20;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP; //starts with full hp

        timeForBullet = 0.2f * 1 / bulletSpeed; //time between bullets according to bullet speed

        currMissiles = startMissiles;
        missile.missileDamage = this.MissileDamage;
        if (playing)
        {
            UpdateMissileText();
        }



    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            moneyText.text = money.ToString();
            if (!Pause.gameIsPaused && !dead) // will not move if game paused or died
            {
                if (Input.GetKey("d"))
                {
                    if (this.transform.position.x < RightWall.transform.position.x)
                    {
                        this.transform.position = this.transform.position + new Vector3(0.3f * speed * Time.deltaTime, 0, 0);
                    }
                }
                if (Input.GetKey("a"))
                {
                    if (this.transform.position.x > LeftWall.transform.position.x)
                    {
                        this.transform.position = this.transform.position + new Vector3(-0.3f * speed * Time.deltaTime, 0, 0);
                    }
                }
                if (Input.GetKey("space"))
                {
                    if (!canCreateBullet)
                    {
                        if (Time.time - stopperForBullet >= timeForBullet)
                        {
                            canCreateBullet = true;
                            stopperForBullet = 0;
                        }
                    }

                    //shoot
                    if (canCreateBullet)
                    {
                        bullet.speedBullet = 1f;
                        bullet.damage = this.bulletDamage;
                        bullet.playerBullet = true;
                        if (bulletLevel == 0 || bulletLevel == 2)
                        {
                            //Up bullet:
                            bullet.bulletDirection = "Up";
                            Instantiate(bullet, this.transform.position + new Vector3(0, 10, 0), new Quaternion());
                        }
                        else //bulet level ==1 or >=3
                        {
                            //Up bullet:
                            bullet.bulletDirection = "Up";
                            Instantiate(bullet, this.transform.position + new Vector3(2, 10, 0), new Quaternion());
                            //Up bullet:
                            // bullet.bulletDirection = "Up";
                            Instantiate(bullet, this.transform.position + new Vector3(-2, 10, 0), new Quaternion());

                        }
                        if (bulletLevel >=2)
                        {
                            bullet.degreeBullet = 15;
                            //right bullet:
                            bullet.bulletDirection = "Right";
                            Instantiate(bullet, this.transform.position + new Vector3(5, 10, 0), Quaternion.Euler(0, 0, -15));
                            //left bullet:
                            bullet.bulletDirection = "Left";
                            Instantiate(bullet, this.transform.position + new Vector3(-5, 10, 0), Quaternion.Euler(0, 0, 15));
                        }
                        if (bulletLevel >= 4)
                        {
                            bullet.degreeBullet = 20;
                            //right bullet:
                            bullet.bulletDirection = "Right";
                            Instantiate(bullet, this.transform.position + new Vector3(8, 10, 0), Quaternion.Euler(0, 0, -15));
                            //left bullet:
                            bullet.bulletDirection = "Left";
                            Instantiate(bullet, this.transform.position + new Vector3(-8, 10, 0), Quaternion.Euler(0, 0, 15));
                        }


                        canCreateBullet = false;
                        stopperForBullet = Time.time;
                    }
                }

                if (Input.GetKeyDown("c")) // shoot missile if have
                {
                    //check if have missiles
                    if (currMissiles >= 1)
                    {
                        Instantiate(missile, this.transform.position + new Vector3(0, 10, 0), new Quaternion());
                        currMissiles -= 1;
                        UpdateMissileText();
                    }


                }
            }
        }



    }

    internal void IncreaceMissilecount(int num)
    {
        currMissiles += num;
        UpdateMissileText();
    }

    private void UpdateMissileText()
    {
        missileCount.text = "x " + currMissiles;
    }

    internal void AttackBoost()
    {
        //each level adds 5 more to attack
        this.bulletDamage += 5 + 5 * poweruplevels[2];
    }

    internal void ColorRed()
    {
        this.GetComponent<SpriteRenderer>().color = Color.red;
        Invoke("ColorNormal", 0.5f);
    }

    internal void ColorNormal()
    {
        this.GetComponent<SpriteRenderer>().color = Color.white;
    }

    internal void startShield()
    {
        shield.ShieldUP();
    }

    internal void IncreaseBulletSpeed(float v) // multiply speed by v
    {
        bulletSpeed *= v;
        timeForBullet = 0.2f * 1 / bulletSpeed;
    }

    internal void IncreaseSpeed(float v) // multiply speed by v
    {
        speed *= v;
        if (speed > maxSpeed)
        {
            speed = maxSpeed;
        }
    }

    internal void IncreaseHP(int number)
    {
        float newHP = this.hp + number;
        if (newHP > this.maxHP)
        {
            newHP = this.maxHP;
        }
        this.hp = newHP;
        this.hpBar.setHealth((int)newHP);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hp <= 0)
        {
            if (!dead)
            {
                dead = true;
                ///Game Over
                ExplodeAndDestroy();
                gm.GameLose();
                Debug.Log("Lose");
            }
        }
    }

    private void ExplodeAndDestroy()
    {
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<BoxCollider2D>().enabled = false;
        explosion.SetActive(true);
    }

    public void PlayHitSound()
    {

        SettingsData data = SaveSystem.LoadSettings();
        if (data != null)
        {
            if (data.soundEffectMute)
            {
                soundHit.volume = 0;
            }
            else
            {
                soundHit.volume = 0.2f * data.soundEffectVol; // normalize by 0.2
            }
        }
        soundHit.Play();


    }

    public void SetMoney(int m) //no need?
    {
        money = m;
        moneyText.text = m.ToString();
    }
}
