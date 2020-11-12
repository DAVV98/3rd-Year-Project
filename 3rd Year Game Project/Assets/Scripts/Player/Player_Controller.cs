using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float horizontalInput;

    [Header("Jumping")]
    public bool isGrounded;
    public Transform feetPos;
    public float groundedRaycastLength;
    public LayerMask groundLayer;
    public float jumpForce;

    public float fallMultiplier;
    public float lowJumpMultiplier;

    [Header("Coins")]
    public int coins;


    // Settings
    Rigidbody2D rb;

    private void Start()
    {
        // get this.objects rigidbody.
        rb = this.GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get horizontal imput.
        horizontalInput = Input.GetAxisRaw("Horizontal");
        Jumping();
    }

    private void FixedUpdate()
    {
        // call movement function
        Movement();        
    }

    void Movement()
    {
        // change velocity to move player.
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }

    void constantMovement(float input)
    {
        // makes float input into integer.
        if (input < 0)
        {
            horizontalInput = -1;
        }
        else if (input > 0)
        {
            horizontalInput = 1;
        }
        else
        {
            horizontalInput = 0;
        }
    }

    void Jumping()
    {
        // Check grounding
        isGrounded = Physics2D.Raycast(feetPos.position, Vector2.down, groundedRaycastLength, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        }

        
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(feetPos.position, feetPos.position + Vector3.down * groundedRaycastLength);
    }


}
