using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float damage = 10f;  // Damage dealt by the bullet

    void Update()
    {
        if (GameObject.Find("Game Manager").GetComponent<GameManager>().hpCount == 0
            || GameObject.Find("Game Manager").GetComponent<GameManager>().killCount == 10) {
                return;
        }

        gameObject.transform.Translate(Vector3.forward * Time.deltaTime * 100f);
    }

    /* void OnCollisionEnter(Collision collision)
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
    }*/

    /* void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger collision detected!");

        // Check if the bullet collided with an object tagged as "Enemy"
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("Enemy hit!");
            }

            // Destroy the bullet after hitting the enemy
            Destroy(gameObject);
        }
        else
        {
            // Optionally destroy the bullet if it hits anything else
            // Destroy(gameObject);
        }
    } */
}
