using System.Diagnostics;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    // display game over screen
    public static bool isGameOver;
    public GameObject gameOverScreen;
    public GameObject pauseMenuScreen;
    private CountdownController myCountdown;

    public static int numberOfCoins;
    public TextMeshProUGUI coinsText;
    
    private void Awake()
    {
        numberOfCoins = PlayerPrefs.GetInt("NumberOfCoins", 0);
        isGameOver = false;
        myCountdown = GetComponent<CountdownController>();
    }

    // Update is called once per frame
    void Update()
    {
        // allow coins to be kept the same even if the game is restarted
        coinsText.text = numberOfCoins.ToString();
        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
        }
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
        SceneManager.LoadScene(0);
    }


}
