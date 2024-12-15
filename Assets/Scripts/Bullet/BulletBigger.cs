using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletBigger : MonoBehaviour
{
    public float scaleIncreaseAmount = 1.2f; // 敌人体积增大的倍数

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 增大敌人体积
            IncreaseEnemySize(collision.gameObject);
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Trap"))
        {
            Debug.Log("Bullet hit trap");
            // 触发重物掉落
            collision.gameObject.GetComponent<Trap>().Fall();
            Destroy(gameObject); // 销毁子弹
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
            Destroy(gameObject); // 销毁子弹
        }
        if (collision.gameObject.CompareTag("BouncePad"))
        {
            Debug.Log("Hit BouncePad");
            BouncePad bouncePad = collision.gameObject.GetComponent<BouncePad>();
            Vector3 newSize = bouncePad.transform.localScale * 2;
            // 设置一个尺寸上限，例如原始尺寸的两倍
            Vector3 maxSize = bouncePad.originalScale * 2;
            bouncePad.transform.localScale = Vector3.Min(newSize, maxSize);
            if (bouncePad.bounceStrength * 2 >= bouncePad.maxBounceStrength)
            {
                bouncePad.bounceStrength = bouncePad.maxBounceStrength;
            }
            Destroy(gameObject); // 销毁子弹
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject); // 销毁子弹
        }
    }

    void IncreaseEnemySize(GameObject enemy)
    {
        enemy.transform.localScale *= scaleIncreaseAmount;
    }
}

