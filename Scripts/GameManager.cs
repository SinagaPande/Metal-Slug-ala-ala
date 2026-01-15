using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public int playerLives = 3;
    public int score = 0;
    
    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void AddScore(int points)
    {
        score += points;
        Debug.Log($"Score: {score}");
    }
    
    public void PlayerDied()
    {
        playerLives--;
        Debug.Log($"Lives remaining: {playerLives}");
        
        if (playerLives <= 0)
        {
            GameOver();
        }
        else
        {
            // Respawn player
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
    
    void GameOver()
    {
        Debug.Log("GAME OVER!");
        // Tampilkan UI game over
        // SceneManager.LoadScene("GameOverScene");
    }
}