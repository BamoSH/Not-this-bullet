using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public GameObject explosionEffect; // 爆炸效果的预制体
    public float explosionRadius = 5f; // 爆炸半径
    public float maxExplosionRadius = 10f;
    public int explosionDamage = 50; // 爆炸伤害
    public float explosionForce = 10f; // 爆炸击退力
    public Vector3 originalScale;
    public float delayBeforeDestroy = 1f; // 击退后延迟销毁敌人的时间


    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Bullet"))
    //     {
    //         Debug.Log("Barrel hit something");
    //         Explode();
    //         Destroy(gameObject); // 销毁油桶
    //     }
    // }

    private void Awake()
    {
        originalScale = transform.localScale;
    }

    public void Explode()
    {
        // 显示爆炸效果
        Debug.Log("Barrel explode");
        Instantiate(explosionEffect, transform.position, Quaternion.identity);

        // 检测爆炸范围内的所有对象
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var collider in colliders)
        {
            // 如果对象是敌人，则造成伤害
            if (collider.CompareTag("Enemy"))
            {
                Debug.Log("Hit enemy with explosion");
                // 如果对象有刚体，则添加爆炸击退力
                if (collider.GetComponent<Rigidbody2D>() != null)
                {
                    Debug.Log("Add force to enemy");
                    Vector2 direction = collider.transform.position - transform.position;
                    collider.GetComponent<Rigidbody2D>().AddForce(direction.normalized * explosionForce);
                }
                StartCoroutine(DestroyAfterDelay(collider.gameObject, delayBeforeDestroy));
                StartCoroutine(DestroyBarrelAfterDelay());
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // 在编辑器中绘制爆炸半径，方便调试
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
    
    IEnumerator DestroyAfterDelay(GameObject target, float delay)
    {
        Debug.Log("Before destroy enemy");
        yield return new WaitForSeconds(delay);
        Debug.Log("Destroy after delay");
        target.GetComponent<Enemy>().Die();
    }
    IEnumerator DestroyBarrelAfterDelay()
    {
        Debug.Log("Before destroy barrel");
        // 等待最长的延迟时间
        yield return new WaitForSeconds(delayBeforeDestroy);
        Debug.Log("Destroy barrel after delay");
        // 销毁油桶对象
        Destroy(gameObject);
    }
}
