using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeFromCursor : MonoBehaviour
{
    public float escapeSpeedThreshold = 5.0f; // The speed threshold at which the prefab starts to escape
    public float escapeDistance = 3.0f;       // Distance the prefab will move away
    public float moveSpeed = 10.0f;           // Speed of the prefab's movement

    private Vector3 lastMousePosition;
    private Vector3 targetPosition;
    private bool isEscaping = false;

    void Start()
    {
        // Set the initial last mouse position
        lastMousePosition = Input.mousePosition;
    }

    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        float cursorSpeed = (mousePosition - lastMousePosition).magnitude / Time.deltaTime;

        // Check if the cursor speed is above the threshold to escape
        if (cursorSpeed > escapeSpeedThreshold)
        {
            // Move away from the cursor
            isEscaping = true;
            Vector3 direction = (transform.position - Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane))).normalized;
            targetPosition = transform.position + direction * escapeDistance;
        }
        else
        {
            isEscaping = false;
        }

        // Update prefab position if escaping
        if (isEscaping)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }

        // Update last mouse position for next frame's speed calculation
        lastMousePosition = mousePosition;
    }

    private void OnMouseDown()
    {
        // Load the next scene when the prefab is clicked
        SceneManager.LoadScene("NextScene"); // Replace "NextScene" with your desired scene name
    }
}
