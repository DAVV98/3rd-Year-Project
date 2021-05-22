using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladder_portal : MonoBehaviour
{
    public GameObject portal; // object that holds teleport collider
    public GameObject children; // visuals of blocker
    public bool blocker_off; // bool to check if blocker is on or off

    // Update is called once per frame
    void Update()
    {
        // turns blocker off
        if(blocker_off)
        {
            // enables collider that detects player for teleportation
            portal.GetComponent<CircleCollider2D>().enabled = true;
            // hides portal protector
            children.SetActive(false);
        }
        // turns blocker on
        else
        {
            // disables collider that detects player for teleportation
            portal.GetComponent<CircleCollider2D>().enabled = false;

            // shows portal blocker
            children.SetActive(true);
        }
    }
}
