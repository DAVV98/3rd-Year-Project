using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snake_attack_collider : MonoBehaviour
{
    // time between attacks
    private float timer_length = 0.4f;
    // current time
    private float curr_timer;

    private void Start()
    {
        // set current time to timer length
        curr_timer = timer_length;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // when colliding with player
        if (collision.gameObject.tag == "Player")
        {
            // tell snake to attack - for animation
            GetComponentInParent<ice_snake_controller>().attack = true;
            // tell player its been hit
            collision.GetComponent<Player_Controller>().hit = true;
           
        }

        // Move to next floor hub when using ladder
        if (collision.gameObject.tag == "Player_Sleep")
        {
            // if collision with sleep player no attack
            GetComponentInParent<ice_snake_controller>().attack = false;

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Move to next floor hub when using ladder
        if (collision.gameObject.tag == "Player")
        {
            // if collsion with player stops - stop attack mode
            GetComponentInParent<ice_snake_controller>().attack = false;
           
        }

    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        
        // Move to next floor hub when using ladder
        if (collider.gameObject.tag == "Player")
        {
            // countdown timer
            if(curr_timer > 0)
            {
                curr_timer -= Time.deltaTime;
            }
            // if timer <=0
            else
            {
                // stop attack
                GetComponentInParent<ice_snake_controller>().attack = false;
                // restet timer
                curr_timer = timer_length;
            }


        }

        // if collision with sleep player no attack
        if (collider.gameObject.tag == "Player_Sleep")
        {
            GetComponentInParent<ice_snake_controller>().attack = false;

        }
    }
}
