using UnityEngine;
using UnityEngine.UI;
using TMPro;
// using UnityEditor.Rendering.Universal;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PetManager : MonoBehaviour
{
    [Header("Carrot Sprites")]
    public Sprite[] carrotSprites = new Sprite[6]; // Match feedAmounts array
    public Button feedButton; // The button you press to feed the bunny

    [Header("Pet Stats")]
    public int petHunger = 0; // Current Strength
    public TMP_Text hungerText;
    public TMP_Text feedAmountText;
    public TMP_Text feedAmountText1;
    public TMP_Text feedAmountText2;
    public TMP_Text feedAmountText3;
    public TMP_Text feedAmountText4;
    public TMP_Text feedAmountText5;

    public TMP_Text idleAmountText;
    public TMP_Text idleAmountText1;
    public TMP_Text idleAmountText2;
    public TMP_Text idleAmountText3;
    public TMP_Text idleAmountText4;
    public TMP_Text idleAmountText5;
    public int idleStrength = 0;

    [Header("Feed Upgrade System")]
    public int[] feedAmounts = new int[6];
    public int[] baseFeedAmounts = new int[6] { 10, 180, 1000, 5000, 10000, 50000 };
    public bool[] unlocked = new bool[6] { true, false, false, false, false, false }; // Carrot 1 unlocked by default
    public int totalFeedAmount;
    public int[] idleFeedAmounts = new int[6];
    public int[] idleBaseFeedAmounts = new int[6] { 10, 180, 1000, 5000, 10000, 50000 };

    [Header("Upgrade Costs")]
    public int upgradeCost = 10;
    public int upgradeCost1 = 1000;
    public int upgradeCost2 = 10000;
    public int upgradeCost3 = 100000;
    public int upgradeCost4 = 1000000;
    public int upgradeCost5 = 2000000;

    [Header("Unlock Costs")]
    public int unlockCost1 = 1000;
    public int unlockCost2 = 10000;
    public int unlockCost3 = 100000;
    public int unlockCost4 = 1000000;
    public int unlockCost5 = 2000000;

    [Header("Buttons")]
    public Button upgradeButton;
    public Button upgradeButton1;
    public Button upgradeButton2;
    public Button upgradeButton3;
    public Button upgradeButton4;
    public Button upgradeButton5;

    public Button unlockButton1;
    public Button unlockButton2;
    public Button unlockButton3;
    public Button unlockButton4;
    public Button unlockButton5;
    public Button idleUpgradeButton0;
    public Button idleUpgradeButton1;
    public Button idleUpgradeButton2;
    public Button idleUpgradeButton3;
    public Button idleUpgradeButton4;
    public Button idleUpgradeButton5;
     public Button idleUnlockButton0;
    public Button idleUnlockButton1;
    public Button idleUnlockButton2;
    public Button idleUnlockButton3;
    public Button idleUnlockButton4;
    public Button idleUnlockButton5;

    [Header("Upgrade Cost Texts")]
    public TMP_Text upgradeCostText;
    public TMP_Text upgradeCostText1;
    public TMP_Text upgradeCostText2;
    public TMP_Text upgradeCostText3;
    public TMP_Text upgradeCostText4;
    public TMP_Text upgradeCostText5;

    [Header("Unlock Cost Texts")]
    public TMP_Text unlockCostText0;
    public TMP_Text unlockCostText1;
    public TMP_Text unlockCostText2;
    public TMP_Text unlockCostText3;
    public TMP_Text unlockCostText4;
    public TMP_Text unlockCostText5;

    public float costMultiplier = 1.4f;
    public float feedMultiplier = 1.2f;

    [Header("Idle Upgrade System")]
    public int idleUpgradeCost = 20;
    // public Button idleUpgradeButton;
    // public TMP_Text idleUpgradeCostText;
    // public float costMultiplier2 = 1.1f;
    public int perSec = 3;
    public TMP_Text currentIdle;
    [Header("Idle Upgrade System")]
    public int[] idleUpgradeCosts = new int[6] { 20, 200, 2000, 20000, 200000, 2000000 };
    public bool[] idleUnlocked = new bool[6] { false, false, false, false, false, false };
    // public int[] idleIncrements = new int[6] { 3, 20, 200, 2000, 20000, 200000 }; // perSec increment per upgrade
    public float idleCostMultiplier = 1.1f;

    // [Header("Idle Upgrade Buttons")]
    

    [Header("Idle Upgrade Texts")]
    public TMP_Text idleUpgradeCostText0;
    public TMP_Text idleUpgradeCostText1;
    public TMP_Text idleUpgradeCostText2;
    public TMP_Text idleUpgradeCostText3;
    public TMP_Text idleUpgradeCostText4;
    public TMP_Text idleUpgradeCostText5;

    [Header("Idle Unlock Costs")]
    public int[] idleUnlockCosts = new int[6] {1000, 10000, 50000, 100000, 1000000, 2000000 };

    [Header("Idle Unlock Cost Texts")]
    public TMP_Text idleUnlockCostText0;
    public TMP_Text idleUnlockCostText1;
    public TMP_Text idleUnlockCostText2;
    public TMP_Text idleUnlockCostText3;
    public TMP_Text idleUnlockCostText4;
    public TMP_Text idleUnlockCostText5;

    [Header("Gacha System")]
    public GachaSys gacha;
    public TMP_Text gachaResTxt;
    public Image gachaRewardIcon;
    public Button gachaButton;
    public GachaSys gachaSystem;
    public GachaItemScroll gachaScroll;

    [Header("Inventory")]
    public TMP_Text inventoryText;
    public Inventory playerInventory;
    public InventoryUI inventoryUI;

    [Header("Bunny Components")]
    public Animator bunnyAnimator;
    public AudioSource bunnyAudio;

   



   
    void Start()
    {
        feedAmounts[0] = baseFeedAmounts[0]; // Carrot 1 starts unlocked
        // idleFeedAmounts[0] = idleBaseFeedAmounts[0];
        RecalculateTotalFeedPower();

        // // Hide all upgrade buttons for locked carrots
        // Button[] upgradeButtons = { upgradeButton, upgradeButton1, upgradeButton2, upgradeButton3, upgradeButton4, upgradeButton5 };
        // Button[] idleUpgradeButtons = { idleUnlockButton0,idleUnlockButton1,idleUnlockButton2,idleUnlockButton3,idleUnlockButton4,idleUnlockButton5};

        // for (int i = 1; i < upgradeButtons.Length; i++)
        // {
        //     if (upgradeButtons[i] != null)
        //         upgradeButtons[i].gameObject.SetActive(unlocked[i]);
        // }
        // for (int i = 1; i < idleUpgradeButtons.Length; i++)
        // {
        //     if (idleUpgradeButtons[i] != null)
        //         idleUpgradeButtons[i].gameObject.SetActive(idleUnlocked[i]);
        // }

        UpdateUI();
    }

    float idleTimer = 0f;

    void Update()
    {
        idleTimer += Time.deltaTime;

        if (idleTimer >= 1f)
        {
            int ticks = Mathf.FloorToInt(idleTimer);
            petHunger += idleStrength * ticks;
            // Debug.Log(idleStrength);
            idleTimer -= ticks;

            if (StrengthPopUpGenerator.current != null && (idleStrength * ticks) > 0)
                StrengthPopUpGenerator.current.CreatePopup("+" + (idleStrength * ticks));

            UpdateUI();
        }
    }

    // ================= Feed Pet =================
    public void FeedPet()
    {
        if (bunnyAnimator != null)
            bunnyAnimator.SetTrigger("Eat");

        if (bunnyAudio != null)
            bunnyAudio.Play();
        petHunger += totalFeedAmount;

        if (StrengthPopUpGenerator.current != null)
            StrengthPopUpGenerator.current.CreatePopup("+" + totalFeedAmount);

        UpdateUI();
    }

    // ================= Feed Upgrades =================
    public void BuyUpgrade(int index)
    {
        if (!unlocked[index]) return;

        int currentCost = 0;

        switch (index)
        {
            case 0: currentCost = upgradeCost; break;
            case 1: currentCost = upgradeCost1; break;
            case 2: currentCost = upgradeCost2; break;
            case 3: currentCost = upgradeCost3; break;
            case 4: currentCost = upgradeCost4; break;
            case 5: currentCost = upgradeCost5; break;
        }

        if (petHunger >= currentCost)
        {
            petHunger -= currentCost;
            feedAmounts[index] = Mathf.CeilToInt(feedAmounts[index] * idleCostMultiplier);

            switch (index)
            {
                case 0: upgradeCost = Mathf.CeilToInt(upgradeCost * feedMultiplier); break;
                case 1: upgradeCost1 = Mathf.CeilToInt(upgradeCost1 * costMultiplier); break;
                case 2: upgradeCost2 = Mathf.CeilToInt(upgradeCost2 * costMultiplier); break;
                case 3: upgradeCost3 = Mathf.CeilToInt(upgradeCost3 * costMultiplier); break;
                case 4: upgradeCost4 = Mathf.CeilToInt(upgradeCost4 * costMultiplier); break;
                case 5: upgradeCost5 = Mathf.CeilToInt(upgradeCost5 * costMultiplier); break;
            }

            RecalculateTotalFeedPower();
            UpdateUI();
        }
    }
    // ================= Idle Upgrade =================
    public void BuyIdleUpgrade(int index)
    {
         if (!idleUnlocked[index]) return;

        int idleCurrentCost = 0;

        switch (index)
        {
            case 0: idleCurrentCost = idleUpgradeCosts[0]; break;
            case 1: idleCurrentCost = idleUpgradeCosts[1]; break;
            case 2: idleCurrentCost = idleUpgradeCosts[2]; break;
            case 3: idleCurrentCost = idleUpgradeCosts[3]; break;
            case 4: idleCurrentCost = idleUpgradeCosts[4]; break;
            case 5: idleCurrentCost = idleUpgradeCosts[5]; break;
        }

        if (petHunger >= idleCurrentCost)
        {
            petHunger -= idleCurrentCost;
            idleFeedAmounts[index] = Mathf.CeilToInt(idleFeedAmounts[index] * idleCostMultiplier);

            switch (index)
            {
                case 0: idleUpgradeCosts[0] = Mathf.CeilToInt(idleUpgradeCosts[0] * feedMultiplier); break;
                case 1: idleUpgradeCosts[1] = Mathf.CeilToInt(idleUpgradeCosts[1] * feedMultiplier); break;
                case 2: idleUpgradeCosts[2] = Mathf.CeilToInt(idleUpgradeCosts[2] * feedMultiplier); break;
                case 3: idleUpgradeCosts[3] = Mathf.CeilToInt(idleUpgradeCosts[3] * feedMultiplier); break;
                case 4: idleUpgradeCosts[4] = Mathf.CeilToInt(idleUpgradeCosts[4] * feedMultiplier); break;
                case 5: idleUpgradeCosts[5] = Mathf.CeilToInt(idleUpgradeCosts[5] * feedMultiplier); break;
            }

            RecalculateIdleStrength();
            UpdateUI();
        }
        // if (!idleUnlocked[index]) return;

        // int currentCost = idleUpgradeCosts[index];

        // if (petHunger >= currentCost)
        // {
        //     petHunger -= currentCost;

        //     // Multiply idle increment similar to feed multiplier
        //     idleIncrements[index] = Mathf.CeilToInt(idleIncrements[index] * feedMultiplier); // or a separate multiplier if you want
        //     idleUpgradeCosts[index] = Mathf.CeilToInt(currentCost * idleCostMultiplier);

        //     // Optional: unlock next idle upgrade
        //     if (index + 1 < idleUnlocked.Length)
        //         idleUnlocked[index + 1] = true;

        //     UpdateUI();
        // }
    }

    // ================= Unlock Carrots =================
    public void UnlockCarrot(int index)
    {
        if (unlocked[index]) return;

        int cost = 0;
        switch (index)
        {
            case 1: cost = unlockCost1; break;
            case 2: cost = unlockCost2; break;
            case 3: cost = unlockCost3; break;
            case 4: cost = unlockCost4; break;
            case 5: cost = unlockCost5; break;
        }

        if (petHunger >= cost)
        {
            petHunger -= cost;
            unlocked[index] = true;
            feedAmounts[index] = baseFeedAmounts[index];
            RecalculateTotalFeedPower();

            // Instead of hiding, disable interactability and make it invisible
            Button[] unlockButtons = { null, unlockButton1, unlockButton2, unlockButton3, unlockButton4, unlockButton5 };
            if (unlockButtons[index] != null)
            {
                unlockButtons[index].interactable = false;

                // Optional: make button invisible but still in hierarchy
                CanvasGroup cg = unlockButtons[index].GetComponent<CanvasGroup>();
                if (cg == null)
                    cg = unlockButtons[index].gameObject.AddComponent<CanvasGroup>();

                cg.alpha = 0;        // Make invisible
                cg.blocksRaycasts = false; // Don’t block clicks
            }


            // Show the upgrade button
            Button[] upgradeButtons = { upgradeButton, upgradeButton1, upgradeButton2, upgradeButton3, upgradeButton4, upgradeButton5 };
            if (upgradeButtons[index] != null)
                upgradeButtons[index].gameObject.SetActive(true);
            // UpdateFeedButtonSprite(index);
            UpdateUI();
        }
    }
    
     // ================= Unlock Rituals =================
    public void UnlockRitual(int index)
    {
        if (idleUnlocked[index]) return; // already unlocked

        // cost for this ritual unlock
        int cost = idleUpgradeCosts[index];

        if (petHunger >= cost)
        {
            petHunger -= cost;
            idleUnlocked[index] = true;

            // enable the corresponding idle upgrade button
            Button[] idleUpgradeButtonsArray = { idleUpgradeButton0,idleUpgradeButton1, idleUpgradeButton2, idleUpgradeButton3, idleUpgradeButton4, idleUpgradeButton5 };
            if (idleUpgradeButtonsArray[index] != null)
                idleUpgradeButtonsArray[index].gameObject.SetActive(true);

            // hide or disable the unlock button after unlocking
            Button[] idleUnlockButtonsArray = { idleUnlockButton0, idleUnlockButton1, idleUnlockButton2, idleUnlockButton3, idleUnlockButton4, idleUnlockButton5 };
            if (idleUnlockButtonsArray[index] != null)
            {
                idleUnlockButtonsArray[index].interactable = false;

                CanvasGroup cg = idleUnlockButtonsArray[index].GetComponent<CanvasGroup>();
                if (cg == null)
                    cg = idleUnlockButtonsArray[index].gameObject.AddComponent<CanvasGroup>();

                cg.alpha = 0;                // invisible
                cg.blocksRaycasts = false;   // no longer blocks clicks
            }
            RecalculateIdleStrength();
            UpdateUI();
        }
        else
        {
            // Debug.Log($"Not enough petHunger to unlock ritual {index}.");
        }
    }
    void RecalculateTotalFeedPower()
    {
        totalFeedAmount = 0;
        for (int i = 0; i < feedAmounts.Length; i++)
        {
            if (unlocked[i])
                totalFeedAmount += feedAmounts[i];
        }
    }
    void RecalculateIdleStrength()
    {
        idleStrength = 0;
        for (int i = 0; i < idleFeedAmounts.Length; i++)
        {
            if (idleUnlocked[i])
                idleStrength += idleFeedAmounts[i];
        }
    }





    // ================= UI =================
    void UpdateUI()
    {
        if (hungerText != null)
            hungerText.text = "Strength: " + petHunger;

        // ===== Feed Display =====
        TMP_Text[] feedTexts = { feedAmountText, feedAmountText1, feedAmountText2, feedAmountText3, feedAmountText4, feedAmountText5 };
        for (int i = 0; i < feedTexts.Length; i++)
        {
            if (feedTexts[i] == null) continue;
            feedTexts[i].text = unlocked[i] ? $"Carrot {i + 1}: +{feedAmounts[i]} / Tap" : $"Carrot {i + 1}: Locked";
        }
        TMP_Text[] idleFeedTexts = { idleAmountText, idleAmountText1, idleAmountText2, idleAmountText3, idleAmountText4, idleAmountText5 };
        for (int i = 0; i < idleFeedTexts.Length; i++)
        {
            if (idleFeedTexts[i] == null) continue;
            idleFeedTexts[i].text = idleUnlocked[i] ? $"Carrot {i + 1}: +{idleFeedAmounts[i]} / Second" : $"Carrot {i + 1}: Locked";
        }
        
        if (currentIdle != null)
            currentIdle.text = "Current Idle: " + idleStrength;

        // ===== Carrot Upgrade Buttons =====
        Button[] upgradeButtons = { upgradeButton, upgradeButton1, upgradeButton2, upgradeButton3, upgradeButton4, upgradeButton5 };
        TMP_Text[] upgradeTexts = { upgradeCostText, upgradeCostText1, upgradeCostText2, upgradeCostText3, upgradeCostText4, upgradeCostText5 };
        int[] costs = { upgradeCost, upgradeCost1, upgradeCost2, upgradeCost3, upgradeCost4, upgradeCost5 };

        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            if (upgradeButtons[i] != null)
            {
                upgradeButtons[i].gameObject.SetActive(unlocked[i]);
                upgradeButtons[i].interactable = unlocked[i] && petHunger >= costs[i];
            }
            if (upgradeTexts[i] != null)
                upgradeTexts[i].text = unlocked[i] ? $"Upgrade: {costs[i]}" : "";
        }

        // ===== Carrot Unlock Buttons =====
        Button[] unlockButtons = { null, unlockButton1, unlockButton2, unlockButton3, unlockButton4, unlockButton5 };
        TMP_Text[] unlockTexts = { null, unlockCostText1, unlockCostText2, unlockCostText3, unlockCostText4, unlockCostText5 };
        int[] unlockCosts = { 0, unlockCost1, unlockCost2, unlockCost3, unlockCost4, unlockCost5 };

        for (int i = 1; i < unlockButtons.Length; i++)
        {
            if (unlockButtons[i] != null)
            {
                unlockButtons[i].gameObject.SetActive(true); // always visible
                unlockButtons[i].interactable = !unlocked[i] && petHunger >= unlockCosts[i];
            }
            if (unlockTexts[i] != null)
                unlockTexts[i].text = !unlocked[i] ? $"Unlock: {unlockCosts[i]}" : "";
        }

        // ===== Idle (Ritual) Upgrade Buttons =====
        Button[] idleUpgradeButtons = { idleUpgradeButton0,idleUpgradeButton1, idleUpgradeButton2, idleUpgradeButton3, idleUpgradeButton4, idleUpgradeButton5 };
        TMP_Text[] idleUpgradeTexts = { idleUpgradeCostText0, idleUpgradeCostText1, idleUpgradeCostText2, idleUpgradeCostText3, idleUpgradeCostText4, idleUpgradeCostText5 };

        for (int i = 0; i < idleUpgradeButtons.Length; i++)
        {
            if (idleUpgradeButtons[i] != null)
            {
                idleUpgradeButtons[i].gameObject.SetActive(idleUnlocked[i]);
                idleUpgradeButtons[i].interactable = idleUnlocked[i] && petHunger >= idleUpgradeCosts[i];
            }
            if (idleUpgradeTexts[i] != null)
                idleUpgradeTexts[i].text = idleUnlocked[i] ? $"Upgrade: {idleUpgradeCosts[i]}" : "";
        }

        // ===== Idle (Ritual) Unlock Buttons & Texts =====
        Button[] idleUnlockButtons = { idleUnlockButton0, idleUnlockButton1, idleUnlockButton2, idleUnlockButton3, idleUnlockButton4, idleUnlockButton5 };
        TMP_Text[] idleUnlockTexts = { idleUnlockCostText0, idleUnlockCostText1, idleUnlockCostText2, idleUnlockCostText3, idleUnlockCostText4, idleUnlockCostText5 };

        for (int i = 0; i < idleUnlockButtons.Length; i++)
        {
            if (idleUnlockButtons[i] != null)
            {
                idleUnlockButtons[i].gameObject.SetActive(!idleUnlocked[i]); // Show only if locked
                idleUnlockButtons[i].interactable = !idleUnlocked[i] && petHunger >= idleUnlockCosts[i];
            }

            if (idleUnlockTexts[i] != null)
            {
                idleUnlockTexts[i].text = !idleUnlocked[i] ? $"Unlock: {idleUnlockCosts[i]}" : "";
            }
        }

        // ===== Gacha =====
        if (gachaButton != null && gacha != null)
            gachaButton.interactable = petHunger >= gacha.rollCost;

        // ===== Feed Button Sprite =====
        if (feedButton != null)
        {
            int highestCarrot = GetHighestUnlockedCarrot();
            if (carrotSprites.Length > highestCarrot && carrotSprites[highestCarrot] != null)
            {
                Image btnImage = feedButton.GetComponent<Image>();
                if (btnImage != null)
                    btnImage.sprite = carrotSprites[highestCarrot];
            }
        }
        
}

    
// ===== Update Button Sprites =====
    int GetHighestUnlockedCarrot()
    {
        int highest = 0; // Start with carrot 0
        for (int i = 0; i < unlocked.Length; i++)
        {
            if (unlocked[i])
                highest = i;
        }
        return highest;
    }



        // ================= Gacha System =================
public void OnRollButton()
{
    GachaReward reward = gacha.Roll();
    if (reward != null && reward.rewardItem != null)
    {
        // Start scroll and update UI *after* it finishes
        gachaScroll.StartScroll(reward.rewardItem, () =>
        {
            gachaResTxt.text = "You got: " + reward.rewardItem.itemName;
            if (gachaRewardIcon != null && reward.rewardItem.icon != null)
                gachaRewardIcon.sprite = reward.rewardItem.icon;
        });
    }
}

    // ================= Exit Game =================
    public void ExitGame()
    {
        Debug.Log("Exiting game...");
        Application.Quit();

    }
}
