using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    public int triggerCount = 0;
    public int maxTriggers = 10;

    public ParticleSystem explosionParticle;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the tag "EnemyBullet"
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            triggerCount++;

            // Check if the trigger count has reached the max limit
            if (triggerCount >= maxTriggers)
            {
                if (explosionParticle != null)
                {
                    explosionParticle.Play();
                }
                Destroy(gameObject);
            }
        }
    }
}
