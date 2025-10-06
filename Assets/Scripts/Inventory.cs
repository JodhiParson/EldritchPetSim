using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    public ItemData item;
    public int count;

    public InventorySlot(ItemData item, int count = 1)
    {
        this.item = item;
        this.count = count;
    }
}

public class Inventory : MonoBehaviour
{
    public List<InventorySlot> slots = new List<InventorySlot>();

    public void AddItem(ItemData newItem, int amount = 1)
    {
        if (newItem.isStackable)
        {
            foreach (var slot in slots)
            {
                if (slot.item == newItem)
                {
                    slot.count += amount;
                    return;
                }
            }
        }

        slots.Add(new InventorySlot(newItem, amount));
    }

    public void RemoveItem(ItemData item)
    {
        InventorySlot slot = slots.Find(s => s.item == item);
        if (slot != null)
        {
            if (item.isStackable)
            {
                slot.count--;
                if (slot.count <= 0)
                    slots.Remove(slot);
            }
            else
            {
                slots.Remove(slot);
            }
        }
    }
}
