using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float hp = 50f;  // Initial HP of the enemy

    public void TakeDamage(float amount)
    {
        hp -= amount;

        Debug.Log("HP: " + hp);

        if (hp <= 0)
        {
            Debug.Log("Enemy destroyed!");
            Destroy(gameObject);  // Destroy the enemy if HP is 0 or below
        }
    }
}