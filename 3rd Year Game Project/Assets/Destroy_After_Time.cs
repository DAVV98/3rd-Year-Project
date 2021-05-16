using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_After_Time : MonoBehaviour
{

    public float time_till_destory; // time object should live for

    // Update is called once per frame
    void Update()
    {
        // destory object x secondsafter instantiation 
        Destroy(this.gameObject, time_till_destory);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // destroy projectile if it hits ground
        if(collision.gameObject.layer == 8 || collision.gameObject.layer == 13)
        {
            Destroy(this.gameObject);
        }
    }
}
