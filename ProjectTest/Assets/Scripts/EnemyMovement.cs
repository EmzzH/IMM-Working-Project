using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Player Object
    private GameObject player;

    private void Start()
    {
        // Set player object
        GameObject player = GameObject.Find("Player");
    }

    private void Update()
    {
        
    }

    public void MoveEnemy(Rigidbody enemyRb, float speed, float rotationSpeed, float maxSpeed)
    {

        // Set player object
        GameObject player = GameObject.Find("Player");
        
        // Create Vector3 for recording distance
        Vector3 playerDistance = player.transform.position - enemyRb.transform.position;
        // Float for recording distance
        float currentDistance = playerDistance.magnitude;

        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = player.transform.position - enemyRb.transform.position;
        // Rotate them
        Vector3 newDirection = Vector3.RotateTowards(enemyRb.transform.forward, lookDirection, rotationSpeed * Time.deltaTime, 0.0f);
        // Add the force
        enemyRb.AddForce(lookDirection * speed * Time.deltaTime);

            // Speed limit for enemies
            if (enemyRb.velocity.magnitude > maxSpeed)
            {
            // enemyRb.velocity = enemyRb.velocity.normalized * maxSpeed;
            enemyRb.velocity = Vector3.zero;
        }
    }

    public void MoveShooterEnemy(Rigidbody enemyRb, float speed, float maxSpeed, float radius) 
    {
        // Set player object
        GameObject player = GameObject.Find("Player");

        // Calculate the direction to the player
        Vector3 directionToPlayer = player.transform.position - enemyRb.transform.position;

        // Calculate the distance to the player
        float distanceToPlayer = directionToPlayer.magnitude;

        // Check if the enemy is outside the desired radius
        if (distanceToPlayer > radius)
        {
            // Normalize the direction vector
            directionToPlayer.Normalize();

            // Move the enemy toward the player
            enemyRb.AddForce(directionToPlayer * speed * Time.deltaTime);

            // Speed limit for enemies
            if (enemyRb.velocity.magnitude > maxSpeed)
            {
                enemyRb.velocity = enemyRb.velocity.normalized * maxSpeed;
            }
        }
        else 
        {
            Vector3 backwardVelocity = new Vector3(0.0f, 0.0f, -3.0f);
            enemyRb.velocity = backwardVelocity;
        }
    }
}

