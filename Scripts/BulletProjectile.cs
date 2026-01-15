using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private float speed;
    private int damage;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // Pastikan tidak jatuh
    }

    // Method ini WAJIB dipanggil saat Instantiate untuk mengisi data
    public void Initialize(float _speed, int _damage, float _lifeTime)
    {
        speed = _speed;
        damage = _damage;
        
        // Hancur otomatis setelah waktu habis
        Destroy(gameObject, _lifeTime);
        
        // Mulai gerak
        if (rb != null)
        {
            // Bergerak lurus sesuai arah hadap (lokal) object
            rb.velocity = transform.right * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        // Logika damage musuh
        Enemy enemy = hitInfo.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }

        // Logika tembok/tanah
        if (hitInfo.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}