using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab; 

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 3f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        // Generate random position within the specified boundaries
        float randomX = Random.Range(209, 282);
        float randomZ = Random.Range(245, 266);
        Vector3 spawnPosition = new Vector3(randomX, 45.6f, randomZ);

        // Instantiate the enemy at the random position
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
