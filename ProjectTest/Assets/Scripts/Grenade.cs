using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    // Weapons variables
    private int price = 3;
    public int localMoney;
    // DataManager
    private DataManager dataManager;
    // Player controller
    public PlayerController playerController;

    void Start()
    {
        // Get the datamanager
        playerController = FindObjectOfType<PlayerController>();
        // Get the dataManager
        dataManager = FindObjectOfType<DataManager>();
    }


    void Update()
    {
        // Get the players money
        localMoney = dataManager.coinsCollected;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Buying item
        if (localMoney >= price)
        {
            // Change price
            localMoney -= price;
            // Set the datamanger price
            dataManager.coinsCollected = localMoney;
            Destroy(gameObject);
            Destroy(other.gameObject);

            dataManager.hasGrenade = true;
            dataManager.grenadeFireRate = 3f;
        }
    }
}
