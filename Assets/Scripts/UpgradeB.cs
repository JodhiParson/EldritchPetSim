using TMPro;
using UnityEngine;

public class UpgradeB : MonoBehaviour
{
    public UIManager uiManager;           // Reference to your existing UIManager
    public int upgradeLevel = 0;          // Current upgrade level
    public float baseUpgradeCost = 20f;   // Starting cost
    public float costMultiplier = 1.5f;   // Cost increase per click
    public TMP_Text upgradeCostText;      // UI text to show current cost

    private float currentCost;

    void Start()
    {
        currentCost = baseUpgradeCost;
        UpdateUpgradeUI();
    }

    // Call this method from the Button OnClick()
    public void OnUpgradeButtonClick()
    {
        if (uiManager.petHunger >= currentCost)
        {
            // Spend petHunger
            uiManager.petHunger -= Mathf.RoundToInt(currentCost);

            // Increase level
            upgradeLevel++;

            // Increase cost for next upgrade
            currentCost *= costMultiplier;

            // Apply upgrade effect
            ApplyUpgrade();

            // Update UI
            UpdateUpgradeUI();

            Debug.Log("Upgrade applied! Level: " + upgradeLevel);
        }
        else
        {
            Debug.Log("Not enough Strength (petHunger) to upgrade!");
        }
    }

    void ApplyUpgrade()
    {
        // Example effect: increase petHunger gain by 5 (optional)
        Debug.Log("Upgrade effect applied at level " + upgradeLevel);
    }

    void UpdateUpgradeUI()
    {
        if (upgradeCostText != null)
            upgradeCostText.text = "Upgrade Cost: " + Mathf.Round(currentCost);
    }
}
