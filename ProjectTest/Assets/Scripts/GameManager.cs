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
    // Declare initial state variables
    private int initialEnemiesKilled;
    private int initialCoinsCollected;
    private int initialRoundCounter;
    private int initialHealth;

    // Declare variables that will change
    private int enemiesKilled;
    public int coinsCollected;
    private int roundCounter;
    private int playerHealth;

    // Initial health - To be modified by difficulty
    

    private float timeLeft;
    // Game Active
    public bool isGameActive = true;
    private bool hasRoundStarted = true;
    public bool playerHit = false;
    // Enemies drop coin
    private float coinChance;

    // UI elements
    public TextMeshProUGUI killedText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI shopText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI playerHealthText;

    // Game Objects
    public GameObject coinPrefab;
    // Spawn manager
    private SpawnManager spawnManager;
    // UI Controller
    private UIController uiController;
    // Player
    private PlayerController playerController;
    // Shop
    public GameObject shopPrefab;
    // Ground
    public GameObject groundObject;
    // Shop manager
    private ShopManager shopManager;
    // DataManager
    private DataManager dataManager;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial variables - We can edit these to change difficulty
        initialHealth = 3;
        initialEnemiesKilled = 0;
        initialRoundCounter = 0;
        initialCoinsCollected = 0;

        // Get the dataManager
        dataManager = FindObjectOfType<DataManager>();
        
        // Logic for first round
        if(dataManager.roundCounter == 0) 
        { 
            // Initial game state
            enemiesKilled = 0;
            coinsCollected = 0;
            playerHealth = initialHealth;
            roundCounter++;
            dataManager.SaveData(enemiesKilled, coinsCollected, roundCounter, playerHealth);
        }

        if (dataManager.roundCounter > 1) 
        { 
            // Get data from dataManager
            enemiesKilled = dataManager.enemiesKilled;
            coinsCollected = dataManager.coinsCollected;
            // Update coins collected, enemies killed
            UpdateCoinCollected(0);
            UpdateEnemiesKilled(0);
            // Increment round
            //roundCounter++;
            roundCounter = dataManager.roundCounter;
            playerHealth = dataManager.playerHealth;
        }
        
        timeLeft = 5;
        coinChance = 0.5f;
        isGameActive = true;
        hasRoundStarted = true;
        playerHit = false;

        // Call SpawnManager
        spawnManager = FindObjectOfType<SpawnManager>();
        // Get the UIController
        uiController = FindObjectOfType<UIController>();

        // Get the PlayerController
        playerController = FindObjectOfType<PlayerController>();

        // Get the ShopManager
        shopManager = FindObjectOfType<ShopManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        {
            timeLeft -= Time.deltaTime;
            Timer(timeLeft);
            PlayerHealth();
            RoundActive();
            // End the round
            if (timeLeft < 0)
            {
                RoundEnded();
            }
        }
    }
    // Time
    public void Timer(float timeLeft) 
    {
        timerText.SetText("Time: " + Mathf.Round(timeLeft));

    }
    // Update enemy killed UI
    public void UpdateEnemiesKilled(int killsToAdd) 
    {
        enemiesKilled += killsToAdd;
        killedText.text = "Enemies Killed: " + enemiesKilled;
    }

    // Coins collected
    public void UpdateCoinCollected(int coinsToAdd) 
    {
        coinsCollected += coinsToAdd;
        coinsText.text = "Coins: " + coinsCollected;
    }

    public void UpdateRoundText(int roundCounter)
    {
        roundText.text = "Round: " + dataManager.roundCounter;
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
        roundCounter++;
        timeLeft = 10;
        isGameActive = false;
        spawnManager.SetRoundActive(false);
        spawnManager.CullEnemies();
        dataManager.SaveData(enemiesKilled, coinsCollected, roundCounter, playerHealth);
        ShopTime();
    }

    public void RoundActive() 
    {
        // Manage thr round being active
        if (isGameActive == true && hasRoundStarted == true)
        {
            UpdateRoundText(roundCounter);
            uiController.ShowUI(timerText);
            uiController.ShowUI(killedText);
            uiController.HideUI(shopText);
            spawnManager.SpawnRandomEnemy();
            hasRoundStarted = false;
        }
    }

    // Time for shop
    public void ShopTime()
    {
        //Load Shop Scene
        SceneManager.LoadScene(2);
        //groundObject.SetActive(false);
       // shopManager.SpawnShop();
        //playerController.MovePlayerToShop();
       // uiController.HideUI(timerText);
    //uiController.HideUI(killedText);
       // uiController.ShowUI(shopText);
    }

    public void NextRound() 
    {
        // Despawn shop
        //shopManager.DespawnShop();
        // Spawn the ground
        groundObject.SetActive(true);
        // Increment the round counter
        
        // Reset the time for the new round (e.g., 10 seconds)
        timeLeft = 30.0f;
        // Set the game as active for the new round
        isGameActive = true;
        // Reset the flag to allow spawning new enemies
        hasRoundStarted = true;
        spawnManager.SetRoundActive(true);
        // Call the method to spawn enemies for the new round
        RoundActive();
    }

    // Player health UI
    public void PlayerHealth() 
    {
        playerHealthText.text = "Health: " + playerHealth;
        if (playerHit) {
            playerHealth -= 1;
            playerHit = false;
        }
        if (playerHealth < 1)
        {
            //You're Dead
            // Reset variables
            playerHealth = initialHealth;
            coinsCollected = 0;

            // Reset the variables and load them into the data manager
            ResetVariables();
            dataManager.SaveData(enemiesKilled, coinsCollected, roundCounter, playerHealth);

            SceneManager.LoadScene(4);

            //RestartGame();
        }
    }

    public void ResetVariables()
    {
        playerHealth = initialHealth;
        coinsCollected = initialCoinsCollected;
        roundCounter = initialRoundCounter;
        enemiesKilled = initialEnemiesKilled;
    }
}
