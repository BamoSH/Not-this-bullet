using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BulletManager : MonoBehaviour
{
    
    public GameObject[] allBullets; // 存储所有子弹预制体的数组
    public GameObject[] weaponBag; // 武器袋数组
    public int weaponBagSize = 3; // 武器袋的大小

    private void Awake()
    {
        Debug.Log("BulletManager Awake Start!");
        weaponBag = new GameObject[weaponBagSize];
        RandomizeWeaponBag();
        for (int i = 0; i < weaponBag.Length; i++)
        {
            Debug.Log("WeaponBag: "+weaponBag[i].name);
        }
        if (BulletClip.Instance != null)
        {
            BulletClip.Instance.SetCurrentWeaponBag(weaponBag);
            BulletClip.Instance.Reload();
            Debug.Log("bulletClip: " + BulletClip.Instance.clips.Length);
        }
    }

    void Start()
    {
        // weaponBag = new GameObject[weaponBagSize];
        // RandomizeWeaponBag();
        // for (int i = 0; i < weaponBag.Length; i++)
        // {
        //     Debug.Log("WeaponBag: "+weaponBag[i].name);
        // }
        // if (BulletClip.Instance != null)
        // {
        //     BulletClip.Instance.SetCurrentWeaponBag(weaponBag);
        //     BulletClip.Instance.Reload();
        //     Debug.Log("bulletClip: " + BulletClip.Instance.clips.Length);
        // }
    }
    
    void RandomizeWeaponBag()
    {
        Debug.Log("RandomizeWeaponBag Start!");
        GameObject[] shuffledBullets = (GameObject[])allBullets.Clone();
        for (int i = 0; i < shuffledBullets.Length; i++)
        {
            int randomIndex = Random.Range(i, shuffledBullets.Length);
            (shuffledBullets[i], shuffledBullets[randomIndex]) = (shuffledBullets[randomIndex], shuffledBullets[i]);
        }

        for (int i = 0; i < weaponBagSize; i++)
        {
            weaponBag[i] = shuffledBullets[i];
        }
    }
}