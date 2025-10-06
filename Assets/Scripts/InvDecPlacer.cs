using UnityEngine;

public class InventoryDecorationPlacer : MonoBehaviour
{
    public ItemData selectedItem;

    public Inventory playerInventory;
    public DecorationPlacer decorationPlacer;

    // Called when an inventory slot is clicked
    public void OnClickItem(ItemData item)
    {
        if (item == null) return;

        selectedItem = item;

        Debug.Log("Selected item: " + item.itemName);

        // Only enter edit mode if DecorationPlacer is set and item has a prefab
        if (decorationPlacer != null && selectedItem.prefab != null)
        {
            decorationPlacer.EnterEditMode(selectedItem.prefab, selectedItem);
        }
        else if (selectedItem.prefab == null)
        {
            Debug.LogWarning("Selected item has no prefab assigned!");
        }
        else
        {
            Debug.LogError("DecorationPlacer reference is missing!");
        }
    }
}
