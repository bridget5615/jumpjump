using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameObject levelCompleteScreenNext;
    public GameObject levelCompleteScreenchangeleg;
    public static int levelcompletetimes;



    private void OnTriggerEnter2D(Collider2D collideFinish)
    {
        if (collideFinish.gameObject.CompareTag("Finish"))
        {
            levelcompletetimes = levelcompletetimes + 1;
            if (levelcompletetimes%2 == 0)
            {

                levelCompleteScreenNext.SetActive(true);
                Time.timeScale = 0;

            }

            else
            {

                levelCompleteScreenchangeleg.SetActive(true);
                Time.timeScale = 0;
                
            }
            
        }

    }
}
