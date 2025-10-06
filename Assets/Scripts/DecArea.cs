using UnityEngine;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlacementArea : MonoBehaviour
{
    private SpriteRenderer sr;
    [HideInInspector] public bool isOccupied = false;

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false; // start hidden
    }

    // Show or hide the placement zone
    public void Show(bool show)
    {
        if (sr != null)
            sr.enabled = show; // always respect 'show', ignore isOccupied here
    }

    public bool IsInsideArea(Vector2 point)
    {
        Collider2D col = GetComponent<Collider2D>();
        return col != null && col.OverlapPoint(point) && !isOccupied;
    }
}
