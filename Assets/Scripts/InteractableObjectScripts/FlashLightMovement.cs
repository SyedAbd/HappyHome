using UnityEngine;

public class FlashLightMovement : MonoBehaviour
{
    public Transform player; // Reference to the player Transform
    public float maxAngleOffset = 45f; // Maximum angle offset from the player's forward direction

    void Update()
    {
        // Check if there is an active main camera
        if (Camera.main != null)
        {
            // Get the mouse position in screen space
            Vector3 mousePosition = Input.mousePosition;

            // Convert the mouse position to world space using the active main camera
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.nearClipPlane));
            worldMousePosition.z = 0; // Ensure the z-position is 0

            // Calculate the direction vector from the player to the mouse position
            Vector3 direction = worldMousePosition - player.position;

            // Calculate the angle in degrees
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Determine if the player is facing right (positive x) or left (negative x)
            bool isFacingRight = player.localScale.x >= 0;

            // Adjust the angle based on the player's direction
            if (!isFacingRight)
            {
                // If facing left, invert the angle by adding 180 degrees
                angle += 180f;
            }

            // Calculate the player's facing direction in the local scale
            float playerFacingAngle = isFacingRight ? 0 : 180; // 0 if facing right, 180 if facing left

            // Determine the clamped angle limits based on the player's facing direction
            float minAngle = playerFacingAngle - maxAngleOffset;
            float maxAngle = playerFacingAngle + maxAngleOffset;

            // Normalize the angles to the range [0, 360)
            float normalizedAngle = NormalizeAngle(angle);
            float normalizedMinAngle = NormalizeAngle(minAngle);
            float normalizedMaxAngle = NormalizeAngle(maxAngle);

            // Clamp the angle to the specified range
            float clampedAngle = Mathf.Clamp(normalizedAngle, normalizedMinAngle, normalizedMaxAngle);

            // Set the rotation of the flashlight
            transform.rotation = Quaternion.Euler(0, 0, clampedAngle);
        }
    }

    // Normalize an angle to be within 0 to 360 degrees
    private float NormalizeAngle(float angle)
    {
        while (angle < 0) angle += 360;
        while (angle >= 360) angle -= 360;
        return angle;
    }
}
