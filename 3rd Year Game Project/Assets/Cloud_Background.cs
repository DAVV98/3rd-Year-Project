using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud_Background : MonoBehaviour
{
    public float cloud_speed;
    private float speed;
    private float scale;
    private Rigidbody2D rb;
    public Transform start_clouds;

    void Start()
    {
        // get cloud rigidbody.
        rb = this.GetComponent<Rigidbody2D>();

        speed = Random.Range(cloud_speed - 0.15f, cloud_speed + 0.15f);
        scale = Random.Range(-0.15f, 0.15f);

        this.transform.localScale = new Vector3(this.transform.localScale.x + scale, this.transform.localScale.y + scale, this.transform.localScale.z);

    }
    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Move to next floor hub when using ladder
        if (collision.gameObject.tag == "cloud_stopper")
        {
            this.transform.position = new Vector2(start_clouds.position.x, this.transform.position.y);
        }
    }
}