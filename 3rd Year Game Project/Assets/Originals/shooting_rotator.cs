using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting_rotator : MonoBehaviour
{
    [Header("Players")]
    private GameObject player; // list of players in game

    [Header("Target Mechanism")]
    public float attack_dist; // max attack range
    public int rotator_level; // floor targetoir is on

    [Header("Dart")]
    public GameObject dart; // shooting prefab
    public Transform dart_start; // pos projectile shoots from
    public GameObject shoot_Direction; // shooting direction
    public float dart_force; // force of shot
    public float dist;

    [Header("Shooting Timer")]
    public float time_between_shots; //time between shots
    public float timer; // shoot timer

    [Header("Sound")]
    public GameObject sound_effect;


    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function Start():
    ///     - Runs at the start of the run time
    ///     - Used for Player set up.
    /// </summary>
    void Start()
    {
        // set initial timer lenght timer
        timer = 0.5f;

    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function Update():
    ///     - Runs at every frame
    ///     - Used to update player.
    /// </summary>
    void Update()
    {
       
        // adds active players to player list
        player = GameObject.FindGameObjectWithTag("Player");

        // aims and shoots
        aim_shoot();


    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function aim_shoot():
    ///     - Find angle to player
    ///     - Fire if timer 0
    /// </summary>
    void aim_shoot()
    {
        // if player exists check diastance to player
        if (player != null)
        {
            // check distance to each player
            dist = Vector2.Distance(transform.position, player.transform.position);
        }
        // if player does noit exit set diatcne to large unrealistic number - avoids errors
        else
        {
            dist = 1000;
        }

        // check distance to player is less than attack distance 
        // if so attack
        if (dist <= attack_dist && rotator_level == player.GetComponent<Player_Controller>().level)
        {
            // Trigonometry to calculate angle to fire - Diagram in report
            float hyp = dist;
            float adj = this.transform.position.y - player.transform.position.y;
            float cos = adj / hyp;
            float inv_cos = Mathf.Acos(cos);
            float angle_deg = inv_cos * Mathf.Rad2Deg;

            // if player to the right negative angle
            if (player.transform.position.x <= this.transform.position.x)
            {
                transform.rotation = Quaternion.Euler(0, 0, -angle_deg);
            }
            // if player to right positve angle
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, angle_deg);
            }

            // if timer at 0 - fire
            if (timer <= 0)
            {
                // instantiate projectile
                
                GameObject dart_objects = Instantiate(dart, dart_start.position, Quaternion.identity);
                // shooting soundeffect
                sound_effect.GetComponent<AudioSource>().Play(); // play cannon sound effect

                // direction to fire
                Vector3 Direction = (shoot_Direction.transform.position - this.transform.position).normalized;

                // make projectile fire
                dart_objects.GetComponent<Rigidbody2D>().AddForce(Direction.normalized * dart_force, ForceMode2D.Impulse);
                   
                // reset timer
                timer = time_between_shots;
            }
            // if timer at > 0
            else
            {
                //reduce timer by a second
                timer -= Time.deltaTime;
            }
        }

    }
    
    // check what floor targetor is on to ensure it is not running when player on another floor.
    private void OnTriggerStay2D(Collider2D collision)
    {
        // Check what level rotator is on and change level var depending on floor.
        if (collision.gameObject.tag == "Level 1")
        {
            rotator_level = 1;
        }

        if (collision.gameObject.tag == "Level 2")
        {
            rotator_level = 2;

        }

        if (collision.gameObject.tag == "Level 3")
        {
            rotator_level = 3;

        }

    }

}
