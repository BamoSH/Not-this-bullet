using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Transform targetTeleporter; // 目标传送点
    public float delayTime = 2.0f; // 玩家需要在传送点上停留的时间
    private bool isPlayerInTeleporter = false;
    private float timer = 0f;

    private void Update()
    {
        if (isPlayerInTeleporter)
        {
            // 累计停留时间
            timer += Time.deltaTime;
            if (timer >= delayTime)
            {
                // 达到延迟时间，执行传送
                TeleportPlayer();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // 玩家进入传送点
            isPlayerInTeleporter = true;
            timer = 0f; // 重置计时器
        }
    }
    
    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // 玩家离开传送点
            isPlayerInTeleporter = false;
            timer = 0f; // 重置计时器
        }
    }

    private void TeleportPlayer()
    {
        if (targetTeleporter != null)
        {
            // 将玩家传送到目标传送点的位置
            GameObject.FindGameObjectWithTag("Player").transform.position = targetTeleporter.position;
            isPlayerInTeleporter = false; // 防止立即再次触发传送
            timer = 0f; // 重置计时器
        }
    }
}
