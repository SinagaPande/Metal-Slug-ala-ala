using UnityEngine;
using UnityEngine.UI; // Jika pakai UI Slider

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    // Opsional: UI Slider reference
    // public Slider healthSlider; 

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Player hit! HP: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Died!");
        // Panggil GameManager jika ada
        if (GameManager.Instance != null) GameManager.Instance.PlayerDied();
        
        Destroy(gameObject);
    }
}