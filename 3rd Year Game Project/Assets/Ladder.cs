using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Ladder : MonoBehaviour
{
    [Header("Translate Player")]
    public Transform player;
    public Transform end;
    public bool movedToEnd;

    [Header("Randomiser")]
    private int[] random = { 1, 2, 3, 4, 5, 6 };
    public int usedRandomiser;
    public bool fire1Pressed;
    public bool canPress;


    // Start is called before the first frame update
    void Start()
    {
        movedToEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonUp("Fire1"))
        {
            usedRandomiser = randomiser();
            Debug.Log(usedRandomiser.ToString());
        }

        if(usedRandomiser == 3)
        {
           
            player.position = end.position;
            usedRandomiser = 0;
            
            
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canPress = true;
            Debug.Log("Yes");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canPress = false;
        }
    }

    int randomiser()
    {
        int value = 0;
        int indexNumber = Random.Range(0, random.Length);

        value = random[indexNumber];

        return value;
    }
}


/*
    
*/