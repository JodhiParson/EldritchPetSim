using UnityEngine;
using System.Collections;

public class BunnyMovement : MonoBehaviour
{
    public int moveSpeed = 30;
    public float reachThreshold = 0.1f; // how close before switching target
    public float stopTime = 10f;         // time to stay idle at each point

    // The 3 triangle points (set in Inspector)
    public Transform pointA; // Top corner
    public Transform pointB; // Left corner
    public Transform pointC; // Right corner

    private Transform[] points;
    private int currentIndex = 0;  // start at pointA
    private int direction = 1;     // 1 = clockwise, -1 = counter-clockwise

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool isStopped = true; // is the bunny currently stopped

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Put points into an array for easy cycling
        points = new Transform[] { pointA, pointB, pointC };

        // Start at first point (pointA)
        currentIndex = 0;
    }

    void Update()
    {
        if (points == null || points.Length < 3) return;

        if (!isStopped)
        {
            // Move toward target
            Transform target = points[currentIndex];
            Vector2 directionToTarget = (target.position - transform.position).normalized;
            // Debug.Log(directionToTarget);
            rb.linearVelocity = directionToTarget * moveSpeed;

            animator.SetBool("isMoving", true);

            // Flip only on X axis
            if (rb.linearVelocity.x > 0.01f) spriteRenderer.flipX = false;
            else if (rb.linearVelocity.x < -0.01f) spriteRenderer.flipX = true;

            // Check if bunny is close enough to "arrive"
            if (Vector2.Distance(transform.position, target.position) < reachThreshold)
            {
                StartCoroutine(StopAtPoint());
            }
        }
        else
        {
            // Stop movement while idle
            rb.linearVelocity = Vector2.zero;
            animator.SetBool("isMoving", false);
        }
    }

    void PickNextPoint()
    {
        // Randomly decide to go clockwise or counter-clockwise
        direction = (Random.value < 0.5f) ? 1 : -1;

        // Move to next index with wrap-around
        currentIndex = (currentIndex + direction + points.Length) % points.Length;
    }

    IEnumerator StopAtPoint()
    {
        isStopped = true;         // stop moving
        rb.linearVelocity = Vector2.zero;
        animator.SetBool("isMoving", false);

        yield return new WaitForSeconds(stopTime);

        PickNextPoint();          // choose next point
        isStopped = false;        // resume moving
    }
}
