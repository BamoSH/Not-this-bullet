using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int currentLevelIndex = 0;

    public int totalLevels = 2;

    public string endGameSceneName = "End";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Debug.Log("Restarting level " + currentLevelIndex);
        SceneManager.LoadScene("Level "+ currentLevelIndex);
        SceneManager.LoadSceneAsync("UIandPlayerScene", LoadSceneMode.Additive);
    }

    public void LoadNextLevel()
    {
        if (currentLevelIndex < totalLevels - 1)
        {
            currentLevelIndex++;
            SceneManager.LoadScene("Level " + currentLevelIndex);
            SceneManager.LoadSceneAsync("UIandPlayerScene", LoadSceneMode.Additive);
        }
        else
        {
            SceneManager.LoadScene(endGameSceneName);
        }
    }

    public void LoadLevelByIndex(int levelIndex)
    {
        if (levelIndex >= 0 && levelIndex < totalLevels)
        {
            currentLevelIndex = levelIndex;
            SceneManager.LoadScene(levelIndex);
        }
        else
        {
            Debug.LogError("Trying to load level out of bounds.");
        }
    }

    public void PlayerDied()
    {
        SceneManager.LoadScene("Dead");
    }

    public void PlayerReachedPortal()
    {
        LoadNextLevel();
    }
}
