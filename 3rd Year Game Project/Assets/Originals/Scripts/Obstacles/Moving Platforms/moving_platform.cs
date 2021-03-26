using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving_platform : MonoBehaviour
{
    [Header("Plat_Type")]
    public bool isHorizontal;
    public bool isVertical;
    public bool type_1;
    public bool type_2;

    [Header("Positions")]
    public Transform left_pos;
    public Transform right_pos;
    public Transform up_pos;
    public Transform bot_pos;

    public Transform start_pos;
    public bool move_right;
    public bool move_left;
    public bool move_up;
    public bool move_down;

    [Header("Movement")]
    public float platform_move_speed;

    public Vector2 nextPos;
    
    // Start is called before the first frame update
    void Start()
    {
        if (type_1)
        {
            move_right = true;
            move_left = false;
            move_up = true;
            move_down = false;
        }
        else if (type_2)
        {
            move_right = false;
            move_left = true;
            move_up = false;
            move_down = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isHorizontal)
        {
            horizontal_plat();
        }
        else if(isVertical)
        {
            vertical_plat();
        }
    }

    private void horizontal_plat()
    {
        if (this.transform.position.x <= left_pos.position.x)
        {
            move_right = true;
            move_left = false;
        }
        else if (this.transform.position.x >= right_pos.position.x)
        {
            move_right = false;
            move_left = true;
        }


        if (move_right == true && move_left == false)
        {
            transform.position = new Vector2(transform.position.x + platform_move_speed * Time.deltaTime, transform.position.y);
        }
        else if (move_left == true && move_right == false)
        {
            transform.position = new Vector2(transform.position.x - platform_move_speed * Time.deltaTime, transform.position.y);
        }
    }

    private void vertical_plat()
    {
        if (this.transform.position.y <= bot_pos.position.y)
        {
            move_up = true;
            move_down = false;
        }
        else if (this.transform.position.y >= up_pos.position.y)
        {
            move_up = false;
            move_down = true;
        }


        if (move_up == true && move_down == false)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + platform_move_speed * Time.deltaTime);
        }
        else if (move_down == true && move_up == false)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - platform_move_speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Stop_Projectiles")
        {
            collision.collider.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Stop_Projectiles")
        {
            collision.collider.transform.SetParent(null);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(bot_pos.position, up_pos.position);
    }
}
