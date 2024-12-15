using UnityEngine;

public class BulletArc : MonoBehaviour, IBullet
{
    public float launchSpeed = 5.0f; // 发射速度

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 direction)
    {
        // 使用传入的方向和预设的发射速度来设置刚体的速度
        rb.velocity = direction.normalized * launchSpeed;
    }

    public bool IsBiggerBullet { get; set; }
}
