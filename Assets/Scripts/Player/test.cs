using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public float moveSpeed = 5f;
    public List<BulletType> bullets;
    public Transform firePoint;
    private PlayerInput _playerInput;
    private Vector2 moveInput;
    private Vector2 shootDirection;

    private void Awake()
    {
        _playerInput = new PlayerInput();

        _playerInput.GamePlay.Move.performed += context => moveInput = context.ReadValue<Vector2>();
        _playerInput.GamePlay.Move.canceled += context => moveInput = Vector2.zero;
        _playerInput.GamePlay.Fire.performed += context => Shoot();
    }

    private void OnEnable()
    {
        _playerInput.GamePlay.Enable();
    }

    private void OnDisable()
    {
        _playerInput.GamePlay.Disable();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 movement = new Vector3(moveInput.x, moveInput.y, 0) * moveSpeed * Time.deltaTime;
        transform.Translate(movement);

        // 更新射击方向为最后的移动方向
        if (moveInput != Vector2.zero)
        {
            shootDirection = moveInput;
        }
    }

    private void Shoot()
    {
        GameObject bulletPrefab = ChooseRandomBullet();
        if (bulletPrefab != null)
        {
            var bulletInst = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            bulletInst.GetComponent<Rigidbody2D>().velocity = shootDirection.normalized * 10f; // 子弹速度
        }
    }

    private GameObject ChooseRandomBullet()
    {
        float totalProbability = 0;
        foreach (var bullet in bullets)
        {
            totalProbability += bullet.probability;
        }

        float randomPoint = Random.value * totalProbability;
        float currentProbability = 0;
        foreach (var bullet in bullets)
        {
            currentProbability += bullet.probability;
            if (randomPoint <= currentProbability)
            {
                return bullet.prefab;
            }
        }

        return null;
    }
}
