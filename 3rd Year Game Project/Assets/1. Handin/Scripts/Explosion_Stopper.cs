using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Stopper : MonoBehaviour
{
    [Header("Explosion Effect")]
    public ParticleSystem explosion;
    public Transform exp_pos;

    // On coliison with projectile run explosion particle effect
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            // instatiate cannon poarticel effect
            Instantiate(explosion, exp_pos);

            // play effect
            explosion.Play();

        }
    }
    
    // On coliison exit with projectile stop explosion particle effect
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            // stop effect
            explosion.Stop();
        }
    }
}
