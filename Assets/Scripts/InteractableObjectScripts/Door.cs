using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string targetScene; // Set the target scene name in the Inspector
    public string nextRoomText; // Set the tooltip text in the Inspector, e.g., "Go to the Living Room"

    private bool isHovering = false;

    void OnMouseOver()
    {
        isHovering = true;
    }

    void OnMouseExit()
    {
        isHovering = false;
    }

    void OnMouseDown()
    {
        if (!string.IsNullOrEmpty(targetScene))
        {
            SceneManager.LoadScene(targetScene);
        }
    }

    void OnGUI()
    {
        if (isHovering)
        {
            Vector3 mousePosition = Input.mousePosition;
            GUI.Label(new Rect(mousePosition.x + 15, Screen.height - mousePosition.y, 200, 20), nextRoomText);
        }
    }
}
