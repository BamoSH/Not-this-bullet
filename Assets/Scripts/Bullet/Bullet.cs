using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    
    public float surviveTime = 2f; // 子弹存活时间
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
            // 触发重物掉落
            collision.gameObject.GetComponent<Trap>().Fall();
            Destroy(gameObject); // 销毁子弹
        }
        
        if (collision.gameObject.CompareTag("Barrel"))
        {
            Debug.Log("Bullet hit barrel");
            GameObject o;
            (o = collision.gameObject).GetComponent<Barrel>().Explode(); // 触发油桶爆炸
            o.GetComponent<Renderer>().enabled = false; // 隐藏油桶
            Destroy(gameObject); // 子弹击中油桶后销毁
        }
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Bullet hit enemy");
            // 对敌人造成伤害
            collision.gameObject.GetComponent<Enemy>().TakeDamage(2);
            Destroy(gameObject); // 销毁子弹
        }
    }

    private void OnDestroy()
    {
        Destroy(gameObject, surviveTime);
    }
}
