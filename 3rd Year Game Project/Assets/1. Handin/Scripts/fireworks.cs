using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireworks : MonoBehaviour
{
    public GameObject _fireworks;

    // Start is called before the first frame update
    void Start()
    {
        // set fireworks to inactive
        _fireworks.SetActive(false);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // on collision with player
        if(collision.gameObject.tag == "Player")
        {
            // activate fireworks
            _fireworks.SetActive(true);
        }
    }
}
