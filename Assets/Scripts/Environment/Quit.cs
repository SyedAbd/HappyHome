using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // This method can be called when a button is pressed
    public void Quit()
    {
        // This will only work in a built (executable) version of the game
        Application.Quit();

        // If running in the editor, log a message
#if UNITY_EDITOR
        Debug.Log("Quit called in Editor");
#endif
    }
}
