using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
    // public void QuitGame()
    // {
    //     UnityEditor.EditorApplication.isPlaying = false;
    //     Application.Quit();
    // }
}
