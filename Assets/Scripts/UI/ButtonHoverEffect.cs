using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHoverEffect : MonoBehaviour
{
    public Sprite defaultImage; 
    public Sprite hoverImage; 
    public Sprite clickImage; 


    private Image buttonImage; 

    private void Awake()
    {
        buttonImage = GetComponent<Image>();
    }

    public void OnHoverEnter()
    {
        buttonImage.sprite = hoverImage; 
    }

    public void OnHoverExit()
    {
        buttonImage.sprite = defaultImage; 
    }
    
    public void OnClick()
    {
        buttonImage.sprite = clickImage;
    }
}
