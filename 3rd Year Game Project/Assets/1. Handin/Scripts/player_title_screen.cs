using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_title_screen : MonoBehaviour
{
    [Header("Start_End")]
    public Transform start;
    public Transform end;

    [Header("Movement")]
    public float move_Speed;
    public Animator animator;

    private bool right;
    private bool left;
    private Rigidbody2D rb;
   

    // Start is called before the first frame update
    void Start()
    {
        // starts players moving right
        right = true;
        left = false;

        // get players rigidbody.
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // changes direction bool depending on positions
        if (this.transform.position.x <= start.position.x)
        {
            left = false;
            right = true;
        }
        else if (this.transform.position.x >= end.position.x)
        {
            right = false;
            left = true;
        }

        // changes velocity and rotation for players to move
        // sets animator speed so run animation plays
        if (right)
        {
            rb.velocity = new Vector2(1 * move_Speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            // Set move animation
            animator.SetFloat("Speed", Mathf.Abs(1));

        }
        else if(left)
        {
            rb.velocity = new Vector2(-1 * move_Speed, rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            // Set move animation
            animator.SetFloat("Speed", Mathf.Abs(-1));
        }
    }
}
