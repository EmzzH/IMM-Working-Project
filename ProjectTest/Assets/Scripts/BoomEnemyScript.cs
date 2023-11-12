using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomEnemyScript : MonoBehaviour
{
    // Set variables
    private float speed = 15f;
    private float rotationSpeed = 50f;
    private float maxSpeed = 20f;
    private Rigidbody enemyRb;
    // Game manager
    private GameManager gameManager;
    // Explosion
    public GameObject explosion;

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
        // Move the enemy
        movement.MoveEnemy(enemyRb, speed, rotationSpeed, maxSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet") || other.CompareTag("Explosion") || other.CompareTag("Player"))
        {
            // Get the position of the enemy
            Vector3 enemyPositon = transform.position;
            // Drop coin
            gameManager.CoinDrop(enemyPositon);
            Instantiate(explosion, enemyPositon, Quaternion.identity); 

            // Destroy the shooter enemy GameObject
            Destroy(gameObject);
            // Update Score
            gameManager.UpdateEnemiesKilled(1);
        }
    }
}
