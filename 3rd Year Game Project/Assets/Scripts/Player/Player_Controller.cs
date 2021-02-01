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
    public bool L_Grounded;
    public bool M_Grounded;
    public bool R_Grounded;
    public Transform feetPos_L;
    public Transform feetPos;
    public Transform feetPos_R;
    public float groundedRaycastLength;
    public LayerMask groundLayer;
    public Transform level_1_max_jump;
    public Transform level_2_max_jump;
    public Transform level_3_max_jump;
    private float jumpForce;
    private float fallMultiplier;
    private float lowJumpMultiplier;

    // turn many of these global
    [Header("Coins")]
    private int coins;
    private int ladderPrice;
    public int snakePunishment;
    [SerializeField] Text Coins_Text;

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

    // Use Object
    public bool useObj;
    public bool inLadderTrigger;
    public bool inPriceTrigger;
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

        // set use object to false
        useObj = false;

        // set globals from global player script
        moveSpeed = GPC_Object.GetComponent<global_player_controller>().GLOBAL_movementSpeed;
        jumpForce = GPC_Object.GetComponent<global_player_controller>().GLOBAL_jumpForce;
        fallMultiplier = GPC_Object.GetComponent<global_player_controller>().GLOBAL_fallMultiplier;
        lowJumpMultiplier = GPC_Object.GetComponent<global_player_controller>().GLOBAL_lowJumpMultiplier;

        min_time = GPC_Object.GetComponent<global_player_controller>().GLOBAL_min_time;
        max_time = GPC_Object.GetComponent<global_player_controller>().GLOBAL_max_time;

        coins = GPC_Object.GetComponent<global_player_controller>().GLOBAL_coins;

        ladderPrice = GPC_Object.GetComponent<global_player_controller>().GLOBAL_ladderPrice;


        // LAdder trigger
        inLadderTrigger = false;
    }

    private void Update()
    {
        // Get horizontal imput.
        horizontalInput = Input.GetAxisRaw("Horizontal");


        //if input is z timer start, turn start
        if (Input.GetKey("z") && isMyTurn && !nextPlayer || Input.GetKeyDown("joystick button 0") && isMyTurn && !nextPlayer)
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


        useLadder();

        // call jumping function
        Jumping();


        Coins_Text.text = coins.ToString();
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
        // Check mid grounding
        M_Grounded = Physics2D.Raycast(feetPos.position, Vector2.down, groundedRaycastLength, groundLayer);
        // Check left grounding
        L_Grounded = Physics2D.Raycast(feetPos_L.position, Vector2.down, groundedRaycastLength, groundLayer);
        // Check right grounding
        R_Grounded = Physics2D.Raycast(feetPos_R.position, Vector2.down, groundedRaycastLength, groundLayer);

        // if any grounded raycast true, allow jumping
        if (M_Grounded || L_Grounded || R_Grounded)
        {
            isGrounded = true;
        }
        else if(!M_Grounded && !L_Grounded && !R_Grounded)
        {
            isGrounded = false;
        }

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

        //if level = x, dont allow jumping over level max height.
        if(level == 1)
        {
            if(transform.position.y >= level_1_max_jump.position.y)
            {
                rb.velocity += Vector2.up * Physics.gravity * (lowJumpMultiplier - 1) * Time.deltaTime; // multipliers global
            }
        }
        //if level = x, dont allow jumping over level max height.
        if (level == 2)
        {
            if (transform.position.y >= level_2_max_jump.position.y)
            {
                rb.velocity += Vector2.up * Physics.gravity * (lowJumpMultiplier - 1) * Time.deltaTime; // multipliers global
            }
        }
        //if level = x, dont allow jumping over level max height.
        if (level == 3)
        {
            if (transform.position.y >= level_3_max_jump.position.y)
            {
                rb.velocity += Vector2.up * Physics.gravity * (lowJumpMultiplier - 1) * Time.deltaTime; // multipliers global
            }
        }
    }

    // turn timer
    void timer()
    {
        // if timer started lower from max time and change text values
        if (timerStarted == true)
        {
            timer_length -= (1.0f * Time.deltaTime);
            countdown_text.text = (timer_length + 1).ToString("0");

            // allow movement if timer is counting
            if (timer_length > -1)
            {
                canMove = true;
            }
            // once timer ends
            else if(timer_length < -1)
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


    void useLadder()
    {
        // Get input for use object
        if (Input.GetKeyDown("a") && inLadderTrigger || Input.GetKeyDown("joystick button 4") && inLadderTrigger)
        {
            // move
            if (coins >= ladderPrice)
            {
                this.transform.position = levels[level].transform.position;
                coins -= ladderPrice;
            }
        }
    }



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(feetPos.position, feetPos.position + Vector3.down * groundedRaycastLength);
        Gizmos.DrawLine(feetPos_L.position, feetPos.position + Vector3.down * groundedRaycastLength);
        Gizmos.DrawLine(feetPos_R.position, feetPos.position + Vector3.down * groundedRaycastLength);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Move to next floor hub when using ladder
        if (collision.gameObject.tag == "Ladder")
        {
            inLadderTrigger = true;
        }

        // Move to next floor hub when using ladder
        if (collision.gameObject.tag == "Ladder_Price")
        {
            inPriceTrigger = true;
        }

        // Move to next floor hub when reaching end of level
        if (collision.gameObject.tag == "Level_End")
        {
           this.transform.position = levels[level].transform.position;
        }

        // Move to previous floor hub when using snake
        if (collision.gameObject.tag == "Snake")
        {
            if (coins < snakePunishment)
            {
                this.transform.position = levels[level - 2].transform.position;
            }
        }

        // Check what level player is on and change level var depending on floor.
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

        if (collision.gameObject.tag == "Projectile")
        {
            this.transform.position = levels[level - 1].transform.position;
        }

        //If falling out of game send to start
        if (collision.gameObject.tag == "Kill_Bottom")
        {
            this.transform.position = levels[0].transform.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Move to next floor hub when using ladder
        if (collision.gameObject.tag == "Ladder")
        {
            inLadderTrigger = false;
        }

        // Move to next floor hub when using ladder
        if (collision.gameObject.tag == "Ladder_Price")
        {
            inPriceTrigger = false;
        }
    }











}
