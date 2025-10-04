using UnityEngine;

public class CarrotArc : MonoBehaviour
{
    public Transform target;       // The bunny
    public float height = 2f;      // How high the arc goes
    public float duration = 1f;    // How long the carrot takes to reach the bunny

    public void LaunchTowards(Transform bunny)
    {
        target = bunny;
        StartCoroutine(ArcMovement());
    }

    private System.Collections.IEnumerator ArcMovement()
    {
        Vector3 startPos = transform.position;
        Vector3 endPos = target.position;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            // Parabolic arc formula
            float yOffset = height * 4 * t * (1 - t);
            transform.position = Vector3.Lerp(startPos, endPos, t) + new Vector3(0, yOffset, 0);

            yield return null;
        }

        // Snap to target and destroy carrot (or disable)
        transform.position = endPos;
        Destroy(gameObject);
        // Later you can trigger the bunny eating animation here
        // target.GetComponent<Bunny>().Eat(); 
    }
}
