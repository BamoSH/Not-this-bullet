using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePadAni : MonoBehaviour
{
    private Animator animator; // 引用Animator组件
    private static readonly int PlayBounce = Animator.StringToHash("PlayBounce");

    void Start()
    {
        // 获取Animator组件
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D other)
    { 
        Debug.Log("BouncePadAni: Player hit the BouncePad");
        if (other.gameObject.CompareTag("Player"))
        {
            // 触发动画，假设动画状态名为"Activate"
            animator.SetTrigger(PlayBounce);
        }
    }
    
}
