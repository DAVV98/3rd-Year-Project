﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Player_Controller : MonoBehaviour
{
    [Header("Health")]
    public int health; // player health
    private int maxHealth; // max value for helath
    public Image[] hearts;

    [Header("Current_Chunk")]
    public int current_chunk;
    public int number_chunks;
    public GameObject Map;
    public bool map_mid_turn;
    public bool is_at_hub;

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

    [Header("Audio")]
    private GameObject hurt_sound_effect;
    private GameObject countdown_sound;

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
    public bool ice_ground;
    public bool onMovingPlat;
    public bool hit;
 


    [Header("Jumping")]
    // grounding checks
    private bool isGrounded;

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
    private GameObject[] coin_hubs;
    private GameObject bronze_coin;
   

    [Header("Current Floor")]
    public int level; // checks what current floor is

    [Header("Floor Transitions")]
    public Transform[] levels; // floor hubs
    public bool transition_cam;
    public bool start_transition;
    public float transition_cam_Ypos;
    public float transition_timer;
    private float curr_transition_timer;
    public bool canUseLadder;
    public bool ladderUsed;
    public bool canTeleport;
    public bool stop_following;
    public bool has_finished;

    [Header("Turn Timer")]
    public Image timerBar;
    public bool canStartTurn;
    public bool timerStarted;
    public bool canMove; // checks if player is allowed to move
    public bool isMyTurn; // checks if it is players turn
    public bool nextPlayer; // checks it its next players turn
    public bool endTurn; // checks if turn should end
    private int random_timer; // used from random turn lenght timer
    private float timer_length; // turn timer length

    [Header("Explosion Effect")]
    public ParticleSystem explosion;
    public Transform exp_pos;
    
    [Header("Pause")]
    // grounding checks
    private bool pause;
    public GameObject pause_menu;


    // slide protection
    private float start_slide_timer = 0.1f;
    private float curr_slide_timer;

    private int min_time; // minimum turn length
    private int max_time; // maximum turn length

    // Use Object
    public bool useObj;
    public bool inLadderTrigger;
    public bool inPriceTrigger;


    private bool canPlayCountdown;
    private int countdown_sound_Played;

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

        // get image component from timer bar
        timerBar = timerBar.GetComponent<Image>();

        canStartTurn = true;
        map_mid_turn = false;

        curr_transition_timer = transition_timer;

        stop_following = false;
        has_finished = false;
        hit = false;
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

        hurt_sound_effect = GPC_Object.GetComponent<global_player_controller>().GLOBAL_hurt_Sound;
        
        countdown_sound = GPC_Object.GetComponent<global_player_controller>().GLOBAL_countdown;

        maxHealth = GPC_Object.GetComponent<global_player_controller>().GLOBAL_Max_Health;

        number_chunks = GPC_Object.GetComponent<global_player_controller>().GLOBAL_num_chunks;

        startTimeBetweenAttacks = GPC_Object.GetComponent<global_player_controller>().GLOBAL_start_time_between_attacks;

        attackRange = GPC_Object.GetComponent<global_player_controller>().GLOBAL_attack_range;

        transition_timer = GPC_Object.GetComponent<global_player_controller>().GLOBAL_transition_timer;

        groundedRaycastLength = GPC_Object.GetComponent<global_player_controller>().GLOBAL_raycast_length;

        coin_hubs = GPC_Object.GetComponent<global_player_controller>().GLOBAL_coin_hubs;

        bronze_coin = GPC_Object.GetComponent<global_player_controller>().GLOBAL_bronze_coin;
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function Update():
    ///     - Runs at every frame
    ///     - Used to update player.
    /// </summary>
    private void Update()
    {
        // Checks if grounded
        Grounding();

        //only allow the functions if player turn.
        if (isMyTurn == true)
        {
            // Function that allows for attack of coins boxes and to push rivals
            attack();

            
        }
       
        // get horizontal input for movement
        horizontalInput = Input.GetAxisRaw("Horizontal");
  
        // sets coin text UI if coin text exists
        if(Coins_Text != null)
        {
            // Sets UI Coins text accoridng to number of coins player posses.
            Coins_Text.text = coins.ToString();
        }


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

        // Function that sets player UI hearts 
        heart_containers();

        // Fucntion that shows game map
        show_map();

        // Camera transition when teleporting - to avoid jitter ( TODO STILL NOT PERFECT)
        transition();

        // Sets health when health scenareos change
        health_controller();

        // Fucntion that controls player audios.
        AUDIO();

        // function that checks if player is hurt - plays effect and lowers health
        hurt();

        // pauses game
        Pause_Menu();

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
        // if players turn tag - player
        if (canMove)
        {
            gameObject.tag = "Player";
            gameObject.layer = 11;
        }
        // if not player turn - player sleep
        else
        {
            gameObject.tag = "Player_Sleep";
            gameObject.layer = 14;
        }


    }
    
    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function setAnimations():
    ///      - Changes player animations, and look direction.    
    /// </summary>
    void setAnimations()
    {
        // If players turn
        if (isMyTurn && !pause)
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
    ///     - either slippy movment if on ice or controlled movment if on regular ground
    /// </summary>
    void Movement()
    {
        
        // if players turn allow movement.
        if (canMove == true)
        {

            curr_slide_timer = start_slide_timer;

            // if on ice ground make movement slippy
            if (ice_ground == true && isGrounded)
            {
                Vector2 ice_movement = new Vector2(horizontalInput * (moveSpeed - 2), 0);

                if(rb.velocity.magnitude <= (moveSpeed * 1.5))
                {
                    rb.AddForce(ice_movement);
                }
             
            }
            // else use regular movement 
            else
            {

                // change horizontal velocity to move player.
                rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);     
            }
        }
        // Stop player movement once timer runs out
        else
        {
            if(curr_slide_timer > 0)
            {
                // change horizontal velocity to move player.
                rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
                curr_slide_timer -= Time.deltaTime;
            }
            else
            {
                // change horizontal velocity to move player.
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
           
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
        if (canMove == true)
        {
            // Get Jump Input, and if player grounded allow vertical movement
            if (Input.GetButtonDown("Button_One") && isGrounded)
            {
                // set gravity to zero - if already in air
                rb.velocity = new Vector2(rb.velocity.x, 0);
                // add jump force
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
        if (rb.velocity.y > 0 && !Input.GetButton("Button_One"))
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
        bool M_Grounded = Physics2D.Raycast(feetPos.position, Vector2.down, groundedRaycastLength, groundLayer);
        // Check left grounding
        bool L_Grounded = Physics2D.Raycast(feetPos_L.position, Vector2.down, groundedRaycastLength, groundLayer);
        // Check right grounding
        bool R_Grounded = Physics2D.Raycast(feetPos_R.position, Vector2.down, groundedRaycastLength, groundLayer);

        // if any grounded raycast true, allow jumping
        if (M_Grounded || L_Grounded || R_Grounded)
        {
            isGrounded = true;
            this.GetComponent<Rigidbody2D>().drag = 0;
            this.GetComponent<Rigidbody2D>().angularDrag = 0;
        }
        else if (!M_Grounded && !L_Grounded && !R_Grounded)
        {
            isGrounded = false;
            this.GetComponent<Rigidbody2D>().drag = 0.5f;
            this.GetComponent<Rigidbody2D>().angularDrag = 0.5f;
        }


    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function startTurn(): 
    ///     - Allows player to start turn, sets a random timer value, to have a random turn length. 
    /// </summary>
    void startTurn()
    {
        if(canStartTurn)
        {
            //if input is z timer start, turn start
            if (Input.GetKey("z") && isMyTurn|| Input.GetButton("Button_Four") && isMyTurn)
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

                // set so you cannot start turn again
                canStartTurn = false;

                // sets coutndown sound timer to zero
                countdown_sound_Played = 0;
            }
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
            // Timer UI mechanism
            timer_length -= (1.0f * Time.deltaTime);
            countdown_text.text = (timer_length + 1).ToString("0");
            timerBar.fillAmount = timer_length / random_timer;

            // allow movement if timer is counting
            if (timer_length > -1)
            {
                canMove = true;

                // algorithm that starts countdown timer audio
                if(timer_length >= 2.1 && timer_length <= 2.2)
                {
                    canPlayCountdown = true;    
                }
                else { canPlayCountdown = false; }
               
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

                // stop player from starting turn too early
                canStartTurn = false;

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
        if (Input.GetKeyDown("y") && canUseLadder || Input.GetButtonDown("Button_Five") && canUseLadder)
        {
            // checks if player can affors use of ladder
            if (coins >= ladderPrice)
            { 
                // deducts coins if ladder used
                coins -= ladderPrice;
                ladderUsed = true;
                canTeleport = true;


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
            if (Input.GetButton("Button_Zero") || Input.GetKey("a"))
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
    /// Function transition(): 
    ///     - mechanism that sets camera transition when teleporting
    /// </summary>
    void transition()
    {
        // if start transiton true
        if(start_transition)
        {
            // when timer is reaches zero teleport player, rest camera
            if (curr_transition_timer <= 0)
            {
                transition_cam = false;
                // teleport player
                this.transform.position = levels[level].transform.position;
                // rest timer
                curr_transition_timer = transition_timer;
                start_transition = false;


            }
            // transition algorithm
            else
            {
                transition_cam = true;
                // set cam pos
                transition_cam_Ypos = levels[level].transform.position.y;
                // start timer 
                curr_transition_timer -= Time.deltaTime;

            }
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
        if (collision.gameObject.tag == "Ladder_Tele" && canTeleport)
        {
            // teleport player
            this.transform.position = levels[level].transform.position;
            canTeleport = false;
            ladderUsed = false;
        }

        // Move to next floor hub when using ladder
        if (collision.gameObject.tag == "Ladder_Price")
        {
            inPriceTrigger = true;
        }

        // Move to next floor hub when reaching end of level
        if (collision.gameObject.tag == "Level_End")
        {
            start_transition = true;
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
        if (collision.gameObject.tag == "Level 4")
        {
            level = 4;
            camY = 30; // sets what Y pos the player camera should have
        }

        // if collision with projectile check health
        if (collision.gameObject.tag == "Projectile" && this.gameObject.tag == "Player")
        {
            // if player has health
            if (health > 0)
            {
                // destory projectile
                Destroy(collision.gameObject);
                // deduct health
                health --;
                // create explosion FX
                Instantiate(explosion, exp_pos);
                // play explosion fx
                explosion.Play();
                // play hurt soundeffect
                hurt_sound_effect.GetComponent<AudioSource>().Play();
            }

        }

        //If falling out of game send to start
        if (collision.gameObject.tag == "Kill_Bottom")
        {
            this.transform.position = levels[0].transform.position;
            health = 0;
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
        if (collision.gameObject.tag == "Health_Up" && health <= maxHealth)
        {
            // increase health by one
            health += 1;

            // deactivate power up asset
            collision.gameObject.SetActive(false);
        }

        // check if at floor hub
        if (collision.gameObject.tag == "Hub")
        {
            // increase health by one
            is_at_hub = true;
        }

        // check if at floor hub
        if (collision.gameObject.tag == "end")
        {
            // increase health by one
            stop_following = true;
        }    
        
        // check if at floor hub
        if (collision.gameObject.tag == "finish_line")
        {
            has_finished = true;
        }

        // set player position on map
        for (int i = 1; i <= number_chunks; i++)
        {
            if (collision.gameObject.name == "Chunk Collider " + i)
            {
                current_chunk = i;
            }
        }
       
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function hurt(): 
    ///     - deducts health if hit, playes hurt sound effect
    ///     - used for snake hit
    /// </summary>
    void hurt()
    {
        if (hit == true)
        {
            health -= 1;
            hurt_sound_effect.GetComponent<AudioSource>().Play();
            hit = false;
        }
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function AUDIO(): 
    ///     - plays audio effecrs
    /// </summary>
    void AUDIO()
    {
        // countdown effect
        if(canPlayCountdown == true && countdown_sound_Played < 1)
        {
            canPlayCountdown = false;
            // play countdown effect
            countdown_sound.GetComponent<AudioSource>().Play();
            // set counter for effect algo
            countdown_sound_Played++;
        }
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function heart_containers(): 
    ///     - enables UI hearts depending on health
    /// </summary>
    void heart_containers()
    {
        // loop through hearts
        for(int i = 0; i <= 3; i++)
        {
            // if life matches loop enable heart
            if (health >= i + 1)
            {
                hearts[i].enabled = true;
            }
            // if loop > life disable heart
            else
            {
               hearts[i].enabled = false;
            }
        }
        
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function show_map(): 
    ///     - algop that enables map
    /// </summary>
    void show_map()
    {
        // only allow if player can move
        if(canMove)
        {
            // if player presses input activate
            if (Input.GetButton("Button_Three"))
            {
                Map.SetActive(true);
            }
            // else deactivate
            else
            {
                Map.SetActive(false);
            }
        }

        // if mid turn show map
        else if (map_mid_turn)
        {
            Map.SetActive(true);
        }
        // else disable map
        else
        {
            Map.SetActive(false);
        }

    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function health_controller(): 
    ///     - sends player to start and sets lives to 3 when dead
    /// </summary>
    void health_controller()
    {
        if (health <= 0)
        {
            // teleport player to floor hub
            this.transform.position = levels[level - 1].transform.position;
            Instantiate(bronze_coin, coin_hubs[level - 1].transform.position, Quaternion.identity);
            // rest lives
            health = 3;
        }
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function Pause_Menu()
    ///     - Pauses game
    ///     - allows resume
    ///     - allows to go to main menu
    /// </summary>
    void Pause_Menu()
    {
        // if pause input switch bool
        if (Input.GetButtonDown("Button_Seven"))
        {
            pause = !pause;
        }

        // if paused
        if(pause)
        {
            // if menus button pressed go to menu
            if (Input.GetButtonDown("Button_Two"))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(0);
            }
            // else pause time and activate mpause menu
            else
            {
                Time.timeScale = 0;
                pause_menu.SetActive(true);
            }
            
        }
        // if not paused make sure time is regular
        else
        {
            Time.timeScale = 1;
            pause_menu.SetActive(false);

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

        // On coliison exit with projectile stop explosion particle effect
        if (collision.gameObject.tag == "Projectile")
        {
            explosion.Stop();
        }

        // check if at floor hub
        if (collision.gameObject.tag == "Hub")
        {
            // increase health by one
            is_at_hub = false;
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
        Gizmos.DrawLine(feetPos_L.position, feetPos_L.position + Vector3.down * groundedRaycastLength);
        Gizmos.DrawLine(feetPos_R.position, feetPos_R.position + Vector3.down * groundedRaycastLength);
        
        // attack overlap circle
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
