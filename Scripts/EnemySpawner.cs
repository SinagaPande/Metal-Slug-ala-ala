using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2.5f;
    
    [Header("Spawn Settings")]
    public float spawnDistanceX = 15f; 
    public float spawnY = -2.5f;       
    
    private float nextSpawnTime;
    private Transform camTransform;

    void Start()
    {
        if (Camera.main != null)
            camTransform = Camera.main.transform;
    }

    void Update()
    {
        // Jangan spawn jika game over
        if (GameManager.Instance != null && GameManager.Instance.isGameOver) return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnDynamic();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    void SpawnDynamic()
    {
        if (camTransform == null) return;

        float direction = (Random.value > 0.5f) ? 1f : -1f;
        Vector3 spawnPos = new Vector3(
            camTransform.position.x + (direction * spawnDistanceX), 
            spawnY, 
            0
        );

        Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
    }

    public void EnemyDied()
    {
        // Tambah skor 10 poin
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(10);
        }
    }
}