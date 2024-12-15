using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Porter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Portal: OnTriggerEnter2D");
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached the portal!");
            // 玩家通过了传送门
            GameManager.Instance.PlayerReachedPortal();
        }
    }
}