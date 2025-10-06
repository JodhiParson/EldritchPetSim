using System.Collections.Generic;
using UnityEngine;

public class GachaSys : MonoBehaviour
{
    [Header("Rewards List")]
    public List<GachaReward> rewards;

    [Header("Economy")]
    public int rollCost = 100;
    public PetManager pethunger;
    public Inventory playerInventory;
    public InventoryUI inventoryUI;
    public GachaReward Roll()
    {
        if (pethunger == null || playerInventory == null) return null;
        if (pethunger.petHunger < rollCost) return null;

        pethunger.petHunger -= rollCost;

        float totalRate = 0f;
        foreach (var reward in rewards) totalRate += reward.dropRate;

        if (totalRate <= 0) return null;

        float roll = Random.value * totalRate;
        float cumulative = 0f;

        foreach (var reward in rewards)
        {
            cumulative += reward.dropRate;
            if (roll < cumulative)
            {
                if (reward.rewardItem != null)
                {
                    playerInventory.AddItem(reward.rewardItem);
                    inventoryUI?.UpdateInventoryUI();
                    Debug.Log("Rolled: " + reward.rewardItem.itemName);
                }
                return reward; // Only returns here, no fallback needed
            }
        }

        // If somehow no reward is chosen, just return null (don’t add anything)
        return null;
    }
}
