using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Shop : MonoBehaviour
{
    // Get the canvas
    public Canvas shopCanvas;
    // Get the shop
    public GameObject uiPrefabs;

    public GameObject contentPanel;
    // Generate Array for prices
    private string[] price = {"2", "4", "6", "8" };
    private string[] itemNames = {"Grenade", "Shotgun", "Rocketlauncher", "Nuke" };
    private string[] itemDescription = {"A throwable grenade that goes boom",
                    "A shotgun that shoots 3 projectiles",
                    "Fires a rocket that explodes on impact",
                    "Find out!"};
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnShopItems() 
    {
        for (int i=0; i < price.Length; i++) 
        {
            
        }
    }
}
