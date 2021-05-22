using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_End : MonoBehaviour
{
    // this scene value in scene manager
    public int scene_value;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // on collision with player
        if(collision.gameObject.tag == "Player")
        {
            // next scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene_value + 1);
        }
    }
}
