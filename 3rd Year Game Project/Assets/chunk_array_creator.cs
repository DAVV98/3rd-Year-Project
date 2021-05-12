using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chunk_array_creator : MonoBehaviour
{
    public GameObject[] level_1_chunks;
    public GameObject[] level_2_chunks;
    public GameObject[] level_3_chunks;
    public GameObject[] level_4_chunks;

    public static GameObject[] floor_1_loaded;
    public static GameObject[] floor_2_loaded;
    public static GameObject[] floor_3_loaded;
    public static GameObject[] floor_4_loaded;

    public GameObject[] all_chunks;
    public int all_chunks_length;
    public float chunks_per_floor;

    public bool run_array_bool;

    private void Start()
    {
        run_array_bool = true;
    }
 
    void Update()
    {
        if(run_array_bool == true)
        {
            function();
        }
        
    }

    void function()
    {
        Debug.Log(all_chunks.Length);

        all_chunks_length = all_chunks.Length;
        chunks_per_floor = Mathf.Floor(all_chunks_length / 4);



        randomizeArray(all_chunks);

        System.Array.Resize(ref level_1_chunks, (int)chunks_per_floor);
        System.Array.Resize(ref level_2_chunks, (int)chunks_per_floor);
        System.Array.Resize(ref level_3_chunks, (int)chunks_per_floor);
        System.Array.Resize(ref level_4_chunks, (int)chunks_per_floor);

        for (int i = 0; i < chunks_per_floor; i++)
        {
            level_1_chunks[i] = all_chunks[i];
        }

        for (int j = (int)chunks_per_floor; j < (chunks_per_floor * 2); j++)
        {
            level_2_chunks[j - (int)chunks_per_floor] = all_chunks[j];
        }


        for (int k = ((int)chunks_per_floor * 2); k < (chunks_per_floor * 3); k++)
        {
            level_3_chunks[k - ((int)chunks_per_floor * 2)] = all_chunks[k];
        }

        for (int l = ((int)chunks_per_floor * 3); l < (chunks_per_floor * 4); l++)
        {
            level_4_chunks[l - ((int)chunks_per_floor * 3)] = all_chunks[l];
        }

        floor_1_loaded = level_1_chunks;
        floor_2_loaded = level_2_chunks;
        floor_3_loaded = level_3_chunks;
        floor_4_loaded = level_4_chunks;

        run_array_bool = false;

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
        for (int i = 0; i < a.Length; i++)
        {
            GameObject obj = a[i];
            int random = Random.Range(0, i);
            a[i] = a[random];
            a[random] = obj;
        }
    }

}
