using UnityEngine;

public class SwitchToInkFromTutorial : MonoBehaviour
{
    private bool isNearObject = false;
    private PlayerInventory playerInventory;

    private void Start()
    {
        // You can add initialization logic here if needed.
    }

    private void Update()
    {
        // Check if player is near the object and presses E to interact
        if (isNearObject && Input.GetKeyDown(KeyCode.E))
        {
            if (playerInventory != null && playerInventory.HasKey("BedroomKey"))
            {
                // Call the method in GameManager to change the scene to Ink from the Tutorial
                GameManager.Instance.ChnageSceneToInkFromTutorial();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // When the player enters the trigger area, set 'isNearObject' to true and fetch the PlayerInventory component
        if (other.CompareTag("Player"))
        {
            isNearObject = true;
            playerInventory = other.GetComponent<PlayerInventory>();

            // Optionally, you can update UI instructions or add logic for showing key availability here
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // When the player exits the trigger area, set 'isNearObject' to false
        if (other.CompareTag("Player"))
        {
            isNearObject = false;
        }
    }
}
