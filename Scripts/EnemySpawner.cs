using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 3f;
    public Transform[] spawnPoints;
    public int maxEnemies = 5;
    
    private int currentEnemyCount = 0;
    
    void Start()
    {
        // Mulai spawn enemy setelah 2 detik, ulang setiap spawnInterval detik
        InvokeRepeating("SpawnEnemy", 2f, spawnInterval);
    }
    
    void SpawnEnemy()
    {
        // Cek jika sudah mencapai maksimal enemy
        if (currentEnemyCount >= maxEnemies) return;
        
        if (spawnPoints.Length == 0) 
        {
            Debug.LogWarning("No spawn points assigned!");
            return;
        }
        
        // Pilih spawn point random
        Transform randomPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        
        // Spawn enemy
        Instantiate(enemyPrefab, randomPoint.position, Quaternion.identity);
        
        currentEnemyCount++;
        
        Debug.Log($"Enemy spawned! Total: {currentEnemyCount}/{maxEnemies}");
    }
    
    // Method ini bisa dipanggil dari Enemy.cs saat enemy mati
    public void EnemyDied()
    {
        currentEnemyCount--;
        if (currentEnemyCount < 0) currentEnemyCount = 0;
    }
}