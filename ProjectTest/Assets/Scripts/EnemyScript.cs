using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Set variables
    private float speed = 5f;
    private float rotationSpeed = 50f;
    private float maxSpeed = 20f;
    private Rigidbody enemyRb;
    private float distance = 0f;
    // Game manager
    private GameManager gameManager;
    // Get the player
    private Transform player;
    private Transform enemyLocation;


    EnemyMovement movement = new EnemyMovement();

    void Start()
    {
        // Set objects
        enemyRb = GetComponent<Rigidbody>();
        // Set the player
        player = GameObject.Find("Player").transform;
        // Set look direction
        enemyLocation = transform;
        // Set Game Gamager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    
    void Update()
    {
        // Calculate player direction
        Vector3 playerDirection = player.position - transform.position;
        // Set the enemy to look at the player
        enemyLocation.forward = playerDirection.normalized;
        movement.MoveEnemy(enemyRb, speed, rotationSpeed, maxSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet") || other.CompareTag("Explosion") || other.CompareTag("Player"))
        {
            // Get the position of the enemy
            Vector3 enemyPosition = transform.position;
            // Drop coin
            gameManager.CoinDrop(enemyPosition);

            // Destroy the shooter enemy GameObject
            Destroy(gameObject);
            // Update Score
            gameManager.UpdateEnemiesKilled(1);
        }
    }
}
