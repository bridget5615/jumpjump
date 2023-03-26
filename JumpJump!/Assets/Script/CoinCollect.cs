using UnityEngine;

public class CoinCollect : MonoBehaviour {
    
    [SerializeField] private AudioSource coin1;
    [SerializeField] private AudioSource coin2;


    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Coin"))
        {
            PlayerManager.numberOfCoins++;
            PlayerPrefs.SetInt("NumberOfCoins", PlayerManager.numberOfCoins);
            coin1.Play();
            Destroy(collider2D.gameObject);
        }

        if (collider2D.gameObject.CompareTag("RedCoin"))
        {
            PlayerManager.numberOfCoins += 5;
            PlayerPrefs.SetInt("NumberOfCoins", PlayerManager.numberOfCoins);
            coin2.Play();
            Destroy(collider2D.gameObject);

        }
    }
}
