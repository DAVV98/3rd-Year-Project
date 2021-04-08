using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dragon_controller : MonoBehaviour
{
    public float moveSpeed; // movement speed
    public bool move_right;

    [Header("Direction")]
    public Transform left;
    public Transform right;

    [Header("Timer")]
    public float time_between_attacks;
    private float timer_curr;

    [Header("Attack")]
    public float attack_range;
    public bool attacking;
    public float follow_time;

    [Header("Eggs")]
    public GameObject[] eggs;
    public Transform eggs_spawn;
    public float time_between_eggs;
    private float curr_time_between_eggs;
    public bool canDrop;

    private GameObject[] player; // list of players

    ///---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function Start():
    ///     - Runs at the start of the run time
    ///     - Used for Player set up.
    /// </summary>
    void Start()
    {
        // sets initial movemetn and attack parameters 
        move_right = true;
        attacking = false;


    }
    
    ///---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function Update():
    ///     - Runs at every frame
    ///     - Used to update player.
    /// </summary>
    void Update()
    {
        // create an array of players
        player = GameObject.FindGameObjectsWithTag("Player");

        // sets dragon movment direction
        set_direction();
        
        // when attack timer not at zero patrol area
        if(attacking == false)
        { 
            patrol(); 
        }

        // runs attack timer, whihc also calls attack functions
        attack_timer();


        
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function: Patrol():
    /// - Used to make the enemy patrol an area
    /// </summary>
    void patrol()
    {

        // if player moving right move towards end
        if (move_right)
        {
            transform.position = new Vector2(transform.position.x + moveSpeed * Time.deltaTime, transform.position.y); // increment x pos to move right
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0)); //makes enemy look right
        }
        // if player is not moving right go towards starts
        else
        {
            transform.position = new Vector2(transform.position.x - moveSpeed * Time.deltaTime, transform.position.y); // reduce x pos to move left
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0)); // makes enemy look left
        }
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function: set_direction()
    /// - Changes direction enemy flies
    /// </summary>
    void set_direction()
    {
        // move right if you hit left pos
        if(this.transform.position.x <= left.position.x)
        {
            move_right = true;
        }
        // move left if you hit right pos
        else if (this.transform.position.x >= right.position.x)
        {
            move_right = false;
        }
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function: attack_timer()
    /// - timer that decides on dragon behaviour
    ///     - dragon targtes player when timer at zero
    ///     - floats above player until timer is less than -follow time
    ///     - countdown timer
    /// </summary>
    void attack_timer()
    {
        // if timer zero target player, continue counting down timer
        if(timer_curr <= 0 && timer_curr >= -follow_time)
        {
            // targets player
            target_player();
            timer_curr -= Time.deltaTime;
        }
        // counts down until -follow_time, this is the time the dragon floats abopve the player
        else if(timer_curr < -follow_time)
        {
            timer_curr = time_between_attacks;
            attacking = false;
        }
        // else count down timer
        else 
        {
            timer_curr -= Time.deltaTime;
        }

    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function: target_player()
    /// - checks distance and targets player ifin range
    /// </summary>
    void target_player()
    {
        if(player.Length > 0)
        {
            // check distance to each players
            float dist = Vector3.Distance(transform.position, player[0].transform.position);

            // if inside distance range attack player
            if (dist <= attack_range)
            {
                attacking = true;
                attack(player[0].transform);
            }
        }
       

    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function: attack() - parameter: player (Transform), this is the player beiong targeted.
    /// - checks distance and targets player ifin range
    /// </summary>
    void attack(Transform player)
    {
        // vector of the position of the target player
        Vector3 target = new Vector3(player.position.x, this.transform.position.y, this.transform.position.z);

        // move dragon towards target
        transform.position = Vector3.MoveTowards(this.transform.position, target, moveSpeed * Time.deltaTime);

        // vector of position to spawn eggs
        Vector3 spawn_point = new Vector3(eggs_spawn.position.x, eggs_spawn.position.y, 1);
        
        // if dragon canDrop eggs, if above player, instantiate egg, set canDrop to false
        if(canDrop)
        {
            if (this.transform.position.x < (player.position.x + 1) && this.transform.position.x > (player.position.x - 1))
            {
                
                Instantiate(eggs[Random.Range(0,3)], spawn_point, Quaternion.identity);
                canDrop = false;
            }
           
        }

        // if cannot drop egg, use timer to decide when next egg can be dropped
        if(canDrop == false)
        {
            if(curr_time_between_eggs <= 0)
            {
                canDrop = true;
                curr_time_between_eggs = time_between_eggs;
            }
            else
            {
                curr_time_between_eggs -= Time.deltaTime;
            }
        }

        // sets direction of dragon to be towards player
        if(target.x < this.transform.position.x)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0)); //makes enemy look left
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0)); //makes enemy look right
        }
    }


}

