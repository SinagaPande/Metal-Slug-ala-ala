using UnityEngine;

public class WeaponConfig : MonoBehaviour
{
    [Header("Stats Senjata")]
    public GameObject bulletPrefab;
    public int damage = 10;
    public float bulletSpeed = 15f;
    public float fireRate = 0.2f; // Jeda antar tembakan (detik)
    public float bulletLifeTime = 2f;

    // Referensi internal untuk lokasi spawn
    [HideInInspector] public Transform firePoint;

    void Awake()
    {
        // Mencari child bernama "Fire Point" di dalam object Rifle ini
        firePoint = transform.Find("Fire Point");

        if (firePoint == null)
        {
            Debug.LogError($"[WeaponConfig] CRITICAL: 'Fire Point' tidak ditemukan di dalam {gameObject.name}!");
        }
    }
}