using System.Collections;
using UnityEngine;
using TMPro;

public class DoorWithoutKey : MonoBehaviour
{
    public string message = "This is the message to display."; // The message to display
    public float letterDelay = 0.05f; // Delay between each letter appearing
    public KeyCode activationKey = KeyCode.E; // Key to press to show the message
    public TextMeshProUGUI messageText; // Reference to the TextMeshProUGUI component

    private bool playerInRange = false;
    private Coroutine messageCoroutine = null; // Keep track of the active coroutine

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Assuming the player has the tag "Player"
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            StopMessageCoroutine(); // Stop showing the message if the player exits the trigger
            messageText.text = ""; // Clear the text
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(activationKey))
        {
            if (messageCoroutine != null)
            {
                StopMessageCoroutine(); // Stop the current coroutine if running
            }
            messageCoroutine = StartCoroutine(DisplayMessage()); // Start a new coroutine
        }
    }

    private IEnumerator DisplayMessage()
    {
        messageText.text = ""; // Clear any previous text

        foreach (char letter in message)
        {
            messageText.text += letter; // Add each letter to the text
            yield return new WaitForSeconds(letterDelay); // Wait for a bit before adding the next letter
        }

        messageCoroutine = null; // Clear the reference when done
    }

    private void StopMessageCoroutine()
    {
        if (messageCoroutine != null)
        {
            StopCoroutine(messageCoroutine); // Stop the active coroutine
            messageCoroutine = null; // Clear the reference
        }
    }
}
