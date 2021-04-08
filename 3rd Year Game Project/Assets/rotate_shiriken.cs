using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotate_shiriken : MonoBehaviour
{
    public float rotation;
    public float r_speed;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(0, 0, 50);
    }
}
