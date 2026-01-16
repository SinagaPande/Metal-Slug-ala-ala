using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        // Jangan kurangi darah jika sudah Game Over
        if (GameManager.Instance != null && GameManager.Instance.isGameOver) return;

        currentHealth -= damage;
        // Debug.Log($"Player HP: {currentHealth}"); // Optional

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Panggil GameManager
        if (GameManager.Instance != null) 
        {
            GameManager.Instance.PlayerDied();
        }
        
        // Hilangkan player dari layar (tapi jangan Destroy object agar script tidak error, cukup matikan render)
        // Atau Destroy dan biarkan kamera diam. Kita pilih Destroy untuk simple.
        Destroy(gameObject); 
    }
}