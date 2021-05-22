using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class check_ground_Player : MonoBehaviour
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
        // checks if ground bellow is regular ground - if so tells parent
        if (collision.gameObject.layer == 8)
        {
            GetComponentInParent<Player_Controller>().ice_ground = false;
        }
        // checks if ground bellow is icy ground - if so tells parent
        else if (collision.gameObject.layer == 13)
        {
            GetComponentInParent<Player_Controller>().ice_ground = true;
        }
    }
}