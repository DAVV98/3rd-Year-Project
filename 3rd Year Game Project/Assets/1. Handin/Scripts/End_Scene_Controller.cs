using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class End_Scene_Controller : MonoBehaviour
{
  
    // Update is called once per frame
    void Update()
    {
        // if button pressed go to main menu
        if(Input.GetButton("Button_One"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
