using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Web : MonoBehaviour
{
    void Start()
    {
       //StartCoroutine(GetUsers());
       //StartCoroutine(Login("testuser3")); 
       //StartCoroutine(RegisterUser("testuser3"));
       //StartCoroutine(GetCoins(Mainlog.Instance.Userinfo.userID));

    }


    IEnumerator GetUsers()
    {
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/jumpjump/Getusers.php"))
        {
            yield return www.SendWebRequest();

            if(www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                byte[] results = www.downloadHandler.data;
            }
        }
    }

    public IEnumerator Login(string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/jumpjump/login.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }
        }
    }

    public IEnumerator RegisterUser(string username)
    {
        WWWForm form = new WWWForm();
        form.AddField("loginUser", username);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/jumpjump/RegisterUser.php", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);
            }

        }
    }

public IEnumerator GetCoins(string username){
    WWWForm form = new WWWForm();
    form.AddField("username", username);

    using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/jumpjump/Coinretrieve.php",form))
        {
            yield return www.SendWebRequest();

            if(www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                string currentcoin = www.downloadHandler.text;

            }
        }
}

public IEnumerator Getscore(string username){
    WWWForm form = new WWWForm();
    form.AddField("username", username);

    using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/jumpjump/Scoreretrieve.php",form))
        {
            yield return www.SendWebRequest();

            if(www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text);

                string currenthighscore = www.downloadHandler.text;

            }
        }
}
}

