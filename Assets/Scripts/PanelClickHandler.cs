using UnityEngine;
using UnityEngine.UI;

public class PanelClickHandler : MonoBehaviour
{
    public DialogueManager targetScript; // Reference to the script containing the 'skipTextOnClick' variable

    private void Start()
    {
        // Ensure the component has a Button, and add the click listener
        Button panelButton = GetComponent<Button>();
        if (panelButton != null)
        {
            panelButton.onClick.AddListener(OnPanelClick);
        }
        else
        {
            Debug.LogError("PanelClickHandler requires a Button component on the same GameObject.");
        }
    }

    public void OnPanelClick()
    {
        if (targetScript != null)
        {
            targetScript.skipTextOnClick = true; // Set the variable to true
        }
        else
        {
            Debug.LogError("Target script is not assigned in the PanelClickHandler.");
        }
    }
}
