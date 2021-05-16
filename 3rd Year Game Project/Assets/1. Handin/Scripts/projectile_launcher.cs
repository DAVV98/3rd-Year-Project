using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile_launcher : MonoBehaviour
{
    [Header("Shooting Timer")]
    public float time_between_shots; // time between fired projectiles
    private float timer; // current time

    [Header("Projectiles")]
    public GameObject projectile; // projectile ejcted from launcher
    public Transform launch_pos; // position to launch projectile from
    public GameObject explosion_audio; // audio of fired shot
    public bool audio_on; // turn audio on
    public GameObject EXPLOSION_COLLIDER; // explosion cillider
    public GameObject Player; // active player 
    public int cannon_level; // check what floor cannon is on
    private float dist_to_player; // distance toa ctive player

    // awake called before game starts
    // called here object transforms before first instantiation
    void awake()
    {
        timer = time_between_shots; // set timer to time between shots
    }

    // Update is called once per frame
    void Update()
    {
        // set player as active player
        Player = GameObject.FindGameObjectWithTag("Player");

        // if player exists find distance
        if(Player != null)
        {
            dist_to_player = Vector3.Distance(Player.transform.position, this.transform.position);
        }
        // if no player set distance to unimaginable value
        else
        {
            dist_to_player = 10000;
        }
       

        // shoot timer
        // only shoot when timer at zero
        // else timer --
        if (timer <= 0)
        {
            Instantiate(projectile, launch_pos.position, Quaternion.identity); // instaniate projectile
            
            // only play sound effect is player is close to cannon and on same floor - avioid cray cray noise
            if (dist_to_player <= 20 && cannon_level == Player.GetComponent<Player_Controller>().level)
            {
                explosion_audio.GetComponent<AudioSource>().Play(); // play cannon sound effect
            }
            
            // reset timer
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
