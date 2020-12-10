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
    public int ladderPrice;
    public int snakePunishment;

    [Header("Ladder")]
    //public GameObject target_pos;
    public GameObject Starget_pos;
    public int level;

    [Header("Levels")]
    public Transform[] levels;

    private level_hubs hubs;


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


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ladder")
        {
            if(coins >= ladderPrice)
            {
                this.transform.position = levels[level].transform.position;
            }
        }

        if (collision.gameObject.tag == "Snake")
        {
            if (coins < snakePunishment)
            {
                this.transform.position = Starget_pos.transform.position;
            }
        }

        if (collision.gameObject.tag == "Level 1")
        {
            level = 1;
            Debug.Log(level);
        }

        if (collision.gameObject.tag == "Level 2")
        {
            level = 2;
            Debug.Log(level);
        }

        if (collision.gameObject.tag == "Level 3")
        {
            level = 3;
            Debug.Log(level);
        }
    }


    // problem maybe is hubs is empty












}
