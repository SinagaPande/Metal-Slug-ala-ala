using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;
    
    [Header("Settings")]
    public float smoothTime = 0.3f;      // Kehalusan pergerakan (0.1 = cepat, 0.5 = lambat)
    public Vector3 offset = new Vector3(0, 1f, -10f); // Posisi default relatif player
    
    [Header("Look Ahead Effect")]
    public float lookAheadDstX = 3f;     // Seberapa jauh kamera mengintip ke depan
    public float lookSmoothTime = 0.5f;  // Kehalusan transisi look ahead
    
    private float currentLookAheadX;
    private float targetLookAheadX;
    private float lookAheadVel;
    
    private Vector3 currentVelocity;
    
    void LateUpdate()
    {
        if (target == null) return;

        // 1. Deteksi arah gerak player (berdasarkan input keyboard atau velocity)
        // Kita gunakan posisi x target relatif terhadap localScale player (karena player anda di-flip scale-nya)
        float xMoveDelta = target.localScale.x; 
        
        // Tentukan target look ahead (kanan atau kiri)
        // Jika player diam, logic ini mungkin perlu disesuaikan dengan input, 
        // tapi asumsi player selalu menghadap arah terakhir:
        bool isMoving = Mathf.Abs(target.GetComponent<Rigidbody2D>().velocity.x) > 0.1f;
        
        if (isMoving)
        {
            // Jika scale.x positif (hadap kanan) -> lookAhead positif
            // Jika scale.x negatif (hadap kiri) -> lookAhead negatif
            targetLookAheadX = Mathf.Sign(xMoveDelta) * lookAheadDstX;
        }
        else
        {
            targetLookAheadX = 0f; // Kembali ke tengah saat diam
        }

        // 2. Smooth transisi look ahead
        currentLookAheadX = Mathf.SmoothDamp(currentLookAheadX, targetLookAheadX, ref lookAheadVel, lookSmoothTime);

        // 3. Hitung posisi akhir
        // Posisi Target + Offset dasar + Efek Look Ahead
        Vector3 targetPos = new Vector3(target.position.x + offset.x + currentLookAheadX, target.position.y + offset.y, offset.z);

        // 4. Gerakkan Kamera
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, smoothTime);
    }
}