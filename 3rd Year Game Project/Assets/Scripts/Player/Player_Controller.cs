using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    [Header("Global Controls")]
    public GameObject GPC_Object;



    [Header("Movement")]
    private float moveSpeed;
    private float horizontalInput;

    [Header("Jumping")]
    public bool isGrounded;
    public Transform feetPos;
    public float groundedRaycastLength;
    public LayerMask groundLayer;
    private float jumpForce;

    private float fallMultiplier;
    private float lowJumpMultiplier;

    // turn many of these global
    [Header("Coins")]
    public int coins;
    public int ladderPrice;
    public int snakePunishment;

    [Header("Current Floor")]
    private int level;

    [Header("Floor Start Point")]
    public Transform[] levels;

    // turn many of these global
    [Header("Turn Timer")]
    public bool timerStarted;
    public bool canMove;
    public bool isMyTurn;
    public bool nextPlayer;
    public bool endTurn;
    private int random_timer;
    private float timer_length;
    [SerializeField] Text countdown_text;
    private int min_time;
    private int max_time;


    // Settings
    Rigidbody2D rb;

    private void Start()
    {
        // get this.objects rigidbody.
        rb = this.GetComponent<Rigidbody2D>();
        
        // Set turn inactive at start of game
        canMove = false;

        // Set next players turn inactive at start of game.
        nextPlayer = false;



        // set globals from global player script
        moveSpeed = GPC_Object.GetComponent<global_player_controller>().GLOBAL_movementSpeed;
        jumpForce = GPC_Object.GetComponent<global_player_controller>().GLOBAL_jumpForce;
        fallMultiplier = GPC_Object.GetComponent<global_player_controller>().GLOBAL_fallMultiplier;
        lowJumpMultiplier = GPC_Object.GetComponent<global_player_controller>().GLOBAL_lowJumpMultiplier;

        min_time = GPC_Object.GetComponent<global_player_controller>().GLOBAL_min_time;
        max_time = GPC_Object.GetComponent<global_player_controller>().GLOBAL_max_time;
    }

    private void Update()
    {
        // Get horizontal imput.
        horizontalInput = Input.GetAxisRaw("Horizontal");


        //if input is z timer start, turn start
        if (Input.GetKey("z") && isMyTurn && !nextPlayer)
        {
            // set random turn length. TODO set values from global player field.
            random_timer = Random.Range(min_time,max_time);
            
            // set timer length
            timer_length = random_timer;

            // set timer has started to true.
            timerStarted = true;

            // Allow player to move.
            canMove = true;
        }

        // call jumping function
        Jumping();
    }

    private void FixedUpdate()
    {
        // timer function
        
        timer();
        // call movement function
        Movement();
    }

    void Movement()
    {
        // if players turn allow movement.
        if (canMove == true)
        {
            // change velocity to move player.
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y); // moveSpeed Global?
        }
        
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

        if(canMove == true)
        {
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); // jumpforce global?

            }
        }
       

        
        if(rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics.gravity * (fallMultiplier - 1) * Time.deltaTime; // multipliers global
        }
        else if(rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics.gravity * (lowJumpMultiplier - 1) * Time.deltaTime; // multipliers global
        }

    }

    // turn timer
    void timer()
    {
        // if timer started lower from max time and change text values
        if (timerStarted == true)
        {
            timer_length -= (1.0f * Time.deltaTime);
            countdown_text.text = timer_length.ToString("0");

            // allow movement if timer is counting
            if (timer_length > 0)
            {
                canMove = true;
            }
            // once timer ends
            else if(timer_length < 2)
            {
                //cannot move
                canMove = false;
                // timer off
                timerStarted = false;
                // turn over.
                endTurn = true;
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(feetPos.position, feetPos.position + Vector3.down * groundedRaycastLength);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Up when colliding with ladder
        if (collision.gameObject.tag == "Ladder")
        {
            if(coins >= ladderPrice)
            {
                this.transform.position = levels[level].transform.position;
            }
        }

        // Up when colliding with ladder
        if (collision.gameObject.tag == "Level_End")
        {
           this.transform.position = levels[level].transform.position;
        }

        // Down when colldiing with snake
        if (collision.gameObject.tag == "Snake")
        {
            if (coins < snakePunishment)
            {
                this.transform.position = levels[level - 2].transform.position;
            }
        }

        // Check what level player is on
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
        if (collision.gameObject.tag == "Level 4")
        {
            level = 4;
            Debug.Log(level);
        }
        if (collision.gameObject.tag == "Level 5")
        {
            level = 5;
            Debug.Log(level);
        }
    }











}
