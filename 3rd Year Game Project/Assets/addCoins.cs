using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class addCoins : MonoBehaviour
{
    [Header("Coin Value")]

    public int value;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == ("Player"))
        {
            
            collision.gameObject.GetComponent<Player_Controller>().coins += value;
            Destroy(this.gameObject);
        }
    }
}
