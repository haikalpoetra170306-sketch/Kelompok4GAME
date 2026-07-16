using UnityEngine;
using UnityEngine.SceneManagement;

public class BatuBergerak : MonoBehaviour
{
    [Header("Pengaturan")]
    public float Kecepatan = 3f;
    public float BatasKiri = -12f;
    public float PosisiAwalX = 10f;

    private float posisiAwalY;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        posisiAwalY = transform.position.y;
        transform.position = new Vector3(PosisiAwalX, posisiAwalY, 0);
    }

    void FixedUpdate()
    {
        // Gelinding ke kiri terus
        rb.linearVelocity = new Vector2(-Kecepatan, rb.linearVelocity.y);

        // Reset ke posisi awal jika keluar layar
        if (transform.position.x < BatasKiri)
        {
            transform.position = new Vector3(PosisiAwalX, posisiAwalY, 0);
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}