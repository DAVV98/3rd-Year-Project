using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class money_wiz : MonoBehaviour
{
    [Header("Blocker")]
    public GameObject blocker; // used to hide or activate blocker
    public GameObject buttons; // used to hide or activate buttons
    private GameObject active_player; // player who is currently playing

    private void Start()
    {
        //hide controller input hint
        buttons.SetActive(false);

        // activate portal blocker
        blocker.SetActive(true);
    }
    void Update()
    {
        // if player exists
        if(active_player != null)
        {
            // turns on blocker when player goes through ladder
            if (active_player.GetComponent<Player_Controller>().ladderUsed == false)
            {
                blocker.GetComponent<ladder_portal>().blocker_off = false;
            }
        }
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // checks collsion with player
        if (collision.tag == "Player")
        {
            // sets active player as player who colllided 
            active_player = collision.gameObject;

            // activate buttons - tells player what to press
            buttons.SetActive(true);
            
            // allows player to use ladder
            collision.GetComponent<Player_Controller>().canUseLadder = true;
            
            // if ladder has been used
            if(collision.GetComponent<Player_Controller>().ladderUsed == true)
            {
                // turn blocker off
                blocker.GetComponent<ladder_portal>().blocker_off = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            // if player not in collider turn buttons off
            buttons.SetActive(false);

            // player can no longer use ladder
            collision.GetComponent<Player_Controller>().canUseLadder = false;
        }

    }
}
