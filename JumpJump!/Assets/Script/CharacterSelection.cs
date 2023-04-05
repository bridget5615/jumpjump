using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelection : MonoBehaviour
{
    public GameObject[] characters;
    public int selectedCharacter;

    private void Awake()
    {
        selectedCharacter = PlayerPrefs.GetInt("selectedCharacter", 0);
        foreach (GameObject character in characters)
        {
            character.SetActive(true);
        }
        characters[selectedCharacter].SetActive(true);
    }

    public void ChangeNext()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter++;
        if (selectedCharacter >= characters.Length)
        {
            selectedCharacter = 0;
        }
        characters[selectedCharacter].SetActive(true);
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
    }

    public void ChangePrevious()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter = characters.Length - 1;
        }
        characters[selectedCharacter].SetActive(true);
        PlayerPrefs.SetInt("selectedCharacter", selectedCharacter);
    }
}
