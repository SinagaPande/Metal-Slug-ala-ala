using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    public int health = 3;
    public float moveSpeed = 3f;
    public int damageToPlayer = 10;
    
    private Transform playerTarget;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // --- FIX TUMBANG ---
        rb.constraints = RigidbodyConstraints2D.FreezeRotation; 
        
        // Cari player sekali saja di awal
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null) playerTarget = playerObj.transform;
    }

    void FixedUpdate()
    {
        // Jika player sudah mati/hilang, diam saja
        if (playerTarget == null) 
        {
            rb.velocity = Vector2.zero;
            return;
        }

        // 1. Tentukan Arah ke Player
        Vector2 direction = (playerTarget.position - transform.position).normalized;
        
        // 2. Gerak hanya di sumbu X (Biar musuh tidak terbang miring ke atas)
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y);

        // 3. Flip Wajah Musuh
        if (direction.x > 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        else if (direction.x < 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
    }

    // Jika bersentuhan dengan Player -> Player kena damage
// Jika bersentuhan dengan Player -> Player kena damage
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // PERUBAHAN DI SINI: Ganti PlayerController menjadi PlayerHealth
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
            // Beritahu Spawner kalau musuh mati (jika ada script Spawner)
            EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
            if (spawner != null) spawner.EnemyDied();
            
            Destroy(gameObject);
        }
    }
}