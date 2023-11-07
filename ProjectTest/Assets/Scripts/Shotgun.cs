using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    // Weapons variables
    private int price = 3;
    private int fireRate = 1;

    // Get game manager
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // Get the game manager
        gameManager = GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (gameManager.coinsCollected >= price)
        {
            Destroy(gameObject);
        }
    }
}
