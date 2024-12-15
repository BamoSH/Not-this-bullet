using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance; // 单例模式
    public Image bulletIconImage; // UI 中展示子弹图标的 Image 组件
    public Sprite[] bulletIcons; // 引用 BulletIconManager
    public TextMeshProUGUI bulletCountText; // 用于显示子弹数量的 Text 组件

    void Awake()
    {
        Debug.Log("UIManager: Awake");
        // 初始化单例
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
    
    // 假设这是一个方法，用于根据当前子弹索引更新 UI 图标
    public void UpdateBulletIcon(int counter)
    {
        if (BulletClip.Instance != null && counter >= 0 && counter < BulletClip.Instance.clips.Length)
        {
            Debug.Log("Counter:" + counter);
            var index = int.Parse(BulletClip.Instance.clips[counter].name); // 确保 clips[counter] 不是 null
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
