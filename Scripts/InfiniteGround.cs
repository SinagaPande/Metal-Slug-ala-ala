using UnityEngine;

public class InfiniteGround : MonoBehaviour
{
    private float spriteWidth;
    private Transform camTransform;

    void Start()
    {
        camTransform = Camera.main.transform;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        spriteWidth = sr.bounds.size.x;
    }

    void Update()
    {
        // Cek jarak horizontal antara ground ini dengan kamera
        float distance = camTransform.position.x - transform.position.x;

        // Jika kamera sudah melewati ground ini sejauh lebar sprite (ditambah sedikit buffer),
        // Pindahkan ground ini ke depan
        if (Mathf.Abs(distance) > spriteWidth)
        {
            // Arah pergerakan kamera (kanan atau kiri)
            float direction = Mathf.Sign(distance);
            
            // Reposisi: Pindahkan sejauh (Lebar * 3) ke arah tersebut
            // Angka 3 karena kita punya 3 ground (Left, Center, Right)
            transform.position += new Vector3(spriteWidth * 3 * direction, 0, 0);
        }
    }
}