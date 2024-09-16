using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Add this to use TextMeshPro

public class InteractItem : MonoBehaviour
{
    [SerializeField] GameObject player; // Reference to the player
    [SerializeField] TextMeshProUGUI interactionText; // Reference to TMP for UI text
    private bool isPickedUp;
    private bool isInRange; // To check if the player is in range to pick up the item
    private Vector2 speed;
    public float smoothTime;

    void Start()
    {
        isPickedUp = false;
        isInRange = false;
        interactionText.enabled = false; // Hide the message initially
    }

    void Update()
    {
        if (isInRange && !isPickedUp)
        {
            interactionText.text = "Press E to pick up the key";
            interactionText.enabled = true; // Show the message when the player is in range

            if (Input.GetKeyDown(KeyCode.E))
            {
                isPickedUp = true;
                interactionText.enabled = false; // Hide the message once the key is picked up
            }
        }

        if (isPickedUp)
        {
            Vector3 offset = new Vector3(0, 1.5f, 0);
            transform.position = Vector2.SmoothDamp(transform.position, player.transform.position + offset, ref speed, smoothTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isPickedUp)
        {
            isInRange = true; // Player is in range
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isPickedUp)
        {
            isInRange = false; // Player is out of range
            interactionText.enabled = false; // Hide the message when the player leaves
        }
    }
}
