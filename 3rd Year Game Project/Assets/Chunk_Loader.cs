using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chunk_Loader : MonoBehaviour
{
    public GameObject chunk;

    // Start is called before the first frame update
    void Start()
    {
        Instantiate(chunk, new Vector3(this.transform.position.x, this.transform.position.y, 0), Quaternion.identity);
    }

}

