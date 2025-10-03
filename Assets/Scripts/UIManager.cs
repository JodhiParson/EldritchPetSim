using UnityEngine;
using TMPro; // Required for TextMeshPro

public class UIManager : MonoBehaviour
{
    public int petHunger = 50;          // 0 = starving, 100 = full
    public TMP_Text hungerText;         // Will display hunger
    public float hungerDecayRate = 1f;  // Hunger points lost per second

    void Start()
    {
        // Automatically find the HungerText object in the scene
        if (hungerText == null)
        {
            GameObject obj = GameObject.Find("HungerText"); // Name must match exactly
            if (obj != null)
                hungerText = obj.GetComponent<TMP_Text>();
            else
                Debug.LogError("HungerText object not found in the scene!");
        }
    }

    void Update()
    {
        // Decrease hunger over time
        petHunger -= Mathf.RoundToInt(hungerDecayRate * Time.deltaTime);


        // Update the UI text safely
        if (hungerText != null)
            hungerText.text = "Strength: " + petHunger;
    }

    public void FeedPet()
    {
        petHunger += 10;
        Debug.Log("Feeding the pet... Strength is now: " + petHunger);
    }

    public void CleanPet()
    {
        Debug.Log("Cleaning the pet...");
    }

    public void DoRitual()
    {
        Debug.Log("Performing ritual...");
    }
}
