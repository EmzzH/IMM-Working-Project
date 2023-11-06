using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    // Shop location
    private float shopSpawnPosX = 10.0f;
    private float shopSpawnPosZ = 10.0f;
    // y position
    private float yPos = 1;

    // Shop Prefabs
    public GameObject[] shopPrefabs;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnShop()
    {
        for (int i = 0; i < 5; i++)
        {
            if (i == 0)
            {
                Vector3 spawnPos = new Vector3(shopSpawnPosX, yPos, shopSpawnPosZ);
                Instantiate(shopPrefabs[i], spawnPos, shopPrefabs[i].transform.rotation);
            }
            else if (i == 1)
            {
                Vector3 spawnPos = new Vector3(-shopSpawnPosX, yPos, -shopSpawnPosZ);
                Instantiate(shopPrefabs[i], spawnPos, shopPrefabs[i].transform.rotation);
            }
            else if (i == 2)
            {
                Vector3 spawnPos = new Vector3(-shopSpawnPosX, yPos, shopSpawnPosZ);
                Instantiate(shopPrefabs[i], spawnPos, shopPrefabs[i].transform.rotation);
            }
            else if (i == 3)
            {
                Vector3 spawnPos = new Vector3(shopSpawnPosX, yPos, -shopSpawnPosZ);
                Instantiate(shopPrefabs[i], spawnPos, shopPrefabs[i].transform.rotation);
            }
            else if (i == 4)
            {
                Vector3 spawnPos = new Vector3(shopSpawnPosX, yPos, 0);
                Instantiate(shopPrefabs[i], spawnPos, shopPrefabs[i].transform.rotation);
            }
        }

    }
}
