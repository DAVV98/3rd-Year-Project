using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow_player : MonoBehaviour
{
    [Header("Target_Player")]
    public Transform target; // target the camera will follow
    [Range(10,20)]
    public float smoothSpeed; // makes thge camera move smoothly
    public float camYpos; // y axis position camera, will be center of level player. 
    public bool is_at_hub; // checks if player at start hub
    public bool runLateUpdate; // checks which update should be run

    [Header("Offset")]
    public float x_offset;// offest on x axis

    // timer used on moving platfroms to take away jittering
    [Header("Timer")]
    public float startTimer = 0.05f;
    public float curr_startTimer;

    private void Start()
    {
        curr_startTimer = startTimer;   
    }
    private void Update()
    {
        // recieves where canera should be from player controller.
        // This depends on what floor the player is
        camYpos = target.GetComponent<Player_Controller>().camY;

        // checks if player is ayt hub from player controller
        is_at_hub = target.GetComponent<Player_Controller>().is_at_hub;
    }

    // runs right after update.
    void FixedUpdate()
    {

        // set desired position
        // x_pos = target position + offest
        // y_pos = level center
        Vector3 desiredPos = new Vector3(target.transform.position.x + x_offset, camYpos, -20);

        // foollows desired position but uses lerp for smooth movement of the camera
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, smoothSpeed * Time.deltaTime);

        // if player should be follwed
        if(target.GetComponent<Player_Controller>().stop_following == false)
        { 
            // checks if transition camera is active if not continue
            if(target.GetComponent<Player_Controller>().transition_cam == false)
            {
                // if at hub follow using camera to reducxe teleport jitter
                if(is_at_hub)
                {
                    transform.position = desiredPos;
                }
                // if on moving plat use late update
                else if(target.GetComponent<Player_Controller>().onMovingPlat == true)
                {
                    // set late update  to true
                    runLateUpdate = true;
                }
                // if fixedupdate cam should be used - uses smoothPos to follow player
                else
                {
                    // make camera follow desired position - lerp for smooth movement of camera
                    transform.position = smoothedPos;
                    runLateUpdate = false;
                   

                }
            
            }
            else
            {
                // camera position during transitions on teleproters
                this.transform.position = new Vector3(-1.5f, (target.GetComponent<Player_Controller>().transition_cam_Ypos - 0.4f), -20);
            }
        }
    
    }

    // runs after updates.
    private void LateUpdate()
    {
        // desired position when late update camera required - this is due to jittering on moving platfroms
        Vector3 late_desiredPos = new Vector3(target.transform.position.x + x_offset, camYpos, -20);
        Vector3 late_smoothPos = Vector3.Lerp(transform.position, late_desiredPos, smoothSpeed * Time.fixedDeltaTime);

        // makes the camera follow the set desired position
        if (runLateUpdate == true)
        {
            // initially allow smoothed camarea movemetn so when landing on platform no camera jitter
            if(curr_startTimer > 0)
            {
                // follow player using smoothed camera
                transform.position = late_smoothPos;
                //start timer
                curr_startTimer -= Time.deltaTime;
            }
            // once landed follow deisred position to take away camera jitter
            else if(curr_startTimer <= 0)
            {
                transform.position = late_desiredPos;
            }
            
        }
        // if platfrom left reset timer
        else
        {
            curr_startTimer = startTimer;
        }
    }

}


