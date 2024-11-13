using UnityEngine;

public class FollowPlayerToShowInteracteIcon : MonoBehaviour
{
    public RectTransform uiElement;      // The UI element to position (e.g., a Canvas animation)
    public Transform targetObject;       // The in-game object to follow
    public Vector2 offset;               // Offset in screen space (optional)

    private Camera activeCamera;

    private void Start()
    {
        FindActiveMainCamera();
    }

    private void Update()
    {
        if (uiElement != null && targetObject != null)
        {
            // Check for active camera each frame in case it has changed
            if (activeCamera == null || !activeCamera.isActiveAndEnabled)
            {
                FindActiveMainCamera();
            }

            if (activeCamera != null)
            {
                // Convert the target object's world position to screen position
                Vector2 screenPosition = activeCamera.WorldToScreenPoint(targetObject.position);

                // Apply the offset and set the UI element’s position
                uiElement.position = screenPosition + offset;
            }
        }
    }

    // Method to find the currently active main camera
    private void FindActiveMainCamera()
    {
        // Find all cameras tagged "MainCamera" and select the active one
        Camera[] mainCameras = Camera.allCameras;
        foreach (Camera cam in mainCameras)
        {
            if (cam.CompareTag("MainCamera") && cam.isActiveAndEnabled)
            {
                activeCamera = cam;
                break;
            }
        }
    }

    // Optionally, call this method to activate the UI element and play an animation
    public void ShowUIElement()
    {
        if (uiElement != null)
        {
            uiElement.gameObject.SetActive(true);

            // Assuming the UI element has an Animator attached, play the animation
            Animator animator = uiElement.GetComponent<Animator>();
            if (animator != null)
            {
                animator.Play("YourAnimationName");  // Replace with your actual animation name
            }
        }
    }

    // Optionally, call this method to hide the UI element
    public void HideUIElement()
    {
        if (uiElement != null)
        {
            uiElement.gameObject.SetActive(false);
        }
    }
}
