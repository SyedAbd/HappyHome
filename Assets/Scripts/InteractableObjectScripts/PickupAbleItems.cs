using UnityEngine;
using UnityEngine.SceneManagement;

public class DraggableObject : MonoBehaviour
{
    private bool isDragging = false;
    private Vector3 originalPosition;
    private Vector3 screenPoint;
    private Vector3 offset;

    private void Start()
    {
        // Store the original position of the object
        originalPosition = transform.position;
    }

    private void OnMouseDown()
    {
        // If the object is not currently being dragged
        if (!isDragging)
        {
            // Move to the top-left corner when clicked
            transform.position = new Vector3(0, Screen.height, 0);
            transform.position = Camera.main.ScreenToWorldPoint(transform.position);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
        }

        // Begin dragging when the mouse is pressed and held
        isDragging = true;
        screenPoint = Camera.main.WorldToScreenPoint(transform.position);
        offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    private void OnMouseDrag()
    {
        // Allow dragging while the mouse is held down
        if (isDragging)
        {
            Vector3 cursorScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorScreenPoint) + offset;
            transform.position = new Vector3(cursorPosition.x, cursorPosition.y, 0);
        }
    }

    private void OnMouseUp()
    {
        // Stop dragging when the mouse is released
        if (isDragging)
        {
            isDragging = false;

            // Check if the object is in the top-left corner
            if (transform.position.x == 0 && transform.position.y == Screen.height)
            {
                // Load the next scene if the object is released in the corner
                SceneManager.LoadScene("NextSceneName"); // Change to your scene name
            }
            else
            {
                // Return to original position if not picked up
                transform.position = originalPosition;
            }
        }
    }

    private void OnEnable()
    {
        // Reset the original position when the object is enabled
        originalPosition = transform.position;
    }
}
