using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class headhog_AI : MonoBehaviour
{
    [Header("hubs")]
    public Transform start; // position where player starts and turns
    public Transform end; // position where player goes towards and turns

    [Header("Movement")]
    public float speed;
    public float extra_speed;
    public float zoom_dist;
    public bool zoom;
    private float moveSpeed; // movement speed
    private bool isMoving;
    public Rigidbody2D rb;
    private bool movingRight;

    [Header("Animator")]
    public Animator animator;


    private GameObject player; // list of players

    private float dist; // distance to player to go sonic mode

    private float push_force = 10; // strength to pushback enemy

    void Start()
    {
        // set move speed to regualr speed
        moveSpeed = speed;
        movingRight = true; // always starts enemy moving right
    }

    void Update()
    {
        // create an array of players
        player = GameObject.FindGameObjectWithTag("Player");


        // sets move direction
        setBool();
        // makes enemy patrol set area
        patrol();

        // makes the player use its "sonic" mechanic
        speed_up();
    }
    
    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function: Patrol
    /// 
    /// - Used to make the enemy patrol an area
    /// </summary>
    void patrol()
    {
        // if player moving right move towards end
        if(movingRight)
        {
           
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0)); //makes enemy look right

            rb.velocity = new Vector2(moveSpeed * 1, rb.velocity.y);
        }
        // if player is not moving right go towards starts
        else
        {
           
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0)); // makes enemy look left

            rb.velocity = new Vector2(moveSpeed * -1, rb.velocity.y);
        }
    }
    
    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function: setBool
    /// 
    /// - Changes movingRight bool depending on position
    /// </summary>
    void setBool()
    {
        // if player reaches end change to move left
        if(movingRight && transform.position.x >= end.position.x)
        {
            movingRight = false; // makes player move left
        }
        // if player reaches start change to move right
        else if(!movingRight && transform.position.x <= start.position.x)
        {
            movingRight = true; // makes player move right
        }
    }
    
    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function: Animation_HH
    /// 
    /// - Changes enemy animations
    /// </summary>
    void animation_HH()
    {
        // check if player is moving
        if(moveSpeed != 0)
        {
            isMoving = true; // set is moving to true if player moving
        }

        // Set move animation
        animator.SetBool("isRunning", isMoving); // set animation to running if running
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function: speed_up
    /// 
    /// - Increases move speed of enemy when near a certain distance
    /// </summary>
    void speed_up()
    {
        if(player != null)
        {
            // check distance to each players
            dist = Vector2.Distance(transform.position, player.transform.position);
        }

        // if player in distance less than zoom dist
        if(dist <= zoom_dist)
        {
            moveSpeed = extra_speed; // increase speed
              
        }
        // if no player in distance less than zoom dist
        else if(dist > zoom_dist)
        {
            moveSpeed = speed; // retrun to regular speed
        }
 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        // if collision with player or stop_projectile block:
        //      - Set platform as parent so children move with platform
        if (collision.gameObject.tag == "Player")
        {

            // set direction of push
            Vector2 pushDir = transform.position + collision.transform.position;

            // normalise direction
            Vector2 pushDirN = pushDir * pushDir.normalized;
            
            // push right if player hits from right
            if(collision.transform.position.x > this.transform.position.x) collision.GetComponent<Rigidbody2D>().AddForce(pushDirN * push_force);

            // push left if player hits from left
            if (collision.transform.position.x < this.transform.position.x) collision.GetComponent<Rigidbody2D>().AddForce(-pushDirN * push_force);



        }
    }



}
