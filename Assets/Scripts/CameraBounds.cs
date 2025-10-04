using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public float wallThickness = 1f; // thickness of the invisible walls

    void Start()
    {
        CreateWalls();
    }

    void CreateWalls()
    {
        Camera cam = Camera.main;
        float height = cam.orthographicSize * 2f;
        float width = height * cam.aspect;

        // Top Wall
        CreateWall("TopWall", new Vector2(0, height / 2f + wallThickness / 2f), new Vector2(width + 2*wallThickness, wallThickness));

        // Bottom Wall
        CreateWall("BottomWall", new Vector2(0, -height / 2f - wallThickness / 2f), new Vector2(width + 2*wallThickness, wallThickness));

        // Left Wall
        CreateWall("LeftWall", new Vector2(-width / 2f - wallThickness / 2f, 0), new Vector2(wallThickness, height + 2*wallThickness));

        // Right Wall
        CreateWall("RightWall", new Vector2(width / 2f + wallThickness / 2f, 0), new Vector2(wallThickness, height + 2*wallThickness));
    }

    void CreateWall(string name, Vector2 position, Vector2 scale)
    {
        GameObject wall = new GameObject(name);
        wall.transform.position = position;
        wall.transform.localScale = scale;

        BoxCollider2D collider = wall.AddComponent<BoxCollider2D>();
        collider.isTrigger = false; // set to true if you want trigger instead of solid

        // Optional: make wall invisible (no renderer)
    }
}
