using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Rendering.Universal;

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

    [Header("Gatcha System")]
    public GatchaSys gatcha;
    public TMP_Text gatchaResTxt;
    public Image gatchaRewardIcon;

    void Start()
    {
        UpdateUI();
    }

    // ================= Feed Pet =================
    public void FeedPet()
    {
        petHunger += feedAmount;
        Debug.Log("Feeding the pet... Strength is now: " + petHunger);
        UpdateUI();
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

            Debug.Log("Idle added " + (idleStrength * ticks) + " Strength. Total: " + petHunger);
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


    }
    // ================= Gatcha System =================
    public void OnRollButton()
    {
        var reward = gatcha.Roll();
        if (reward != null)
        {
            gatchaResTxt.text = "You got: " + reward.rewardName;
            if (reward.icon != null)
                gatchaRewardIcon.sprite = reward.icon;
        }
        else
        {
            gatchaResTxt.text = "Not enough currency";
        }
    }
}
