using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    // Weapons variables
    private int price = 3;
    private int fireRate = 1;
    public int localMoney;

   public PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        localMoney = playerController.playerMoney;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (localMoney >= price)
        {
            
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
