using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    // Game manager object
    private GameManager gameManager;
    // UI Elements
    public TextMeshProUGUI killedText;
    public TextMeshProUGUI coinsText;
    public TextMeshProUGUI shopText;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI roundText;
    public TextMeshProUGUI playerHealthText;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        // Set start UI
        roundText.text = "Round: " + gameManager.roundCounter;
        coinsText.text = "Coins: " + gameManager.coinsCollected;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Enemies Killed UI
    public void EnemiesKilledUI(int enemiesKilled) 
    {
        killedText.text = "Enemies Killed: " + enemiesKilled;
    }
    // Coins Collected UI
    public void CoinsCollectedUI(int coinsCollected) 
    {
        coinsText.text = "Coins: " + coinsCollected;
    }
    // Round timer UI
    public void RoundTimerUI(float timeLeft) 
    {
        timerText.SetText("Time: " + Mathf.Round(timeLeft));
    }
    // Round Counter UI
    public void RoundCounterUI(int roundCounter)
    {
        roundText.text = "Round: " + roundCounter;
    }
    // Player Health UI
    public void PlayerHealthUI(int playerHealth) 
    {
        playerHealthText.text = "Health: " + playerHealth;
    }
    // ShopUI
    public void ShopUI() 
    {
        HideUI(timerText);
        HideUI(killedText);
        ShowUI(shopText);
    }
    // RoundUI
    public void RoundUI() 
    {
        ShowUI(timerText);
        ShowUI(killedText);
        HideUI(shopText);
    }
    // Method for hiding text
    public void HideUI(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(false);
    }

    // Method for showing text
    public void ShowUI(TextMeshProUGUI text)
    {
        text.gameObject.SetActive(true);
    }

    // Methods for hiding and showing canvas
    public void HideCanvas(Canvas canvas) 
    { 
        canvas.gameObject.SetActive(false);
    }

    public void ShowCanvas(Canvas canvas) 
    {
        canvas.gameObject.SetActive(true);
    }
}
