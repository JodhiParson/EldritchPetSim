using UnityEngine;

public class FullscreenToggle : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F11))
        {
            ToggleFullscreen();
        }
    }

    public void ToggleFullscreen()
    {
        // Toggle fullscreen on/off
        Screen.fullScreen = !Screen.fullScreen;

        // Log the current state
        if (Screen.fullScreen)
        {
            Debug.Log("Entered fullscreen mode.");
        }
        else
        {
            Debug.Log("Exited fullscreen mode.");
        }
    }
}
