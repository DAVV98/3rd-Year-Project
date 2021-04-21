using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk_Controller : MonoBehaviour
{
    [Header("Level Settings")]
    public int floor_length; // length of floor

    [Header("Load Chunks")]
    public GameObject ladder_chunk; 
    public GameObject start_chunk;
    public GameObject end_chunk;


    [Header("Add Snake and/or Ladder")]
    public bool addLadder; // adds ladder chunk to floor

    [Header("Snake & Ladder Randomiser")]
    public int randomStart_L; // starting range of ladder chunk position (inclusive)
    public int randomEnd_L; // ending range of snake chunk position (exclusive)
    private int random_L; // the point where ladder instantiates
    private int Ladder_Pos; //

    [Header("Chunk List")]
    public GameObject[] play_chunks;

    [Header("Chunk Seperation")]
    public int chunk_seperation;

    // runs before start
    void Awake()
    {
        // randomises Play_chunks array, so chunks are placed in random order
        randomizeArray(play_chunks);
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
        Instantiate(end_chunk, new Vector3(this.transform.position.x + (floor_length * chunk_seperation), this.transform.position.y, 0), Quaternion.identity);

        // If Snake and/or ladder instatiate at position set by S_L_Pos()
        if (addLadder)Instantiate(ladder_chunk, new Vector3(Ladder_Pos, this.transform.position.y, 0), Quaternion.identity);

        // Procedural Generation Algorithm
        // - For loop that instatiates chunks to create floor
        for (int i = 1; i < floor_length; i++)
        {
            // If ladder chunk exists, dont instantiate from list at that pos but do elsewhere.
            if (addLadder)
            {
                if (i != random_L)
                {
                    Instantiate(play_chunks[i], new Vector3(i * chunk_seperation, this.transform.position.y, 0), Quaternion.identity);
                }
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
