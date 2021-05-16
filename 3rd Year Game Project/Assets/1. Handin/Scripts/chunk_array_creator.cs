using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chunk_array_creator : MonoBehaviour
{
    [Header("All Chunks")]
    private int all_chunks_length; // length of all chunks array
    private float chunks_per_floor; // chunks per floor
    public GameObject[] all_chunks; // array of all chunks

    [Header ("Floor Arrays")]
    // manoevrable arrays 
    public GameObject[] level_1_chunks;
    public GameObject[] level_2_chunks;
    public GameObject[] level_3_chunks;
    public GameObject[] level_4_chunks;

    // static arrays sent to game screen
    public static GameObject[] floor_1_loaded;
    public static GameObject[] floor_2_loaded;
    public static GameObject[] floor_3_loaded;
    public static GameObject[] floor_4_loaded;

    [Header("Stop Function")]
    public bool run_array_bool; // turn on and off algo

    private void Awake()
    {
        // length of all chunks array
        all_chunks_length = all_chunks.Length;

        // calculate how many chunks there should be per floor
        chunks_per_floor = Mathf.Floor(all_chunks_length / 4);

        // randomise all_chunks array for randomised level generation
        randomizeArray(all_chunks);

    }

    private void Start()
    {
        // when game scene starts run randomization + seperation algorithm
        run_array_bool = true;
    }
 
    void Update()
    {
        // if can run array, run function
        if(run_array_bool == true)
        {
            // function that seperates randomised array of all chunks 
            seperate_chunks();
        }
        
    }


    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// Function seperate_chunks(): 
    ///     - seperates chunks into arrays that will be implemented into the seperate chunk controllers in game
    /// </summary>
    void seperate_chunks()
    {
        // resize the lenght of seperate level arrays to be lenght of chunks per floor
        System.Array.Resize(ref level_1_chunks, (int)chunks_per_floor);
        System.Array.Resize(ref level_2_chunks, (int)chunks_per_floor);
        System.Array.Resize(ref level_3_chunks, (int)chunks_per_floor);
        System.Array.Resize(ref level_4_chunks, (int)chunks_per_floor);

        // add first group of chunks to level one chunks
        for (int i = 0; i < chunks_per_floor; i++)
        {
            level_1_chunks[i] = all_chunks[i];
        }

        // add second group of chunks to level one chunks
        for (int j = (int)chunks_per_floor; j < (chunks_per_floor * 2); j++)
        {
            level_2_chunks[j - (int)chunks_per_floor] = all_chunks[j];
        }

        // add third group of chunks to level one chunks
        for (int k = ((int)chunks_per_floor * 2); k < (chunks_per_floor * 3); k++)
        {
            level_3_chunks[k - ((int)chunks_per_floor * 2)] = all_chunks[k];
        }

        // add fourth group of chunks to level one chunks
        for (int l = ((int)chunks_per_floor * 3); l < (chunks_per_floor * 4); l++)
        {
            level_4_chunks[l - ((int)chunks_per_floor * 3)] = all_chunks[l];
        }

        // make static arrays = to manouverable arrays to send to next scene
        floor_1_loaded = level_1_chunks;
        floor_2_loaded = level_2_chunks;
        floor_3_loaded = level_3_chunks;
        floor_4_loaded = level_4_chunks;

        // run algo bool to false so this onlt run once
        run_array_bool = false;

    }

    //---------------------------------------------------------------------------------------------------------------------------------------------------------------

    /// <summary>
    /// function randomizeArray(GameObject[] a):
    ///     Paramater:
    ///     - a = array to shuffle
    ///     Job:
    ///     - shuffles the array positions
    /// </summary>
    void randomizeArray(GameObject[] a)
    {
        for (int i = 0; i < a.Length; i++)
        {
            GameObject obj = a[i];
            int random = Random.Range(0, i);
            a[i] = a[random];
            a[random] = obj;
        }
    }

}
