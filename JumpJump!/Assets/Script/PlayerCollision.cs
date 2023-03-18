using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Water")
        {
            PlayerManager.isGameOver = true;
            gameObject.SetActive(false);
        }
    }
}
