using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    public int index;
    public int triggerCount = 0;
    public int maxTriggers = 10;
    public GameObject GameManager;

    public ParticleSystem explosionParticle;
    public GameObject explosionVFX;

    public GameObject HealthDisplayManager;

    void Awake()
    {
        GameManager = GameObject.Find("Game Manager");
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has the tag "EnemyBullet"
        if (other.gameObject.CompareTag("EnemyBullet"))
        {
            triggerCount++;

            ParticleSystem particle = Instantiate(explosionParticle, other.transform.position, Quaternion.identity);
            particle.Play();
            Destroy(other.gameObject);
            Destroy(particle.gameObject, particle.main.duration);

            HealthDisplayManager.GetComponent<HealthDisplayManager>().UpdatePillarHealth(index, maxTriggers - triggerCount);

            // Check if the trigger count has reached the max limit
            if (triggerCount >= maxTriggers)
            {
                // Instantiate the explosion VFX
                GameObject explosion = Instantiate(explosionVFX, transform.position, Quaternion.identity);

                GameManager.GetComponent<GameManager>().PillarDestroyed();
                Destroy(gameObject);
                Destroy(explosion, 2f);
            }
        }
    }
}
