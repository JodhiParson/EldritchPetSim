using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    public int petHunger = 50;
    public TMP_Text hungerText;

    void Start()
    {
        UpdateUI();
    }

    public void FeedPet()
    {
        petHunger += 10;
        Debug.Log("Feeding the pet... Strength is now: " + petHunger);
        UpdateUI();
    }

    void UpdateUI()
    {
        if (hungerText != null)
            hungerText.text = "Strength: " + petHunger;
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
