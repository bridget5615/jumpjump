using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Savedata : MonoBehaviour
{
    public Inventory inventory = new Inventory();

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            SaveToJson();
        }
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadFromJson();
        }
    }

    public void SaveToJson()
    {
        string inventoryData = JsonUtility.ToJson(inventory);
        string filePath = Application.persistentDataPath + "/InventoryData.json";
        Debug.Log(filePath);
        System.IO.File.WriteAllText(filePath,inventoryData);
        Debug.Log("Data saved.");
    }

    public void LoadFromJson()
    {
        string filePath = Application.persistentDataPath + "/InventoryData.json";
        string inventoryData = System.IO.File.ReadAllText(filePath);

        inventory = JsonUtility.FromJson<Inventory>(inventoryData);
        Debug.Log("Data loaded.");
    }
}

[System.Serializable]
public class Inventory
{
    public string username;
    public int Coins;
    public int Level;
    public int Highscore;
    public List<Items> items = new List<Items>();
}
[System.Serializable]
public class Items
{
    public string name;
}