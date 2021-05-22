using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu_Music : MonoBehaviour
{
    public GameObject music;

    // Start is called before the first frame update
    void Awake()
    {
        /*
            The following code was taken from Superrodan Jan 16, 2015 https://answers.unity.com/questions/878382/audio-or-music-to-continue-playing-between-scene.html
        */

        //When the scene loads it checks if there is an object called "MUSIC".
        music = GameObject.Find("MUSIC");
        
        if (music == null)
        {
            //If this object does not exist then it does the following:
            //1. Sets the object this script is attached to as the music player
            music = this.gameObject;
            this.GetComponent<AudioSource>().Play();

            //2. Renames THIS object to "MUSIC" for next time
            music.name = "MUSIC";

            //3. Tells THIS object not to die when changing scenes.
            if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 1 || SceneManager.GetActiveScene().buildIndex == 2)
            {
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                if (this.gameObject.name != "MUSIC")
                {
                    //If there WAS an object in the scene called "MUSIC" (because we have come back to
                    //the scene where the music was started) then it just tells this object to 
                    //destroy itself if this is not the original
                    Destroy(this.gameObject);
                }
            }

        }    

        
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            Destroy(this.gameObject);
        }
    }
}
