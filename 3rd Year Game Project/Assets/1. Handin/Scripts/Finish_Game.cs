using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Finish_Game : MonoBehaviour
{
    // checks which player has won and loads winning scene for that player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player_1.0")
        {
            SceneManager.LoadScene(5);
        }
        else if (collision.gameObject.name == "Player_2")
        {
            SceneManager.LoadScene(6);
        }
        else if(collision.gameObject.name == "Player_3")
        {
            SceneManager.LoadScene(7);
        }
        else if(collision.gameObject.name == "Player_4")
        {
            SceneManager.LoadScene(8);
        }
    }
}
