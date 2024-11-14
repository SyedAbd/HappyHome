using UnityEngine;
using TMPro;
using System.Collections;

public class SpiderEncounter : MonoBehaviour
{
    [SerializeField] private GameObject flashlightObject; // The object to check if it's active
    [SerializeField] private TextMeshProUGUI messageText; // The UI text component to show the message
    [SerializeField] private float letterDelay = 0.05f; // Delay between each letter when typing
    [SerializeField] private float messageDisplayTime = 1.5f; // Time to display the full message before disappearing

    private Coroutine displayCoroutine;

    void Start()
    {
        messageText.text = ""; // Ensure the message starts off hidden
        messageText.gameObject.SetActive(false); // Hide the text at the start
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Stop any ongoing message display
            if (displayCoroutine != null)
            {
                StopCoroutine(displayCoroutine);
            }

            // Determine the message based on flashlight's active status
            string message = flashlightObject.activeSelf
                ? "Oh no, a spider! I need to find the flashlight to make it go away"
                : "Oh no, a spider! Good thing I have a flashlight...";

            // Start the coroutine to display the message
            displayCoroutine = StartCoroutine(DisplayMessageLetterByLetter(message));
        }
    }

    private IEnumerator DisplayMessageLetterByLetter(string message)
    {
        messageText.gameObject.SetActive(true); // Show the text UI
        messageText.text = ""; // Clear the text

        // Display each letter one by one with a delay
        foreach (char letter in message)
        {
            messageText.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }

        // Wait for a moment before hiding the message
        yield return new WaitForSeconds(messageDisplayTime);
        messageText.text = ""; // Clear the text
        messageText.gameObject.SetActive(false); // Hide the text UI
    }
}
