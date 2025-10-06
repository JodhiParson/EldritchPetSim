using UnityEngine;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour
{
    public GameObject menu;
    public GameObject upgradeMenu;
    public GameObject storeMenu;
    public GameObject decMenu;
    public GameObject optionsMenu;
    public GameObject ritualMenu;
    public bool menuOpen = false;
    public bool upgradeOpen = false;
    public bool storeOpen = false;
    public bool decOpen = false;
    public bool optionsOpen = false;
    public bool ritualOpen = false;

    [Header("Slide Settings")]
    public float slideSpeed = 500f;
    private RectTransform upgradeRect;
    private Vector2 upgradeClosedPos;
    private Vector2 upgradeOpenPos;

    private RectTransform decRect;
    private Vector2 decClosedPos;
    private Vector2 decOpenPos;

    private RectTransform optionsRect;
    private Vector2 optionsClosedPos;
    private Vector2 optionsOpenPos;
    private RectTransform ritualRect;
    private Vector2 ritualClosedPos;
    private Vector2 ritualOpenPos;
    private Vector2 menuOpenPos;
    private Vector2 menuClosedPos;
    private RectTransform menuRect;

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
        if (optionsMenu != null)
        {
            optionsRect = optionsMenu.GetComponent<RectTransform>();
            if (optionsRect == null)
            {
                Debug.LogError("Options menu is missing RectTransform!");
                return;
            }

            optionsClosedPos = optionsRect.anchoredPosition;
            optionsOpenPos = optionsClosedPos + new Vector2(-optionsRect.rect.width, 0);
            // optionsOpenPos = optionsClosedPos + new Vector2(0, 0);
            optionsMenu.SetActive(false); // start closed
        }
        if (menu != null)
        {
            menuRect = menu.GetComponent<RectTransform>();
            if (menuRect == null)
            {
                Debug.LogError("menu is missing RectTransform!");
                return;
            }

            menuClosedPos = menuRect.anchoredPosition;
            menuOpenPos = menuClosedPos + new Vector2(-menuRect.rect.width, 0);
            menu.SetActive(false); // start closed
        }
        if (ritualMenu != null)
        {
            ritualRect = ritualMenu.GetComponent<RectTransform>();
            if (ritualRect == null)
            {
                Debug.LogError("ritual menu is missing RectTransform!");
                return;
            }

            ritualClosedPos = ritualRect.anchoredPosition;
            ritualOpenPos = ritualClosedPos + new Vector2(0, ritualRect.rect.height);
            // ritualOpenPos = ritualClosedPos + new Vector2(0, 0);
            ritualMenu.SetActive(false); // start closed
        }


    }

    void Update()
    {
        // Close menus with Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (menuOpen) CloseMenu();
            if (upgradeOpen) CloseUpgradeMenu();
            if (storeOpen) CloseStoreMenu();
            if (decOpen) CloseDecMenu();
            if (ritualOpen) CloseRitualMenu();
        }

        // Detect clicks outside the upgrade menu
        if (upgradeOpen && Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIObject(upgradeRect))
            {
                CloseUpgradeMenu();
            }
        }
        if (menuOpen && Input.GetMouseButtonDown(0))
        {
            GameObject clickedObject = EventSystem.current.currentSelectedGameObject;

            // Ignore if clicked inside menu or on the toggle button
            if (!IsPointerOverUIObject(menu.GetComponent<RectTransform>()) &&
                (clickedObject == null || clickedObject.tag != "MenuButton"))
            {
                CloseMenu();
            }
        }
        if (decOpen && Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIObject(decRect))
                CloseDecMenu();
        }

        if (optionsOpen && Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIObject(optionsRect))
                CloseOptionsMenu();
        }
        if (ritualOpen && Input.GetMouseButtonDown(0))
        {
            if (!IsPointerOverUIObject(ritualRect))
            {
                CloseRitualMenu();
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
        // Smoothly slide ritual menu
        if (ritualRect != null)
        {
            Vector2 target = ritualOpen ? ritualOpenPos : ritualClosedPos;
            ritualRect.anchoredPosition = Vector2.MoveTowards(ritualRect.anchoredPosition, target, slideSpeed * Time.deltaTime);

            // Optionally deactivate when fully closed
            if (!ritualOpen && Vector2.Distance(ritualRect.anchoredPosition, ritualClosedPos) < 0.1f)
            {
                ritualMenu.SetActive(false);
            }
        }
        if (decRect != null)
        {
            Vector2 target = decOpen ? decOpenPos : decClosedPos;
            decRect.anchoredPosition = Vector2.MoveTowards(decRect.anchoredPosition, target, slideSpeed * Time.deltaTime);

            if (!decOpen && Vector2.Distance(decRect.anchoredPosition, decClosedPos) < 0.1f)
                decMenu.SetActive(false);
        }
        if (optionsRect != null)
        {
            Vector2 target = optionsOpen ? optionsOpenPos : optionsClosedPos;
            optionsRect.anchoredPosition = Vector2.MoveTowards(optionsRect.anchoredPosition, target, slideSpeed * Time.deltaTime);

            if (!optionsOpen && Vector2.Distance(optionsRect.anchoredPosition, optionsClosedPos) < 0.1f)
                optionsMenu.SetActive(false);
        }
        if (menuRect != null)
        {
            Vector2 target = menuOpen ? menuOpenPos : menuClosedPos;
            menuRect.anchoredPosition = Vector2.MoveTowards(menuRect.anchoredPosition, target, slideSpeed * Time.deltaTime);

            if (!menuOpen && Vector2.Distance(menuRect.anchoredPosition, menuClosedPos) < 0.1f)
                menu.SetActive(false);
        }
    }

    // ================= Helper =================
    private bool IsPointerOverUIObject(RectTransform rect)
    {
        return RectTransformUtility.RectangleContainsScreenPoint(rect, Input.mousePosition, null);
    }
    // ================= Menu =================
    public void OpenMenu()
    {
        CloseAllMenus();
        menuOpen = true;
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        menuOpen = false;
    }

    public void ToggleMenu()
    {
        menu.SetActive(true);
        menuOpen = !menuOpen;
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
        Debug.Log("clicked 1");
        CloseAllMenus();
        decMenu.SetActive(true);
        decOpen = true;
    }

    public void CloseDecMenu()
    {
        decOpen = false;
    }

    // public void ToggleDecMenu()
    // {
    //     if (decOpen) CloseDecMenu();
    //     else OpenDecMenu();
    // }

    // ================= Store =================
    public void OpenStoreMenu()
    {
        Debug.Log("clicked 2");
        if (menuOpen) CloseMenu();
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
        if (menuOpen) CloseMenu();
        if (upgradeOpen) CloseUpgradeMenu();
        if (storeOpen) CloseStoreMenu();
        if (decOpen) CloseDecMenu();
        if (ritualOpen) CloseRitualMenu();
    }

    // ================= Options Menu =================

    public void OpenOptionsMenu()
    {
        // Debug.Log("clicked 2");
        CloseAllMenus();
        optionsMenu.SetActive(true);
        optionsOpen = true;
    }
    public void CloseOptionsMenu()
    {
        optionsOpen = false;
    }
    
    public void OpenRitualMenu()
    {
        CloseAllMenus();
        ritualOpen = true;
        ritualMenu.SetActive(true);
    }

    public void CloseRitualMenu()
    {
        ritualOpen = false;
        // panel will slide down and deactivate automatically
    }

    public void ToggleRitualMenu()
    {
        if (ritualOpen) CloseRitualMenu();
        else OpenRitualMenu();
    }

    // public void ToggleOptionsMenu()
    // {
    //     if (optionsOpen) CloseDecMenu();
    //     else OpenDecMenu();
    // }
}
