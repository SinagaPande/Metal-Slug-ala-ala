using UnityEngine;

public class ShootingController : MonoBehaviour
{
    private WeaponConfig currentWeapon;
    private float nextFireTime = 0f;

    void Start()
    {
        // Otomatis mengambil senjata yang dipegang (ada di child)
        currentWeapon = GetComponentInChildren<WeaponConfig>();

        if (currentWeapon == null)
        {
            Debug.LogError("[ShootingController] Player tidak memegang senjata (WeaponConfig tidak ditemukan di anak)!");
        }
    }

    // Method ini dipanggil oleh PlayerInputHandler
    public void TryShoot(float directionSign)
    {
        // Validasi: Ada senjata? Cooldown selesai?
        if (currentWeapon == null || Time.time < nextFireTime) return;

        // Validasi: Fire Point ada?
        if (currentWeapon.firePoint == null) return;

        ExecuteShoot(directionSign);
        
        // Set cooldown
        nextFireTime = Time.time + currentWeapon.fireRate;
    }

    private void ExecuteShoot(float directionSign)
    {
        // 1. Spawn Peluru di Fire Point
        GameObject bullet = Instantiate(currentWeapon.bulletPrefab, currentWeapon.firePoint.position, Quaternion.identity);

        // 2. Atur Visual (Flip) sesuai arah player
        // Jika player hadap kiri (-1), kita putar peluru 180 derajat agar transform.right-nya menunjuk ke kiri
        if (directionSign < 0)
        {
            bullet.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            bullet.transform.rotation = Quaternion.identity;
        }

        // 3. INJECT DATA (Penting!)
        BulletProjectile projectile = bullet.GetComponent<BulletProjectile>();
        if (projectile != null)
        {
            projectile.Initialize(currentWeapon.bulletSpeed, currentWeapon.damage, currentWeapon.bulletLifeTime);
        }
    }
}