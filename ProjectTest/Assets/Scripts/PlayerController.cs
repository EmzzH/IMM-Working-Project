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
    // Rocket prefab
    public GameObject playerRocket;
    // Gun prefab
    public GameObject playerGun;
    // Grenade prefab
    public GameObject playerGrenade;
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

    // Gun logic
    private bool isReloading;
    public string playerWeapon;

    private float fireTimer = 0.0f;

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

        // Timer for firing
        fireTimer += Time.deltaTime;

        // Fire weapon
        if (Input.GetKey(KeyCode.Mouse0))
        {
            playerWeapon = dataManager.playerWeapon;
            // Check for reload
            if (isReloading)
            {
                return;            
            }

            // Fire bullet
            if (playerWeapon == "pistol" && dataManager.ammunition > 0)
            {
                Fire();
            }

            if (playerWeapon == "shotgun" && dataManager.ammunition > 0)
            {
                Fire();
            }

            if (playerWeapon == "rocketlauncher" && dataManager.ammunition > 0)
            {
                Fire();
            }

            if (playerWeapon == "machinegun" && dataManager.ammunition > 0) 
            {
                Fire();
            }

            // Reload
            else if (dataManager.ammunition <= 0)
            {
                StartCoroutine(Reload());
            }
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && dataManager.hasGrenade)
        {
            throwGrenade();
        }

            // Reload Logic
            IEnumerator Reload()
        {
            isReloading = true;
            if (gameManager != null)
            {
                gameManager.UpdateAmmoText(isReloading);
            }
            // Wait for the specified reload time
            yield return new WaitForSeconds(dataManager.reloadTime);

            // Reset ammunition
            dataManager.ammunition = dataManager.initialAmmunition;
            isReloading = false;
            if (gameManager != null)
            {
                gameManager.UpdateAmmoText(isReloading);
            }
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
        // Checking for fireRate
        if (fireTimer < dataManager.fireRate)
        {
            return; // Not enough time has passed, do not fire
        }

        // Only reset the timer if we're actually firing
        fireTimer = 0.0f;


        if (playerWeapon == "pistol")
        {
            // Decrease ammunition
            dataManager.ammunition--;
            // Instantiate a bullet at the fire point's position and rotation
            Instantiate(playerBullet, firePoint.position, Quaternion.LookRotation(fireDirection));
        }

        if (playerWeapon == "shotgun")
        {
            // Decrease ammunition
            dataManager.ammunition--;
            // Offset to shoot multiple projectiles
            float shotgunOffset = 20.0f;

            // Convert fire direction to a rotation
            Quaternion baseRotation = Quaternion.LookRotation(fireDirection);

            // Get the offset rotations
            Quaternion leftRotation = baseRotation * Quaternion.Euler(0, -shotgunOffset, 0);
            Quaternion rightRotation = baseRotation * Quaternion.Euler(0, shotgunOffset, 0);

            // Instantiate bullets with offset rotations
            // Central bullet
            Instantiate(playerBullet, firePoint.position, baseRotation);
            // Left bullet
            Instantiate(playerBullet, firePoint.position, leftRotation);
            // Right bullet
            Instantiate(playerBullet, firePoint.position, rightRotation); 
        }

        if (playerWeapon == "rocketlauncher")
        {
            // Decrease ammunition
            dataManager.ammunition--;
            // Shoot bullet and rocket
            Instantiate(playerRocket, firePoint.position, Quaternion.LookRotation(fireDirection));
        }

        if (playerWeapon == "machinegun")
        {
            // Decrease ammunition
            dataManager.ammunition--;
            // Shoot bullet and rocket
            Instantiate(playerBullet, firePoint.position, Quaternion.LookRotation(fireDirection));
        }

        // Check for game manager
        if (gameManager != null)
        {
            gameManager.UpdateAmmoText(isReloading);
        }
    }

    public void throwGrenade() 
    {
        float offsetHeight = 1f;
        // Offset to instantiate the grenade above the player
        Vector3 throwStartPosition = firePoint.position + Vector3.up * offsetHeight;

        GameObject grenade = Instantiate(playerGrenade, throwStartPosition, firePoint.rotation);
        // Get the Rigidbody component from the instantiated grenade
        Rigidbody rb = grenade.GetComponent<Rigidbody>();

        // Calculate the throw force (adjust these values as needed)
        float throwForce = 10f; // Overall strength of the throw
        float throwUpwardForce = 5f; // Upward force to give it an arc

        // Apply force to the Rigidbody
        Vector3 throwDirection = firePoint.forward * throwForce + firePoint.up * throwUpwardForce;
        rb.AddForce(throwDirection, ForceMode.VelocityChange);
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
