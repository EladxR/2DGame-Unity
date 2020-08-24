using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleExplosion : MonoBehaviour
{
    public GameObject CircleDamaged;
    public float speedBullet = 1;
    public float explodeTime=1f; //seted to 0.2f
    private float startExplodeTime;
    private bool exploded = false;
    public float missileDamage; // start: 20

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!exploded)
        {
            this.transform.position = this.transform.position + new Vector3(0, speedBullet * 50 * Time.deltaTime, 0);
        }
        if(exploded && Time.time - startExplodeTime >= explodeTime) // wont do more damage after explosion time
        {
            CircleDamaged.GetComponent<Collider2D>().enabled = false;
        }
    }

    public void Explode()
    {
        //disapear
        this.GetComponent<SpriteRenderer>().enabled = false;
        this.GetComponent<Collider2D>().enabled = false;
        Invoke("SelfDestroy", 2f);

        //explode animation:
        CircleDamaged.SetActive(true);
        exploded = true;
        startExplodeTime = Time.time;
    }
    private void SelfDestroy()
    {
        Destroy(this.gameObject);
    }
}
