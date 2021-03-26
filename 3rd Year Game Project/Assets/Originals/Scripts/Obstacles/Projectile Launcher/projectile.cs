using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    [Header("Speed and Direction")]
    public float speed;
    public bool right;
    public bool left;
    public bool up;
    public bool down;



    private Rigidbody2D m_Rigidbody;

    void Start()
    {
        //Fetch the Rigidbody component
        m_Rigidbody = GetComponent<Rigidbody2D>();

    }
    void FixedUpdate()
    {
        if (right)
        {
            // when lauched go right
            transform.position += transform.right * (speed / 100);
        }
        else if (left)
        {
            // when lauched go left
            transform.position -= transform.right * (speed / 100);
        }
        else if(up)
        {

            // when lauched go up;
            transform.position += transform.up * (speed / 100);
        }
        else if(down)
        {
            // when launched go down
            transform.position -= transform.up * (speed / 100);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Stop_Projectiles")
        {
            Destroy(this.gameObject);
        }
    }
}
