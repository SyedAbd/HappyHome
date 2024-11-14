using UnityEngine;
using TMPro;

public class FlashlightPickup : MonoBehaviour
{
    public GameObject player;                // Reference to the player
    public GameObject flashlightLight;       // Reference to the flashlight's light (Light component or the object to be enabled)
    public TextMeshProUGUI interactionText;  // Reference to the TextMeshProUGUI for interaction text

    private bool isPickedUp = false;
    private bool isInRange = false;
    private Renderer flashlightRenderer;     // To hide the flashlight object
    private Collider2D flashlightCollider;   // To disable interaction after picking it up
    private bool isFlashlightOn = false;     // To track the flashlight's on/off state

    [Header("Flashlight Toggle Settings")]
    public GameObject flashLightStatus;      // UI element or object that shows flashlight status
    [SerializeField] private AudioClip toggleSound;  // Sound clip for toggling the flashlight
    private AudioSource audioSource;         // Audio source to play toggle sound

    void Start()
    {
        flashlightRenderer = GetComponent<Renderer>();
        flashlightCollider = GetComponent<Collider2D>();

        audioSource = gameObject.AddComponent<AudioSource>(); // Add AudioSource to this GameObject
        audioSource.clip = toggleSound;
        audioSource.playOnAwake = false;

        interactionText.text = "";  // Start with interaction text hidden by setting it to an empty string
    }

    void Update()
    {
        if (isInRange && !isPickedUp)
        {
            interactionText.text = "Press E to pick up the flashlight";

            if (Input.GetKeyDown(KeyCode.E))
            {
                PickUpFlashlight();
                flashLightStatus.gameObject.SetActive(true);
            }
        }
        else if (!isInRange || isPickedUp)
        {
            //interactionText.text = "";  // Clear the interaction text when out of range or after picking it up
        }

        // Toggle flashlight if picked up and "F" key is pressed
        if (isPickedUp && Input.GetKeyDown(KeyCode.F))
        {
            ToggleFlashlight();
        }
    }

    void PickUpFlashlight()
    {
        isPickedUp = true;
        flashlightLight.SetActive(false);  // Start with the flashlight off
        flashlightRenderer.enabled = false;  // Hide the flashlight object
        flashlightCollider.enabled = false;  // Disable further interaction
    }

    void ToggleFlashlight()
    {
        isFlashlightOn = !isFlashlightOn;
        flashlightLight.SetActive(isFlashlightOn);  // Enable or disable the light
        audioSource.Play();                         // Play toggle sound
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            interactionText.text = "";  // Clear the text when player exits the range
        }
    }
}
