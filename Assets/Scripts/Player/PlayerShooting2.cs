using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting2 : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 5f;
    private int _damage;
    protected Vector2 direction;
    private PlayerInput _playerInput;
    private Camera _mainCamera;
    public GameObject bullet;
    public float survivalTime = 2;
    private PlayerController _playerController;
    private bool _facingRight;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _mainCamera = Camera.main;
        _facingRight = _playerController._facingRight;
    }
    private void OnEnable()
    {
        _playerInput.Enable();
        // _playerInput.GamePlay.Fire.performed += Shoot;
    }
    private void OnDisable()
    {
        _playerInput.Disable();
    }
    void Update()
    {
        if (_playerInput.GamePlay.Fire.triggered)
        {
            Shoot();
        }
        Vector3 shootDirection = Vector3.right; 
        
    }
    
    private void Shoot()
    {
        var bulletInst = Instantiate(bullet);
        bulletInst.SetActive(true);
        bulletInst.transform.position = transform.position;
        // bulletInst.AddComponent<BulletMove>().SetDirection(direction);
        Destroy(bulletInst, survivalTime);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            Debug.Log("Hit trap");
            // 触发重物掉落
            collision.gameObject.GetComponent<Trap>().Fall();
            Destroy(gameObject); // 销毁子弹
        }
    }
}
