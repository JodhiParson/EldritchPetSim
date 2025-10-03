using UnityEditor.Build.Content;
using UnityEngine;

public class OptionsMenuController : MonoBehaviour
{
    public GameObject optionsMenu; // drag your panel here in Inspector
    public bool isOpen = false;

    void Update()
    {
        // Only close menu if Esc is pressed AND menu is open
        if (Input.GetKeyDown(KeyCode.Escape) && isOpen)
        {
            ToggleMenu();
        }
    }

    // This function toggles the menu open/close
    public void ToggleMenu()
    {
        isOpen = !isOpen; // flip the state
        optionsMenu.SetActive(isOpen);
    }
}
public class UpgradeMenu : MonoBehaviour
{
    public GameObject upgradesMenu;

    public void OpenMenu()
    {
        upgradesMenu.SetActive(true);
    }
}