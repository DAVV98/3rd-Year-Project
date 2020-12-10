using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ladder_V2 : MonoBehaviour
{

    private Player_Controller player_controller;

    [Header("Payment")]
    public int price;
    public bool canPress;
    public bool buttonPressed;

    [Header("Teleport Player")]
    public Transform player;
    public Transform end;


    // Start is called before the first frame update
    void Start()
    {
        player_controller = GameObject.FindObjectOfType<Player_Controller>();

        GameObject player_object = GameObject.FindWithTag("Player");

        player = player_object.transform;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            buttonPressed = true;

        }
        else
        {
            buttonPressed = false;
        }

        chargePlayer();
    }

    void chargePlayer()
    {
        if (player_controller.coins >= price && canPress == true)
        {
            if (buttonPressed == true)
            {
                player_controller.coins = player_controller.coins - price;
                player.position = end.position;
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canPress = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canPress = false;
        }
    }
}
