using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    [Header("Global Controls")]
    public GameObject GPC_Object;

    [Header("Animator")]
    public Animator animator;

    [Header("Attack")]
    public bool isAttacking;
    private float timeBetweenAttack;
    public float startTimeBetweenAttacks;
    public Transform attackPoint;
    public LayerMask enemiesHit, blocksHit;
    public float attackRange;
    public float pushForce;
    public bool beenPushed;


    [Header("Movement")]
    private float moveSpeed;
    private float horizontalInput;

    [Header("Jumping")]
    private bool isGrounded;
    private bool L_Grounded;
    private bool M_Grounded;
    private bool R_Grounded;
    private float jumpForce;
    private float fallMultiplier;
    private float lowJumpMultiplier;

    [Header("Grounding Mechanic")]
    public Transform feetPos_L;
    public Transform feetPos;
    public Transform feetPos_R;
    public float groundedRaycastLength;
    public LayerMask groundLayer;

    [Header("Floor Climbing Blockers")]
    public Transform level_1_max_jump;
    public Transform level_2_max_jump;
    public Transform level_3_max_jump;

    [Header("UI Text")]
    [SerializeField] Text Coins_Text;
    [SerializeField] Text countdown_text;

    // turn many of these global
    [Header("Coins")]
    public int coins;
    private int ladderPrice;
   

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


        beenPushed = false;
    }

    private void Update()
    {
        // Gets horizontal input each frame, and sets this as a float variable
        horizontalInput = Input.GetAxisRaw("Horizontal");

        Debug.Log(coins);

        int curr_coins = coins;

        // Sets UI Coins text accoridng to number of coins player posses.
        Coins_Text.text = curr_coins.ToString();

        // Function thta gets imput to start turn, and uses timer
        startTurn();

        // Function that gets input if player wants to use ladder, if used checks if enough money.
        useLadder();

        // Function that allows player to jump and allows changing jump parameters
        Jumping();

        //only  allow attack if on turn
        if(isMyTurn == true)
        {
            // Function that allows for attack of coins boxes and to push rivals
            attack();
        }
        

        // Function that changes player animations depending on conditions
        setAnimations();


    }

    private void FixedUpdate()
    {
        // Function that creates a countdown timer  
        timer();

        // Function that is in control of horizontal player movement. 
        Movement();

       
    }

    void setAnimations()
    {
        if (isMyTurn)
        {
            if (horizontalInput > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else if (horizontalInput < 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }
        }

        if(isAttacking && isMyTurn)
        {
            animator.SetTrigger("Attack"); ;
        }


        if (isGrounded && isMyTurn)
        {
            // Set move animation
            animator.SetFloat("Speed", Mathf.Abs(horizontalInput * moveSpeed));
            animator.SetBool("isJumping", false);
        }
        else if(isGrounded && !isMyTurn)
        {
            // Set move animation
            animator.SetFloat("Speed", Mathf.Abs(horizontalInput * 0));
            animator.SetBool("isJumping", false);
        }
        else if(!isGrounded)
        {
            animator.SetBool("isJumping", true);
        }
        
    }
    /// <summary>
    /// Movement: 
    /// Checks if it is players turn, if so allows input to change player horizontal velocity.
    /// </summary>
    void Movement()
    {
        // if players turn allow movement.
        if (canMove == true)
        {
            // change horizontal velocity to move player.
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);

         
        }
        
    }

    /// <summary>
    /// Jumping:
    /// Calls Grounding Function to check if player is grounded, 
    ///     if so and it is also the players turn allow jump input to change players horizontal velocity.
    /// Calls JumpGravity so that gravity can be used to improve the feel of players jump
    /// Calls floorBArriers to set up the points where player shouldnt be allowed to jump between floors.
    /// </summary>
    void Jumping()
    {
        // Checks if grounded
        Grounding();

        //Check if it is players turn
        if(canMove == true)
        {
            // Get Jump Input, and if player grounded allow vertical movement
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); 

            }
        }

        // Affect gravity once in the air
        JumpGravity();

        // Checks so you cant jump from floor to floor when not desired
        floorBarriers();


    }

    void JumpGravity()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics.gravity * (fallMultiplier - 1) * Time.deltaTime; // multipliers global
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics.gravity * (lowJumpMultiplier - 1) * Time.deltaTime; // multipliers global
        }
    }

    void floorBarriers()
    {
        //if level = x, dont allow jumping over level max height.
        if (level == 1)
        {
            if (transform.position.y >= level_1_max_jump.position.y)
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
    void Grounding()
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
        else if (!M_Grounded && !L_Grounded && !R_Grounded)
        {
            isGrounded = false;
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
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
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




    void startTurn()
    {
        //if input is z timer start, turn start
        if (Input.GetKey("z") && isMyTurn && !nextPlayer || Input.GetKeyDown("joystick button 0") && isMyTurn && !nextPlayer)
        {
            // set random turn length. TODO set values from global player field.
            random_timer = Random.Range(min_time, max_time);

            // set timer length
            timer_length = random_timer;

            // set timer has started to true.
            timerStarted = true;

            // Allow player to move.
            canMove = true;
        }
    }

    /// <summary>
    /// Set a timer so you cannt spam attack
    /// if attack input:
    /// check if overlap circles is hitting
    /// 
    /// if circle hitting an enemy push them back
    /// if circle hitting coin block - break down until; coin pops out.
    /// 
    /// timer countdown one attacked, once ti gets to zero allow attacks to happen
    /// </summary>
    void attack()
    {

        // if attack time is zero allow attack
        if(timeBetweenAttack <= 0)
        {
            
            // if attack button pressed
            if (Input.GetKeyDown("joystick button 5"))
            {
                // player is attack 
                isAttacking = true;

                // Circle collider that checks for enemies or coin blocks
                Collider2D[] hitenemy = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemiesHit);
                Collider2D[] hitblock = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, blocksHit);

                // do this to each enemy in the circle collider.
                
                foreach (Collider2D enemy in hitenemy)
                {
                    // Push enemy right if enemy is to the right
                    if(enemy.transform.position.x > gameObject.transform.position.x)
                    {
                        enemy.GetComponent<Rigidbody2D>().velocity = new Vector3(pushForce, 0, 0);
                    }
                    // Push enemy left if enemy to the left.
                    else if(enemy.transform.position.x < gameObject.transform.position.x)
                    {

                        enemy.GetComponent<Rigidbody2D>().velocity = new Vector3(-pushForce, 0, 0);
                    }
                    
                }
                
                // do this for every block in circle collider
                foreach(Collider2D block in hitblock)
                {
                    // reduce health by one
                    block.GetComponent<coin_block>().health -= 1;
                }
                
                // Set timer to timer length, this also starts countodwon timer 
                timeBetweenAttack = startTimeBetweenAttacks;
            }      
        }
        else
        {
            // player is no longer attacking
            isAttacking = false;

            // reduce timer by one second.
            timeBetweenAttack -= Time.deltaTime;
        }
        


    }





}
