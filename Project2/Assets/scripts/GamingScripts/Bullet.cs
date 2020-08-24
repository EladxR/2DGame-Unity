using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public string bulletDirection;
    public float speedBullet=1;
    public float damage;
    public bool playerBullet; // player or enemy bullet;
    public float degreeBullet = 15;
    private float degreeBulletInRedians;

    // Start is called before the first frame update
    void Start()
    {
        degreeBulletInRedians = (float)(Math.PI * degreeBullet / 180);
    }

    // Update is called once per frame
    void Update()
    {
        if (bulletDirection.Equals("Up"))
        {
            this.transform.position = this.transform.position + new Vector3(0, speedBullet*50 * Time.deltaTime, 0);
        }
        else if(bulletDirection.Equals("Down"))
        {
            this.transform.position = this.transform.position + new Vector3(0, speedBullet*(-50) * Time.deltaTime, 0);

        }else if (bulletDirection.Equals("Right"))
        {
            this.transform.position = this.transform.position + new Vector3((float)Math.Sin(degreeBulletInRedians) * speedBullet * 50 * Time.deltaTime,(float)Math.Cos(degreeBulletInRedians) * speedBullet * 50 * Time.deltaTime, 0);


            // this.transform.position = this.transform.position + new Vector3(0.268f*speedBullet * 50 * Time.deltaTime, speedBullet * 50 * Time.deltaTime, 0);
        }
        else if (bulletDirection.Equals("Left"))
        {
            this.transform.position = this.transform.position + new Vector3(-(float)Math.Sin(degreeBulletInRedians) * speedBullet * 50 * Time.deltaTime, (float)Math.Cos(degreeBulletInRedians) * speedBullet * 50 * Time.deltaTime, 0);

            //this.transform.position = this.transform.position + new Vector3(-0.268f * speedBullet * 50 * Time.deltaTime, speedBullet * 50 * Time.deltaTime, 0);

        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.tag == "Wall")
        { 
            Destroy(this.gameObject);
        }
        if (other.tag == "enemy" && playerBullet)
        {
            Destroy(this.gameObject);
        }
        if (other.tag == "Player" && !playerBullet)
        {
            // get player and change hp:
            Player p = other.GetComponent<Player>();
            p.hp -= this.damage;
            p.hpBar.setHealth((int)(p.hp));
            // change color and play hit sound
            p.ColorRed();
            p.PlayHitSound();
            // disapear of bullet
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;

            //destroy with delay of 3 secound for sound effect
            Invoke("DestroyObject", 3f); 

        }
        if (other.tag == "Shield")
        {
            Shield s = other.GetComponent<Shield>();
            if (s.isActive)
            {
                Destroy(this.gameObject);
            }

        }
    }

    private void DestroyObject() // destroy the the current object
    {
        Destroy(this.gameObject);
    }

}
