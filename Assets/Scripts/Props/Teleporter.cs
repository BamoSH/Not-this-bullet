using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform targetTeleporter; 
    public float delayTime = 2.0f; 
    private bool isPlayerInTeleporter = false;
    private float timer = 0f;

    private void Update()
    {
        if (isPlayerInTeleporter)
        {
            timer += Time.deltaTime;
            if (timer >= delayTime)
            {
                TeleportPlayer();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInTeleporter = true;
            timer = 0f; 
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isPlayerInTeleporter = false;
            timer = 0f; 
        }
    }

    private void TeleportPlayer()
    {
        if (targetTeleporter != null)
        {
            GameObject.FindGameObjectWithTag("Player").transform.position = targetTeleporter.position;
            isPlayerInTeleporter = false; 
            timer = 0f; 
        }
    }
}
