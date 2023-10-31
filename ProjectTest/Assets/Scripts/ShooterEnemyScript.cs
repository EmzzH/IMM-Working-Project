using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ShooterEnemyScript : MonoBehaviour
{
    // Set variables for movment
    private float speed = 40f;
    private float rotationSpeed = 50f;
    private float maxSpeed = 50f;
    private float radius = 10f;

    // Variable for rigid body
    public Rigidbody enemyRb;

    // Object for movment
    EnemyMovement movement = new EnemyMovement();

    // Firing!
    private Transform player;
    private Transform firePoint;
    public GameObject enemyBullet;
    // Cooldown for shooting
    private float fireRate = 1f;
    private float reload = 1f;

    // Game manager
    private GameManager gameManager;

    void Start()
    {
        // Set objects
        enemyRb = GetComponent<Rigidbody>();

        // Set the firepoint
        firePoint = transform;
        // Set the player
        player = GameObject.Find("Player").transform;

        // Set Game Gamager
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    void Update()
    {
        movement.MoveShooterEnemy(enemyRb, speed, maxSpeed, radius);

        // if statement for firing
        if (Time.time >= reload)
        {
            Fire();
            reload = Time.time + 1f/fireRate;
        }
    }

    void Fire() 
    {
        // Calculate player direction
        Vector3 fireDirection = player.position - firePoint.position;

        // Rotate the firePoint to face the player (optional)
        firePoint.forward = fireDirection.normalized;

        // Instantiate a bullet at the fire point's position and rotation
        GameObject bullet = Instantiate(enemyBullet, firePoint.position, Quaternion.LookRotation(fireDirection));
    }

    private void OnTriggerEnter(Collider other)
    {
        // Enemy die
        if (other.CompareTag("PlayerBullet"))
        {
            // Get the position of the enemy
            Vector3 enempyPositon = transform.position;
            // Drop coin
            gameManager.CoinDrop(enempyPositon);
            // Update Score
            gameManager.UpdateEnemiesKilled(1);
            // Destroy the shooter enemy GameObject
            Destroy(gameObject);
            
        }
    }
}
