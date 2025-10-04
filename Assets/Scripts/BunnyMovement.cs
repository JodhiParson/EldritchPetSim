using UnityEngine;

public class BunnyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float changeDirectionTime = 2f;

    private Vector2 moveDirection;
    private float timer;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        PickNewDirection();
    }

    void Update()
    {
        // Move the bunny
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

        // Update Animator
        if (moveDirection.magnitude > 0.01f)
            animator.SetBool("isMoving", true);
        else
            animator.SetBool("isMoving", false);

        // Timer for changing direction
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            PickNewDirection();
        }
    }

    void PickNewDirection()
    {
        float x = Random.Range(-1f, 1f);
        float y = Random.Range(-1f, 1f);
        moveDirection = new Vector2(x, y).normalized;
        timer = changeDirectionTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        moveDirection = Vector2.Reflect(moveDirection, collision.contacts[0].normal);
    }
}
