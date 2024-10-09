using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Reference to the player's transform
    public float smoothSpeed = 0.125f;  // Smooth transition speed

    public float minX;  // Minimum X-axis limit for the camera
    public float maxX;  // Maximum X-axis limit for the camera

    private float fixedY;  // The fixed Y position for the camera
    private float fixedZ;  // The fixed Z position for the camera

    void Start()
    {
        // Save the initial Y and Z positions of the camera so they don't change
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        // Get the desired position based on the player's X position, but keep Y and Z fixed
        Vector3 desiredPosition = new Vector3(player.position.x, fixedY, fixedZ);

        // Clamp the desired X position between the minimum and maximum limits
        float clampedX = Mathf.Clamp(desiredPosition.x, minX, maxX);

        // Create the final clamped position
        Vector3 clampedPosition = new Vector3(clampedX, fixedY, fixedZ);

        // Smoothly interpolate between the current camera position and the clamped position
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, clampedPosition, smoothSpeed);

        // Update the camera's position
        transform.position = smoothedPosition;
    }
}
