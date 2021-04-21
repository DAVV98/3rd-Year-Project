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

    [Header("Offset")]
    public float x_offset;// offest on x axis

    private void Update()
    {
        // recieves where canera should be from player controller.
        // This depends on what floor the player is
        camYpos = target.GetComponent<Player_Controller>().camY;
    }

    // runs right after update.
    void FixedUpdate()
    {

        // set desired position
        // x_pos = target position + offest
        // y_pos = level center
        Vector3 desiredPos = new Vector3(target.transform.position.x + x_offset, camYpos, -20);

        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);


        // make camera follow desired position
        transform.position = smoothedPos;
        
        
    }
}


