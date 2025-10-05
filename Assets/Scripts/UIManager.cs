using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Rendering.Universal;
using System.Collections.Generic;

public class PetManager : MonoBehaviour
{
    [Header("Pet Stats")]
    public int petHunger = 50;           // Current Strength
    public int feedAmount = 10;          // How much each feed adds
    public TMP_Text hungerText;          // Text showing current Strength
    public TMP_Text feedAmountText;      // Text showing how much a feed gives

    [Header("Upgrade System")]
    public int upgradeCost = 10;         // Starting upgrade cost
    public int costMultiplier = 10;  // How much cost increases per upgrade
    public int feedMultiplier = 1;   // How much feedAmount increases per upgrade
    public Button upgradeButton;         // Upgrade button
    public TMP_Text upgradeCostText;     // Text showing upgrade cost

    [Header("Upgrade Auto")]

    public int idleStrength = 0;
    public int upgradeCost2 = 20;
    public int costMultiplier2 = 10;
    public int perSec = 2;
    public Button upgradeButton2;         // Upgrade button
    public TMP_Text currentIdle;
    public TMP_Text upgradeCostText2;

    [Header("Gacha System")]
    public GachaSys gacha;
    public TMP_Text gachaResTxt;
    public Image gachaRewardIcon;
    public Button gachaButton;


    [Header("Inventory")]
    public TMP_Text inventoryText;
    public Inventory playerInventory;
    public InventoryUI inventoryUI;


    void Start()
    {
        UpdateUI();
    }

    // ================= Feed Pet =================
    public void FeedPet()
    {
        petHunger += feedAmount;
        // Debug.Log("Feeding the pet... Strength is now: " + petHunger);
        UpdateUI();
    }

    // ================= Upgrade =================
    public void BuyUpgrade()
    {
        if (petHunger >= upgradeCost)
        {
            petHunger -= upgradeCost;          // Deduct Strength
            feedAmount = Mathf.CeilToInt(feedAmount + feedMultiplier);
            UpdateUI();

            // Increase the upgrade cost
            upgradeCost = Mathf.CeilToInt(upgradeCost + costMultiplier);

            UpdateUI();
            Debug.Log("Upgrade purchased! New feed amount: " + feedAmount);
        }
        else
        {
            Debug.Log("Not enough Strength to buy upgrade!");
        }
    }
    public void BuyIdleUpgrade()
    {
        if (petHunger >= upgradeCost2)
        {
            petHunger -= upgradeCost2;

            // Instead of always adding 2, we multiply perSec by a factor
            idleStrength = Mathf.CeilToInt(idleStrength + perSec);

            // Increase cost for next upgrade
            upgradeCost2 = Mathf.CeilToInt(upgradeCost2 + costMultiplier2);

            UpdateUI();
            Debug.Log("Bought Idle Upgrade! Now generating " + idleStrength + " Strength/sec");
        }
        else
        {
            Debug.Log("Not enough Strength to buy idle upgrade!");
        }
    }

    float idleTimer = 0f;

    void Update()
    {
        idleTimer += Time.deltaTime;

        if (idleTimer >= 1f)
        {
            int ticks = Mathf.FloorToInt(idleTimer);
            petHunger += idleStrength * ticks;
            idleTimer -= ticks;

            // Debug.Log("Idle added " + (idleStrength * ticks) + " Strength. Total: " + petHunger);
            UpdateUI();
        }
    }
    // ================= UI =================
    void UpdateUI()
    {
        if (hungerText != null)
            hungerText.text = "Strength: " + petHunger;

        if (feedAmountText != null)
            feedAmountText.text = "Feed Gives: " + feedAmount;

        if (currentIdle != null)
            currentIdle.text = "Current Idle: " + idleStrength;

        if (upgradeCostText != null)
            upgradeCostText.text = "Upgrade: " + upgradeCost;

        if (upgradeButton != null)
            upgradeButton.interactable = petHunger >= upgradeCost;

        if (upgradeCostText2 != null)
            upgradeCostText2.text = "Upgrade: " + upgradeCost2;

        if (upgradeButton2 != null)
            upgradeButton2.interactable = petHunger >= upgradeCost2;

        if (gachaButton != null && gacha != null)
            gachaButton.interactable = petHunger >= gacha.rollCost;


    }
    // ================= Gatcha System =================
    public void OnRollButton()
    {
        GachaReward reward = gacha.Roll();
        if (reward != null)
        {
            playerInventory.AddItem(reward.rewardItem); // Only add here
            gachaResTxt.text = "You got: " + reward.rewardItem.itemName;

            if (gachaRewardIcon != null && reward.rewardItem.icon != null)
                gachaRewardIcon.sprite = reward.rewardItem.icon;

            inventoryUI.UpdateInventoryUI();
        }
    }
}
