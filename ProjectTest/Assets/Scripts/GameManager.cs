using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    // Enemies killed
    private int enemiesKilled;
    private int killsToAdd;

    // Coins collected
    public int coinsCollected;

    // Round timer & text
    private float timeLeft = 0.00f;
    public int roundCounter = 1;

    // Game Active
    public bool isGameActive;
    private bool hasRoundStarted;

    // Enemies drop coin
    private float coinChance = 0.5f;
    // Coin & enemy object
    public GameObject coinPrefab;

    // Spawn manager
    private SpawnManager spawnManager;
    // Camera 
    private FollowPlayer playerCamera;
    // UI Controller
    private UIController uiControl;
    // Player
    private PlayerController playerController;
    // Player health
    private int playerHealth;

    // Shop
    public GameObject shopPrefab;
    private bool isInShop = true;

    // Ground
    public GameObject groundObject;
    // Shop manager
    private ShopManager shopManager;

    // Start is called before the first frame update
    void Start()
    {
        // Get the uiControl
        uiControl = FindObjectOfType<UIController>();
        // UI elements for start
        enemiesKilled = 0;
        UpdateEnemiesKilled(killsToAdd);

        // Call spawn manager & shooter enemy
        spawnManager = FindObjectOfType<SpawnManager>();
        
        // Set game as active
        isGameActive = true;
        timeLeft = 30;

        // Set round as active
        hasRoundStarted = true;

        // Get the player camera
        playerCamera = FindObjectOfType<FollowPlayer>();
        
        // Get the shop manager
        shopManager = FindObjectOfType<ShopManager>();

        // Get the player controller
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isGameActive)
        {
            timeLeft -= Time.deltaTime;
            uiControl.RoundTimerUI(timeLeft);
            RoundActive();
            PlayerHealthUI();
            
            // End the round
            if (timeLeft < 0)
            {
                RoundEnded();
            }
        }
    }

    // Update enemy killed UI
    public void UpdateEnemiesKilled(int killsToAdd) 
    {
        enemiesKilled += killsToAdd;
        uiControl.EnemiesKilledUI(enemiesKilled);
        
    }

    // Coins collected
    public void UpdateCoinCollected(int coinsToAdd) 
    {
        print(coinsToAdd);
        print(coinsCollected);
        coinsCollected += coinsToAdd;
        print(coinsCollected);
        uiControl.CoinsCollectedUI(coinsCollected);
    }

    public void CoinDrop(Vector3 enemyPosition) 
    {
        // Spawn coin 50% chance
        float coinDropChance = Random.Range(0.00f, 1.00f);
        if (coinChance <= coinDropChance)
        {
            // Soawn the coin with spin from CoinScript
            Instantiate(coinPrefab, enemyPosition, Quaternion.identity);
        }
    }

    public void RoundEnded() 
    {
        // Logic for ending the round
        timeLeft = 10;
        isInShop = true;
        playerCamera.isInShop = isInShop;

        isGameActive = false;
        spawnManager.SetRoundActive(false);
        spawnManager.CullEnemies();
        ShopTime();
    }

    public void RoundActive() 
    {
        if (isGameActive == true && hasRoundStarted == true)
        {
           spawnManager.SpawnRandomEnemy();
           hasRoundStarted = false;
        }
    }

    // Time for shop
    public void ShopTime()
    {
        groundObject.SetActive(false);
        shopManager.SpawnShop();
        playerController.MovePlayerToShop();
        uiControl.ShopUI();
    }

    public void NextRound() 
    {
        // Despawn shop
        shopManager.DespawnShop();
        // Spawn the ground
        groundObject.SetActive(true);

        // Increment the round counter and update UI
        roundCounter++;
        uiControl.RoundCounterUI(roundCounter);
        
        // Add back in hidden UI Elements
        uiControl.RoundUI();


        // Reset the time for the new round (e.g., 10 seconds)
        timeLeft = 30.0f;

        // Set the game as active for the new round
        isGameActive = true;

        // Reset the flag to allow spawning new enemies
        hasRoundStarted = true;

        // Set isInShop to false if it's intended to exit the shop for the new round
        isInShop = false;
        playerCamera.isInShop = isInShop;

        spawnManager.SetRoundActive(true);
        // Call the method to spawn enemies for the new round
        RoundActive();
    }

    // Player health UI
    public void PlayerHealthUI() 
    {
        // Get the player health
        playerHealth = playerController.playerHealth;
        // Set text for player health
        uiControl.PlayerHealthUI(playerHealth);
    }
}
