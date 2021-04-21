using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_launcher : MonoBehaviour
{
    [Header("Shooting Timer")]
    public float time_between_shots;
    private float timer;

    [Header("Projectiles")]
    public GameObject projectile; // projectile ejcted from launcher
    private GameObject clone; // clone of projectile
    public Transform launch_pos; // position to launch projectile from

    // awake called before game starts
    // called here object transforms before first instantiation
    void awake()
    {
        timer = time_between_shots; // set timer to time between shots
        clone = (GameObject)Instantiate(projectile); // clone projectile
    }

    // Update is called once per frame
    void Update()
    {
        // shoot timer
        // only shoot when timer at zero
        // else timer --
        if (timer <= 0)
        {
            Instantiate(projectile, launch_pos.position, Quaternion.identity); // instaniate projectile
            timer = time_between_shots; // timer = start time
        }
        else
        {
            timer -= Time.deltaTime; // reduce timer 
        }
    }
}
