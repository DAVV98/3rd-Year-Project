﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coin_block : MonoBehaviour
{
    [Header("Health")]

    public int startHealth;
    public int health;

    [Header("Box Stages")]
    public GameObject[] stages;

    [Header("Prizes")]
    public GameObject[] prizes;
    public bool callOnce;

    [SerializeField]
    void Start()
    {
        health = startHealth;

        callOnce = true;
    }
    
    // Update is called once per frame
    void Update()
    {
        change_stages();
    }

    void change_stages()
    {
       if(health == startHealth)
        {
            stages[0].SetActive(true);

            stages[1].SetActive(false);
            stages[2].SetActive(false);
            stages[3].SetActive(false);


        }
       else if(health == 2)
        {
            stages[1].SetActive(true);

            stages[0].SetActive(false);
            stages[2].SetActive(false);
            stages[3].SetActive(false);
        }
        else if (health == 1)
        {
            stages[2].SetActive(true);

            stages[1].SetActive(false);
            stages[0].SetActive(false);
            stages[3].SetActive(false);
        }
        else
        {
           
            stages[3].SetActive(true);
            stages[1].SetActive(false);
            stages[2].SetActive(false);
            stages[0].SetActive(false);
            
            if (callOnce == true)
            {
                activateCoin();
            }
            
            callOnce = false;
           
        }
    }

    void activateCoin()
    {
        int randomIndex = Random.Range(0, prizes.Length);
        
        prizes[randomIndex].SetActive(true);


       
    }
}
