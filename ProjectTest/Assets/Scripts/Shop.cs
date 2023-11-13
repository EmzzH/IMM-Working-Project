using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public GameObject player;

     // Shop Prefabs
    public GameObject[] shopPrefabs; 
    private float shopSpawnPosX = 10.0f;
    private float shopSpawnPosZ = 10.0f;
    // y position
    private float yPos = 1;




    // Start is called before the first frame update
    void Start()
    {
        //Create Player in Shop
         //Instantiate(player, new Vector3(0f, 0f, 0f), Quaternion.identity);

        //Add Shop Prefab Items        
        for (int i = 0; i < 5; i++)
        {
            if (i == 0)
            {
                Vector3 tempSpawnPos = new Vector3(shopSpawnPosX, yPos, shopSpawnPosZ);
                Instantiate(shopPrefabs[i], tempSpawnPos, shopPrefabs[i].transform.rotation);
                
            }
            else if (i == 1)
            {
                Vector3 tempSpawnPos = new Vector3(-shopSpawnPosX, yPos, -shopSpawnPosZ);
                Instantiate(shopPrefabs[i], tempSpawnPos, shopPrefabs[i].transform.rotation);
                
            }
            else if (i == 2)
            {
                Vector3 tempSpawnPos = new Vector3(-shopSpawnPosX, yPos, shopSpawnPosZ);
                Instantiate(shopPrefabs[i], tempSpawnPos, shopPrefabs[i].transform.rotation);
                
            }
            else if (i == 3)
            {
                Vector3 tempSpawnPos = new Vector3(shopSpawnPosX, yPos, -shopSpawnPosZ);
                Instantiate(shopPrefabs[i], tempSpawnPos, shopPrefabs[i].transform.rotation);
               
            }
           
        }

    }

    // Update is called once per frame
    void Update()
    {
       
    

    }
}
