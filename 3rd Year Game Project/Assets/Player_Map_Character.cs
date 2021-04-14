using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Map_Character : MonoBehaviour
{
    [Header("Map Positions")]
    public Transform[] map_position;
    public int curr_pos;

    [Header("Represented Player")]
    public GameObject player;

    // Update is called once per frame
    void Update()
    {
        curr_pos = player.GetComponent<Player_Controller>().current_chunk;

        this.transform.position = map_position[curr_pos - 1].position;
    }
}
