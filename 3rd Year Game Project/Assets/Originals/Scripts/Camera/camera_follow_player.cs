using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow_player : MonoBehaviour
{
    [Header("Target_Player")]
    public Transform target; // target the camera will follow
    [Range(10,20)]
    public float smoothSpeed;
    public float camYpos; // y axis position camera, will be center of level player. 
    public bool is_at_hub;

    [Header("Offset")]
    public float x_offset;// offest on x axis

    private void Update()
    {
        // recieves where canera should be from player controller.
        // This depends on what floor the player is
        camYpos = target.GetComponent<Player_Controller>().camY;

        is_at_hub = target.GetComponent<Player_Controller>().is_at_hub;
    }

    // runs right after update.
    void FixedUpdate()
    {

        // set desired position
        // x_pos = target position + offest
        // y_pos = level center
        Vector3 desiredPos = new Vector3(target.transform.position.x + x_offset, camYpos, -20);

        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);


        if(target.GetComponent<Player_Controller>().transition_cam == false)
        {
            if(is_at_hub)
            {
                // make camera follow desired position - no lerp - for smooth transition
                transform.position = desiredPos;
            }
            else
            {
                // make camera follow desired position - lerp for smooth movement of camera
                transform.position = smoothedPos;
            }
            
        }
        else
        {
            this.transform.position = new Vector3(-1.5f, (target.GetComponent<Player_Controller>().transition_cam_Ypos - 0.4f), -20);
        }



    }

}


