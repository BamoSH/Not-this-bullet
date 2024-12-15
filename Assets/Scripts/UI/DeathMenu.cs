using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    public Button restartButton; // 用于在Inspector中引用按钮

    void Start()
    {
        if (restartButton != null)
        {
            // 为按钮添加点击事件监听器
            restartButton.onClick.AddListener(OnRestartButtonClicked);
        }
    }

    void OnRestartButtonClicked()
    {
        // 确保GameManager已经被正确加载
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartLevel();
        }
        else
        {
            Debug.LogError("GameManager instance not found.");
        }
    }
    
    public void MainMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }
}
