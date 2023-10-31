using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    // Player Speed
    private float speed = 10.0f;
    //bullet prefab
    public GameObject playerBullet;
    // Gun prefab
    public GameObject playerGun;

    //for shooting
    private Transform player;
    private Transform firePoint;
    Vector3 fireDirection;

    // Look at mouse
    public float speedCam;
    public Camera cam;
    public Collider planeCollider;
    RaycastHit hit;
    Ray ray;



    void Start()
    {
        // Set up for looking at mouse
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        planeCollider = GameObject.Find("Ground").GetComponent<Collider>();

        // Set the firepoint
        firePoint = playerGun.transform;
    }

    void Update()
    {
        // Create input vector 3
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical"));
        // Move player character
        transform.position += input.normalized * speed * Time.deltaTime;

        // Fire weapon
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Fire();
        }

        // Look at mouse
        ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            {
                Vector3 lookAtPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);

                // Set fire direction = player direction
                this.fireDirection = lookAtPosition - transform.position;

                // Rotate the player's forward direction without affecting the up direction
                transform.forward = this.fireDirection.normalized;

                // Set the fire point forward to match the fire direction
                firePoint.forward = this.fireDirection.normalized;
            }
        }

    }

    void Fire()
    {
        // Instantiate a bullet at the fire point's position and rotation
        GameObject bullet = Instantiate(playerBullet, firePoint.position, Quaternion.LookRotation(fireDirection));
    }

    // Player gets killed
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            
            //Destroy(gameObject);
           // SceneManager.LoadScene("Game");
        }
    }
}
