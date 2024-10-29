using UnityEngine;
using TMPro;

public class FlashlightPickup : MonoBehaviour
{
    public GameObject player;  // Reference to the player
    public GameObject flashlightLight;  // Reference to the flashlight's light (Light component or the object to be enabled)
    public TextMeshProUGUI interactionText; // Reference to the TextMeshProUGUI for interaction text

    private bool isPickedUp = false;
    private bool isInRange = false;
    private Renderer flashlightRenderer;  // To hide the flashlight object
    private Collider2D flashlightCollider;  // To disable interaction after picking it up

    void Start()
    {
        flashlightRenderer = GetComponent<Renderer>();  
        flashlightCollider = GetComponent<Collider2D>();  
        //interactionText.gameObject.SetActive(false); 
    }

    void Update()
    {
        if (isInRange && !isPickedUp)
        {
            interactionText.text = "Press E to pick up the flashlight"; // Set the interaction text
            interactionText.gameObject.SetActive(true); // Show the interaction text

            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUpFlashlight();
            }
        }
        else if (!isInRange || isPickedUp)
        {
            //interactionText.gameObject.SetActive(false); // Hide the interaction text if not in range or if picked up
        }
    }

    void PickUpFlashlight()
    {
        isPickedUp = true;
        flashlightLight.SetActive(true);  // Enable the flashlight when picked up

        // Optionally, you can attach the light to the player here
        // flashlightLight.transform.SetParent(player.transform);
        // flashlightLight.transform.localPosition = new Vector3(0, 1.5f, 0);  // Adjust position (above player’s head)

        flashlightRenderer.enabled = false;  // Disable the flashlight renderer (hide the object)
        flashlightCollider.enabled = false;  // Disable the collider (no further interaction)
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;  // Player is in range to pick up the flashlight
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            //interactionText.gameObject.SetActive(false);// Player is out of range
        }
    }
}
