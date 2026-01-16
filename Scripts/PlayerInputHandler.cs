using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 8f;
    public float jumpForce = 15f;
    
    [Header("References")]
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.3f;

    [Header("Mobile Controls (Assign di Inspector)")]
    public MobileButton btnLeft;
    public MobileButton btnRight;
    public MobileButton btnJump;
    public MobileButton btnShoot;

    private Rigidbody2D rb;
    private ShootingController shooter;
    private bool isGrounded;
    private float facingDirection = 1f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shooter = GetComponent<ShootingController>();
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
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

        // 2. Input Jalan (Keyboard + Mobile)
        float xInput = Input.GetAxisRaw("Horizontal"); // Default Keyboard

        // Override jika tombol mobile ditekan
        if (btnLeft != null && btnLeft.isPressed) xInput = -1f;
        if (btnRight != null && btnRight.isPressed) xInput = 1f;

        // 3. Eksekusi Gerak
        rb.velocity = new Vector2(xInput * moveSpeed, rb.velocity.y);

        // 4. Flip Karakter
        if (xInput != 0)
        {
            facingDirection = Mathf.Sign(xInput);
            transform.localScale = new Vector3(facingDirection * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        // 5. Lompat (Keyboard Space/W + Mobile Jump)
        bool jumpInput = Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W);
        
        // Cek mobile jump (Kita pakai trik: jika isPressed dan sebelumnya di tanah, lompat sekali)
        // Namun untuk simplifikasi, kita cek isPressed biasa tapi dikunci logic ground
        if (btnJump != null && btnJump.isPressed)
        {
             // Agar tidak lompat terus menerus saat ditahan, logic ini bisa dikembangkan. 
             // Tapi untuk Metal Slug style biasanya tap.
             // Di sini kita anggap 'isPressed' sebagai trigger sesaat (akan loncat terus kalau ditahan, mirip kelinci).
             // Jika ingin sekali tekan, logic harus diubah sedikit. Untuk sekarang biarkan hold = lompat berulang (bunny hop).
             jumpInput = true;
        }

        // Agar tombol mobile jump responsif (sekali tap langsung naik), kita pakai Velocity Override
        if (jumpInput && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    void HandleShooting()
    {
        bool shootInput = Input.GetButton("Fire1");

        if (btnShoot != null && btnShoot.isPressed)
        {
            shootInput = true;
        }

        if (shootInput && shooter != null)
        {
            shooter.TryShoot(facingDirection);
        }
    }

    void OnDrawGizmos()
    {
        if (groundCheck != null)
        {
            Gizmos.color = isGrounded ? Color.green : Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}