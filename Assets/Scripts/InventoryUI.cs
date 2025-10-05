using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [Header("References")]
    public Inventory playerInventory;       // Inventory script
    public Transform slotsParent;           // Panel with GridLayoutGroup
    public GameObject slotPrefab;           // Prefab with Image + TMP_Text

    [Header("Optional")]
    public Sprite emptySlotIcon;            // Placeholder icon if item icon is missing

    public void UpdateInventoryUI()
    {
        if (playerInventory == null || slotsParent == null || slotPrefab == null)
        {
            Debug.LogWarning("InventoryUI references missing!");
            return;
        }

        // Clear old slots
        foreach (Transform child in slotsParent)
            Destroy(child.gameObject);

        // Spawn slot prefab for each inventory item
        foreach (var slot in playerInventory.slots)
        {
            GameObject slotGO = Instantiate(slotPrefab, slotsParent);

            // Find the icon and count in the prefab
            Image iconImage = slotGO.transform.Find("Icon")?.GetComponent<Image>();
            TMP_Text countText = slotGO.transform.Find("Count")?.GetComponent<TMP_Text>();

            // Set icon
            if (iconImage != null)
                iconImage.sprite = slot.item.icon != null ? slot.item.icon : emptySlotIcon;

            // Set count (only if stackable)
            if (countText != null)
                countText.text = slot.item.isStackable ? slot.count.ToString() : "";
        }
    }
}
