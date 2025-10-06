using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    [HideInInspector] public ItemData itemData;

    public Image iconImage;
    public TMP_Text countText;

    public void SetSlot(ItemData item, int count, Sprite emptyIcon)
    {
        itemData = item;
        if (iconImage != null)
            iconImage.sprite = item != null ? item.icon : emptyIcon;
        if (countText != null)
            countText.text = item != null && item.isStackable ? count.ToString() : "";
    }
}
