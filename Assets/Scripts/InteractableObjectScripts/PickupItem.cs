using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupItem : MonoBehaviour
{
    [SerializeField] protected GameObject player; 
    [SerializeField] protected TextMeshProUGUI instructionText; 
    protected bool isPickedUp;
    protected bool isInRange;
    protected Vector2 speed;
    public float smoothTime;
    protected Vector3 initialPosition; 

    protected virtual void Start()
    {
        isPickedUp = false;
        isInRange = false;
        //interactionText.enabled = false; // Hide the message initially
        instructionText.text = "";
        initialPosition = transform.position; // Store the initial position of the item
    }

    protected virtual void Update()
    {
        if (player == null || instructionText == null)
        {
            Debug.LogError("Player or InteractionText is not assigned in the Inspector.");
            return;  // Exit early if either is missing
        }
        if (isInRange && !isPickedUp)
        {
            instructionText.text = "Press E/R to pick & Drop the item";
            //instructionText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                isPickedUp = true;
                player.GetComponent<PlayerInventory>().PickUpKey(gameObject,tag);
                
                //instructionText.gameObject.SetActive(false);
                instructionText.text = "";
            }
        }

        if (isPickedUp)
        {
            CarryItem();

            // Check if the player wants to drop the item by pressing 'R'
            if (Input.GetKeyDown(KeyCode.R))
            {
                DropItem();
                player.GetComponent<PlayerInventory>().DropKey();
            }
        }
    }

    protected virtual void CarryItem()
    {
        
        Vector3 offset = new Vector3(0, 1.5f, 0);
        transform.position = Vector2.SmoothDamp(transform.position, player.transform.position + offset, ref speed, smoothTime);
    }

    protected virtual void DropItem()
    {
        // Return item to its initial position
        transform.position = player.transform.position;
        isPickedUp = false; 
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isPickedUp)
        {
            isInRange = true; 
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isPickedUp)
        {
            isInRange = false;
            //instructionText.gameObject.SetActive(false);
            instructionText.text = "";
        }
    }
}
