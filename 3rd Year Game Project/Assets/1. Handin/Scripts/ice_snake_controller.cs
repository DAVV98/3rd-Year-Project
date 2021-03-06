﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ice_snake_controller : MonoBehaviour
{
    [Header("Movement")]
    public Transform left;
    public Transform right;
    public bool movingRight;
    public float moveSpeed;
    private float reg_speed;

    [Header("Attack")]
    public bool attack;

    [Header("Animator")]
    public Animator animator; // players animator.

    // Start is called before the first frame update
    void Start()
    {
        reg_speed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        // sets move direction bool
        setBool();

        // patrols between set points
        patrol();

        // attacks player - plays attack anim
        Attack();
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
        if (movingRight)
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
    /// Function: setBool
    /// 
    /// - Changes movingRight bool depending on position
    /// </summary>
    void setBool()
    {
        // if player reaches end change to move left
        if (movingRight && transform.position.x >= right.position.x)
        {
            movingRight = false; // makes player move left
        }
        // if player reaches start change to move right
        else if (!movingRight && transform.position.x <= left.position.x)
        {
            movingRight = true; // makes player move right
        }

       
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function: Attack()
    /// 
    ///     - play attack animation if attack or stop if attack false
    ///     - stops snake movement
    /// </summary>
    void Attack()
    {
        if(attack == true)
        {
            // play attack anim
            animator.SetBool("Attack", true);

            // stop moving
            moveSpeed = 0;
        }
        else
        {
            // stop anim
            animator.SetBool("Attack", false);

            // return to regular speed
            moveSpeed = reg_speed;
        }
    }

}
