using UnityEngine;

public class GuardPatrol : MonoBehaviour
{
    public float speed = 2f;
    public float leftLimit = -4f;
    public float rightLimit = 4f;

    private bool movingRight = true;
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);

            if (transform.position.x >= rightLimit)
            {
                movingRight = false;
                sr.flipX = true;
            }
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);

            if (transform.position.x <= leftLimit)
            {
                movingRight = true;
                sr.flipX = false;
            }
        }
    }
}