using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    public GameObject trap; // 引用要控制的机关对象

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 检查触发器的碰撞是否来自特定对象，例如玩家
        if (other.CompareTag("Enemy"))
        {
            // 当玩家触碰到开关时，使机关消失
            if (trap != null)
            { 
                Destroy(trap); // 完全销毁机关对象
            }
        }
    }
}
