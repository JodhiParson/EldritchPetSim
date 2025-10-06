using UnityEngine;
using UnityEngine.EventSystems;

public class DecorationPlacer : MonoBehaviour
{
    [Header("References")]
    public Camera mainCamera;
    public Inventory playerInventory;
    public InventoryUI inventoryUI;

    private GameObject currentDecoration;
    private PlacementArea[] placementZones;
    private bool isEditMode = false;
    private ItemData selectedItem;

    void Start()
    {
        // Get all placement zones in the scene
           placementZones = GameObject.FindObjectsByType<PlacementArea>(FindObjectsSortMode.None);
    }

    void Update()
    {
        if (!isEditMode || currentDecoration == null) return;

        FollowMouse();

        // Left-click placement
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorld, Vector2.zero);

            if (hit.collider != null)
            {
                PlacementArea zone = hit.collider.GetComponent<PlacementArea>();
                if (zone != null && !zone.isOccupied)
                    PlaceDecorationAtZone(zone);
            }
        }

        // Right-click cancels placement
        if (Input.GetMouseButtonDown(1))
            CancelPlacement();
    }

    public void EnterEditMode(GameObject decorationPrefab, ItemData item)
    {
        if (decorationPrefab == null || item == null) return;

        currentDecoration = Instantiate(decorationPrefab);
        selectedItem = item;
        isEditMode = true;

        UpdatePlacementZones();
    }

    private void UpdatePlacementZones()
    {
        if (placementZones == null) return;

        foreach (var zone in placementZones)
        {
            // Show zones only if edit mode is active and zone is free
            zone.Show(isEditMode && !zone.isOccupied);
        }
    }

    private void FollowMouse()
    {
        if (currentDecoration == null || selectedItem == null) return;

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(mousePos);

        // Apply optional offset from ItemData
        Vector3 offset = selectedItem.placementOffset;
        currentDecoration.transform.position = worldPos + offset;
    }

private void PlaceDecorationAtZone(PlacementArea zone)
{
    if (currentDecoration == null || selectedItem == null) return;

    Vector3 offset = selectedItem.placementOffset;
    currentDecoration.transform.position = zone.transform.position + offset;
    zone.isOccupied = true;

    // Add PlacedDecoration component to track removal
    PlacedDecoration pd = currentDecoration.AddComponent<PlacedDecoration>();
    pd.itemData = selectedItem;
    pd.placer = this;
    pd.placementArea = zone;

    // ✅ Remove one item from inventory AND update UI
    if (playerInventory != null && selectedItem != null)
    {
        playerInventory.RemoveItem(selectedItem);   // this is void in your Inventory
        if (inventoryUI != null)
            inventoryUI.UpdateInventoryUI();
    }

    currentDecoration = null;
    selectedItem = null;
    isEditMode = false;

    // Hide zones after placement
    UpdatePlacementZones();
}

    public void RemoveDecoration(PlacedDecoration placed)
    {
        if (placed == null) return;

        // Return item to inventory
        if (playerInventory != null && placed.itemData != null)
        {
            playerInventory.AddItem(placed.itemData, 1);
            inventoryUI?.UpdateInventoryUI();
        }

        // Free the zone
        if (placed.placementArea != null)
            placed.placementArea.isOccupied = false;

        Destroy(placed.gameObject);

        // Update zones in case edit mode is active
        UpdatePlacementZones();
    }

    public void CancelPlacement()
    {
        if (currentDecoration != null)
            Destroy(currentDecoration);

        isEditMode = false;
        selectedItem = null;
        currentDecoration = null;

        // Hide all zones
        UpdatePlacementZones();
    }
}
