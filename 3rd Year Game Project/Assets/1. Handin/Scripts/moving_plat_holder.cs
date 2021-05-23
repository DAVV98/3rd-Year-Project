using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving_plat_holder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // if collision with player or stop_projectile block:
        //      - Set platform as parent so children move with platform
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player_Sleep" || collision.gameObject.tag == "Stop_Projectiles")
        {
            collision.transform.SetParent(transform);
            collision.GetComponent<Player_Controller>().onMovingPlat = true; 
        }
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function OnCollisionExit2D(): 
    ///     Parameter:
    ///     - Collision (Collision2D): collider of object collided with
    ///     
    ///     Role:
    ///     - check if collsion with another collider has stopped
    /// </summary>

    private void OnTriggerExit2D(Collider2D collision)
    {
        // if collision with player or stop_projectile block stops:
        //      - Remove platform as parent

        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Player_Sleep" || collision.gameObject.tag == "Stop_Projectiles")
        {
            collision.transform.SetParent(null);
            collision.GetComponent<Player_Controller>().onMovingPlat = false;
        }
    }
}
