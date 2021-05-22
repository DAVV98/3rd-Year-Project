using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_turn_controller : MonoBehaviour
{
    [Header("Players")]
    public GameObject[] players; // list of players in game
    public bool isPlayer1_turn; // sets player 1 active level
    public bool isPlayer2_turn; // sets player 2 active level
    public bool isPlayer3_turn; // sets player 3 active level
    public bool isPlayer4_turn; // sets player 4 active level 
    public int start_player;
    

    [Header("Camera_Controller")]
    public Camera[] player_cam; // list of player cameras
    public float cam_timer_1; // player 1 camera timer
    public float cam_timer_2; // player 2 camera timer
    public float cam_timer_3; // player 3 camera timer
    public float cam_timer_4; // player 4 camera timer

    public float switch_cam_length = 3; // time it takes to switch cameras

    /// <summary>
    /// Function Awake(): 
    ///     - Runs before start
    /// </summary>
    private void Awake()
    {
        
       // if only two players are active reduce players array by two
        if (player_select_controller.active_players == 2)
        {
            System.Array.Resize(ref players, players.Length - 1);
            System.Array.Resize(ref players, players.Length - 1);
        }
        // if only three players are active reduce players array by one
        else if (player_select_controller.active_players == 3)
        {
            System.Array.Resize(ref players, players.Length - 1);
        }

        // Used to randomly select which player starts the game
        start_player = Random.Range(0, players.Length);

    }

    /// <summary>
    /// Function Awake(): 
    ///     - Runs when game begins
    /// </summary>
    private void Start()
    {
        // sets plays whos starting game to active, all others unactive
        for (int i = 0; i < players.Length; i++)
        {
            if (i != start_player)
            {
                players[i].GetComponent<Player_Controller>().isMyTurn = false;
            }
            // set starting players turn as active
            else if(i == start_player)
            {
                players[i].GetComponent<Player_Controller>().isMyTurn = true;
            }
        }

        // Sets if player turns
        player_chooser();

        // sets all between turn timers to negative at start of game
        cam_timer_1 = -1;
        cam_timer_2 = -1;


        // Runs camera controller, selects which camera to turn active
        camera_controller();
    }

    /// <summary>
    /// Function Update(): 
    ///     - Runs every frame
    /// </summary>
    private void Update()
    {           
        // Run turn switcher
        next();

        // Update player controller turns depending on switcher
        isPlayer1_turn = players[0].GetComponent<Player_Controller>().isMyTurn;
        isPlayer2_turn = players[1].GetComponent<Player_Controller>().isMyTurn;
        if(players.Length > 2)isPlayer3_turn = players[2].GetComponent<Player_Controller>().isMyTurn;
        if (players.Length > 3) isPlayer4_turn = players[3].GetComponent<Player_Controller>().isMyTurn;
        
        // Run camera controller
        camera_controller();
    }


    /// <summary>
    /// Function next(): 
    ///     - sets the booleans from active players player controller, to control which pplayers turn it is 
    /// </summary>
    void next()
    {
        // if players 1's turn is over
        //      set player two turn to active
        if(players[0].GetComponent<Player_Controller>().endTurn == true)
        {
            players[0].GetComponent<Player_Controller>().nextPlayer = true;
            players[0].GetComponent<Player_Controller>().isMyTurn = false;      
            players[0].GetComponent<Player_Controller>().endTurn = false;

            players[1].GetComponent<Player_Controller>().isMyTurn = true;
        }

        // if players 2's turn is over
        //      if players = 2, set player one turn to active
        //      if players > 2, set player three turn to active
        if (players[1].GetComponent<Player_Controller>().endTurn == true)
        {
            players[1].GetComponent<Player_Controller>().nextPlayer = true;
            players[1].GetComponent<Player_Controller>().isMyTurn = false;
            players[1].GetComponent<Player_Controller>().endTurn = false;


            if(players.Length == 2)
            {
                players[0].GetComponent<Player_Controller>().isMyTurn = true;
            }
            else if (players.Length > 2)
            {
                players[2].GetComponent<Player_Controller>().isMyTurn = true;
            }
        }

        if (players.Length >= 3)
        {
            // if players 3's turn is over
            //      if players = 3, set player one turn to active
            //      if players > 3, set player four turn to active
            if (players[2].GetComponent<Player_Controller>().endTurn == true)
            {
                players[2].GetComponent<Player_Controller>().nextPlayer = true;
                players[2].GetComponent<Player_Controller>().isMyTurn = false;
                players[2].GetComponent<Player_Controller>().endTurn = false;


                if (players.Length == 3)
                {
                    players[0].GetComponent<Player_Controller>().isMyTurn = true;
                }
                else if (players.Length > 3)
                {
                    players[3].GetComponent<Player_Controller>().isMyTurn = true;
                }
            }
        }

        if (players.Length >= 4)
        {
            // if players 4's turn is over
            //      set player 1 as active
            if (players[3].GetComponent<Player_Controller>().endTurn == true)
            {
                players[3].GetComponent<Player_Controller>().nextPlayer = true;
                players[3].GetComponent<Player_Controller>().isMyTurn = false;
                players[3].GetComponent<Player_Controller>().endTurn = false;


                players[0].GetComponent<Player_Controller>().isMyTurn = true;

            }
        }
    }

    /// <summary>
    /// Function player_chooser(): 
    ///     - assures only one player can be active at a time.
    /// </summary>
    void player_chooser()
    {
        if(isPlayer1_turn)
        {
            isPlayer2_turn = false;
            isPlayer3_turn = false;
            isPlayer4_turn = false;
        }
        else if(isPlayer2_turn)
        {
            isPlayer1_turn = false;
            isPlayer3_turn = false;
            isPlayer4_turn = false;
        }
        else if (isPlayer3_turn)
        {
            isPlayer1_turn = false;
            isPlayer2_turn = false;
            isPlayer4_turn = false;
        }
        else if (isPlayer4_turn)
        {
            isPlayer1_turn = false;
            isPlayer2_turn = false;
            isPlayer3_turn = false;
        }
    }

    /// <summary>
    /// Function camera_controller(): 
    ///     - assures only one camera can be active at a time.
    /// </summary>
    void camera_controller()
    {
        // if player 1 turn.
        if(isPlayer1_turn == true)
        {
            // player camera timer 
            cam_timer_1 -= Time.deltaTime;

            // set mid turn map of previous player to active
            if(players.Length == 2)players[1].GetComponent<Player_Controller>().map_mid_turn = true;
            if (players.Length == 3) players[2].GetComponent<Player_Controller>().map_mid_turn = true;
            if (players.Length == 4) players[3].GetComponent<Player_Controller>().map_mid_turn = true;

            // once timer is less than zero
            if (cam_timer_1 < 0)
            {
                cam_timer_2 = switch_cam_length;

                // set camera timer of previous player to start time.
                cam_timer_2 = switch_cam_length;
                cam_timer_3 = switch_cam_length;
                cam_timer_4 = switch_cam_length;


                player_cam[0].gameObject.SetActive(true);
                player_cam[1].gameObject.SetActive(false);
                if (players.Length > 2) player_cam[2].gameObject.SetActive(false);
                if (players.Length > 3) player_cam[3].gameObject.SetActive(false);

                // set mid turn map of previous player to inactive
                if (players.Length == 2) players[1].GetComponent<Player_Controller>().map_mid_turn = false;
                if (players.Length == 3) players[2].GetComponent<Player_Controller>().map_mid_turn = false;
                if (players.Length == 4) players[3].GetComponent<Player_Controller>().map_mid_turn = false;

                // allow player to start turn once camera has focused on him
                if (cam_timer_1 > -0.1)
                {
                    players[0].GetComponent<Player_Controller>().canStartTurn = true;
                }
            }
        }


        // if player 1 turn.
        else if (isPlayer2_turn == true)
        {
            // player camera timer 
            cam_timer_2 -= Time.deltaTime;

            // set mid turn map of previous player to active
            players[0].GetComponent<Player_Controller>().map_mid_turn = true;
     

            // once timer is less than zero
            if (cam_timer_2 < 0)
            {

                // set camera timer of previous player to start time.
                cam_timer_1 = switch_cam_length;
                cam_timer_3 = switch_cam_length;
                cam_timer_4 = switch_cam_length;

                // set camera of player to active, set all others to inactive
                player_cam[1].gameObject.SetActive(true);
                player_cam[0].gameObject.SetActive(false);
                if (players.Length > 2) player_cam[2].gameObject.SetActive(false);
                if (players.Length > 3) player_cam[3].gameObject.SetActive(false);

                // set mid turn map of previous player to inactive
                players[0].GetComponent<Player_Controller>().map_mid_turn = false;
              

                // allow player to start turn once camera has focused on him
                if (cam_timer_2 > -0.1)
                {
                    players[1].GetComponent<Player_Controller>().canStartTurn = true;
                }
            }
        }

        // if player 1 turn.
        else if (isPlayer3_turn == true)
        {
            // player camera timer 
            cam_timer_3 -= Time.deltaTime;

            // set mid turn map of previous player to active
            players[1].GetComponent<Player_Controller>().map_mid_turn = true;


            // once timer is less than zero
            if (cam_timer_3 < 0)
            {

                // set camera timer of previous player to start time.
                cam_timer_1 = switch_cam_length;
                cam_timer_2 = switch_cam_length;
                cam_timer_4 = switch_cam_length;

                // set camera of player to active, set all others to inactive
                player_cam[2].gameObject.SetActive(true);
                player_cam[0].gameObject.SetActive(false);
                player_cam[1].gameObject.SetActive(false);
                player_cam[3].gameObject.SetActive(false);

                // set mid turn map of previous player to inactive
                players[1].GetComponent<Player_Controller>().map_mid_turn = false;


                // allow player to start turn once camera has focused on him
                if (cam_timer_3 > -0.1)
                {
                    players[2].GetComponent<Player_Controller>().canStartTurn = true;
                }
            }
        }


        // if player 1 turn.
        else if (isPlayer4_turn == true)
        {
            // player camera timer 
            cam_timer_4 -= Time.deltaTime;

            // set mid turn map of previous player to active
            players[2].GetComponent<Player_Controller>().map_mid_turn = true;


            // once timer is less than zero
            if (cam_timer_4 < 0)
            {

                // set camera timer of previous player to start time.
                cam_timer_1 = switch_cam_length;
                cam_timer_2 = switch_cam_length;
                cam_timer_3 = switch_cam_length;

                // set camera of player to active, set all others to inactive
                player_cam[3].gameObject.SetActive(true);
                player_cam[0].gameObject.SetActive(false);
                player_cam[1].gameObject.SetActive(false);
                player_cam[2].gameObject.SetActive(false);

                // set mid turn map of previous player to inactive
                players[2].GetComponent<Player_Controller>().map_mid_turn = false;


                // allow player to start turn once camera has focused on him
                if (cam_timer_4 > -0.1)
                {
                    players[3].GetComponent<Player_Controller>().canStartTurn = true;
                }
            }
        }

    }

}
