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
    public RectTransform popupArea;

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
        if (prefab == null || popupArea == null) return;

        // Create popup inside panel
        GameObject popup = Instantiate(prefab, popupArea);

        // Set text
        var tmp = popup.GetComponentInChildren<TextMeshProUGUI>();
        tmp.text = text;

        // Random position within panel bounds
        RectTransform popupRect = popup.GetComponent<RectTransform>();
        Rect panelRect = popupArea.rect;

        float randX = Random.Range(panelRect.xMin, panelRect.xMax);
        float randY = Random.Range(panelRect.yMin, panelRect.yMax);

        popupRect.anchoredPosition = new Vector2(randX, randY);

        // Destroy after animation
        Destroy(popup, 1f);
    }

}
