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

    [Header("Animator")]
    public Animator animator;

    private GameObject[] players;


    private bool movingRight;

    void Start()
    {
        moveSpeed = speed;
        movingRight = true; // always starts enemy moving right
    }

    void Update()
    {
        // create an array of players
        players = GameObject.FindGameObjectsWithTag("Player");


        // sets move direction
        setBool();
        // makes enemy patrol set area
        patrol();

        speed_up();
    }

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

    /// <summary>
    /// Function: speed_up
    /// 
    /// - Increases move speed of enemy when near a certain distance
    /// </summary>
    void speed_up()
    {

        // Loop through players in game
        for (int i = 0; i < players.Length; i++)
        {
            // check distance to each players
            float dist = Vector3.Distance(transform.position, players[i].transform.position);

            // if player in distance less than zoom dist
            if(dist <= zoom_dist)
            {
                moveSpeed = extra_speed; // increase speed
                break; // if player close by end loop
            }
            // if no player in distance less than zoom dist
            else
            {
                moveSpeed = speed; // retrun to regular speed
            }

 

        }
    }


 }
