using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving_platform : MonoBehaviour
{
    [Header("Plat_Type")]
    public bool isHorizontal; // if true platfrom moves horizontally
    public bool isVertical; // if true platform moves vertically

    [Header("Start/End Positions")]
    // 4 vertices positions
    public Transform left_pos;
    public Transform right_pos;
    public Transform up_pos;
    public Transform bot_pos;
    public Transform start_pos;
    
    // for movement algorithm
    private bool move_right;
    private bool move_left;
    private bool move_up;
    private bool move_down;

    [Header("Movement")]
    public float platform_move_speed; // speed which platform moves


    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function Start():
    ///     - Runs at the start of the run time
    ///     - Used for Player set up.
    /// </summary>
    
    void Start()
    {

        // sets bool for horizontal platform algorithm
        if (isHorizontal)
        {
            move_right = true;
            move_left = false;
            move_up = true;
            move_down = false;
        }

        // sets bool for vertical platform algorithm
        else if (isVertical)
        {
            move_right = false;
            move_left = true;
            move_up = false;
            move_down = true;
        }
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function Update():
    ///     - Runs at every frame
    ///     - Used to update player.
    /// </summary>

    void Update()
    {
        // runs horizontal platform algorithm if isHorizontal == true
        if(isHorizontal)
        {
            horizontal_plat();
        }

        // runs vertical platform algorithm if isVertical == true
        else if (isVertical)
        {
            vertical_plat();
        }
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function horizontal_plat():
    ///     - Makes the platform move in a horizontal direction, between right pos and left pos.
    /// </summary>

    private void horizontal_plat()
    {
        // if platform reaches left pos move right
        if (this.transform.position.x <= left_pos.position.x)
        {
            move_right = true;
            move_left = false;
        }
        // if platform reaches right pos move left
        else if (this.transform.position.x >= right_pos.position.x)
        {
            move_right = false;
            move_left = true;
        }

        // if more right is true, move plaform right by changing transform.position
        if (move_right == true)
        {
            transform.position = new Vector2(transform.position.x + platform_move_speed * Time.deltaTime, transform.position.y);
        }
        // if more left is true, move plaform left by changing transform.position
        else if (move_left == true)
        {
            transform.position = new Vector2(transform.position.x - platform_move_speed * Time.deltaTime, transform.position.y);
        }
    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function vertical_plat():
    ///     - Makes the platform move in a vertical direction, between bot pos and up pos.
    /// </summary>

    private void vertical_plat()
    {
        // if platform reaches bot pos move up
        if (this.transform.position.y <= bot_pos.position.y)
        {
            move_up = true;
            move_down = false;
        }

        // if platform reaches up pos move down
        else if (this.transform.position.y >= up_pos.position.y)
        {
            move_up = false;
            move_down = true;
        }

        // if more up is true, move plaform up by changing transform.position
        if (move_up == true)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + platform_move_speed * Time.deltaTime);
        }
        // if more down is true, move plaform down by changing transform.position
        else if (move_down)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - platform_move_speed * Time.deltaTime);
        }
    }


    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function OnDrawGizmos(): 
    ///     - draws gizmos 
    /// </summary>
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(bot_pos.position, up_pos.position);
    }
}
