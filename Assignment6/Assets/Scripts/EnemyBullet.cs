using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject GameManager;

    private void Awake()
    {
        GameManager = GameObject.Find("Game Manager");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.GetComponent<GameManager>().SubtractHpCount();
            Destroy(gameObject);
        }
    }
}
