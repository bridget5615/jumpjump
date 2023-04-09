using System.Diagnostics;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    // display game over screen
    public static bool isGameOver;
    public static bool isLevelComplete;
    public GameObject gameOverScreen;
    public GameObject pauseMenuScreen;
    private CountdownController myCountdown;

    public static int numberOfCoins;
    public TextMeshProUGUI coinsText;

    public PlayfabManager playfabManager;
    public int maxPlatform = 0;

    private void Awake()
    {
        UnityEngine.Debug.Log("PlayerManager Awake called");
        numberOfCoins = PlayerPrefs.GetInt("NumberOfCoins", 0);
        coinsText = GameObject.Find("CoinsText").GetComponent<TextMeshProUGUI>();
        coinsText.SetText(numberOfCoins.ToString());
        isGameOver = false;
        isLevelComplete = false;
        playfabManager = FindObjectOfType<PlayfabManager>();
        myCountdown = GetComponent<CountdownController>();
        if (numberOfCoins < 0)
        {
            numberOfCoins = 0;
        }
        UnityEngine.Debug.Log("coinsText: " + coinsText);
    }


    private void Update()
    {
        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
            StartCoroutine(playfabManager.SendLeaderboardCoroutine(maxPlatform));
        }
    }

    // Update the number of coins and update the UI text
    public void UpdateCoins(int amount)
    {
        numberOfCoins += amount;
        if (numberOfCoins < 0)
        {
            numberOfCoins = 0;
        }
        coinsText.text = numberOfCoins.ToString();
        UnityEngine.Debug.Log("UpdateCoinsText called");

    }

    // replay level again
    public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        myCountdown.enabled = true;
    }
    
    // pause button functions

    // pause game 
    public void PauseGame()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        pauseMenuScreen.SetActive(true);
    }

    // resume game button
    public void ResumeGame()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        pauseMenuScreen.SetActive(false);
        myCountdown.enabled = true;
    }

    // go back to home button 
    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
        myCountdown.enabled = true;
        playfabManager.SendLeaderboardCoroutine(maxPlatform);
    }
}
