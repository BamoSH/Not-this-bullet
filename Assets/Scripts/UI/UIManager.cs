using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; 
    public Image bulletIconImage;
    public Sprite[] bulletIcons; 
    public TextMeshProUGUI bulletCountText; 

    void Awake()
    {
        Debug.Log("UIManager: Awake");
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void OnEnable()
    {
        BulletClip.OnBulletCountChanged += UpdateBulletCount;
        PlayerController.OnBulletCountChanged += UpdateBulletCount;
    }

    private void OnDisable()
    {
        BulletClip.OnBulletCountChanged -= UpdateBulletCount;
        PlayerController.OnBulletCountChanged -= UpdateBulletCount;
    }
    
    public void UpdateBulletIcon(int counter)
    {
        if (BulletClip.Instance != null && counter >= 0 && counter < BulletClip.Instance.clips.Length)
        {
            Debug.Log("Counter:" + counter);
            var index = int.Parse(BulletClip.Instance.clips[counter].name); 
            Debug.Log("index:" + index);
            bulletIconImage.sprite = bulletIcons[index];
        }
        else
        {
            Debug.LogError("BulletClip instance is null or counter is out of range.");
        }
    }
    
    public void UpdateBulletCount(int bulletCount)
    {
        bulletCountText.text = $"Bullets: {bulletCount}";
    }
}
