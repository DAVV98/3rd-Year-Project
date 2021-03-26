using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dart : MonoBehaviour
{
    [Header("Speed")]
    public float moveSpeed;
    private Rigidbody2D m_Rigidbody;
    
    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Rigidbody component
        m_Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }
}
