using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using TMPro; 
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayfabManager : MonoBehaviour
{

    [Header("Leaderboard")]
    public GameObject rowPrefab;
    public Transform rowsParent;

    [Header("Windows")]
    public GameObject nameWindow;
    public GameObject leaderboardWindow;


    [Header("Display name window")]
    public GameObject nameError;
    public TMP_InputField nameinput;

    [Header("UI")]
    public TextMeshProUGUI messageText;
    public TMP_InputField usernameInput;

    private string displayName;


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
        string name = null;
        if(result.InfoResultPayload.PlayerProfile != null)
            name = result.InfoResultPayload.PlayerProfile.DisplayName;
        
        if (name == null)
            nameWindow.SetActive(true);
        else
            leaderboardWindow.SetActive(true);
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

        messageText.text = "Please key in your username to play :)";
    }

    public void Login()
    {
        var request = new LoginWithCustomIDRequest { 
            CustomId = SystemInfo.deviceUniqueIdentifier, 
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
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
        foreach (Transform item in rowsParent)
        {
            Destroy(item.gameObject);
        }

        foreach (var item in result.Leaderboard)
        {
            GameObject newGo = Instantiate(rowPrefab,rowsParent);
            Text[] texts = newGo.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position +1).ToString();
            texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();

            Debug.Log(string.Format("PLACE:{0}| ID:{1}| VALUE:{2}",
            item.Position, item.PlayFabId, item.StatValue));

        }
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        UnityEngine.Debug.Log("Leaderboard updated!");
    }

    public void SubmitNameButton()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = nameinput.text,
        };
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Updated display name!");
        leaderboardWindow.SetActive(true);
    }
}
