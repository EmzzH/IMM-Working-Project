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
    // Enemies killed UI
    private int enemiesKilled;
    public TextMeshProUGUI killedText;
    private int killsToAdd;

    // Coins collected UI
    private int coinsCollected;
    public TextMeshProUGUI coinsText;
    private int coinsToAdd;

    // Round timer & text
    private float timeLeft = 0.00f;
    public TextMeshProUGUI timerText;
    private int roundCounter = 0;
    public TextMeshProUGUI roundText;

    // Game Active
    public bool isGameActive;

    // Enemies drop coin
    private float coinChance = 0.5f;
    // Coin & enemy object
    public GameObject coinPrefab;

    // Spawn manager
    private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        // UI elements for start
        enemiesKilled = 0;
        UpdateEnemiesKilled(killsToAdd);

        // Call spawn manager
        spawnManager = new SpawnManager();

        // Set game as active
        isGameActive = true;
        timeLeft = 20;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameActive)
        {
            timeLeft -= Time.deltaTime;
            timerText.SetText("Time: " + Mathf.Round(timeLeft));
            if (timeLeft < 0)
            {
                RoundEnded();
                
                
            }
        }
    }

    // Update enemy killed UI
    public void UpdateEnemiesKilled(int killsToAdd) 
    {
        
        killedText.text = "Enemies Killed: ";
        enemiesKilled += killsToAdd;
        killedText.text = "Enemies Killed: " + enemiesKilled;
    }

    public void UpdateCoinCollected(int coinsToAdd) 
    {
        // coinsText.text = "Coins: ";
        Debug.Log("Coin Collected");
        coinsCollected += coinsToAdd;
        coinsText.text = "Coins: " + coinsCollected;
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
        print("round over");
        roundCounter++;
        roundText.text = "Round: " + roundCounter;

        timeLeft = 20;
    }
}
