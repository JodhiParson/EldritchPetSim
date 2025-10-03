using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public int petHunger = 50;          // 0 = starving, 100 = full
    public Text hungerText;             // Drag your HungerText UI here
    public float hungerDecayRate = 1f;  // Hunger points lost per second

    void Update()
    {
        // Decrease hunger over time
        petHunger -= Mathf.RoundToInt(hungerDecayRate * Time.deltaTime);
        if (petHunger < 0) petHunger = 0;

        // Update the UI text
        hungerText.text = "Hunger: " + petHunger;
    }

    public void FeedPet()
    {
        petHunger += 10;
        if (petHunger > 100) petHunger = 100;
        Debug.Log("Feeding the pet... Hunger is now: " + petHunger);
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
