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
    public bool isSkippedTutorial;
    public int highScore;


    // Weapon data
    public int initialMagazine;
    public int initialAmmunition;

    public int magazineSize;
    public int ammunition;
    
    public float fireRate;
    public float reloadTime;

    public string playerWeapon;

    public void Start()
    {
        playerWeapon = "pistol";
        initialAmmunition = 10;
        ammunition = 10;

        reloadTime = 2f;

        fireRate = 0.3f;
    }

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

        if (roundCounter > 3)
        {
            highScore = enemiesKilled * roundCounter;
        }
    }

    public void SetSkippedTutorial(bool skippedTutorial)
    {
        this.isSkippedTutorial = skippedTutorial;
    }

    public bool GetSkippedTutorial()
    {
        return this.isSkippedTutorial;
    }

    public void SetRound(int roundCounter)
    {
        this.roundCounter = roundCounter;
    }
}


