using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public bool isGold;

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
        // if collsion with layer 8 (ground) destroy egg
        if(collision.gameObject.layer == 8)
        {
            Destroy(this.gameObject);
        }

        // if egg hits player
        if(collision.gameObject.tag == "Player")
        {
            // if gold egg add coins
            if(isGold)
            {
                collision.gameObject.GetComponent<Player_Controller>().coins += 6000;
                Destroy(this.gameObject);
            }
            // if hit egg deduct life
            else
            {
                collision.gameObject.GetComponent<Player_Controller>().hit = true;
                Destroy(this.gameObject);
            }
           
        }
    }
}
