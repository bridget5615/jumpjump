using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

public class CountdownController : MonoBehaviour
{

    public float timeLeft = 3.0f;
    public TextMeshProUGUI startText; //used for showing countdown from 3,2,1 


    void Update()
    {
        timeLeft -= Time.deltaTime;
        startText.text = (timeLeft).ToString("0");
        if (timeLeft < 1)
        {
            startText.text = "GO!"; 
            Destroy(startText.gameObject, 1.0f); //destroy the GO! text after 1 second
        }

    }
}
