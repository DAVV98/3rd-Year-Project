using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using AlanZucconi.Snake;

public class SwitchAIs : MonoBehaviour
{

    public SnakeGame Snake;

    public SnakeAI AIA;
    public SnakeAI AIB;

    private bool first = true;

    public KeyCode Key;

	void Update ()
    {
		if (Input.GetKeyDown(Key))
        {
            Snake.AI = first ? AIB : AIA;
            Snake.ReloadAI();

            first = !first;
        }
	}
}
