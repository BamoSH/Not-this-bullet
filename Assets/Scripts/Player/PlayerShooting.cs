using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    private int _damage;
    protected Vector2 direction;
    private PlayerInput _playerInput;
    private Camera _mainCamera;
    public GameObject bullet;
    public float survivalTime = 2;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _mainCamera = Camera.main;
        direction = Direction();
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

    private void Update()
    {
        if (_playerInput.GamePlay.Fire.triggered)
        {
            Shoot();
        }
        
    }

    protected virtual Vector3 Direction()
    {
        Vector2 mousePos = _playerInput.GamePlay.Look.ReadValue<Vector2>();
        Vector3 mouseWorldPos = _mainCamera.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, _mainCamera.nearClipPlane));
        mouseWorldPos.z = 0;
        Vector3 direction = (mouseWorldPos - transform.position).normalized;
        return direction;
    }
    
    private void Shoot()
    {
        direction = Direction();
        var bulletInst = Instantiate(bullet);
        bulletInst.SetActive(true);
        bulletInst.transform.position = transform.position;
        // bulletInst.AddComponent<BulletMove>().SetDirection(direction);
        Destroy(bulletInst, survivalTime);
    }
}
