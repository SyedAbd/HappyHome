using UnityEngine;

public class SpawnPointMovement : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float followSpeed = 5f; // Speed at which the spawn point follows the player

    private void Update()
    {
        if (player != null)
        {
            // Calculate the new position by interpolating between the current position and the player's position
            Vector3 newPosition = Vector3.Lerp(transform.position, player.position, followSpeed * Time.deltaTime);
            transform.position = newPosition;
        }
        else
        {
            Debug.LogWarning("Player transform is not assigned!");
        }
    }
}
