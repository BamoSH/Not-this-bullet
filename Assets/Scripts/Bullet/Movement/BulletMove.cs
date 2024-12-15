using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour, IBullet
{
    private PlayerInput _playerInput;
    private Camera _mainCamera;
    private int _damage=1;

     
    private Rigidbody2D _rb;
    public float moveSpeed = 5; 

    private Vector2 _direction;
    private PlayerController _playerController;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>(); 
    }

    private void Update()
    {
        BMove();
    }
    
    private void BMove()
    {
        transform.position += (Vector3)_direction * (moveSpeed * Time.deltaTime);
    }
    
    public void Initialize(Vector2 dir)
    {
        this._direction = dir.normalized; 
    }
    
    
    
    // private void OnTriggerStay2D(Collider2D other)
    // {
    //     var enemyCs = other.gameObject.GetComponent<AbstractEnemy>();
    //     if (enemyCs)
    //     {
    //         if (enemyCs.hp>0)
    //         {
    //             enemyCs.hp -= _damage;
    //         }
    //
    //         if (enemyCs.hp<=0)
    //         {
    //             Destroy(other.gameObject);
    //         }
    //         Debug.Log("hp-" + _damage);
    //         Destroy(gameObject);
    //     }
    // }

}
