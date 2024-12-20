using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class BulletClip : MonoBehaviour
{
    public static BulletClip Instance { get; private set; }
    public GameObject[] clips; 
    public GameObject[] weaponBag; 
    public BulletManager bulletManager; 
    public int clipsSize = 50;
    public PlayerController PlayerController;
    
    public delegate void BulletCountChanged(int newBulletCount);
    public static event BulletCountChanged OnBulletCountChanged;

    private void Awake()
    {
        Debug.Log("BulletClips Awake Start!");
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        clips = new GameObject[clipsSize];
        for (int i = 0; i < weaponBag.Length; i++)
        {
            Debug.Log("bulletClip weapon bag:"+weaponBag[i].name);
        }
    }
    
    private void Start()
    {
        // clips = new GameObject[clipsSize];
        // for (int i = 0; i < weaponBag.Length; i++)
        // {
        //     Debug.Log("bulletClip weapon bag:"+weaponBag[i].name);
        // }
    }

    public void SetCurrentWeaponBag(GameObject[] weaponBag)
    {
        Debug.Log("SetCurrentWeaponBag Start!");
        this.weaponBag = weaponBag;
        // for (int i = 0; i < this.weaponBag.Length; i++)
        // {
        //     Debug.Log(weaponBag[i].name);
        //     Debug.Log(this.weaponBag[i].name);
        // }
    }

    public void Reload()
    {
        Debug.Log("Reload Start!");
        if (weaponBag.Length == 0)
        {
            Debug.Log("Warning: weaponBag is empty.");
            return; 
        }
        for (int i = 0; i < clips.Length; i++)
        {
            Debug.Log("Clips: " + i);
            clips[i] = weaponBag[Random.Range(0, bulletManager.weaponBag.Length)];
        }
    }
    
    public void AddExtraBullets(int extraBullets)
    {
        Debug.Log("AddExtraBullets");
        int currentClipsLength = clips.Length;
        int newClipsLength = currentClipsLength + extraBullets;
        GameObject[] newClips = new GameObject[newClipsLength];

        for (int i = 0; i < currentClipsLength; i++)
        {
            newClips[i] = clips[i];
        }

        for (int i = currentClipsLength; i < newClipsLength; i++)
        {
            int randomIndex = Random.Range(0, weaponBag.Length);
            newClips[i] = weaponBag[randomIndex];
        }

        clips = newClips;
        PlayerController.bulletCounter += extraBullets;
        OnBulletCountChanged?.Invoke(PlayerController.bulletCounter);
        Debug.Log("extraBullets: " + extraBullets);
        Debug.Log("bulletCounter: " + PlayerController.bulletCounter);
    }
}
