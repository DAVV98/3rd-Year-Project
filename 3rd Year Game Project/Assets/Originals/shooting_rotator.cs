using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooting_rotator : MonoBehaviour
{
    [Header("Players")]
    private GameObject[] players;
    public GameObject curr_player;

    [Header("Target Mechanism")]
    public float attack_dist;

    [Header("Dart")]
    public GameObject dart;
    public Transform dart_start;
    public GameObject shoot_Direction;
    public float dart_force;

    [Header("Shooting Timer")]
    public float time_between_shots;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        // set timer
        timer = time_between_shots;

    }

    // Update is called once per frame
    void Update()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        aim_shoot();

    }

    void aim_shoot()
    {

        // Loop through players in game
        for (int i = 0; i < players.Length; i++)
        {
            // check distance to each players
            float dist = Vector3.Distance(transform.position, players[i].transform.position);
            //transform.rotation = Quaternion.Euler(0, 0, 0);

            if (dist <= attack_dist)
            {
                float hyp = dist;
                float adj = this.transform.position.y - players[i].transform.position.y;

                float cos = adj / hyp;

                float inv_cos = Mathf.Acos(cos);

                float angle_deg = inv_cos * Mathf.Rad2Deg;

                if (players[i].transform.position.x <= this.transform.position.x)
                {
                    transform.rotation = Quaternion.Euler(0, 0, -angle_deg);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, angle_deg);
                }

                if (timer <= 0)
                {
                    GameObject dart_objects = Instantiate(dart, dart_start.position, Quaternion.identity);

                    Vector3 Direction = (shoot_Direction.transform.position - this.transform.position).normalized;

                    dart_objects.GetComponent<Rigidbody2D>().AddForce(Direction.normalized * dart_force, ForceMode2D.Impulse);
                   
                    timer = time_between_shots;
                }
                else
                {
                    timer -= Time.deltaTime;
                }

                break;
            }



        }
    }
}
