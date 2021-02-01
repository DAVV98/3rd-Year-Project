using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_turn_controller : MonoBehaviour
{
    [Header("Players")]
    public GameObject player_1;
    public bool isPlayer1_turn;
    public GameObject player_2;
    public bool isPlayer2_turn;

    [Header("Camera_Controller")]
    public Camera player_1_cam;
    public Camera player_2_cam;
    public float cam_timer_1;
    public float cam_timer_2;
    public float switch_cam_length = 3;

    [Header("UI_Controller")]
    public Text player_1_timer;
    public Text player_2_timer;
    public Text player_1_coins;
    public Text player_2_coins;

    private void Start()
    {
        // Start game with player 1.
        isPlayer1_turn = true;

        // Run player choser
        player_chooser();

        // Set Turns start of game
        player_1.GetComponent<Player_Controller>().isMyTurn = true;
        player_2.GetComponent<Player_Controller>().isMyTurn = false;

        player_1.GetComponent<Player_Controller>().nextPlayer = false;
        player_2.GetComponent<Player_Controller>().nextPlayer = true;

        // Run camera controller
        camera_controller();

        cam_timer_1 = switch_cam_length;
        cam_timer_2 = switch_cam_length;
    }

    private void Update()
    {
        // Run turn switcher
        next();

        // Update player controller turns depending on switcher
        isPlayer1_turn = player_1.GetComponent<Player_Controller>().isMyTurn;
        isPlayer2_turn = player_2.GetComponent<Player_Controller>().isMyTurn;

        // Run camera controller
        camera_controller();
    }

    void next()
    {
        if(player_1.GetComponent<Player_Controller>().endTurn == true)
        {
            player_1.GetComponent<Player_Controller>().nextPlayer = true;
            player_1.GetComponent<Player_Controller>().isMyTurn = false;
            player_2.GetComponent<Player_Controller>().nextPlayer = false;
            player_2.GetComponent<Player_Controller>().isMyTurn = true;
            player_1.GetComponent<Player_Controller>().endTurn = false;
        }

        if (player_2.GetComponent<Player_Controller>().endTurn == true)
        {
            player_2.GetComponent<Player_Controller>().nextPlayer = true;
            player_2.GetComponent<Player_Controller>().isMyTurn = false;
            player_1.GetComponent<Player_Controller>().nextPlayer = false;
            player_1.GetComponent<Player_Controller>().isMyTurn = true;
            player_2.GetComponent<Player_Controller>().endTurn = false;
        }
    }


    void player_chooser()
    {
        if(isPlayer1_turn)
        {
            isPlayer2_turn = false;
        }
        else if(isPlayer2_turn)
        {
            isPlayer1_turn = false;
        }
    }

    void camera_controller()
    {
        if(isPlayer1_turn == true)
        {
            cam_timer_1 -= Time.deltaTime;

            if (cam_timer_1 < 0)
            {
                cam_timer_2 = switch_cam_length;
                player_1_cam.gameObject.SetActive(true);
                player_2_cam.gameObject.SetActive(false);

                player_1_timer.gameObject.SetActive(true);
                player_2_timer.gameObject.SetActive(false);

                player_1_coins.gameObject.SetActive(true);
                player_2_coins.gameObject.SetActive(false);
            }
        }
        else if(isPlayer2_turn == true)
        {
            cam_timer_2 -= Time.deltaTime;

            if (cam_timer_2 < 0)
            {
                cam_timer_1 = switch_cam_length;
                player_1_cam.gameObject.SetActive(false);
                player_2_cam.gameObject.SetActive(true);

                player_1_timer.gameObject.SetActive(false);
                player_2_timer.gameObject.SetActive(true);

                player_1_coins.gameObject.SetActive(false);
                player_2_coins.gameObject.SetActive(true);
            }
        }

    }
}
