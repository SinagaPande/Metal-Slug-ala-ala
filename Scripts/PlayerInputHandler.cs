using UnityEngine;
using UnityEngine.UI;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;
    public float jumpForce = 15f;
    
    [Header("References")]
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private ShootingController shooter;
    private bool isGrounded;
    
    // Menyimpan arah hadap terakhir (1 = Kanan, -1 = Kiri)
    private float facingDirection = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shooter = GetComponent<ShootingController>();
        
        // Setup Fisika Player (Metal Slug Style)
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb.gravityScale = 3f;
    }

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    void HandleMovement()
    {
        // 1. Cek Tanah
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // 2. Input
        float xInput = Input.GetAxisRaw("Horizontal");

        // 3. Gerak
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);

        // 4. Flip & Catat Arah
        if (xInput != 0)
        {
            facingDirection = Mathf.Sign(xInput);
            transform.localScale = new Vector3(facingDirection * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // 5. Lompat
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void HandleShooting()
    {
        // Deteksi Input Tembak (Klik Kiri)
        if (Input.GetButton("Fire1")) // Gunakan GetButton agar bisa auto-fire jika fireRate senjata cepat
        {
            if (shooter != null)
            {
                // Kirim intent menembak + arah hadap saat ini
                shooter.TryShoot(facingDirection);
            }
        }
    }
}