using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    // Weapons variables
    private int price = 3;
    private int fireRate = 1;
    public int localMoney;

    // DataManager
    private DataManager dataManager;
    // Player controller
    public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        // Get the datamanager
        playerController = FindObjectOfType<PlayerController>();
        // Get the dataManager
        dataManager = FindObjectOfType<DataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        localMoney = dataManager.coinsCollected;
    }

    private void OnTriggerEnter(Collider other)
    {
        // Buying shotgun
        if (localMoney >= price)
        {
            // Change price
            localMoney -= price;
            // Set the datamanger price
            dataManager.coinsCollected = localMoney;
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
