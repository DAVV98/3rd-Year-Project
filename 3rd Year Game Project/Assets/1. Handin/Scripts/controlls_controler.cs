using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class controlls_controler : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // go back to main menu
        if (Input.GetButtonDown("Button_Two"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
