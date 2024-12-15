using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public float surviveTime = 2f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Debug.Log("Bullet hit trap");
            collision.gameObject.GetComponent<Trap>().Fall();
            Destroy(gameObject); 
        }
        
        if (collision.gameObject.CompareTag("Barrel"))
        {
            Debug.Log("Bullet hit barrel");
            GameObject o;
            (o = collision.gameObject).GetComponent<Barrel>().Explode();
            o.GetComponent<Renderer>().enabled = false; 
            Destroy(gameObject); 
        }
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Bullet hit enemy");
            collision.gameObject.GetComponent<Enemy>().TakeDamage(2);
            Destroy(gameObject); 
        }
    }

    private void OnDestroy()
    {
        Destroy(gameObject, surviveTime);
    }
}
