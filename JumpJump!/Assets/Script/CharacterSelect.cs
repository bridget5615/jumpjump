using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterSelect : MonoBehaviour
{
    public GameObject[] skins;
    public int selectedCharacter;
    public Character[] characters;

    public Button unlockButton;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI coinsText;

    public static int numberOfCoins;

    private void Awake()
    {
        numberOfCoins = PlayerPrefs.GetInt("NumberOfCoins", 0);
        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);
        foreach (GameObject player in skins)
            player.SetActive(false);

        skins[selectedCharacter].SetActive(true);

        foreach(Character c in characters)
        {
            if (c.price == 0)
                c.isUnlocked = true;
            else
            {
                c.isUnlocked = PlayerPrefs.GetInt(c.name, 0) == 0 ? false : true;
            }
        }

        if (numberOfCoins < 0)
        {
            numberOfCoins = 0;
        }
        UpdateUI();
    }

    public void ChangeNext()
    {
        skins[selectedCharacter].SetActive(false);
        selectedCharacter++;
        if (selectedCharacter == skins.Length)
            selectedCharacter = 0;

        skins[selectedCharacter].SetActive(true);
        if(characters[selectedCharacter].isUnlocked)
            PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);

        // PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
        UnityEngine.Debug.Log("next button pressed");

        UpdateUI();
    }

    public void ChangePrevious()
    {
        skins[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter == -1)
            selectedCharacter = skins.Length -1;

        skins[selectedCharacter].SetActive(true);
        if (characters[selectedCharacter].isUnlocked)
            PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
        // PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
        UnityEngine.Debug.Log("back button pressed");
        UpdateUI();
    }

    public void UpdateUI()
    {
        coinsText.text = numberOfCoins.ToString();
        nameText.text = characters[selectedCharacter].name;
        if (characters[selectedCharacter].isUnlocked == true)
        {
            unlockButton.gameObject.SetActive(false);
        }
        else
        {
            if (numberOfCoins < characters[selectedCharacter].price)
            {
                unlockButton.interactable = false;
                unlockButton.GetComponentInChildren<TextMeshProUGUI>().text = "Price:" + characters[selectedCharacter].price + "\nNot Enough Coins";
                unlockButton.gameObject.SetActive(true);
            }
            else
            {
                unlockButton.interactable = true;
                unlockButton.GetComponentInChildren<TextMeshProUGUI>().text = "Price:" + characters[selectedCharacter].price;
                unlockButton.gameObject.SetActive(true);
            }
        }
    }




    public void Unlock()
    {
        int coins = PlayerPrefs.GetInt("NumberOfCoins", 0);
        int price = characters[selectedCharacter].price;

        if (coins >= price) // Check if player has enough coins to purchase
        {
            PlayerPrefs.SetInt("NumberOfCoins", coins - price);
            PlayerPrefs.SetInt(characters[selectedCharacter].name, 1);
            PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
            characters[selectedCharacter].isUnlocked = true;
            UpdateUI();
        }
    }

}
