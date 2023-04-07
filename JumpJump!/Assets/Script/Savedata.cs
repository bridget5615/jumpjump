using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savedata : MonoBehaviour
{
    public void CallSaveData()
    {
        StartCoroutine(SavePlayerData());
    }

    IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.username);
        form.AddField("coins", DBManager.coins.ToString());
        form.AddField("highscore", DBManager.highscore.ToString());
        WWW www = new WWW("http://localhost/jumpjump/savedata.php", form);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("GameSaved.");
        }
        else
        {
            Debug.Log("Save failed. Error #" + www.text);
        }

        DBManager.Logout();
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);

    }
}
