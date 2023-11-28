using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownGrenade : MonoBehaviour
{
    private float fireRate = 2f;

    private Rigidbody rb;

    // DataManager
    private DataManager dataManager;
    // Player controller
    public PlayerController playerController;

    private float detTimer = 0f;
    private float explosionTimer = 3.0f;

    public GameObject explosion;
    Vector3 grenadePosition;

    // Game Boundary
    public float mapBoundX = 40.0f;
    public float mapBoundZ = 40.0f;

    // Start is called before the first frame update
    void Start()
    {
        // Get the datamanager
        playerController = FindObjectOfType<PlayerController>();
        // Get the dataManager
        dataManager = FindObjectOfType<DataManager>();
        // Get the rigid body
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        grenadePosition = transform.position;

        if (rb.transform.position.x > mapBoundX || rb.transform.position.x < -mapBoundX ||
            rb.transform.position.z > mapBoundZ || rb.transform.position.z < -mapBoundZ
            && gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject);
        }

        if (detTimer > explosionTimer)
        {
            Instantiate(explosion, grenadePosition, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet") || other.CompareTag("Playerbullet"))
        {
            Instantiate(explosion, grenadePosition, Quaternion.identity);
            // Destroy the shooter enemy GameObject
            Destroy(gameObject);
        }

    }
}
