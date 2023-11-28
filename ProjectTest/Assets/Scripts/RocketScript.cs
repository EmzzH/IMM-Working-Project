using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RocketScript : MonoBehaviour
{
    // Rocket variables
    public float speed = 30.0f;
    private Rigidbody rb;

    private float detTimer = 0f;
    private float explosionTimer = 1.0f;

    // Game Boundary
    public float mapBoundX = 40.0f;
    public float mapBoundZ = 40.0f;

    public GameObject explosion;
    Vector3 rocketPosition;

    // Start is called before the first frame update
    void Start()
    {
        // Get the rigid body
        rb = GetComponent<Rigidbody>();
        // Set the initial velocity in the forward direction
        rb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
        rocketPosition = transform.position;

        if (rb.transform.position.x > mapBoundX || rb.transform.position.x < -mapBoundX ||
            rb.transform.position.z > mapBoundZ || rb.transform.position.z < -mapBoundZ
            && gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject);
        }

        detTimer += Time.deltaTime;

        if (detTimer > explosionTimer ) 
        {
            Instantiate(explosion, rocketPosition, Quaternion.identity);
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            Instantiate(explosion, rocketPosition, Quaternion.identity);
            // Destroy the shooter enemy GameObject
            Destroy(gameObject);
        }

        // Shop object being destroyed
        if (other.CompareTag("ShopObject"))
        {
            Instantiate(explosion, rocketPosition, Quaternion.identity);
            Destroy(gameObject);
        }
        //Exit and Load new Scene
        if (other.CompareTag("Exit"))
        {
            Instantiate(explosion, rocketPosition, Quaternion.identity);
            Destroy(gameObject);
            //Load the Game Again
            SceneManager.LoadScene(1);
        }
    }
}
