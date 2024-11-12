using System.Collections;
using UnityEngine;
using TMPro;

public class FearController : MonoBehaviour
{
    [Header("Fear Trigger Settings")]
    public PlayerController playerController;       // Reference to the PlayerController script
    public TextMeshProUGUI fearMessageText;         // UI Text to display the fear message
    public GameObject objectToDeactivate;           // Parent object to deactivate when flashlight points at it
    public float focusTimeToDeactivate = 2f;        // Time the flashlight needs to be pointed at the object

    private bool isPlayerInFearArea = false;        // Tracks if the player is in the fear area
    private bool isFlashlightPointingAtObject = false; // Tracks if flashlight is pointing at the object
    private float flashlightFocusTimer = 0f;        // Timer to track flashlight focus time

    void Start()
    {
        if (fearMessageText != null)
        {
            fearMessageText.text = ""; // Clear fear message at start
        }
    }

    void Update()
    {
        if (isPlayerInFearArea)
        {
            playerController.isInFear = true; // Enable fear in the PlayerController

            // Display message
            if (fearMessageText != null)
            {
                //fearMessageText.text = "You are scared of the spider! Point the flashlight at it to make it go away.";
            }

            // Check if flashlight is focused on object
            if (isFlashlightPointingAtObject)
            {
                flashlightFocusTimer += Time.deltaTime;

                // Deactivate object if focus time exceeds required duration
                if (flashlightFocusTimer >= focusTimeToDeactivate)
                {
                    playerController.isInFear = false;
                    ResetFearState();
                    objectToDeactivate.SetActive(false);
                    
                }
            }
            else
            {
                // Reset the timer if the flashlight moves away
                flashlightFocusTimer = 0f;
            }
        }
        else
        {
            playerController.isInFear = false; // Disable fear in PlayerController
            if (fearMessageText.text == "You are scared of the spider! Point the flashlight at it to make it go away.")
            {
                fearMessageText.text = ""; // Clear fear message when player leaves area
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInFearArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ResetFearState();
        }
    }

    // Method to reset fear state when player leaves the fear area
    private void ResetFearState()
    {
        isPlayerInFearArea = false;
        playerController.isInFear = false;
        flashlightFocusTimer = 0f;
        isFlashlightPointingAtObject = false;

        if (fearMessageText != null)
        {
            fearMessageText.text = ""; // Clear fear message
        }
    }

    private void OnDestroy()
    {
        playerController.isInFear = false;
    }

    // Call this method when the flashlight is pointing at the object
    public void SetFlashlightPointingAtObject(bool isPointing)
    {
        isFlashlightPointingAtObject = isPointing;
    }
}
