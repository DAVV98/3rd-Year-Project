using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snake_attack_collider : MonoBehaviour
{
    /// <summary>
    /// Function OnTriggerEnter2D(): 
    ///     Parameter:
    ///     - Collision (Collider2D): collider of object collided with
    ///     
    ///     Role:
    ///     - check if collsion with trigger collider.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Move to next floor hub when using ladder
        if (collision.gameObject.tag == "Player")
        {
            GetComponentInParent<ice_snake_controller>().attack = true;
        }

        // Move to next floor hub when using ladder
        if (collision.gameObject.tag == "Player_Sleep")
        {
            GetComponentInParent<ice_snake_controller>().attack = false;
        }
    }

    /// <summary>
    /// Function OnTriggerExit2D(): 
    ///     Parameter:
    ///     - Collision (Collider2D): collider of object collided with
    ///     
    ///     Role:
    ///     - check if stopped collsion with trigger collider.
    /// </summary>
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Move to next floor hub when using ladder
        if (collision.gameObject.tag == "Player")
        {
            GetComponentInParent<ice_snake_controller>().attack = false;
        }
    }
}
