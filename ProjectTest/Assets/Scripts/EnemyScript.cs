using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Set variables
    private float speed = 15f;
    private float rotationSpeed = 50f;
    private float maxSpeed = 20f;
    private Rigidbody enemyRb;
    private float distance = 0f;
    // Game manager
    private GameManager gameManager;


    EnemyMovement movement = new EnemyMovement();

    void Start()
    {
        // Set objects
        enemyRb = GetComponent<Rigidbody>();
        // Set Game Gamager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    
    void Update()
    {
        movement.MoveEnemy(enemyRb, speed, rotationSpeed, maxSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            // Get the position of the enemy
            Vector3 enempyPositon = transform.position;
            // Drop coin
            gameManager.CoinDrop(enempyPositon);

            // Destroy the shooter enemy GameObject
            Destroy(gameObject);
            // Update Score
            gameManager.UpdateEnemiesKilled(1);
        }
    }
}
