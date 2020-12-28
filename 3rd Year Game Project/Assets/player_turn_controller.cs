using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private void Start()
    {
        isPlayer1_turn = true;

        player_chooser();

        player_1.GetComponent<Player_Controller>().isMyTurn = true;
        player_2.GetComponent<Player_Controller>().isMyTurn = false;

        player_1.GetComponent<Player_Controller>().nextPlayer = false;
        player_2.GetComponent<Player_Controller>().nextPlayer = true;

        camera_controller();
    }

    private void Update()
    {
        next();

        isPlayer1_turn = player_1.GetComponent<Player_Controller>().isMyTurn;
        isPlayer2_turn = player_2.GetComponent<Player_Controller>().isMyTurn;

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
            player_1_cam.gameObject.SetActive(true);
            player_2_cam.gameObject.SetActive(false);
        }
        else if(isPlayer2_turn == true)
        {
            player_1_cam.gameObject.SetActive(false);
            player_2_cam.gameObject.SetActive(true);
        }

    }
    
}
