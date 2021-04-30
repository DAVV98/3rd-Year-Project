using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;


public class player_select_controller : MonoBehaviour
{
    [Header("Players")]
    public static int active_players = 2;
    public Text players_num;

    [Header("Barriers")]
    public GameObject P3_Barrier;
    public GameObject P4_Barrier;
    public GameObject P3_Text;
    public GameObject P4_Text;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // shows players how many players are active
        players_num.text = active_players.ToString();

        player_input();

        hidden_players();
    }

    /// <summary>
    /// function player_input():
    ///     function that takes in player input
    /// </summary>
    void player_input()
    {
        // if players inputs for more players add players
        // only if players < 4
        if(Input.GetButtonDown("Button_Three"))
        {
            if(active_players < 4)
            {
                active_players++;
            }
        }

        // if players inputs for less players reduce players
        // only if players > 2
        if (Input.GetButtonDown("Button_Two"))
        {
            if (active_players > 2)
            {
                active_players--;
            }
        }

        // if start game called change scene
        if (Input.GetButtonDown("Button_One"))
        {
            SceneManager.LoadScene(3);
        }

    }

    /// <summary>
    /// function hidden_players():
    ///     function that hiddes or shows players depending on how many active players
    /// </summary>
    void hidden_players()
    {
        // show p3 if players > 2 else hide
        if(active_players > 2)
        {
            P3_Barrier.SetActive(false);
            P3_Text.SetActive(true);
        }
        else
        {
            P3_Barrier.SetActive(true);
            P3_Text.SetActive(false);
        }

        // show p4 if players > 3 else hide
        if (active_players > 3)
        {
            P4_Barrier.SetActive(false);
            P4_Text.SetActive(true);
        }
        else
        {
            P4_Barrier.SetActive(true);
            P4_Text.SetActive(false);
        }


    }
}
