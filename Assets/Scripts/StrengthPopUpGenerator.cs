using TMPro;
using UnityEngine;

public class StrengthPopUpGenerator : MonoBehaviour
{
    public static StrengthPopUpGenerator current;

    [Header("Popup Settings")]
    public GameObject prefab;
    public RectTransform popupParent; // assign your Canvas or UI container
    public RectTransform strengthText; // assign the Strength TMP_Text object
    public Vector2 offset = new Vector2(-180, -70f); // how far below the text

    private void Awake()
    {
        current = this;
    }

    private void Update()
    {

        //debug
        // if (Input.GetKeyDown(KeyCode.F10))
        // {
        //     CreatePopup("+" + Random.Range(0, 1000));
        // }
    }

    public void CreatePopup(string text)
    {
        if (prefab == null || popupParent == null || strengthText == null)
        {
            Debug.LogWarning("Missing references in StrengthPopUpGenerator!");
            return;
        }

        // Create the popup under the UI canvas
        GameObject popup = Instantiate(prefab, popupParent);

        // Set text
        var tmp = popup.GetComponentInChildren<TextMeshProUGUI>();
        tmp.text = text;

        // Position just below the strength text
        RectTransform popupRect = popup.GetComponent<RectTransform>();
        popupRect.position = strengthText.position + (Vector3)offset;

        // Optional: random small x offset so numbers don’t overlap
        popupRect.position += new Vector3(Random.Range(-100f, 100f), 0, 0);

        // Destroy after animation
        Destroy(popup, 1f);
    }
}
