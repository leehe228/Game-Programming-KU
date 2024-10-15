using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;  // Damage dealt by the bullet

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision detected!");
        // Check if the bullet collided with an object tagged as "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("Enemy hit!");
            }

            // Destroy the bullet after hitting the enemy
            Destroy(gameObject);
        } else {
            // Destroy the bullet if it hits anything else
            // Destroy(gameObject);
        }
    }
}
