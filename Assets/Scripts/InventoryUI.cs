using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory playerInventory;
    public Transform slotsParent;
    public GameObject slotPrefab;
    public Sprite emptySlotIcon;

    public void UpdateInventoryUI()
    {
        if (playerInventory == null || slotsParent == null || slotPrefab == null) return;

        foreach (Transform child in slotsParent)
            Destroy(child.gameObject);

        foreach (var slot in playerInventory.slots)
        {
            GameObject slotGO = Instantiate(slotPrefab, slotsParent);
            InventorySlotUI slotUI = slotGO.GetComponent<InventorySlotUI>();
            Button button = slotGO.GetComponent<Button>();

            if (slotUI == null || button == null) continue;

            // Use SetSlot to properly assign icon and count
            slotUI.SetSlot(slot.item, slot.count, emptySlotIcon);

            InventorySlotUI capturedSlotUI = slotUI; // closure fix
            button.onClick.AddListener(() =>
            {
                InventoryDecorationPlacer placer = Object.FindFirstObjectByType<InventoryDecorationPlacer>();
                if (placer != null)
                    placer.OnClickItem(capturedSlotUI.itemData);
            });
        }
    }
}
