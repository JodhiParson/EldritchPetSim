using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public void ToggleFullscreen()
    {
        if (Screen.fullScreen)
        {
            // Exit fullscreen → go windowed
            Screen.fullScreenMode = FullScreenMode.Windowed;
            Screen.fullScreen = false;
            Debug.Log("Exited fullscreen mode.");
        }
        else
        {
            // Enter fullscreen → use borderless fullscreen window
            Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
            Screen.fullScreen = true;
            Debug.Log("Entered fullscreen mode.");
        }
    }
}
