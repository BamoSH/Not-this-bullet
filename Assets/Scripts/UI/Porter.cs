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
            GameManager.Instance.PlayerReachedPortal();
        }
    }
}