using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using TMPro; 

// susing Newtonsoft.Json;
using UnityEngine.UI;

public class PlayfabManager : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI messageText;
    
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;

    // register / login / resetpassword
    public void RegisterButton()
    {
        UnityEngine.Debug.Log("Hello, World!");
        if (passwordInput.text.Length < 6)
        {
            messageText.text = "Password must be at least 6 characters";
            return;
        }
        var request = new RegisterPlayFabUserRequest {
            Username = usernameInput.text, 
            Password = passwordInput.text, 
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }

    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        messageText.text = "Registered and logged in!";
    }

    public void LoginButton()
{
    if (string.IsNullOrEmpty(usernameInput.text))
    {
        messageText.text = "Please enter your username.";
        return;
    }

    if (string.IsNullOrEmpty(passwordInput.text))
    {
        messageText.text = "Please enter your password.";
        return;
    }

    var request = new LoginWithPlayFabRequest { 
        Username = usernameInput.text,
        Password = passwordInput.text
    };

    PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnError);
}

    void OnLoginSuccess(LoginResult result)
    {
        messageText.text = "Logged in successfully!";
    }


    public void ResetPasswordButton()
{
    if (string.IsNullOrEmpty(usernameInput.text))
    {
        var request = new SendAccountRecoveryEmailRequest 
        {
            Email = usernameInput.text, 
            TitleId = PlayFabSettings.TitleId
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, onPasswordReset, OnError);

    };
}

void onPasswordReset(SendAccountRecoveryEmailResult result)
{
    messageText.text = "Password reset email sent!";
}







   // Start is called before the first frame update
    void Start()
    {
        UnityEngine.Debug.Log("PlayfabManager Start called");
        Login();
    }

    // Update is called once per frame
    void Login()
    {
        var request = new LoginWithCustomIDRequest { 
            CustomId = SystemInfo.deviceUniqueIdentifier, 
            CreateAccount = true 
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result)
    {
        UnityEngine.Debug.Log("Successful login/account created!");
    }

    void OnError(PlayFabError error)
    {
        messageText.text = error.ErrorMessage; 
        UnityEngine.Debug.Log("Error while logging in/creating a new account");
        UnityEngine.Debug.Log(error.GenerateErrorReport());
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
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
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
