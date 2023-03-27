using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public GameObject levelCompleteScreen;
    private void OnTriggerEnter2D(Collider2D collideFinish)
    {
        if (collideFinish.gameObject.CompareTag("Finish"))
        {
            levelCompleteScreen.SetActive(true);
            Time.timeScale = 0;
        }

    }
}
