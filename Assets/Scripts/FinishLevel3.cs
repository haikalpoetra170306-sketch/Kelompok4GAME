using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLevel3 : MonoBehaviour
{
    public string NamaSceneBerikutnya = "Level4";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(NamaSceneBerikutnya);
        }
    }
}