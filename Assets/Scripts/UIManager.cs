using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PetManager : MonoBehaviour
{
    [Header("Pet Stats")]
    public int petHunger = 50;           // Current Strength
    public int feedAmount = 10;          // How much each feed adds
    public TMP_Text hungerText;          // Text showing current Strength
    public TMP_Text feedAmountText;      // Text showing how much a feed gives

    [Header("Upgrade System")]
    public int upgradeCost = 10;         // Starting upgrade cost
    public float costMultiplier = 1.5f;  // How much cost increases per upgrade
    public int feedIncreaseAmount = 5;   // How much feedAmount increases per upgrade
    public Button upgradeButton;         // Upgrade button
    public TMP_Text upgradeCostText;     // Text showing upgrade cost

    void Start()
    {
        UpdateUI();
        RefreshUpgradeButton();
    }

    // ================= Feed Pet =================
    public void FeedPet()
    {
        petHunger += feedAmount;
        Debug.Log("Feeding the pet... Strength is now: " + petHunger);
        UpdateUI();
        RefreshUpgradeButton();
    }

    // ================= Upgrade =================
    public void BuyUpgrade()
    {
        if (petHunger >= upgradeCost)
        {
            petHunger -= upgradeCost;          // Deduct Strength
            feedAmount += feedIncreaseAmount;   // Increase feedAmount
            UpdateUI();

            // Increase the upgrade cost
            upgradeCost = Mathf.CeilToInt(upgradeCost * costMultiplier);

            RefreshUpgradeButton();
            Debug.Log("Upgrade purchased! New feed amount: " + feedAmount);
        }
        else
        {
            Debug.Log("Not enough Strength to buy upgrade!");
        }
    }

    // ================= UI =================
    void UpdateUI()
    {
        if (hungerText != null)
            hungerText.text = "Strength: " + petHunger;

        if (feedAmountText != null)
            feedAmountText.text = "Feed Gives: " + feedAmount;
    }

    void RefreshUpgradeButton()
    {
        if (upgradeCostText != null)
            upgradeCostText.text = "Upgrade: " + upgradeCost;

        if (upgradeButton != null)
            upgradeButton.interactable = petHunger >= upgradeCost;
    }
}
