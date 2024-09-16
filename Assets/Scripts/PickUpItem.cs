using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupItem : MonoBehaviour
{
    [SerializeField] protected GameObject player; // Reference to the player
    [SerializeField] protected TextMeshProUGUI interactionText; // Reference to TMP for UI text
    protected bool isPickedUp;
    protected bool isInRange;
    protected Vector2 speed;
    public float smoothTime;
    protected Vector3 initialPosition; // To store the initial position of the item

    protected virtual void Start()
    {
        isPickedUp = false;
        isInRange = false;
        interactionText.enabled = false; // Hide the message initially
        initialPosition = transform.position; // Store the initial position of the item
    }

    protected virtual void Update()
    {
        if (isInRange && !isPickedUp)
        {
            interactionText.text = "Press E to pick up the item";
            interactionText.enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                isPickedUp = true;
                interactionText.enabled = false; // Hide the message once the item is picked up
            }
        }

        if (isPickedUp)
        {
            CarryItem();

            // Check if the player wants to drop the item by pressing 'R'
            if (Input.GetKeyDown(KeyCode.R))
            {
                DropItem();
            }
        }
    }

    protected virtual void CarryItem()
    {
        // Move the item with the player
        Vector3 offset = new Vector3(0, 1.5f, 0);
        transform.position = Vector2.SmoothDamp(transform.position, player.transform.position + offset, ref speed, smoothTime);
    }

    protected virtual void DropItem()
    {
        // Return item to its initial position
        transform.position = player.transform.position;
        isPickedUp = false; // Allow the player to pick it up again
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isPickedUp)
        {
            isInRange = true; // Player is in range to pick up the item
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isPickedUp)
        {
            isInRange = false; // Player is out of range
            interactionText.enabled = false; // Hide the message
        }
    }
}
