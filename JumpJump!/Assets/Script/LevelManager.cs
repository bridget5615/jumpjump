using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour {
    
    public List<string> levelNames;  // List of all the level names in the game
    private int currentLevelIndex = 0;    // The index of the current level the player is on
    
    void Start () {
        LockLevels();   // Lock all levels at the start of the game
    }
    
    void LockLevels() {
        // Loop through all the levels and set them as inactive
        foreach (string levelName in levelNames) {
            SceneManager.LoadScene(levelName, LoadSceneMode.Additive);
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelName));
        }
        
        for (int i = 1; i < levelNames.Count; i++) {
            SceneManager.UnloadSceneAsync(levelNames[i]);
        }
    }
    
    void UnlockLevel(int levelIndex) {
        // Unlock a level by setting it as active
        SceneManager.LoadScene(levelNames[levelIndex], LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelNames[levelIndex]));
    }
    
    public void CompleteLevel() {
        // Called when the player completes a level
        
        if (currentLevelIndex < levelNames.Count - 1) {
            // Unlock the next level if there is one
            UnlockLevel(currentLevelIndex + 1);
            
            // Increment the current level index
            currentLevelIndex++;
        }
    }
}
