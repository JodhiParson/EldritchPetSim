using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GachaSys : MonoBehaviour
{
    [Header("Rewards List")]
    public List<GachaReward> rewards;

    [Header("Economy")]
    public int rollCost = 100;
    public PetManager pethunger;         // reference for cost deduction
    public Inventory playerInventory;    // reference for inventory

    public GachaReward Roll()
    {
        if (pethunger == null) return null;
        if (pethunger.petHunger < rollCost) return null;

        // Deduct roll cost
        pethunger.petHunger -= rollCost;

        // Calculate total drop rate
        float totalRate = 0f;
        foreach (var reward in rewards)
        {
            totalRate += reward.dropRate;
        }

        if (totalRate <= 0)
        {
            Debug.LogWarning("Total Drop rate is 0%");
            return null;
        }

        // Roll
        float roll = Random.value * totalRate;
        float cumulative = 0f;

        foreach (var reward in rewards)
        {
            cumulative += reward.dropRate;
            if (roll < cumulative)
            {
                // Only return the reward here. DO NOT add to inventory.
                Debug.Log("Rolled: " + reward.rewardItem.itemName + " (roll=" + roll + ")");
                return reward;
            }
        }

        // Fallback
        return rewards[rewards.Count - 1];
    }
}
