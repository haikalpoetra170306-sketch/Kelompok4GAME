using UnityEngine;

public class TambahanLompat : MonoBehaviour
{
    [Header("Lompat")]
    public float KekuatanLompat = 8f;
    public Transform TitikCekTanah;
    public float RadiusCek = 0.3f;
    public LayerMask LayerTanah;

    private Rigidbody2D rb;
    private bool diTanah;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Cek apakah di tanah
        if (TitikCekTanah != null)
            diTanah = Physics2D.OverlapCircle(
                TitikCekTanah.position,
                RadiusCek,
                LayerTanah
            );

        // Lompat dengan Space
        if (Input.GetKeyDown(KeyCode.Space) && diTanah)
            rb.linearVelocity = new Vector2(
                rb.linearVelocity.x,
                KekuatanLompat
            );
    }
}