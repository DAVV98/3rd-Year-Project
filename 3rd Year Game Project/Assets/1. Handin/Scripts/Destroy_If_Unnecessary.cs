using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_If_Unnecessary : MonoBehaviour
{

    [Header("Players Necessary")]
    public int players_necessary; // min players needed for this object to be alive
    
    // Start is called before the first frame update
    void Awake()
    {
        // destory object if not necessary for number of players
        if(player_select_controller.active_players < players_necessary)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }

}
