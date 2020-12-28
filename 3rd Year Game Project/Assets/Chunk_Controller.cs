using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk_Controller : MonoBehaviour
{
    [Header("Load Chunks")]
    public GameObject reg_chunk;
    public GameObject ladder_chunk;
    public GameObject snake_chunk;
    public GameObject start_chunk;
    public GameObject end_chunk;


    [Header("Row Type")]
    public bool isSnake;
    public bool isLadder;

    [Header("S || L Pos")]
    public int randomStart_S;
    public int randomEnd_S;
    public int randomStart_L;
    public int randomEnd_L;
    public int random_S;
    public int random_L;
    public int Snake_Pos;
    public int Ladder_Pos;
    public int start;

    [Header("Build Row")]
    public int[] array;
    public GameObject[] play_chunks;
    public int chunk_seperation;

    void Awake()
    {
        randomizeArray(play_chunks);
    }
    void Start()
    {
        start = 0;
        
        random_S = Random.Range(randomStart_S, randomEnd_S);
        Snake_Pos = random_S * chunk_seperation;

        random_L = Random.Range(randomStart_L, randomEnd_L);
        Ladder_Pos = random_L * chunk_seperation;

        load_chunks();


    }

    void load_chunks()
    {
        // Start chunck
        Instantiate(start_chunk, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);

        //End chunk
        Instantiate(end_chunk, new Vector3(this.transform.position.x + (play_chunks.Length * chunk_seperation), this.transform.position.y, 0), Quaternion.identity);

        if (isLadder)Instantiate(ladder_chunk, new Vector3(Ladder_Pos, this.transform.position.y, 0), Quaternion.identity);

        if(isSnake)Instantiate(snake_chunk, new Vector3(Snake_Pos, this.transform.position.y, 0), Quaternion.identity);

        for (int i = 1; i < play_chunks.Length; i++)
        {

            if (isLadder && isSnake)
            {
                if (i != random_L && i != random_S)
                {
                    Instantiate(play_chunks[i], new Vector3(i * chunk_seperation, this.transform.position.y, 0), Quaternion.identity);
                }
            }

            else if(isSnake && !isLadder)
            {
                if (i != random_S)
                {
                    Instantiate(play_chunks[i], new Vector3(i * chunk_seperation, this.transform.position.y, 0), Quaternion.identity);
                }
            } 

            else if(isLadder && !isSnake)
            {
                if (i != random_L)
                {
                    Instantiate(play_chunks[i], new Vector3(i * chunk_seperation, this.transform.position.y, 0), Quaternion.identity);
                }
            }

          



        }
    }

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
}
