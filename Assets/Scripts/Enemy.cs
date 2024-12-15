using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 10; // 敌人的初始生命值
    public int extraBullets = 5; // 敌人死亡后掉落的额外子弹数
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }
    
    public void RecoverHp(int recover )
    {
        health += recover;
    }

    // 敌人死亡的逻辑
    public void Die()
    { 
        Debug.LogWarning("Enemy Die: " + gameObject.name + " died!");
        if (BulletClip.Instance != null) // 确保找到了组件
        {
            BulletClip.Instance.AddExtraBullets(extraBullets);
        }
        else
        {
            Debug.LogWarning("Warning: No BulletClip component found on " + gameObject.name);
        }
        Destroy(gameObject); // 销毁敌人对象
    }
    
    
}
