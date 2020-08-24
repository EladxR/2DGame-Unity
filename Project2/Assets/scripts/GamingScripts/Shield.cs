using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool isActive;
    public SpriteRenderer sp;
    public float ShieldTime; //in seconds
    private float startTime;

    // fields for shield flickering
    private const float beforeEndTime=2f;
    private const float changingTime= 0.2f;
    private float lastTimeChange=0;
    private bool isAppear = true;


    // Start is called before the first frame update
    void Start()
    {
        isActive = false;
        sp.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            float currTime = Time.time - startTime;
            if (ShieldTime - beforeEndTime <= currTime && currTime < ShieldTime)
            {
                if (Time.time - lastTimeChange >= changingTime)
                {
                    //change from appear to disappear and so on
                    if (isAppear)
                    {
                        this.GetComponent<SpriteRenderer>().enabled = false;
                        isAppear = false;
                    }
                    else
                    {
                        this.GetComponent<SpriteRenderer>().enabled = true;
                        isAppear = true;
                    }
                    lastTimeChange = Time.time;
                }
            }
            if (currTime >= ShieldTime)
            {
                ShieldDown();
            }
        }
    }

    private void ShieldDown()
    {
        isActive = false;
        sp.enabled = false;
       
    }

    internal void ShieldUP()
    {
        isActive = true;
        sp.enabled = true;
        startTime = Time.time;
    }
}
