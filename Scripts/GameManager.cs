using UnityEngine;
using UnityEngine.UI; // Wajib untuk UI
using UnityEngine.SceneManagement; // Wajib untuk Restart

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [Header("Game Settings")]
    public int score = 0;
    public bool isGameOver = false;

    [Header("UI References")]
    public Text scoreText;
    public GameObject gameOverPanel; // Panel merah yang kita buat tadi

    void Awake()
    {
        // Singleton pattern sederhana
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddScore(int points)
    {
        if (isGameOver) return;

        score += points;
        
        // Update UI
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
    
    public void PlayerDied()
    {
        if (isGameOver) return;
        
        isGameOver = true;
        Debug.Log("GAME OVER!");
        
        // Munculkan Panel Game Over
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        // Restart game otomatis setelah 2 detik
        Invoke("RestartGame", 2f);
    }

    void RestartGame()
    {
        // Reload scene saat ini
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}