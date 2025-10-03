using UnityEngine;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject upgradeMenu;
    public bool optionsOpen = false;
    public bool upgradeOpen = false;

    [Header("Slide Settings")]
    public float slideSpeed = 500f;
    private RectTransform upgradeRect;
    private Vector2 upgradeClosedPos;
    private Vector2 upgradeOpenPos;

    void Start()
    {
        if (upgradeMenu != null)
        {
            upgradeRect = upgradeMenu.GetComponent<RectTransform>();
            upgradeClosedPos = upgradeRect.anchoredPosition;
            upgradeOpenPos = upgradeClosedPos + new Vector2(0, upgradeRect.rect.height);
            upgradeMenu.SetActive(false);
        }
    }

    void Update()
    {
        // Close menus with Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsOpen) CloseOptionsMenu();
            if (upgradeOpen) CloseUpgradeMenu();
        }

        // Detect clicks outside the upgrade menu
        if (upgradeOpen && Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIObject(upgradeRect))
            {
                CloseUpgradeMenu();
            }
        }

        // Smoothly slide upgrade menu
        if (upgradeRect != null)
        {
            Vector2 target = upgradeOpen ? upgradeOpenPos : upgradeClosedPos;
            upgradeRect.anchoredPosition = Vector2.MoveTowards(upgradeRect.anchoredPosition, target, slideSpeed * Time.deltaTime);

            // Optionally deactivate when fully closed
            if (!upgradeOpen && Vector2.Distance(upgradeRect.anchoredPosition, upgradeClosedPos) < 0.1f)
            {
                upgradeMenu.SetActive(false);
            }
        }
    }

    // ================= Options Menu =================
    public void OpenOptionsMenu()
    {
        if (upgradeOpen) CloseUpgradeMenu();
        optionsOpen = true;
        optionsMenu.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        optionsOpen = false;
        optionsMenu.SetActive(false);
    }

    public void ToggleMenu()
    {
        optionsOpen = !optionsOpen;
        optionsMenu.SetActive(optionsOpen);
    }

    // ================= Upgrade Menu =================
    public void OpenUpgradeMenu()
    {
        if (optionsOpen) CloseOptionsMenu();
        upgradeOpen = true;
        upgradeMenu.SetActive(true);
    }

    public void CloseUpgradeMenu()
    {
        upgradeOpen = false;
        // panel will slide down and deactivate automatically
    }

    public void ToggleUpgradeMenu()
    {
        if (upgradeOpen) CloseUpgradeMenu();
        else OpenUpgradeMenu();
    }

    // ================= Helper =================
    private bool IsPointerOverUIObject(RectTransform rect)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition, null);
    }
}
