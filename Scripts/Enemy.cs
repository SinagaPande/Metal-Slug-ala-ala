using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int health = 3;
    public float moveSpeed = 3f;
    public int damageToPlayer = 10;
    
    [Header("Cleanup")]
    // Jarak maksimal musuh dari player/kamera sebelum dihapus otomatis
    public float despawnDistance = 25f; 
    
    private Transform playerTarget;
    private Rigidbody2D rb;
    private EnemySpawner spawner;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; 
        
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) playerTarget = playerObj.transform;

        spawner = FindObjectOfType<EnemySpawner>();
    }

    void FixedUpdate()
    {
        // 1. Cek Target
        if (playerTarget == null) 
        {
            rb.velocity = Vector2.zero;
            return;
        }

        // 2. Logika Despawn (Pembersihan)
        // Kita gunakan jarak ke KAMERA (lebih akurat untuk infinite run) daripada ke Player
        float distanceToCam = Vector2.Distance(transform.position, Camera.main.transform.position);
        
        if (distanceToCam > despawnDistance)
        {
            Destroy(gameObject);
            return;
        }

        // 3. Logika Pergerakan (Mengejar Player)
        Vector2 direction = (playerTarget.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        // 4. Flip Wajah Musuh
        if (direction.x > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        else if (direction.x < 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damageToPlayer);
            }
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            // Memanggil EnemyDied yang sekarang SUDAH ADA di EnemySpawner
            if (spawner != null) spawner.EnemyDied();
            Destroy(gameObject);
        }
    }
}