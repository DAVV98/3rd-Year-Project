using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snake_attack_collider : MonoBehaviour
{
    private float timer_length = 0.4f;
    private float curr_timer;

    private void Start()
    {
        curr_timer = timer_length;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Move to next floor hub when using ladder
        if (collision.gameObject.tag == "Player")
        {
            GetComponentInParent<ice_snake_controller>().attack = true;
            collision.GetComponent<Player_Controller>().hit = true;
           
        }

        // Move to next floor hub when using ladder
        if (collision.gameObject.tag == "Player_Sleep")
        {
            GetComponentInParent<ice_snake_controller>().attack = false;

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Move to next floor hub when using ladder
        if (collision.gameObject.tag == "Player")
        {
            GetComponentInParent<ice_snake_controller>().attack = false;
           
        }

    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        
        // Move to next floor hub when using ladder
        if (collider.gameObject.tag == "Player")
        {
            if(curr_timer > 0)
            {
                curr_timer -= Time.deltaTime;
            }
            else
            {
                GetComponentInParent<ice_snake_controller>().attack = false;
                curr_timer = timer_length;
            }
            //GetComponentInParent<ice_snake_controller>().attack = false;
            //collider.GetComponent<Player_Controller>().health -= 1;

        }
        

        // Move to next floor hub when using ladder
        if (collider.gameObject.tag == "Player_Sleep")
        {
            GetComponentInParent<ice_snake_controller>().attack = false;

        }
    }
}
