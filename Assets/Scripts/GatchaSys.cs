using System.Collections.Generic;
using System.IO;
using TMPro.EditorUtilities;
using UnityEngine;

public class GatchaSys : MonoBehaviour
{

    [Header("Rewards List")]
    public List<GatchaReward> rewards;

    [Header("Economy")]
    public int rollCost = 100;
    public PetManager pethunger;

    public GatchaReward Roll()
    {
        if (pethunger.petHunger >= rollCost)
        {
            pethunger.petHunger -= rollCost;
            float totalRate = 0f;
            foreach (var reward in rewards)
            {
                totalRate += reward.dropRate;
            }
            float roll = Random.Range(0, totalRate);
            float cumulative = 0f;

            foreach (var reward in rewards)
            {
                cumulative += reward.dropRate;
                if (roll <= cumulative)
                {
                    Debug.Log("You got: " + reward.rewardName);
                    return reward;
                }
            }
            Debug.Log("Rolled! remaining Strength " + pethunger.petHunger);
        }
        else
        {
            Debug.Log("Not enough Strength" + pethunger.petHunger);
        }
        return null;
    }
}
