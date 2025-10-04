using UnityEngine;

public class BunnyMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float changeDirectionTime = 2f;
    public float stopChance = 0.2f;        // 20% chance to stop when picking new direction
    public float minStopTime = 1f;
    public float maxStopTime = 3f;

    private Vector2 moveDirection;
    private float timer;
    private bool isStopped = false;
    private float stopTimer;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        PickNewDirection();
    }

    void Update()
    {
        if (isStopped)
        {
            // Bunny is standing
            stopTimer -= Time.deltaTime;
            if (stopTimer <= 0f)
            {
                isStopped = false;
                PickNewDirection();
            }

            animator.SetBool("isMoving", false);
        }
        else
        {
            // Move the bunny
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

            // Flip sprite based on horizontal direction
            if (moveDirection.x > 0.01f) spriteRenderer.flipX = false;
            else if (moveDirection.x < -0.01f) spriteRenderer.flipX = true;

            // Update Animator
            animator.SetBool("isMoving", moveDirection.magnitude > 0.01f);

            // Timer for changing direction
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                PickNewDirection();
            }
        }
    }

    void PickNewDirection()
    {
        // Randomly decide if bunny should stop
        if (Random.value < stopChance)
        {
            isStopped = true;
            stopTimer = Random.Range(minStopTime, maxStopTime);
            moveDirection = Vector2.zero;
            return;
        }

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
