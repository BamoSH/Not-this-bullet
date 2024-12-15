using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletBigger : MonoBehaviour
{
    public float scaleIncreaseAmount = 1.2f; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            IncreaseEnemySize(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Trap"))
        {
            Debug.Log("Bullet hit trap");
            collision.gameObject.GetComponent<Trap>().Fall();
            Destroy(gameObject); 
        }
        
        if (collision.gameObject.CompareTag("Barrel"))
        {
            Debug.Log("Bullet hit barrel");
            Barrel barrel = collision.GetComponent<Barrel>();
            Vector3 newSize = barrel.transform.localScale * 2;
            Vector3 maxSize = barrel.originalScale * 2;
            barrel.transform.localScale = Vector3.Min(newSize, maxSize);
            if (barrel.explosionRadius * 2 <= barrel.maxExplosionRadius)
            {
                barrel.explosionRadius *= 2;
            }
            Destroy(gameObject); 
        }
        if (collision.gameObject.CompareTag("BouncePad"))
        {
            Debug.Log("Hit BouncePad");
            BouncePad bouncePad = collision.gameObject.GetComponent<BouncePad>();
            Vector3 newSize = bouncePad.transform.localScale * 2;
            Vector3 maxSize = bouncePad.originalScale * 2;
            bouncePad.transform.localScale = Vector3.Min(newSize, maxSize);
            if (bouncePad.bounceStrength * 2 >= bouncePad.maxBounceStrength)
            {
                bouncePad.bounceStrength = bouncePad.maxBounceStrength;
            }
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject); 
        }
    }

    void IncreaseEnemySize(GameObject enemy)
    {
        enemy.transform.localScale *= scaleIncreaseAmount;
    }
}

