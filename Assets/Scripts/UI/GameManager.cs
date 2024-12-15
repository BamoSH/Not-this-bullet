using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // 当前关卡索引
    public int currentLevelIndex = 0;

    // 关卡的总数
    public int totalLevels = 2;

    // 最后一关完成后加载的场景名称
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
        // 检查是否还有更多的关卡
        if (currentLevelIndex < totalLevels - 1)
        {
            // 加载下一关
            currentLevelIndex++;
            SceneManager.LoadScene("Level " + currentLevelIndex);
            SceneManager.LoadSceneAsync("UIandPlayerScene", LoadSceneMode.Additive);
        }
        else
        {
            // 所有关卡完成，加载结束游戏的场景
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
        // 玩家通过当前关卡，加载下一关
        LoadNextLevel();
    }
}
