using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 3f; // How close the player needs to be to interact
    public Transform playerCamera; // Player's camera or the object used for raycasting
    public LayerMask interactableLayer; // Layer mask to filter interactable objects

    private GameObject interactableObject; // The object the player is currently looking at

    // Update is called once per frame
    void Update()
    {
        CheckForInteractable();

        // If the player presses the "E" key and is looking at an interactable object
        if (Input.GetKeyDown(KeyCode.E) && interactableObject != null)
        {
            InteractWithObject();
        }
    }

    // Checks if the player is looking at an interactable object
    void CheckForInteractable()
    {
        RaycastHit hit;
        // Raycast from the player's camera forward
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, interactionRange, interactableLayer))
        {
            interactableObject = hit.collider.gameObject; // Save the interactable object

            // Optional: You can display a UI prompt (e.g., "Press E to interact") here
            Debug.Log("Looking at interactable object: " + interactableObject.name);
        }
        else
        {
            interactableObject = null; // Clear the interactable object when not looking at anything
        }
    }

    // Interact with the object
    void InteractWithObject()
    {
        // You can implement different interactions here (e.g., open door, pick up item)
        Debug.Log("Interacting with: " + interactableObject.name);

        // Example interaction: Destroy the object (you can replace this with any interaction logic)
        // Destroy(interactableObject);

        // Call an interactable object's script (assuming it has one, like "Interactable.cs")
        Interactable interactable = interactableObject.GetComponent<Interactable>();
        if (interactable != null)
        {
            interactable.OnInteract();
        }
    }
}

