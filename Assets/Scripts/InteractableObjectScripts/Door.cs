using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Door : MonoBehaviour
{
    public string targetScene;  // The scene to load when entering the door
    public string instructionTextNoKey = "The door is locked.";  // Message if the player doesn't have the key
    public string instructionTextWithKey = "Press E to unlock."; // Message if the player has the key
    public TextMeshProUGUI enterTextUI;  // Reference to the TextMeshProUGUI component
    private bool isNearDoor = false;  // Tracks if the player is near the door
    private PlayerInventory playerInventory;  // Reference to the player's inventory (script handling key possession)

    void Start()
    {
        // Ensure the instruction text is hidden at the start
        enterTextUI.gameObject.SetActive(false);
        enterTextUI.enabled = true;
    }

    void Update()
    {
        // Check if the player is near the door and presses the 'E' key
        if (isNearDoor && Input.GetKeyDown(KeyCode.E))
        {
            if (playerInventory.HasKey())  // Check if the player has the key
            {
                if (!string.IsNullOrEmpty(targetScene))
                {
                    SceneManager.LoadScene(targetScene);  // Load the specified scene
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the player entered the door's trigger area
        if (other.CompareTag("Player"))
        {
            isNearDoor = true;
            playerInventory = other.GetComponent<PlayerInventory>();  // Get the player's inventory (or key holding)

            // Check if the player has the key
            if (playerInventory != null && playerInventory.HasKey())
            {
                enterTextUI.text = instructionTextWithKey;
            }
            else
            {
                enterTextUI.text = instructionTextNoKey;
            }

            enterTextUI.gameObject.SetActive(true);  // Show the instruction text
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the player exited the door's trigger area
        if (other.CompareTag("Player"))
        {
            isNearDoor = false;
            enterTextUI.gameObject.SetActive(false);  // Hide the instruction text
        }
    }
}
