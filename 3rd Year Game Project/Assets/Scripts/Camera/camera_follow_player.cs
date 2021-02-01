using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera_follow_player : MonoBehaviour
{
    [Header("Target_Player")]
    public Transform target;
    public float camYpos;

    [Header("Offset")]
    public float x_offset;


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



        Vector3 desiredPos = new Vector3(target.position.x + x_offset, camYpos, -20);

        if(target.position.x < 140)
        {
            transform.position = desiredPos;
        }
        
    }
}
