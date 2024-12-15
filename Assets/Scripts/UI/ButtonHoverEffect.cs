using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour
{
    public Sprite defaultImage; // 默认图片
    public Sprite hoverImage; // 鼠标悬浮图片
    public Sprite clickImage; // 鼠标点击图片


    private Image buttonImage; // 按钮的 Image 组件

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
    }

    public void OnHoverEnter()
    {
        buttonImage.sprite = hoverImage; // 切换到悬浮图片
    }

    public void OnHoverExit()
    {
        buttonImage.sprite = defaultImage; // 恢复默认图片
    }
    
    public void OnClick()
    {
        buttonImage.sprite = clickImage; // 切换到点击图片
    }
}
