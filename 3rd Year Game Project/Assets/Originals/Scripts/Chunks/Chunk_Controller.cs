using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk_Controller : MonoBehaviour
{
    [Header("Level Settings")]
    public int floor_length;

    [Header("Load Chunks")]
    public GameObject ladder_chunk;
    public GameObject start_chunk;
    public GameObject end_chunk;


    [Header("Add Snake and/or Ladder")]
    public bool addLadder;

    [Header("Snake & Ladder Randomiser")]
    public int randomStart_L; // starting range of ladder chunk position (inclusive)
    public int randomEnd_L; // ending range of snake chunk position (exclusive)
    private int random_L;
    private int Ladder_Pos;

    [Header("Chunk List")]
    public GameObject[] play_chunks;

    [Header("Chunk Seperation")]
    public int chunk_seperation;

    void Awake()
    {
        // Randomise list of possible chunks to have arandomised floor level
        randomizeArray(play_chunks);
    }
    void Start()
    {
        // Set position of snake and ladder chunk if exists.
        S_L_Pos();

        // Load chunks 
        load_chunks();


    }

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

    // Shuffles values of array randomly: used for randmoised array
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

    void S_L_Pos()
    {
        // Set Position of ladder.
        random_L = Random.Range(randomStart_L, randomEnd_L);
        Ladder_Pos = random_L * chunk_seperation;
    }
}
