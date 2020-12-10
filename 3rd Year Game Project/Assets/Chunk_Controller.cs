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


    [Header("Row Type")]
    public bool isSnake;
    public bool isLadder;

    [Header("S || L Pos")]
    public int randomStart;
    public int randomEnd;
    public int random;
    public int special_pos;
    public int start;

    [Header("Build Row")]
    public int[] array;
    public int chunk_seperation;


    void Start()
    {
        start = 0;
        random = Random.Range(randomStart, randomEnd);
        special_pos = random * chunk_seperation;
        load_chunks();

    }

    void load_chunks()
    {
        Instantiate(start_chunk, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);

        if (isLadder)Instantiate(ladder_chunk, new Vector3(special_pos, this.transform.position.y, 0), Quaternion.identity);

        if(isSnake)Instantiate(snake_chunk, new Vector3(special_pos, this.transform.position.y, 0), Quaternion.identity);

        for (int i = 1; i < array.Length; i++)
        {
            if(i != random)
            {
               Instantiate(reg_chunk, new Vector3(i * chunk_seperation, this.transform.position.y, 0), Quaternion.identity);
            }
           

        }
    }
}
