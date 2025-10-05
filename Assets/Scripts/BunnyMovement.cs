using UnityEngine;
using System.Collections;

public class BunnyMovement : MonoBehaviour
{
    [Header("Wander Area")]
    public float wanderWidth = 9;
    public float wanderHeight = 2;
    public Vector2 startingPosition;

    [Header("Movement Settings")]
    public float speed = 4;
    public float stopTime = 4f; // seconds stopped after hitting wall

    private Vector2 target;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isStopped = false;

    private void Awake()
{
    rb = GetComponent<Rigidbody2D>();
    animator = GetComponent<Animator>();

    // Use initial spawn position as center of wander area
    if (startingPosition == Vector2.zero)
        startingPosition = transform.position;
}

    private Vector3 baseScale;

    private void Start()
    {
        baseScale = transform.localScale; // whatever it was set to in the scene, e.g. (500,500,1)
    }

    private void Update()
    {
    if (isStopped) return;

    // Move toward target
    Vector2 direction = (target - (Vector2)transform.position).normalized;
    rb.linearVelocity = direction * speed;

    // Animation state
    animator.SetBool("isMoving", true);

    // Flip sprite
    if (rb.linearVelocity.x > 0.01f)
        transform.localScale = new Vector3(-Mathf.Abs(baseScale.x), baseScale.y, baseScale.z);
    else if (rb.linearVelocity.x < -0.01f)
        transform.localScale = new Vector3(Mathf.Abs(baseScale.x), baseScale.y, baseScale.z);

    // If reached target, stop and idle
    if (Vector2.Distance(transform.position, target) < 0.1f)
    {
        Debug.Log("Reached target. Stopping to idle...");
        StartCoroutine(StopAtTarget());
    }
}

    // private bool IsInsideBounds(Vector2 pos)
    // {
    //     float halfWidth = wanderWidth / 2f;
    //     float halfHeight = wanderHeight / 2f;

    //     bool inside = pos.x >= startingPosition.x - halfWidth &&
    //                   pos.x <= startingPosition.x + halfWidth &&
    //                   pos.y >= startingPosition.y - halfHeight &&
    //                   pos.y <= startingPosition.y + halfHeight;

    //     if (!inside)
    //         Debug.Log("Position " + pos + " is OUTSIDE bounds.");

    //     return inside;
    // }

    private void OnEnable()
    {
        target = GetRandomTarget();
        Debug.Log("Bunny spawned. First target: " + target);
    }

    private Vector2 GetRandomTarget()
    {
        float halfWidth = wanderWidth / 2;
        float halfHeight = wanderHeight / 2;
        int edge = Random.Range(0, 4);

        Vector2 newTarget = edge switch
        {
            0 => new Vector2(startingPosition.x - halfWidth, Random.Range(startingPosition.y - halfHeight, startingPosition.y + halfHeight)), // left
            1 => new Vector2(startingPosition.x + halfWidth, Random.Range(startingPosition.y - halfHeight, startingPosition.y + halfHeight)), // right
            2 => new Vector2(Random.Range(startingPosition.x - halfWidth, startingPosition.x + halfWidth), startingPosition.y - halfHeight), // bottom
            _ => new Vector2(Random.Range(startingPosition.x - halfWidth, startingPosition.x + halfWidth), startingPosition.y + halfHeight), // top
        };

        Debug.Log("Generated new target: " + newTarget);
        return newTarget;
    }

    // Trigger idle immediately on collision
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isStopped) return; // already stopped

        Debug.Log("Collision detected with " + collision.gameObject.name + ". Stopping bunny.");
        StartCoroutine(StopAtTarget());
    }

    private IEnumerator StopAtTarget()
{
    isStopped = true;

        rb.linearVelocity = Vector2.zero;
    animator.SetBool("isMoving", false);

    // Play one of three idle animations
    int idleIndex = Random.Range(0, 3);
    animator.SetInteger("idleIndex", idleIndex);
    Debug.Log("Idle animation at target: Idle" + idleIndex);

    // Wait for a few seconds
    yield return new WaitForSeconds(stopTime);

    // Resume movement
    target = GetRandomTarget();
    animator.SetInteger("idleIndex", -1); // reset
    isStopped = false;
    Debug.Log("Resuming movement. New target: " + target);
}

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(startingPosition, new Vector3(wanderWidth, wanderHeight, 0));
    }
}



    // public int moveSpeed = 30;
    // public float reachThreshold = 0.1f; // how close before switching target
    // public float stopTime = 10f;         // time to stay idle at each point

    // // The 3 triangle points (set in Inspector)
    // public Transform pointA; // Top corner
    // public Transform pointB; // Left corner
    // public Transform pointC; // Right corner

    // private Transform[] points;
    // private int currentIndex = 0;  // start at pointA
    // private int direction = 1;     // 1 = clockwise, -1 = counter-clockwise

    // private Rigidbody2D rb;
    // private Animator animator;
    // private SpriteRenderer spriteRenderer;

    // private bool isStopped = true; // is the bunny currently stopped

    // void Start()
    // {
    //     rb = GetComponent<Rigidbody2D>();
    //     animator = GetComponent<Animator>();
    //     spriteRenderer = GetComponent<SpriteRenderer>();

    //     // Put points into an array for easy cycling
    //     points = new Transform[] { pointA, pointB, pointC };

    //     // Start at first point (pointA)
    //     currentIndex = 0;
    // }

    // void Update()
    // {
    //     if (points == null || points.Length < 3) return;

    //     if (!isStopped)
    //     {
    //         // Move toward target
    //         Transform target = points[currentIndex];
    //         Vector2 directionToTarget = (target.position - transform.position).normalized;
    //         // Debug.Log(directionToTarget);
    //         rb.linearVelocity = directionToTarget * moveSpeed;

    //         animator.SetBool("isMoving", true);

    //         // Flip only on X axis
    //         if (rb.linearVelocity.x > 0.01f) spriteRenderer.flipX = false;
    //         else if (rb.linearVelocity.x < -0.01f) spriteRenderer.flipX = true;

    //         // Check if bunny is close enough to "arrive"
    //         if (Vector2.Distance(transform.position, target.position) < reachThreshold)
    //         {
    //             StartCoroutine(StopAtPoint());
    //         }
    //     }
    //     else
    //     {
    //         // Stop movement while idle
    //         rb.linearVelocity = Vector2.zero;
    //         animator.SetBool("isMoving", false);
    //     }
    // }

    // void PickNextPoint()
    // {
    //     // Randomly decide to go clockwise or counter-clockwise
    //     direction = (Random.value < 0.5f) ? 1 : -1;

    //     // Move to next index with wrap-around
    //     currentIndex = (currentIndex + direction + points.Length) % points.Length;
    // }

    // IEnumerator StopAtPoint()
    // {
    //     isStopped = true;         // stop moving
    //     rb.linearVelocity = Vector2.zero;
    //     animator.SetBool("isMoving", false);

    //     yield return new WaitForSeconds(stopTime);

    //     PickNextPoint();          // choose next point
    //     isStopped = false;        // resume moving
    // }
