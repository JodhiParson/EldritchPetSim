using UnityEngine;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject upgradeMenu;
    public GameObject storeMenu;
    public GameObject decMenu;
    public bool optionsOpen = false;
    public bool upgradeOpen = false;
    public bool storeOpen = false;
    public bool decOpen = false;

    [Header("Slide Settings")]
    public float slideSpeed = 500f;
    private RectTransform upgradeRect;
    private Vector2 upgradeClosedPos;
    private Vector2 upgradeOpenPos;

    private RectTransform decRect;
    private Vector2 decClosedPos;
    private Vector2 decOpenPos;
    void Start()
    {
        if (upgradeMenu != null)
        {
            upgradeRect = upgradeMenu.GetComponent<RectTransform>();
            upgradeClosedPos = upgradeRect.anchoredPosition;
            upgradeOpenPos = upgradeClosedPos + new Vector2(0, upgradeRect.rect.height);
            upgradeMenu.SetActive(false);
        }
        if (decMenu != null)
        {
            decRect = decMenu.GetComponent<RectTransform>();
            if (decRect == null)
            {
                Debug.LogError("Decorations menu is missing RectTransform!");
                return;
            }

            decClosedPos = decRect.anchoredPosition;
            decOpenPos = decClosedPos + new Vector2(0, decRect.rect.height);
            decMenu.SetActive(false); // start closed
        }
    
    }

    void Update()
    {
        // Close menus with Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsOpen) CloseOptionsMenu();
            if (upgradeOpen) CloseUpgradeMenu();
            if (storeOpen) CloseStoreMenu();
            if (decOpen) CloseDecMenu();
        }

        // Detect clicks outside the upgrade menu
        if (upgradeOpen && Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIObject(upgradeRect))
            {
                CloseUpgradeMenu();
            }
        }
        if (optionsOpen && Input.GetMouseButtonDown(0))
        {
            GameObject clickedObject = EventSystem.current.currentSelectedGameObject;

            // Ignore if clicked inside menu or on the toggle button
            if (!IsPointerOverUIObject(optionsMenu.GetComponent<RectTransform>()) &&
                (clickedObject == null || clickedObject.tag != "OptionsButton"))
            {
                CloseOptionsMenu();
            }
        }
        if (decOpen && Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIObject(decRect))
                CloseDecMenu();
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
        if (decRect != null)
        {
            Vector2 target = decOpen ? decOpenPos : decClosedPos;
            decRect.anchoredPosition = Vector2.MoveTowards(decRect.anchoredPosition, target, slideSpeed * Time.deltaTime);

            if (!decOpen && Vector2.Distance(decRect.anchoredPosition, decClosedPos) < 0.1f)
                decMenu.SetActive(false);
        }
    }

    // ================= Options Menu =================
    public void OpenOptionsMenu()
    {
        CloseAllMenus();
        optionsOpen = true;
        optionsMenu.SetActive(true);
    }

    public void CloseOptionsMenu()
    {
        optionsOpen = false;
        optionsMenu.SetActive(false);
    }

    public void ToggleOptionsMenu()
    {
        optionsOpen = !optionsOpen;
        optionsMenu.SetActive(optionsOpen);
    }

    // ================= Upgrade Menu =================
    public void OpenUpgradeMenu()
    {
        CloseAllMenus();
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
    // ================= Decorations Menu =================
    public void OpenDecMenu()
    {
        CloseAllMenus();
        decMenu.SetActive(true);
        decOpen = true;
    }

    public void CloseDecMenu()
    {
        decOpen = false;
    }

    public void ToggleDecMenu()
    {
        if (decOpen) CloseDecMenu();
        else OpenDecMenu();
    }
    // ================= Helper =================
    private bool IsPointerOverUIObject(RectTransform rect)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition, null);
    }

    // ================= Store =================
    public void OpenStoreMenu()
    {
        if (optionsOpen) CloseOptionsMenu();
        storeOpen = true;
        storeMenu.SetActive(true);
    }
    public void CloseStoreMenu()
    {
        storeOpen = false;
        storeMenu.SetActive(false);
    }
    private void CloseAllMenus()
    {
        if (optionsOpen) CloseOptionsMenu();
        if (upgradeOpen) CloseUpgradeMenu();
        if (storeOpen) CloseStoreMenu();
        if (decOpen) CloseDecMenu();
    }
}
