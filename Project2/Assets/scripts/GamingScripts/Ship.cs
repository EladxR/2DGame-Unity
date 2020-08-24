using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float maxHP=100;
    internal float hp;
    public HealthBar hpBar;
    public float bulletDamage=10; // ship damage (player or enemy)

    // Start is called before the first frame update
    void Start()
    {
        hp = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
