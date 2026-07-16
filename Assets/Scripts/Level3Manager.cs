using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class Level3Manager : MonoBehaviour
{
    [Header("Panel Cutscene")]
    public GameObject PanelCutscene;
    public TMP_Text TeksCerita;
    public TMP_Text TeksHalaman;

    [Header("Tombol")]
    public GameObject TombolLanjut;

    [Header("Background")]
    public GameObject BackgroundCutscene;
    public GameObject BackgroundLevel3;

    [Header("Gameplay")]
    public GameObject[] VisualKarakter;
    public GameObject[] BatuBergerak;

    [Header("Scene")]
    public string NamaSceneBerikutnya = "Level4";

    [Header("Efek Teks")]
    public float KecepatanNgetik = 0.03f;

    private string[] isiCerita = {
        "Sang pahlawan kini berada di lorong kuno bawah kastil.\nSuasana gelap dan mencekam menyelimuti setiap sudut ruangan.",
        "Lorong ini sudah berusia ratusan tahun.\nBerbagai jebakan berbahaya dipasang untuk menghentikan para penyusup.",
        "Tiba-tiba terdengar suara gemuruh dari kejauhan.\nBatu-batu besar mulai menggelinding dengan cepat ke arahnya!",
        "Tidak ada jalan lain menuju ruang tahanan sang putri.\nSang pahlawan harus melewati semua rintangan ini.",
        "Dengan tekad membaja dan keberanian tak tergoyahkan...\nSang pahlawan melangkah maju menembus lorong jebakan!"
    };

    private int indexCerita = 0;
    private int indexKarakterAktif = 0;
    private bool sedangNgetik = false;
    private Coroutine coroutineNgetik;

    void Start()
    {
        // Ambil pilihan karakter dari Level 1
        indexKarakterAktif = PlayerPrefs.GetInt("indexKarakterTerpilih", 0);

        // Sembunyikan SEMUA dulu
        foreach (GameObject v in VisualKarakter) v.SetActive(false);
        foreach (GameObject b in BatuBergerak) b.SetActive(false);

        // Sembunyikan bg level3, tampilkan bg cutscene
        if (BackgroundCutscene != null) BackgroundCutscene.SetActive(true);
        if (BackgroundLevel3 != null) BackgroundLevel3.SetActive(false);

        // Tampilkan panel cutscene
        PanelCutscene.SetActive(true);
        TampilkanCerita(0);
    }

    void TampilkanCerita(int index)
    {
        TeksHalaman.text = (index + 1) + " / " + isiCerita.Length;
        if (coroutineNgetik != null) StopCoroutine(coroutineNgetik);
        coroutineNgetik = StartCoroutine(KetikTeks(isiCerita[index]));
    }

    public void TombolLanjutDitekan()
    {
        if (sedangNgetik)
        {
            StopCoroutine(coroutineNgetik);
            TeksCerita.text = isiCerita[indexCerita];
            sedangNgetik = false;
        }
        else
        {
            indexCerita++;
            if (indexCerita >= isiCerita.Length)
                MulaiGameplay();
            else
                TampilkanCerita(indexCerita);
        }
    }

    void MulaiGameplay()
    {
        // Sembunyikan cutscene
        PanelCutscene.SetActive(false);

        // Ganti background
        if (BackgroundCutscene != null) BackgroundCutscene.SetActive(false);
        if (BackgroundLevel3 != null) BackgroundLevel3.SetActive(true);

        // Aktifkan karakter SETELAH cutscene selesai
        if (indexKarakterAktif < VisualKarakter.Length)
            VisualKarakter[indexKarakterAktif].SetActive(true);

        // Aktifkan batu bergerak
        foreach (GameObject b in BatuBergerak) b.SetActive(true);
    }

    IEnumerator KetikTeks(string kalimat)
    {
        sedangNgetik = true;
        TeksCerita.text = "";
        foreach (char c in kalimat.ToCharArray())
        {
            TeksCerita.text += c;
            yield return new WaitForSeconds(KecepatanNgetik);
        }
        sedangNgetik = false;
    }

    public void MasukLevelBerikutnya()
    {
        SceneManager.LoadScene(NamaSceneBerikutnya);
    }
}