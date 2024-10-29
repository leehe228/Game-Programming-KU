using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    public int triggerCount = 0;
    public int maxTriggers = 10;
    public GameObject GameManager;

    public ParticleSystem explosionParticle;

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

            // Check if the trigger count has reached the max limit
            if (triggerCount >= maxTriggers)
            {
                GameManager.GetComponent<GameManager>().PillarDestroyed();
                Destroy(gameObject);
            }
        }
    }
}
