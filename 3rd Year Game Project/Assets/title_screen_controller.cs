using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class title_screen_controller : MonoBehaviour
{
    [Header("Text")]
    public Color selected_color;
    public Color unselected_color;
    public int selected_fontSize;
    public int unselected_fontSize;
    public Text PS_text;
    public Text T_text;
    public Text C_text;
    private int button;

    [Header("Surfing Timer")]
    public float start_surf_time;
    private float curr_surf_time;

    // Start is called before the first frame update
    void Start()
    {
        // set intial slection colours
        PS_text.color = selected_color;
        T_text.color = unselected_color;
        C_text.color = unselected_color;

        curr_surf_time = start_surf_time;

        button = 1;
    }
 
    // Update is called once per frame
    void Update()
    {


        player_input();

        switch (button)
        {
            case 1:
                color_size_text(PS_text, T_text, C_text);
                if(Input.GetButtonDown("Button_One")) SceneManager.LoadScene(1);
                break;
            case 2:
                color_size_text(T_text, PS_text, C_text);
                break;
            case 3:
                color_size_text(C_text, PS_text, T_text);
                break;
        }
    }

    /// <summary>
    ///  Function colour_selections():
    ///     - sets the text colours of three input text parameters.
    /// </summary>
    /// <param name="on"> Text that is selected </param>
    /// <param name="off_1"> Text that is not selected </param>
    /// <param name="off_2"> Text that is not selected </param>
    void color_size_text(Text on, Text off_1, Text off_2)
    {
        // set slected button colour and size
        on.color = selected_color;
        on.fontSize = selected_fontSize;

        // set unselceted button colour and size
        off_1.color = unselected_color;
        off_1.fontSize = unselected_fontSize;

        // set unselceted button colour and size
        off_2.color = unselected_color;
        off_2.fontSize = unselected_fontSize;


    }

    void player_input()
    {
        float ver_in = Input.GetAxis("Vertical");

        if(ver_in < 0 && button < 3)
        {
            if(curr_surf_time > 0)
            {
                curr_surf_time -= Time.deltaTime;
            }
            else if(curr_surf_time == 0 || curr_surf_time < 0 && curr_surf_time > -0.1)
            {
                curr_surf_time = start_surf_time;
                button++;
            }
            
        }

        if (ver_in > 0 && button > 1)
        {
            if (curr_surf_time > 0)
            {
                curr_surf_time -= Time.deltaTime;
            }
            else if (curr_surf_time == 0 || curr_surf_time < 0 && curr_surf_time > -0.1)
            {
                curr_surf_time = start_surf_time;
                button--;
            }
        }
    }
}
