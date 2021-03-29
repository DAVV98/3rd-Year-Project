﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    [Header("Health")]
    private int health; // player health
    
    [Header("Turn")]
    public int turn = 1; // keeps track of turn - used for turn based powerups
    private int power_up_turn; // saves when latest speed power up used
    private int active_sped_up_turns; // turns speed power up is active

    [Header("Global Controls")]
    public GameObject GPC_Object; // sets global variables from global player controller.

    [Header("Animator")]
    public Animator animator; // players animator.

    [Header("Camera")]
    public float camY; // players animator.

    [Header("Attack")]
    public bool isAttacking; // checks if player is attcking
    private float timeBetweenAttack; // set time between attacks
    public float startTimeBetweenAttacks; // time between attacks - switch to GPC
    public Transform attackPoint; // point where attack spawns from
    public LayerMask enemiesHit, blocksHit; // layers affeted by attack
    public float attackRange; // range of attack collider
    public float pushForce; // force of attack between players. - switch to GPC


    [Header("Movement")]
    private float speed_power_up; // extra speed from power up
    private float moveSpeed; // movement speed
    private float horizontalInput; // controller horizontal input
    public float air_movement_divider; // divides movespeed in air to allow for more realistic jump



    [Header("Jumping")]
    // grounding checks
    private bool isGrounded;
    private bool L_Grounded;
    private bool M_Grounded;
    private bool R_Grounded;

    private float jumpForce; // force of jump

    // jump multipliers
    private float fallMultiplier;
    private float lowJumpMultiplier;

    [Header("Grounding Mechanic")]
    // grounding checkers
    public Transform feetPos_L;
    public Transform feetPos;
    public Transform feetPos_R;
    public float groundedRaycastLength;
    public LayerMask groundLayer;

    [Header("Floor Climbing Blockers")]
    // positions that stop jumping through levels
    public Transform[] level_max_jump;

    [Header("UI Text")]
    // UI Texts
    [SerializeField] Text Coins_Text;
    [SerializeField] Text countdown_text;

    [Header("Coins")]
    public int coins; // coins availabe to use
    private int ladderPrice; // price of ladder
   

    [Header("Current Floor")]
    private int level; // checks what current floor is

    [Header("Floor Start Point")]
    public Transform[] levels; // floor hubs


    [Header("Turn Timer")]
    
    public bool timerStarted;
    public bool canMove; // checks if player is allowed to move
    public bool isMyTurn; // checks if it is players turn
    public bool nextPlayer; // checks it its next players turn
    public bool endTurn; // checks if turn should end
    private int random_timer; // used from random turn lenght timer
    private float timer_length; // turn timer length

    private int min_time; // minimum turn length
    private int max_time; // maximum turn length

    // Use Object
    public bool useObj;
    public bool inLadderTrigger;
    public bool inPriceTrigger;
    
    
    // Settings
    Rigidbody2D rb;




    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function Start():
    ///     - Runs at the start of the run time
    ///     - Used for Player set up.
    /// </summary>
    private void Start()
    {
        // get players rigidbody.
        rb = this.GetComponent<Rigidbody2D>();
        
        // Set turn inactive at start of game
        canMove = false;

        // Set next players turn inactive at start of game.
        nextPlayer = false;

        // set use object to false
        useObj = false;

        // Ladder trigger
        inLadderTrigger = false;

        // sets variables controlled by GPC
        set_GPC_Vars();
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function set_GPC_Vars():
    ///     - Sets variables controlled by GPC.
    ///     - Uses to assure all players have the same behaviours.
    /// </summary>
    void set_GPC_Vars()
    {
        // set variables controlled by global player controller.
        moveSpeed = GPC_Object.GetComponent<global_player_controller>().GLOBAL_movementSpeed;

        jumpForce = GPC_Object.GetComponent<global_player_controller>().GLOBAL_jumpForce;

        fallMultiplier = GPC_Object.GetComponent<global_player_controller>().GLOBAL_fallMultiplier;

        lowJumpMultiplier = GPC_Object.GetComponent<global_player_controller>().GLOBAL_lowJumpMultiplier;

        min_time = GPC_Object.GetComponent<global_player_controller>().GLOBAL_min_time;

        max_time = GPC_Object.GetComponent<global_player_controller>().GLOBAL_max_time;

        coins = GPC_Object.GetComponent<global_player_controller>().GLOBAL_coins;

        ladderPrice = GPC_Object.GetComponent<global_player_controller>().GLOBAL_ladderPrice;

        speed_power_up = GPC_Object.GetComponent<global_player_controller>().GLOBAL_Speed_Up;

        active_sped_up_turns = GPC_Object.GetComponent<global_player_controller>().GLOBAL_Speed_Up_Active_Turns - 1;

        health = GPC_Object.GetComponent<global_player_controller>().GLOBAL_Start_Health;
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function Update():
    ///     - Runs at every frame
    ///     - Used to update player.
    /// </summary>
    private void Update()
    {
        //only allow the functions if player turn.
        if (isMyTurn == true)
        {
            // Function that allows for attack of coins boxes and to push rivals
            attack();

            
        }

        // Gets horizontal input each frame, and sets this as a float variable
        horizontalInput = Input.GetAxisRaw("Horizontal");

   

        // Sets UI Coins text accoridng to number of coins player posses.
        //Coins_Text.text = coins.ToString();

        // changes player tag, depending on, if it is the players turn or not.
        tag_name();
   

        // Function thta gets imput to start turn, and uses timer
        startTurn();

        // Function that gets input if player wants to use ladder, if used checks if enough money.
        useLadder();

        // Function that allows player to jump and allows changing jump parameters
        Jumping();

        // checks if speed power up on, if so changes player behaviour accordingly.
        sped_up();

        // Function that changes player animations depending on conditions
        setAnimations();

    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function FixedUpdate():
    ///     - Runs at every physics frame
    ///     - Used to update players physics.
    /// </summary>
    private void FixedUpdate()
    {
        // Function that creates a countdown timer  
        turn_timer();

        // Function that is in control of horizontal player movement. 
        Movement();

    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function tag_name():
    ///      - Changes player tag, which allows other objects to check if player is awake or not(if player turn or not).       
    /// </summary>
    void tag_name()
    {

        if (isMyTurn)
        {
            gameObject.tag = "Player";
        }
        else
        {
            gameObject.tag = "Player_Sleep";
        }


    }
    
    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function setAnimations():
    ///      - Changes player animations, and look direction.    
    /// </summary>
    void setAnimations()
    {
        if(isMyTurn)
        {
            // make player look in the correct direction
            if (horizontalInput > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }
            else if (horizontalInput < 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }

            // run attack animation if attacking
            if (isAttacking)
            {
                animator.SetTrigger("Attack"); ;
            }

            // run running animation if moving and grounded
            if (isGrounded)
            {
                // Set move animation
                animator.SetFloat("Speed", Mathf.Abs(horizontalInput * moveSpeed));
                animator.SetBool("isJumping", false);
            }
        }
        
        // if player standing still, zero out animator vars
        if(isGrounded && !isMyTurn)
        {
            // Set move animation
            animator.SetFloat("Speed", Mathf.Abs(horizontalInput * 0));
            animator.SetBool("isJumping", false);
        }
        
        // run jumping animation if jumping
        if(!isGrounded)
        {
            animator.SetBool("isJumping", true);
        }
        
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function Movement(): 
    ///     - Allows horizontal movement if it is players turn
    /// </summary>
    void Movement()
    {
        // Checks if grounded
        Grounding();

        // if players turn allow movement.
        if (canMove == true)
        {
           
           
            // change horizontal velocity to move player.
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
            
        

        }
        
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function Jumping(): 
    ///     - Allows positive vertical movement if it is players turn and player is grounded.
    /// </summary>
    void Jumping()
    {
       

        //Check if it is players turn
        if(canMove == true)
        {
            // Get Jump Input, and if player grounded allow vertical movement
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                //rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); 

            }
        }

        // Affect gravity once in the air
        JumpGravity();

        // Checks so you cant jump from floor to floor when not desired
        floorBarriers();

    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function JumpGravity(): 
    ///     - Changes gravity when reaching highest jump.
    ///     - Changes gravity for low jumps 
    /// </summary>
    void JumpGravity()
    {
        // when no more positive vertical movement, adds fall mutiplier. 
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics.gravity * (fallMultiplier - 1) * Time.deltaTime; 
        }
        // whenpositive vertical movement but no jump input add low jump multiplier.
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.velocity += Vector2.up * Physics.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function floorBarriers(): 
    ///     - Checks if hitting floor blocker if so doesnt allow jumping between levels. 
    /// </summary>
    void floorBarriers()
    {
        // if hitting current level max jump blocker increase gravity to not allow inter-floor jumping
        if (transform.position.y >= level_max_jump[level - 1].position.y)
        {
            rb.velocity += Vector2.up * Physics.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function Grounding(): 
    ///     - Checks if player is grounded, to check if the player canjump
    /// </summary>
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

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function startTurn(): 
    ///     - Allows player to start turn, sets a random timer value, to have a random turn length. 
    /// </summary>
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

            turn++; // increments turn
        }
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function turn_timer(): 
    ///     - Timer of random length (from startTurn), when timer ends the turn ends.
    /// </summary>
    void turn_timer()
    {
        // if timer started lower from max time and change text values
        if (timerStarted == true)
        {
            timer_length -= (1.0f * Time.deltaTime);
            //countdown_text.text = (timer_length + 1).ToString("0");

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

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function useLadder(): 
    ///     - Checks if player inputs to use ladder
    ///     - Checks if player has enough money
    ///     - Transforms player to next floor.
    /// </summary>
    void useLadder()
    {
        // Get input for use object
        if (Input.GetKeyDown("a") && inLadderTrigger || Input.GetKeyDown("joystick button 4") && inLadderTrigger)
        {
            // checks if player can affors use of ladder
            if (coins >= ladderPrice)
            {
                // transforms player position to next floor
                this.transform.position = levels[level].transform.position;
                
                // deducts coins if ladder used
                coins -= ladderPrice;
            }
        }
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function attack(): 
    ///     - Allows player to push back fellow players.
    ///     - Allows player to break treasure boxes.
    /// </summary>
    void attack()
    {

        // if attack time is zero allow attack
        if (timeBetweenAttack <= 0)
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
                    if (enemy.transform.position.x > gameObject.transform.position.x)
                    {
                        enemy.GetComponent<Rigidbody2D>().velocity = new Vector3(pushForce, 0, 0);
                    }
                    // Push enemy left if enemy to the left.
                    else if (enemy.transform.position.x < gameObject.transform.position.x)
                    {

                        enemy.GetComponent<Rigidbody2D>().velocity = new Vector3(-pushForce, 0, 0);
                    }

                }

                // do this for every block in circle collider
                foreach (Collider2D block in hitblock)
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

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function sped_up(): 
    ///     - checks how many turns the player has with speed up power up
    ///     - Turns off power-up when turns over.
    /// </summary>
    void sped_up()
    {
        // number of turns power up has been active
        int active_turns = turn - power_up_turn;

        // if power up has been active for set turns
        if (active_turns > active_sped_up_turns)
        {
            // set to zero for next power up
            power_up_turn = 0;

            // set speed back to normal
            moveSpeed = GPC_Object.GetComponent<global_player_controller>().GLOBAL_movementSpeed;
        }
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------


    /// <summary>
    /// Function OnTriggerEnter2D(): 
    ///     Parameter:
    ///     - Collision (Collider2D): collider of object collided with
    ///     
    ///     Role:
    ///     - check if collsion with trigger collider.
    /// </summary>
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
            camY = 0; // sets what Y pos the player camera should have
        }

        if (collision.gameObject.tag == "Level 2")
        {
            level = 2;
            camY = 10; // sets what Y pos the player camera should have
        }

        if (collision.gameObject.tag == "Level 3")
        {
            level = 3;
            camY = 20; // sets what Y pos the player camera should have
        }


        // if collision with projectile check health
        if (collision.gameObject.tag == "Projectile")
        {
            // if health = 0, send to level hub
            if(health <= 0)
            {
                this.transform.position = levels[level - 1].transform.position;
            }
            // deduct health
            else
            {
                health--;
            }
      
        }

        //If falling out of game send to start
        if (collision.gameObject.tag == "Kill_Bottom")
        {
            this.transform.position = levels[0].transform.position;
        }

        // if collision with speed power up
        if (collision.gameObject.tag == "Speed_Up")
        {
            // increase move speed to power up speed
            moveSpeed += speed_power_up;

            // deactivate power up asset
            collision.gameObject.SetActive(false);

            // set turn when power up collected to use as turn "timer"
            power_up_turn = turn;
        }

        // if collecting health power up
        if(collision.gameObject.tag == "Health_Up")
        {
            // increase health by one
            health += 1;

            // deactivate power up asset
            collision.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Function OnTriggerExit2D(): 
    ///     Parameter:
    ///     - Collision (Collider2D): collider of object collided with
    ///     
    ///     Role:
    ///     - check if stopped collsion with trigger collider.
    /// </summary>
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

    /// <summary>
    /// Function OnDrawGizmos(): 
    ///     - draws gizmos 
    /// </summary>
    private void OnDrawGizmos()
    {
        // set gizmo colour
        Gizmos.color = Color.green;
        
        // grounded colliders
        Gizmos.DrawLine(feetPos.position, feetPos.position + Vector3.down * groundedRaycastLength);
        Gizmos.DrawLine(feetPos_L.position, feetPos.position + Vector3.down * groundedRaycastLength);
        Gizmos.DrawLine(feetPos_R.position, feetPos.position + Vector3.down * groundedRaycastLength);
        
        // attack overlap circle
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}