using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] enemyPrefab; 

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 5f, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy()
    {
        if (GameObject.Find("Game Manager").GetComponent<GameManager>().hpCount == 0
            || GameObject.Find("Game Manager").GetComponent<GameManager>().killCount == 10) {
            return;
        } 
        
        // Generate random position within the specified boundaries
        float randomX = Random.Range(209, 282);
        float randomZ = Random.Range(245, 266);
        Vector3 spawnPosition = new Vector3(randomX, 49.2f, randomZ);

        // Instantiate the enemy at the random position
        // 3개중에 1개 랜덤으로 생성
        int randomIndex = Random.Range(0, enemyPrefab.Length);
        Instantiate(enemyPrefab[randomIndex], spawnPosition, Quaternion.identity);
    }
}
