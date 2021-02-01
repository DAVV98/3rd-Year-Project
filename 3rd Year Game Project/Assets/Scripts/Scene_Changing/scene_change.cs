using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scene_change : MonoBehaviour
{
    // this scene value in scene manager
    public int scene_value;
    public bool isLast;

    // Update is called once per frame
    void Update()
    {
        if(!isLast)
        {
            // if any key pressed
            if (Input.anyKey)
            {
                // next scene
                UnityEngine.SceneManagement.SceneManager.LoadScene(scene_value + 1);
            }
        }
        else
        {
            // if any key pressed
            if (Input.anyKey)
            {
                // next scene
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
            }

        }
       
    }
}
