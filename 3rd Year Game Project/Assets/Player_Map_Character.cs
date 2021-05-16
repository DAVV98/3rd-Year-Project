using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Map_Character : MonoBehaviour
{
    [Header("Map Positions")]
    public Transform[] map_position; // positions for player tp spawn on map
    public int curr_pos; // current position
    public float offset; // offset of position

    [Header("Represented Player")]
    public GameObject player; // represented player

    // Update is called once per frame
    void Update()
    {
        // checks where player represnting is located (chunk wise)
        curr_pos = player.GetComponent<Player_Controller>().current_chunk;

        // positions representation according to player position
        this.transform.position = new Vector2(map_position[curr_pos - 1].position.x + offset, map_position[curr_pos - 1].position.y);
    }
}
