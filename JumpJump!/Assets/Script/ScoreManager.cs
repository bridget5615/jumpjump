using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public Text scoreText;
    public Text highscoreText;

    int score = 0;
    int highscore = 0;
    //bool levelCompleted = false;

    private void Awake(){
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        highscore = PlayerPrefs.GetInt("highscore", 0);
        scoreText.text = "SCORE: " + score.ToString();
        highscoreText.text = "HIGHSCORE: " + highscore.ToString();
    }



    public void AddPoint()
    {
        if (Time.timeScale == 1)
        {
            score += 100;
            scoreText.text = "SCORE: " + score.ToString();
            if (highscore < score)
            {
                PlayerPrefs.SetInt("highscore", score);
            }
        }
        else
        {
            score += 0;
        }
    }

    //public void LevelCompleted()
    //{
    //    if (Time.timeScale == 0)
    //    {
    //        levelCompleted = true;
    //    }
    //    else
    //    {
    //        levelCompleted = false;
    //    }

    //}
}
