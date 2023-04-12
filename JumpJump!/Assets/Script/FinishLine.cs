using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameObject levelCompleteScreen;
    // public GameObject levelCompleteScreenchangeleg;
    // public static int levelcompletetimes;

    public PlayfabManager playfabManager;
    // private int maxPlatform = 0;

    private void OnTriggerEnter2D(Collider2D collideFinish)
    {
        if (collideFinish.gameObject.CompareTag("Finish"))
        {
            levelCompleteScreen.SetActive(true);
            Time.timeScale = 0;
            // levelcompletetimes = levelcompletetimes + 1;
            // if (levelcompletetimes % 2 == 0)
            // {
            //     levelCompleteScreenNext.SetActive(true);
            //     Time.timeScale = 0;
            // }
            // else
            // {
            //     levelCompleteScreenchangeleg.SetActive(true);
            //     Time.timeScale = 0;
            // }
            // // assign maxPlatform here
            // var playerManager = FindObjectOfType<PlayerManager>();
            // maxPlatform = playerManager.maxPlatform;
            // StartCoroutine(playfabManager.SendLeaderboardCoroutine(maxPlatform));
        }
    }
}
