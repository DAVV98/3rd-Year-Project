﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_launcher : MonoBehaviour
{
    [Header("Shooting Timer")]
    public float time_between_shots;
    private float timer;

    [Header("Projectiles")]
    public GameObject projectile;
    private GameObject clone;
    public Transform launch_pos;

    // Start is called before the first frame update
    void Start()
    {
        timer = time_between_shots;
        clone = (GameObject)Instantiate(projectile);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0)
        {
            Instantiate(clone, launch_pos.position, Quaternion.identity);
            timer = time_between_shots;
        }
        else
        {
            timer -= Time.deltaTime;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Stop_Projectiles")
        {
            Destroy(this.gameObject);
        }
    }
}
