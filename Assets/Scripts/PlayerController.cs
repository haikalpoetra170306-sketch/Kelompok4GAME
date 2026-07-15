using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float KecepatanJalan = 5f;
    public float KecepatanNaikTurun = 5f;

    public float batasAtas = 3f;
    public float batasBawah = -3f;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sr; 

    // Variabel status pedang
    private bool membawaPedang = false;
    private bool diDekatPedang = false;
    private GameObject pedangYangMauDiambil;

    // --- TAMBAHAN BARU ---
    [Header("Logika Pintu Level")]
    public GameObject PintuLevelSelanjutnya;
    // ---------------------

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Panggil komponen visual SATU KALI saja di awal
        AmbilKomponenVisual();
    }

    void Update()
    {
        // Jaga-jaga jika Level1Manager butuh waktu sepersekian detik untuk mengaktifkan visual Ninja
        if (sr == null || anim == null)
        {
            AmbilKomponenVisual();
            if (sr == null) return; // Jangan jalankan kode di bawah ini jika visual belum aktif
        }

        // 1. Logika Pergerakan
        float moveInput = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rb.linearVelocity = new Vector2(
            moveInput * KecepatanJalan,
            moveVertical * KecepatanNaikTurun
        );

        // 1b. Batas atas dan bawah
        Vector3 posisi = transform.position;
        posisi.y = Mathf.Clamp(posisi.y, batasBawah, batasAtas);
        transform.position = posisi;

        // 2. Logika Flip Karakter (Langsung pakai variabel 'sr' yang sudah disimpan)
        if (moveInput > 0) sr.flipX = false;
        else if (moveInput < 0) sr.flipX = true;

        // 3. Logika Mengambil Pedang
        if (diDekatPedang && Input.GetKeyDown(KeyCode.Return))
        {
            AmbilPedang();
        }

        // 4. Update Animasi
        anim.SetBool("isWalking", moveInput != 0 || moveVertical != 0);
        anim.SetBool("BawaPedang", membawaPedang);
    }

    // Fungsi untuk mencari Animator dan SpriteRenderer dari anak (VisualNinja) yang sedang aktif
    private void AmbilKomponenVisual()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        anim = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Senjata"))
        {
            diDekatPedang = true;
            pedangYangMauDiambil = collision.gameObject;
            Debug.Log("Berada di dekat pedang! Tekan ENTER untuk mengambil.");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Senjata"))
        {
            diDekatPedang = false;
            pedangYangMauDiambil = null;
            Debug.Log("Menjauh dari pedang.");
        }
    }

    // --- FUNGSI YANG DIUBAH ---
    public void AmbilPedang() // Ubah dari 'private' menjadi 'public' agar bisa diklik dari UI Button jika perlu
    {
        membawaPedang = true;
        diDekatPedang = false;

        if (pedangYangMauDiambil != null)
        {
            Destroy(pedangYangMauDiambil);
        }

        // --- TAMBAHAN BARU: Memunculkan Pintu ---
        if (PintuLevelSelanjutnya != null)
        {
            PintuLevelSelanjutnya.SetActive(true);
        }
        // ----------------------------------------

        Debug.Log("Pedang berhasil diambil! Animasi BawaPedang aktif.");
    }

    public bool SudahAmbilPedang()
    {
        return membawaPedang;
    }
}