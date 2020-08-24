using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;


public class enemyShip : Ship
{
    public float speed; //ship's speed
    private bool moveRight = true; //start moving right always
    private float sum = 0; // total distance traveled each direction
    public float distanceMove; //the max distance to travel each direction and change direction
    public GameObject explosion; //explode image
    public float startShoot, endShoot; // random shoot between startShoot to endShoot
    private float timeNextShot; // the time to shoot that was chosen each time
    private float lastTimeShot; //last time enemy shot
    public Bullet bullet; // the bullet enemy shoot
    private bool dead = false; //die only once
    public BoxCollider2D box; // it's own collider
    public GameObject powerUpDrop;


    // Start is called before the first frame update
    void Start()
    {
        //fiels set up:
        hp = maxHP;
        bullet.bulletDirection = "Down";
        //all enemies start as the just shot so they wont shoot at the beginning
        lastTimeShot = Time.time;
        // random shoot between startShoot to endShoot
        timeNextShot = Random.Range(startShoot, endShoot);
    }

    // Update is called once per frame
    void Update()
    {

        // die //
        if (hp <= 0)
        {
            //die only once
            if (!dead)
            {
                dead = true;
                Player.money += (int)this.maxHP; // increase money by ship's health
                GameManager.totalEnemies -= 1; //total enemies decrease
                //Debug.Log(GameManager.totalEnemies);
                //show explosion
                Instantiate(explosion, this.transform.position, new Quaternion());
                //Drop power up
                Drop();

                Destroy(this.gameObject);

            }
        }

        // shoot //
        //check if time pass to shoot
        if (Time.time - lastTimeShot >= timeNextShot)
        {
            Shoot();
            //randomize next shoot:
            lastTimeShot = Time.time;
            timeNextShot = Random.Range(startShoot, endShoot);

        }



        // move //
        //move right then left and so on
        if (moveRight)
        {
            this.transform.position = this.transform.position + new Vector3(0.3f * speed * Time.deltaTime, 0, 0);
            sum += 0.3f * speed * Time.deltaTime; //total distance travelled this direction
            //checks if needs to change direction:
            if (sum >= distanceMove)
            {
                sum = 0;
                moveRight = false;
            }
        }
        else
        {
            //same as move right 
            this.transform.position = this.transform.position + new Vector3(-0.3f * speed * Time.deltaTime, 0, 0);
            sum += 0.3f * speed * Time.deltaTime;
            if (sum >= distanceMove)
            {
                sum = 0;
                moveRight = true;
            }

        }
    }

    private void Drop()
    {
        float rnd = Random.Range(0f, 1f);
        //random what powerup to drop:
        if (rnd < 0.5f)
        {
            // random power up 
            int randomInt = Random.Range(0, 7); // 7 different powerups
            powerUp pu = powerUpDrop.GetComponent<powerUp>();
            pu.type = randomInt;
            Instantiate(powerUpDrop, this.transform.position, new Quaternion());
        }
        /*  else if(rnd<0.6) //the upgrade bullet power up
          {
              powerUp pu = powerUpDrop.GetComponent<powerUp>();
              pu.type = 4;
              Instantiate(powerUpDrop, this.transform.position, new Quaternion());
          }*/
    }

    private void Shoot()
    {
        // bullet fields:
        bullet.bulletDirection = "Down";
        bullet.speedBullet = 0.5f;
        bullet.damage = bulletDamage;
        bullet.playerBullet = false;
        Instantiate(bullet, this.transform.position + new Vector3(0, -box.size.y * transform.localScale.y / 2 - 5, 0), Quaternion.Euler(0, 0, 180));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "bullet")
        {
            //get bullet type and damage
            Bullet b = other.GetComponent<Bullet>();
            if (b.playerBullet) // only player's bullet can hit enemies
            {
                this.hp -= b.damage;

                this.hpBar.setHealth((int)(this.hp)); // set hp in the bar
            }
        }
        if (other.tag == "Missile")
        {
            other.GetComponent<MissleExplosion>().Explode();
        }
        if (other.tag == "ExplosionCircle")
        {
            float damage = other.GetComponentInParent<MissleExplosion>().missileDamage;
            this.hp -= damage; ///// changeeee

            this.hpBar.setHealth((int)(this.hp)); // set hp in the bar
        }
    }
}
