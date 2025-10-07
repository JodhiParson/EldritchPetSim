using UnityEngine;

public class PlacedDecoration : MonoBehaviour
{
    public ItemData itemData;
    public DecorationPlacer placer;
    public PlacementArea placementArea;
    

    void OnMouseDown()
    {
        if (placementArea != null)
            placementArea.isOccupied = false; // Mark the zone as free

        if (placer != null)
            placer.RemoveDecoration(this);
    }
}