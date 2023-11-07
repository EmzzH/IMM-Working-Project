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

    // Shop ground
    public GameObject shopPrefab;
    // Shop Prefabs
    public GameObject[] shopPrefabs;
    // List to keep track of spawned shops
    private List<GameObject> spawnedShops = new List<GameObject>();
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
        Vector3 spawnPos = new Vector3(0, 0, 0);
        GameObject shopGround = Instantiate(shopPrefab, spawnPos, Quaternion.identity);
        spawnedShops.Add(shopGround);

        for (int i = 0; i < 5; i++)
        {
            if (i == 0)
            {
                Vector3 tempSpawnPos = new Vector3(shopSpawnPosX, yPos, shopSpawnPosZ);
                GameObject shop = Instantiate(shopPrefabs[i], tempSpawnPos, shopPrefabs[i].transform.rotation);
                spawnedShops.Add(shop);
            }
            else if (i == 1)
            {
                Vector3 tempSpawnPos = new Vector3(-shopSpawnPosX, yPos, -shopSpawnPosZ);
                GameObject shop = Instantiate(shopPrefabs[i], tempSpawnPos, shopPrefabs[i].transform.rotation);
                spawnedShops.Add(shop);
            }
            else if (i == 2)
            {
                Vector3 tempSpawnPos = new Vector3(-shopSpawnPosX, yPos, shopSpawnPosZ);
                GameObject shop = Instantiate(shopPrefabs[i], tempSpawnPos, shopPrefabs[i].transform.rotation);
                spawnedShops.Add(shop);
            }
            else if (i == 3)
            {
                Vector3 tempSpawnPos = new Vector3(shopSpawnPosX, yPos, -shopSpawnPosZ);
                GameObject shop = Instantiate(shopPrefabs[i], tempSpawnPos, shopPrefabs[i].transform.rotation);
                spawnedShops.Add(shop);
            }
            else if (i == 4)
            {
                Vector3 tempSpawnPos = new Vector3(shopSpawnPosX, yPos, 0);
                GameObject shop = Instantiate(shopPrefabs[i], tempSpawnPos, shopPrefabs[i].transform.rotation);
                spawnedShops.Add(shop);
            }
        }

    }

    public void DespawnShop() 
    {
        // Destroy all the spawned shops
        foreach (var shopItem in spawnedShops)
        {
            Destroy(shopItem);
        }
        
        // Clear the list of spawned shops
        spawnedShops.Clear();

    }
}
