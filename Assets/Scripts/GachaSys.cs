using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GachaSys : MonoBehaviour
{
    [Header("Rewards List")]
    public List<GachaReward> rewards;

    [Header("Economy")]
    public int rollCost = 100;
    public PetManager pethunger;

    public GachaReward Roll()
{
    if (pethunger == null) return null;
    if (pethunger.petHunger < rollCost) return null;

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
        }

    float roll = Random.value * totalRate;
    float cumulative = 0f;

    foreach (var reward in rewards)
    {
        cumulative += reward.dropRate;
        if (roll < cumulative) // Use < instead of <=
        {
            Debug.Log("Rolled: " + reward.rewardName + " (roll=" + roll + ")");
            return reward;
        }
    }

    // Fallback (should never reach here)
    return rewards[rewards.Count - 1];
}
}
