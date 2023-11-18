using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using TreeEditor;
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
    // Game manager
    public GameManager gameManager;
    // Scene manager
    private SceneController sceneManager;
    // Data manager
    private DataManager dataManager;

    // Player location for shop
    private Vector3 playerShopPosition = new Vector3(0, 1, 0);

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

    // Player material
    public Material playerMat;
    // Hurt time
    private float hurtTime = 2f;
    // Time left
    private float timeLeft = 0f;
    // Is hit boolean
    public bool isHit = false;

    // Player money
    public int playerMoney = 0;

    void Start()
    {
        // Set dataManager
        dataManager = FindObjectOfType<DataManager>();
        
        // Set game manager
        gameManager = FindObjectOfType<GameManager>();
        // Set up for looking at mouse
        cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        planeCollider = GameObject.Find("Ground").GetComponent<Collider>();

        // Set the firepoint
        firePoint = playerGun.transform;
        
        // Set Scene manager
        sceneManager = FindObjectOfType<SceneController>();

        // Set the player colour
        playerMat.SetColor("_Color", Color.green);
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
            playerMoney = dataManager.coinsCollected;
            print(playerMoney);
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
        // Player hit
        if (isHit) 
        {
            PlayerHit();
        }    
    }

    void Fire()
    {
        // Instantiate a bullet at the fire point's position and rotation
        Instantiate(playerBullet, firePoint.position, Quaternion.LookRotation(fireDirection));
    }

    public void PlayerHit() 
    {
        playerMat.SetColor("_Color", Color.red);

        timeLeft += Time.deltaTime;
        if (timeLeft >= hurtTime)
        {
            // Set the player to green again
            playerMat.SetColor("_Color", Color.green);
            // Reset the player being hit
            timeLeft = 0;   
            isHit = false;
        }
    }

    // Player gets killed
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet") || other.CompareTag("Explosion"))
        {
            gameManager.playerHit = true;
            // Player is hit
            isHit = true;
        }
    }
}
