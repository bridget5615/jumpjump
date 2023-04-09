using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro; 
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayfabManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI messageText;
    public TMP_InputField usernameInput;

    private string displayName;

    void Start()
    {
        UnityEngine.Debug.Log("PlayfabManager Start called");

        messageText.text = "Please key in your username to play :)";
    }

    public void Login()
    {
        var request = new LoginWithCustomIDRequest { 
            CustomId = SystemInfo.deviceUniqueIdentifier, 
            CreateAccount = true 
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginError);
    }

    void OnLoginSuccess(LoginResult result)
    {
        messageText.text = "Login successful!";
        UnityEngine.Debug.Log("Successful login/account created!");

        // Update the user's display name in PlayFab
        UpdateDisplayName();
        SceneManager.LoadScene("MainMenu"); 
    }

    void OnLoginError(PlayFabError error)
    {
        string errorMessage = error.GenerateErrorReport();
        DisplayErrorMessage(errorMessage);
        UnityEngine.Debug.Log("Error while logging in/creating a new account");
        UnityEngine.Debug.Log(errorMessage);
    }


    void UpdateDisplayName()
    {
        // Set the display name to the value of the username input field
        displayName = usernameInput.text;

        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = displayName
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdateSuccess, OnDisplayNameUpdateError);
    }

    void OnDisplayNameUpdateSuccess(UpdateUserTitleDisplayNameResult result)
    {
        UnityEngine.Debug.Log("Display name updated successfully!");
    }

    void OnDisplayNameUpdateError(PlayFabError error)
    {
        string errorMessage = error.GenerateErrorReport();
        DisplayErrorMessage(errorMessage);
        UnityEngine.Debug.LogError("Failed to update display name: " + errorMessage);
    }
    void DisplayErrorMessage(string message)
    {
        messageText.text = message;
    }

    public IEnumerator SendLeaderboardCoroutine(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "Platform Score",
                    Value = score
                }
            }
        };

        PlayFabClientAPI.UpdatePlayerStatistics(request, result =>
        {
            if (result != null)
            {
                Debug.Log("Leaderboard updated successfully!");
            }
            else
            {
                Debug.LogError("Failed to update leaderboard: result is null.");
            }
        }, error =>
        {
            Debug.LogError("Failed to update leaderboard: " + error.ErrorMessage);
        });

        // Wait for a short delay after the PlayFab API call
        yield return new WaitForSeconds(1f);
    }


    public void SendLeaderboardDelayedButton(int score)
    {
        StartCoroutine(SendLeaderboardWithDelay(score, 1f));
    }

    public IEnumerator SendLeaderboardWithDelay(int score, float delay)
    {
        yield return new WaitForSeconds(delay);
        SendLeaderboardCoroutine(score);
    }

    public void GetLeaderboard()
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = "Platform Score",
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, null);
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (var entry in result.Leaderboard)
        {
            UnityEngine.Debug.Log(entry.Position + " " + entry.PlayFabId + " " + entry.StatValue);
        }
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        UnityEngine.Debug.Log("Leaderboard updated!");
    }
   
}
