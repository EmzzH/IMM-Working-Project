using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Decalre object for enemy prefabs
    public GameObject[] enemyPrefabs;
    // Boundary
    private float minX = -20.0f;
    private float maxX = 20.0f;
    private float minZ = -20.0f;
    private float maxZ = 20.0f;
    // y position
    private float yPos = 1;

    // Delay for Spawn
    private float startDelay = 1.0f;
    private float spawnInterval;


    // Start is called before the first frame update
    void Start()
    {
        // Initial spawn
        SpawnRandomEnemy();
    }

    // Spawn random ball at random x and y position in play area
    void SpawnRandomEnemy()
    {
        // Random position
        float randomX = Random.Range(minX, maxX);
        float randomZ = Random.Range(minZ, maxZ);

        // Generate random enemy index and random spawn position
        Vector3 spawnPos = new Vector3(randomX, yPos, randomZ);

        // instantiate ball at random spawn location
        // Random ball number
        int enemyArray = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[enemyArray], spawnPos, enemyPrefabs[enemyArray].transform.rotation);
        // spawn ball at random times
        spawnInterval = Random.Range(startDelay, 4f);
        Invoke("SpawnRandomEnemy", spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
