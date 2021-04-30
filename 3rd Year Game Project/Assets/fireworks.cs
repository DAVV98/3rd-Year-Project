using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireworks : MonoBehaviour
{
    public GameObject _fireworks;

    // Start is called before the first frame update
    void Start()
    {
        _fireworks.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _fireworks.SetActive(true);
        }
    }
}
