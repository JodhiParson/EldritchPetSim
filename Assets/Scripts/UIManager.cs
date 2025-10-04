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
    public int costMultiplier = 10;      // How much cost increases per upgrade
    public int feedMultiplier = 1;       // How much feedAmount increases per upgrade
    public Button upgradeButton;         // Upgrade button
    public TMP_Text upgradeCostText;     // Text showing upgrade cost

    [Header("Upgrade Auto")]
    public int idleStrength = 0;
    public int upgradeCost2 = 20;
    public int costMultiplier2 = 10;
    public int perSec = 2;
    public Button upgradeButton2;        // Upgrade button
    public TMP_Text currentIdle;
    public TMP_Text upgradeCostText2;

    [Header("Feeding Animation")]
    public GameObject carrotPrefab;      // Carrot prefab
    public Transform carrotSpawn;        // Spawn position for carrot
    public Transform bunnyTransform;     // Bunny transform
    public float carrotArcHeight = 2f;   // Height of the arc
    public float carrotArcDuration = 1f; // Duration of animation

    private float idleTimer = 0f;

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

    // Debug references
    Debug.Log("CarrotPrefab: " + carrotPrefab);
    Debug.Log("CarrotSpawn: " + carrotSpawn);
    Debug.Log("BunnyTransform: " + bunnyTransform);

    // Spawn carrot animation
    if (carrotPrefab != null && carrotSpawn != null && bunnyTransform != null)
    {
        StartCoroutine(SpawnCarrot());
    }
    else
    {
        Debug.LogWarning("Cannot spawn carrot! One or more references are missing.");
    }
}


    private System.Collections.IEnumerator SpawnCarrot()
    {
        GameObject carrot = Instantiate(carrotPrefab, carrotSpawn.position, Quaternion.identity);

        Vector3 startPos = carrotSpawn.position;
        Vector3 endPos = bunnyTransform.position;

        float elapsed = 0f;
        while (elapsed < carrotArcDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / carrotArcDuration;

            // Parabolic arc formula
            float yOffset = carrotArcHeight * 4 * t * (1 - t);
            carrot.transform.position = Vector3.Lerp(startPos, endPos, t) + new Vector3(0, yOffset, 0);

            yield return null;
        }

        carrot.transform.position = endPos;
        Destroy(carrot);

        // TODO: trigger bunny eating animation here
        // bunnyTransform.GetComponent<Bunny>().PlayEatAnimation();
    }

    // ================= Upgrade =================
    public void BuyUpgrade()
    {
        if (petHunger >= upgradeCost)
        {
            petHunger -= upgradeCost;
            feedAmount = Mathf.CeilToInt(feedAmount + feedMultiplier);
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
            idleStrength = Mathf.CeilToInt(idleStrength + perSec);
            upgradeCost2 = Mathf.CeilToInt(upgradeCost2 + costMultiplier2);

            UpdateUI();
            Debug.Log("Bought Idle Upgrade! Now generating " + idleStrength + " Strength/sec");
        }
        else
        {
            Debug.Log("Not enough Strength to buy idle upgrade!");
        }
    }

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
}
