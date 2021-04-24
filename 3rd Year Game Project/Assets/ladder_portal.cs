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
            portal.GetComponent<CircleCollider2D>().enabled = true;
            children.SetActive(false);
        }
        // turns blocker on
        else
        {
            portal.GetComponent<CircleCollider2D>().enabled = false;
            children.SetActive(true);
        }
    }
}
