using NUnit.Framework;
using UnityEditor.Build.Content;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject optionsMenu;
    public GameObject upgradeMenu;
    public bool optionsOpen = false;
    public bool upgradeOpen = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsOpen) CloseOptionsMenu();
            if (upgradeOpen) CloseUpgradeMenu();

        }
    }
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
        optionsOpen = !optionsOpen; // flip the state
        optionsMenu.SetActive(optionsOpen);
    }
    public void OpenUpgradeMenu()
    {
        if (optionsOpen) CloseOptionsMenu();

        upgradeOpen = true;
        upgradeMenu.SetActive(true);
    }
    public void CloseUpgradeMenu()
    {
        upgradeOpen = false;
        upgradeMenu.SetActive(false);
    }
    public void ToggleUpgradeMenu()
    {
        if (upgradeOpen) CloseUpgradeMenu();
        else OpenUpgradeMenu();
    }
}