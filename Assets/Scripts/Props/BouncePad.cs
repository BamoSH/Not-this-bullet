using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public float bounceStrength = 35f; 
    public float maxBounceStrength = 60f;
    public Vector3 originalScale; 

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRigidbody != null)
            {
                playerRigidbody.AddForce(Vector2.up * bounceStrength, ForceMode2D.Impulse);
            }
        }
    }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Player"))
    //     {
    //         Rigidbody2D playerRigidbody = other.gameObject.GetComponent<Rigidbody2D>();
    //         if (playerRigidbody != null)
    //         {
    //             playerRigidbody.AddForce(Vector2.up * bounceStrength, ForceMode2D.Impulse);
    //         }
    //     }
    // }
}
