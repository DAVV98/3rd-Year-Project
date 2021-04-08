using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
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
        // if collsion with layer 8 (ground) destroy egg
        if(collision.gameObject.layer == 8)
        {
            Destroy(this.gameObject);
        }
    }
}
