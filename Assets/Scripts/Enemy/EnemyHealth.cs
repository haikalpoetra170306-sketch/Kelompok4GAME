using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    // Mengecek apakah guard sudah mati
    public static bool guardDead = false;

    private Animator anim;
    private GuardPatrol patrol;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;

    private bool isDead = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        patrol = GetComponent<GuardPatrol>();
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void Die()
    {
        // Supaya tidak mati berkali-kali
        if (isDead)
            return;

        isDead = true;

        // Memberi tahu bahwa guard sudah mati
        guardDead = true;

        // Menghentikan patrol
        if (patrol != null)
            patrol.enabled = false;

        // Memainkan animasi mati
        if (anim != null)
            anim.SetTrigger("Die");

        // Player bisa melewati guard
        if (boxCollider != null)
            boxCollider.enabled = false;

        // Menghentikan physics
        if (rb != null)
            rb.simulated = false;
    }
}