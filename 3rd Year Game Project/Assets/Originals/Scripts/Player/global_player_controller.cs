using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class global_player_controller : MonoBehaviour
{
    [Header("Movement")]
    public float GLOBAL_movementSpeed;
    public float GLOBAL_jumpForce;
    public float GLOBAL_fallMultiplier;
    public float GLOBAL_lowJumpMultiplier;

    [Header("Timer")]
    public int GLOBAL_min_time;
    public int GLOBAL_max_time;

    [Header("Money")]
    public int GLOBAL_coins;

    [Header("Ladder")]
    public int GLOBAL_ladderPrice;

    [Header("Power_Ups")]
    public int GLOBAL_Speed_Up;
    public int GLOBAL_Speed_Up_Active_Turns;

    [Header("Health")]
    public int GLOBAL_Start_Health;

    [Header("Audio")]
    public GameObject GLOBAL_hurt_Sound;

}
