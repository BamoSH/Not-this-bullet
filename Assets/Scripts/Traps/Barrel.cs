using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public GameObject explosionEffect; 
    public float explosionRadius = 5f; 
    public float maxExplosionRadius = 10f;
    public int explosionDamage = 50; 
    public float explosionForce = 10f; 
    public Vector3 originalScale;
    public float delayBeforeDestroy = 1f; 


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
        Debug.Log("Barrel explode");
        Instantiate(explosionEffect, transform.position, Quaternion.identity);

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Debug.Log("Hit enemy with explosion");
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
        yield return new WaitForSeconds(delayBeforeDestroy);
        Debug.Log("Destroy barrel after delay");
        Destroy(gameObject);
    }
}
