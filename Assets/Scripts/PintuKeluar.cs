using UnityEngine;
using UnityEngine.SceneManagement; // Wajib ditambahkan untuk memanggil fungsi pindah Scene

public class PintuKeluar : MonoBehaviour
{
    [Header("Pengaturan Pindah Level")]
    [Tooltip("Ketik nama Scene tujuan persis seperti di Build Settings (contoh: Level2)")]
    public string namaSceneTujuan;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Mengecek apakah yang menyentuh pintu adalah objek dengan Tag "Player"
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Pindah ke scene: " + namaSceneTujuan);
            SceneManager.LoadScene(namaSceneTujuan);
        }
    }
}