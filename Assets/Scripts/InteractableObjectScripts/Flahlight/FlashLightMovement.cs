using UnityEngine;

public class FlashLightMovement : MonoBehaviour
{
    public Transform player;              // Reference to the player Transform
    public float maxAngleOffset = 45f;    // Maximum angle offset from player's forward direction (45 degrees up/down)
    private bool isFacingRight = true;    // Track the player's facing direction

    void Update()
    {
        // Get player's movement input
        float horizontalInput = Input.GetAxisRaw("Horizontal");

        // Update facing direction based on input
        if (horizontalInput > 0)
        {
            isFacingRight = true;
        }
        else if (horizontalInput < 0)
        {
            isFacingRight = false;
        }

        // Set angle limits based on the direction the player is facing
        float minAngle, maxAngle;
        if (isFacingRight)
        {
            minAngle = 315f; // Right-facing angle range (315° to 45°)
            maxAngle = 45f;
        }
        else
        {
            minAngle = 135f; // Left-facing angle range (135° to 225°)
            maxAngle = 225f;
        }

        // Get the direction vector from player to the mouse position in world space
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
        Vector3 direction = worldMousePosition - player.position;

        // Calculate the angle between player and the mouse position
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Adjust the angle to stay within the set range
        angle = ClampAngleToRange(angle, minAngle, maxAngle);

        // Set the flashlight rotation
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    // Helper function to clamp angles within specified min and max range
    private float ClampAngleToRange(float angle, float min, float max)
    {
        // Normalize angle to range [0, 360)
        angle = (angle + 360) % 360;

        // Handle wrapping for right-facing (315° to 45°)
        if (min > max)
        {
            if (angle > 180)
                return Mathf.Max(angle, min);
            else
                return Mathf.Min(angle, max);
        }

        // Standard clamping for left-facing (135° to 225°)
        return Mathf.Clamp(angle, min, max);
    }
}
