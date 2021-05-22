using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion_collider : MonoBehaviour
{
    public GameObject[] cannon_audio; // canon audio clip

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ("Player"))
        {
            for(int i = 0; i < cannon_audio.Length; i++)
            {
                cannon_audio[i].SetActive(true);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            for (int i = 0; i < cannon_audio.Length; i++)
            {
                cannon_audio[i].SetActive(false);
            }
        }
    }
}
