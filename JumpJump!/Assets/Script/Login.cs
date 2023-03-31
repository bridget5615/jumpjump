using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField UsernameInput;
    public Button LoginButton;

    // Start is called before the first frame update
    void Start()
    {
        LoginButton.onClick.AddListener(()=> {
           StartCoroutine(Mainlog.Instance.Web.Login(UsernameInput.text));
           StartCoroutine(Mainlog.Instance.Web.GetCoins(UsernameInput.text));
           StartCoroutine(Mainlog.Instance.Web.Getscore(UsernameInput.text));
        });
    }

}
