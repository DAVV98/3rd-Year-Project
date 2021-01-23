using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class price_trigger : MonoBehaviour
{
    public GameObject[] player;
    public bool player1_trig;
    public bool player2_trig;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player");

        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        player1_trig = player[0].GetComponent<Player_Controller>().inPriceTrigger;
        player2_trig = player[1].GetComponent<Player_Controller>().inPriceTrigger;

        if(player1_trig || player2_trig)
        {
            this.gameObject.SetActive(true);
        }
        else
        {
            this.gameObject.SetActive(false);
        }
               
    }
}
