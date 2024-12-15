using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public int damage = 2; 
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.isKinematic = true; // 初始状态不受物理影响
    }

    public void Fall()
    {
        rb.isKinematic = false; // 开始受物理影响并下落
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 对敌人造成伤害
            collision.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            // Destroy(gameObject); // 销毁重物
        }
    }
}
