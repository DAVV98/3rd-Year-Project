using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk_Controller : MonoBehaviour
{
    [Header("Row One")]
    public GameObject chunk;
    public int[] array;
    public int chunk_seperation;

    void Start()
    {
        load_chunks();
    }

    void load_chunks()
    {
        for(int i = 0; i < array.Length; i++)
        {

            Instantiate(chunk, new Vector3(i * chunk_seperation, this.transform.position.y, 0), Quaternion.identity);
        }
    }
}
