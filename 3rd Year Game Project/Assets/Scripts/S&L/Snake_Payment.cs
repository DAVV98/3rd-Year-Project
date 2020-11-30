using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class Snake_Payment : MonoBehaviour
{
    private Player_Controller player_controller;

    [Header("Payment")]
    bool canFall;
    public int payment;

    [Header("Teleport Player")]
    public Transform player;
    public Transform end;

    // Start is called before the first frame update
    void Start()
    {
        canFall = false;
        player_controller = GameObject.FindObjectOfType<Player_Controller>();
    }

    // Update is called once per frame
    void Update()
    {
        snakeFall();
    }


    void snakeFall()
    {
        if (canFall && player_controller.coins < payment)
        {
            player.position = end.position;
        }
        else if (player_controller.coins >= payment && canFall)
        {
            canFall = false;
            player_controller.coins = player_controller.coins - payment;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canFall = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canFall = false;
        }
    }
}
