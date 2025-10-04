using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class HungerText : MonoBehaviour
{
    public PetManager petS;
    public TMP_Text hungertxt;

    void Update()
    {
        hungertxt.text = "Strength: " + petS.petHunger;
    }
}
