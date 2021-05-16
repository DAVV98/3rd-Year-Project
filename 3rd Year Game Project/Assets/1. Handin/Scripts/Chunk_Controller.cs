using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk_Controller : MonoBehaviour
{
    [Header("Level Settings")]
    public int floor_length; // length of floor
    public int level; // floor level value

    [Header("Load Chunks")]
    public GameObject ladder_chunk; // ladder chunk asset
    public GameObject start_chunk; // floor start chunk asset
    public GameObject end_chunk; // floor end chunk asset


    [Header("Add Snake and/or Ladder")]
    public bool addLadder; // adds ladder chunk to floor

    [Header("Snake & Ladder Randomiser")]
    public int randomStart_L; // starting range of ladder chunk position (inclusive)
    public int randomEnd_L; // ending range of snake chunk position (exclusive)
    private int random_L; // the point where ladder instantiates
    private int Ladder_Pos; // position where ladder should be located

    [Header("Chunk List")]
    public GameObject[] play_chunks; // array of obstacle chunks
    public GameObject[] loaded_chunks; // array of chunks taken from randomised array

    [Header("Chunk Seperation")]
    public int chunk_seperation; // seperation between chunks (length per chunk)

    // runs before start
    void Awake()
    {
        // if this is level 1 - loaded chunks = array creator floor 1
        if(level == 1)
        {
            loaded_chunks = chunk_array_creator.floor_1_loaded;

            if(loaded_chunks != null)
            {
                // resize play chunks array to lenght of chunks per floor
                System.Array.Resize(ref play_chunks, loaded_chunks.Length);
                play_chunks = loaded_chunks;
            }
     
        }
        // if this is level 2 - play chunks = array creator floor 2
        else if (level == 2)
        {
            loaded_chunks = chunk_array_creator.floor_2_loaded;

            if (loaded_chunks != null)
            {
                // resize loaded chunks array to lenght of chunks per floor
                System.Array.Resize(ref play_chunks, loaded_chunks.Length);
                play_chunks = loaded_chunks;
            }
        }
        // if this is level 3 - play chunks = array creator floor 3
        else if (level == 3)
        {
            loaded_chunks = chunk_array_creator.floor_3_loaded;

            if (loaded_chunks != null)
            {
                // resize loaded chunks array to lenght of chunks per floor
                System.Array.Resize(ref play_chunks, loaded_chunks.Length);
                play_chunks = loaded_chunks;
            }
        }
        // if this is level 4 - play chunks = array creator floor 4
        else if (level == 4)
        {
            loaded_chunks = chunk_array_creator.floor_4_loaded;

            if (loaded_chunks != null)
            {
                // resize loaded chunks array to lenght of chunks per floor
                System.Array.Resize(ref play_chunks, loaded_chunks.Length);
                play_chunks = loaded_chunks;
            }
        }
    }
    void Start()
    {
        // set the position for the ladder chunks
        S_L_Pos();

        // instantiates all chunks
        load_chunks();
    }

    /// <summary>
    /// function load_chunks():
    ///     - load start and end chunks
    ///     - adds ladder chunk
    ///     - fills the rest of the positions with play chunks
    /// </summary>
    void load_chunks()
    {
        // instatiate start chunk at beggining of level
        Instantiate(start_chunk, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);

        // instatiate end chunk at end of level
        Instantiate(end_chunk, new Vector3(this.transform.position.x + ((floor_length) * chunk_seperation), this.transform.position.y, 0), Quaternion.identity);

        // loop through lenght of floor
        for(int i = 1; i < floor_length; i++)
        {
            // if not ladder chunk index
            if(i != random_L)
            {
                // instatiate i from play chunks array 
                Instantiate(play_chunks[i - 1], new Vector3(i * chunk_seperation, this.transform.position.y, 0), Quaternion.identity);
            }
            // if ladder chunk index
            else
            {
                // instatiate ladder chunk
                Instantiate(ladder_chunk, new Vector3(random_L * chunk_seperation, this.transform.position.y, 0), Quaternion.identity);
            }
        }


    }

    /// <summary>
    /// function randomizeArray(GameObject[] a):
    ///     Paramater:
    ///     - a = array to shuffle
    ///     Job:
    ///     - shuffles the array positions
    /// </summary>
    void randomizeArray(GameObject[] a)
    {
        for(int i = 0; i < a.Length; i++ )
        {
            GameObject obj = a[i];
            int random = Random.Range(0, i);
            a[i] = a[random];
            a[random] = obj;
        }
    }

    /// <summary>
    /// function S_L_Pos():
    ///     - creates a random point between rwo values for ladder chunk to be placed.
    /// </summary>
    void S_L_Pos()
    {
        // get random value
        random_L = Random.Range(randomStart_L, randomEnd_L);
        // ladder pos is random value * size of chunks
        Ladder_Pos = random_L * chunk_seperation;
    }
}
