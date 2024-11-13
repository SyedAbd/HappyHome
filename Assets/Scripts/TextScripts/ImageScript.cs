using UnityEngine;
using UnityEngine.UI;

public class ToggleButtonImage : MonoBehaviour
{
    // Reference to the button's image component
    public Image buttonImage;

    // Reference to the button itself
    private Button button;

    void Start()
    {
        // Get the Button component attached to the same GameObject
        button = GetComponent<Button>();

        // Ensure the image is visible at the start (optional, can adjust as needed)
        buttonImage.enabled = false;

        // Add a listener to call the ToggleImage function when the button is clicked
        button.onClick.AddListener(ToggleImage);
    }

    // Toggle the visibility of the button image
    void ToggleImage()
    {
        buttonImage.enabled = !buttonImage.enabled;
    }
}
