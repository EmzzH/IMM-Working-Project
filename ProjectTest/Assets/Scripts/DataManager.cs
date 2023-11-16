using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    // tutorial: https://learn.unity.com/tutorial/implement-data-persistence-between-scenes#634f8281edbc2a65c86270ca
    public static DataManager Instance;
    // Declare variables
    public int enemiesKilled;
    public int coinsCollected;
    public int roundCounter;
    public int playerHealth;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SaveData(int enemiesKilled, int coinsCollected, int roundCounter, int playerHealth) 
    {
        DataManager.Instance.enemiesKilled = enemiesKilled;
        DataManager.Instance.coinsCollected = coinsCollected;
        DataManager.Instance.roundCounter = roundCounter;
        DataManager.Instance.playerHealth = playerHealth;
    }

    public int ReturnKills() 
    {
        return enemiesKilled;
    }

    public int ReturnCoins()
    {
        return coinsCollected;
    }

    public int ReturnRounds()
    {
        return roundCounter;
    }

    public int ReturnHealth()
    {
        return playerHealth;
    }
}


