using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePadAni : MonoBehaviour
{
    private Animator animator; 
    private static readonly int PlayBounce = Animator.StringToHash("PlayBounce");

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D other)
    { 
        Debug.Log("BouncePadAni: Player hit the BouncePad");
        if (other.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger(PlayBounce);
        }
    }
    
}
