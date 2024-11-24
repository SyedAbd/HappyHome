using UnityEngine;
using TMPro;

public class DogInteraction : MonoBehaviour
{
    public TextMeshProUGUI interactionText; // Text to display interaction message
    public GameObject objectToActivate;    // Object to activate on interaction
    public KeyCode interactionKey = KeyCode.E; // Key to interact (default is E)

    private bool isPlayerInRange = false; // Tracks if the player is in range

    void Start()
    {
        if (interactionText != null)
        {
            interactionText.text = ""; // Clear text at start
        }

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(false); // Ensure object is initially inactive
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(interactionKey))
        {
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true); // Activate the object
            }

            if (interactionText != null)
            {
                interactionText.text = ""; // Clear interaction message
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (interactionText != null)
            {
                interactionText.text = "Press E to interact with the dog";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (interactionText != null)
            {
                interactionText.text = ""; // Clear interaction message
            }
        }
    }
}
