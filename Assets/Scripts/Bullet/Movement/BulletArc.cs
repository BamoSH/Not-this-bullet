using UnityEngine;

public class BulletArc : MonoBehaviour, IBullet
{
    public float launchSpeed = 5.0f; 

    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Initialize(Vector2 direction)
    {
        rb.velocity = direction.normalized * launchSpeed;
    }

    public bool IsBiggerBullet { get; set; }
}
