using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public string level0;
    public string mainScene;

    private void Awake()
    {
        Time.timeScale = 1f;
    }

    public void StartGame()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene("Level 0");
        SceneManager.LoadSceneAsync(mainScene, LoadSceneMode.Additive);

    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
