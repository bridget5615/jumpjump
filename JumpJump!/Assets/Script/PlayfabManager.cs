using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.Internal;
using TMPro; 
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayfabManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI messageText;
    public TMP_InputField usernameInput;
    

    [Header("PlayFab")]
    [SerializeField] private string titleId = "EBE9E"; // Replace with your title id

    private string displayName;

    void Awake()
    {
        PlayFabSettings.staticSettings.TitleId = titleId;
    }

    public void Login()
    {
        // Check if the username input field is empty
        if (string.IsNullOrEmpty(usernameInput.text))
        {
            DisplayErrorMessage("Please enter a username :)");
            return;
        }

        // Call the PlayFab login API with the device ID as the custom ID
        string customId = SystemInfo.deviceUniqueIdentifier;
        var request = new LoginWithCustomIDRequest { CustomId = customId, CreateAccount = true };
        PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginError);
    }



    IEnumerator DelayedLoginSuccess(LoginResult result)
    {
        yield return new WaitForSeconds(1f);
        OnLoginSuccess(result);
    }

    void OnLoginSuccess(LoginResult result)
    {
        messageText.text = "Login successful!";
        Debug.Log("Successful login/account created!");

        // Update the user's display name in PlayFab
        UpdateDisplayName();

        StartCoroutine(DelayedLoadMainMenu());
    }

    IEnumerator DelayedLoadMainMenu()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("MainMenu");
    }

    void OnLoginError(PlayFabError error)
    {
        string errorMessage = error.GenerateErrorReport();
        DisplayErrorMessage(errorMessage);
        Debug.Log("Error while logging in/creating a new account");
        Debug.Log(errorMessage);
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
        Debug.Log("Display name updated successfully!");
    }

    void OnDisplayNameUpdateError(PlayFabError error)
    {
        string errorMessage = error.GenerateErrorReport();
        DisplayErrorMessage(errorMessage);
        Debug.LogError("Failed to update display name: " + errorMessage);
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

        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnLeaderboardUpdateError);

        // Wait for a short delay after the PlayFab API call
        yield return new WaitForSeconds(1f);
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        UnityEngine.Debug.Log("Leaderboard updated successfully!");
    }

    void OnLeaderboardUpdateError(PlayFabError error)
    {
        string errorMessage = error.GenerateErrorReport();
        DisplayErrorMessage(errorMessage);
        UnityEngine.Debug.LogError("Failed to update leaderboard: " + errorMessage);
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

    void OnLeaderboardUpdateDelayed(UpdatePlayerStatisticsResult result)
    {
        UnityEngine.Debug.Log("Leaderboard updated with delay!");
    }
   
}