using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class PlayerController : MonoBehaviour
{
    [Header("角色属性")]
    public PlayerInput PlayerInput;
    public Vector2 inputDirection;
    public float speed = 5f;
    public float jumpForce = 16.5f;
    public GameObject bullet;
    public float survivalTime = 3;
    public Transform firePoint;
    public BulletManager bulletManager;
    public BulletClip bulletClip;
    public int counter = 0;
    public int bulletCounter = 0;
    
    public float dashSpeed = 20f; // 冲刺速度
    public float dashTime = 0.2f; // 冲刺持续时间

    private bool isDashing;
    private float dashTimeLeft;
    
    private Rigidbody2D rb;
    private PhysicsCheck _physicsCheck;
    public bool _facingRight = true; // 假设角色初始朝向右边
    public Vector2 shootDirection;
    
    public static event Action<int> OnBulletCountChanged;

    private void Awake()
    {
        Debug.Log("PlayerController: Awake");
        PlayerInput = new PlayerInput();
        rb = GetComponent<Rigidbody2D>();
        PlayerInput.GamePlay.Jump.performed += Jump;
        _physicsCheck = GetComponent<PhysicsCheck>();
        PlayerInput.GamePlay.Dash.performed += _ => StartDash();
    }

    private void Start()
    {
        bulletClip = BulletClip.Instance;
        bulletCounter = bulletClip.clipsSize - counter;
        UIManager.Instance.UpdateBulletIcon(counter);
        UIManager.Instance.UpdateBulletCount(bulletCounter);
    }

    private void OnEnable()
    {
        PlayerInput.Enable();
    }

    private void OnDisable()
    {
        PlayerInput.Disable();
    }
    
    public int BulletCount
    {
        get => bulletCounter;
        set
        {
            bulletCounter = value;
            // 当子弹数量发生变化时，触发事件
            OnBulletCountChanged?.Invoke(bulletCounter);
        }
    }
    
    private void Update()
    {
        inputDirection = PlayerInput.GamePlay.Move.ReadValue<Vector2>();
        if (PlayerInput.GamePlay.Fire.triggered)
        {
            Shoot();
        }
        if (isDashing)
        {
            Dash();
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!isDashing)
        {
            rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y);
            float faceDirection = inputDirection.x;
            if (inputDirection.x > 0 && !_facingRight || inputDirection.x < 0 && _facingRight)
            {
                Flip();
            }
        }

        // faceDirection = (float)(inputDirection.x > 0 ? 1.5 : -1.5);
        // Debug.Log(faceDirection);
        // transform.localScale = new Vector3(faceDirection, 1.5f, 1.5f);
        //
    }
    private void Flip()
    {
        _facingRight = !_facingRight;
        var transform1 = transform;
        Vector3 theScale = transform1.localScale;
        theScale.x *= -1; // 只翻转x轴
        transform1.localScale = theScale;
    }
    
    private void StartDash()
    {
        isDashing = true;
        dashTimeLeft = dashTime;
    }
    
    private void Dash()
    {
        if (dashTimeLeft > 0)
        {
            // 应用冲刺速度
            rb.velocity = new Vector2((_facingRight ? 1 : -1) * dashSpeed, rb.velocity.y);
            dashTimeLeft -= Time.deltaTime;
        }
        else
        {
            // 冲刺结束
            isDashing = false;
            rb.velocity = new Vector2(0, rb.velocity.y); // 可以根据需要调整
        }
    }
    
    private void Jump(InputAction.CallbackContext context)
    {
        if (_physicsCheck.isGround)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        }
        // Debug.Log("Jump");
    }
    
    private void Shoot()
    {
        if (inputDirection==Vector2.zero)
        {
            if (_facingRight)
            {
                shootDirection = Vector2.right;
            }
            else if (!_facingRight)
            {
                shootDirection = Vector2.left;
            }
        }
        else
        {
            if (Mathf.Abs(inputDirection.x)<1)
            {
                // 同时按下两个键，将向量长度标准化为1
                shootDirection = inputDirection / Mathf.Sqrt(2) * 2;
            }
            else
            {
                // 只按下了一个键，标准化
                shootDirection = inputDirection.normalized;
            }
        }
        var bulletPrefab = Instantiate(bulletClip.clips[counter++]);        
        if (BulletCount > 0)
        {
            BulletCount--; // 这会自动触发事件
        }
        
        UIManager.Instance.UpdateBulletIcon(counter);
        // UIManager.Instance.UpdateBulletCount(bulletCounter--);
        bulletPrefab.SetActive(true);
        bulletPrefab.transform.position = firePoint.position;
        bulletPrefab.GetComponent<IBullet>().Initialize(shootDirection);
        Destroy(bulletPrefab,survivalTime);
        

        // var bulletPrefab = Instantiate(bulletManager.weaponBag[Random.Range(0, bulletManager.weaponBagSize)]); // 随机生成子弹
        // bulletPrefab.SetActive(true);
        // bulletPrefab.transform.position = firePoint.position;
        // bulletPrefab.AddComponent<BulletMove>().SetDirection(shootDirection);
        // Destroy(bulletPrefab, survivalTime);

        // var bulletInst = Instantiate(bullet);
        // bulletInst.SetActive(true);
        // bulletInst.transform.position = firePoint.position;
        // bulletInst.AddComponent<BulletMove>().SetDirection(shootDirection);
        // Destroy(bulletInst, survivalTime);
    }
    
    // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     if (collision.gameObject.CompareTag("Trap"))
    //     {
    //         Debug.Log("Player hit trap");
    //         // 触发重物掉落
    //         
    //     }
    //
    //     if (collision.gameObject.CompareTag("Lava"))
    //     {
    //         Time.timeScale = 0;
    //         Die();
    //     }
    // }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Lava"))
        {
            Time.timeScale = 0;
            Die();
        }
    }

    private void Die()
    {

        SceneManager.LoadScene("Dead");
    }
    
    void OnDestroy()
    {
        if (UIManager.Instance != null)
        {
            OnBulletCountChanged -= UIManager.Instance.UpdateBulletCount;
        }
    }
}
