using UnityEngine;

public class FlashlightPickup : MonoBehaviour
{
    public GameObject player;  // Reference to the player
    public GameObject flashlightLight;  // Reference to the flashlight's light (Light component or the object to be enabled)
    private bool isPickedUp = false;
    private bool isInRange = false;
    private Renderer flashlightRenderer;  // To hide the flashlight object
    private Collider2D flashlightCollider;  // To disable interaction after picking it up

    void Start()
    {
        //flashlightLight.SetActive(false); // Ensure the flashlight is off at the start
        flashlightRenderer = GetComponent<Renderer>();  // Get the renderer of the flashlight object
        flashlightCollider = GetComponent<Collider2D>();  // Get the collider of the flashlight object
    }

    void Update()
    {
        if (isInRange && !isPickedUp && Input.GetKeyDown(KeyCode.E))
        {
            PickUpFlashlight();
        }
    }

    void PickUpFlashlight()
    {
        isPickedUp = true;
        flashlightLight.SetActive(true);  // Enable the flashlight when picked up

        // Attach the light to the player (optional)
        //flashlightLight.transform.SetParent(player.transform);
        //flashlightLight.transform.localPosition = new Vector3(0, 1.5f, 0);  // Adjust position (above player’s head)

        // Make the flashlight object disappear
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
            isInRange = false;  // Player is out of range
        }
    }
}
