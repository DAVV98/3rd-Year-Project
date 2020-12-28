using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow_player : MonoBehaviour
{
    [Header("Target_Player")]
    public Transform target;
    public float camYpos;

    [Header("Smoothing")]
    public float smoothSpeed;
    public Vector3 offset;


    void LateUpdate()
    {
        if (target.position.y > 0 && target.position.y < 10)
        {
            camYpos = 5;
        }

        if (target.position.y > 10 && target.position.y < 20)
        {
            camYpos = 15;
        }

        if (target.position.y > 20 && target.position.y < 30)
        {
            camYpos = 25;
        }



        Vector3 desiredPos = new Vector3(target.position.x, camYpos, -20) + offset;
        Vector3 smoothedPos = Vector3.Lerp(transform.position, desiredPos, (smoothSpeed* Time.deltaTime));

        if(target.position.x > 0 && target.position.x < 140)
        {
            transform.position = desiredPos;
        }
        
    }
}

// if level 1 5 level 2 + 10, level 3 +10