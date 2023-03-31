using System.Collections.Generic;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 

// public class CountdownController : MonoBehaviour
// {

//     public float timeLeft = 3.0f;
//     public TextMeshProUGUI startText; //used for showing countdown from 3,2,1 


//     void Update()
//     {
//         timeLeft -= Time.deltaTime;
//         startText.text = (timeLeft).ToString("0");
//         if (timeLeft < 1)
//         {
//             startText.text = "GO!"; 
//         }

//     }
// }

public class CountdownController : MonoBehaviour
{
    public int countdownTime;
    public TextMeshProUGUI coundownDisplay;

    private void Start()
    {
        StartCoroutine(CountdownToStart());
        // GameObject.FindWithTag("Player").GetComponent<ArduinoController>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = false;
    }
    
    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            coundownDisplay.text = countdownTime.ToString();
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        coundownDisplay.text = "GO!";
        
        yield return new WaitForSeconds(1f);
        coundownDisplay.gameObject.SetActive(false);
                // GameObject.FindWithTag("Player").GetComponent<ArduinoController>().enabled = true;
                GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = true;
    }
}