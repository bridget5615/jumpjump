using Firebase.Database;
using UnityEngine;
using UnityEngine.UI;

public class Databasemanager : MonoBehaviour
{
    public InputField Name;

    private string userID;
    private DatabaseReference dbReference;

    void Start()
    {
        userID = SystemInfo.deviceUniqueIdentifier;
        dbReference = FirebaseDatabase.DefaultInstance.RootReference;   
    }

    public void CreateUser()
    {
        User newUser = new User(Name.text);
        string json = JsonUtility.ToJson(newUser);

        dbReference.Child("users").Child(userID).SetRawJsonValueAsync(json);
    }

}
