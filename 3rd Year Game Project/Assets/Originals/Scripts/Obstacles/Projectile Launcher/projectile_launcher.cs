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
    public GameObject explosion_audio;
    public bool audio_on;
    public GameObject EXPLOSION_COLLIDER;
    public GameObject Player;
    public int cannon_level;
    private float dist_to_player;

    // awake called before game starts
    // called here object transforms before first instantiation
    void awake()
    {
        timer = time_between_shots; // set timer to time between shots
        clone = (GameObject)Instantiate(projectile); // clone projectile
    }

    private void Start()
    {
        
       
    }

    // Update is called once per frame
    void Update()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        if(Player != null)
        {
            dist_to_player = Vector3.Distance(Player.transform.position, this.transform.position);
        }
        else
        {
            dist_to_player = 1000;
        }
       

        // shoot timer
        // only shoot when timer at zero
        // else timer --
        if (timer <= 0)
        {
            Instantiate(projectile, launch_pos.position, Quaternion.identity); // instaniate projectile
            if (dist_to_player <= 20 && cannon_level == Player.GetComponent<Player_Controller>().level)
            {
                explosion_audio.GetComponent<AudioSource>().Play(); // play cannon sound effect
            }

            timer = time_between_shots; // timer = start time
        }
        else
        {
            timer -= Time.deltaTime; // reduce timer 
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Check what level cannon is on and change level var depending on floor.
        if (collision.gameObject.tag == "Level 1")
        {
            cannon_level = 1;
        }

        if (collision.gameObject.tag == "Level 2")
        {
            cannon_level = 2;

        }

        if (collision.gameObject.tag == "Level 3")
        {
            cannon_level = 3;

        }


        if (collision.gameObject.tag == "Level 4")
        {
            cannon_level = 4;

        }

    }

    private void OnDrawGizmos()
    {
        // set gizmo colour
        Gizmos.color = Color.green;


        // attack overlap circle
        Gizmos.DrawWireSphere(this.transform.position, 20);
    }
}
