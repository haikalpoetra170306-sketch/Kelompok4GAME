using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!EnemyHealth.guardDead)
            return;

        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("CutsceneLevel3");
        }
    }
}