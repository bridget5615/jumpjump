using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginReg : MonoBehaviour
{
    public InputField nameInput;
    public Button submitbutton;

    public void CallLogin()
    {
        StartCoroutine(Login());
    }

    public void CallRegister()
    {
        StartCoroutine(Register());
    }

    IEnumerator Login()
    {
        WWWForm form = new WWWForm();
        form.AddField("name",nameInput.text);
        WWW www = new WWW("http://localhost/jumpjump/login.php", form);
        yield return www;
        if(www.text[0] == '0')
        {
            DBManager.username = nameInput.text;
            DBManager.coins = int.Parse(www.text.Split('\t')[1]);
            DBManager.highscore = int.Parse(www.text.Split('\t')[2]);
        }

        else
        {
            StartCoroutine(Register());
        }
    }

    IEnumerator Register()
    {
        WWWForm form = new WWWForm();
        form.AddField("name",nameInput.text);
        WWW www = new WWW("http://localhost/jumpjump/register.php", form);
        yield return www;
        if(www.text[0] == '0')
        {
            Debug.Log("user registered.");
        }

        else
        {
            Debug.Log("Error #" + www.text);
        }

    }

    public void VerifyInputs()
    {
        submitbutton.interactable = (nameInput.text.Length >= 1);
    }
}
