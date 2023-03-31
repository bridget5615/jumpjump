using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Registerbutton : MonoBehaviour
{
    public InputField UsernameInput;
    public Button registerButton;

    // Start is called before the first frame update
    void Start()
    {
        registerButton.onClick.AddListener(()=> {
            StartCoroutine(Mainlog.Instance.Web.RegisterUser(UsernameInput.text));
        });
    }

}