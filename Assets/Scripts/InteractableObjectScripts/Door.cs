using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public enum Room
    {
        Hallway,
        Bedroom,
        LivingRoom,
        Bathroom
    }
    public enum KeyTags
    {
        BedroomKey,
        HallwayKey,
        BathroomKey,
        LivingroomKey,

    }

    [Header("Room Settings")]
    [SerializeField] private Room targetRoom; // Dropdown menu for room selection
    [SerializeField] private bool requiresKey = false; // Check if door requires a key
    [SerializeField] private KeyTags requiredKeyTag; // Tag for the key needed to unlock this door

    [Header("Instruction Texts")]
    [SerializeField] private string instructionTextWithKey = "Press E to enter "; // Text when door is unlocked
    [SerializeField] private string instructionTextNoKey = "The door is locked."; // Text when door is locked
    [SerializeField] private string instructionTextNoKeyRequired = "Press E to enter "; // Text when no key is required
    [SerializeField] private TextMeshProUGUI instructionText; // TextMeshPro for displaying instructions

    private bool isNearDoor = false;
    private PlayerInventory playerInventory;

    private void Start()
    {
        // Initialize the instruction text UI
        //instructionText.gameObject.SetActive(false);
        
        instructionText.enabled = true;
        instructionText.text = "";
    }

    private void Update()
    {
        // Check if player is near door and presses E to enter
        if (isNearDoor && Input.GetKeyDown(KeyCode.E))
        {
            if (requiresKey)
            {
                // Check if player has the key for this door
                if (playerInventory != null && playerInventory.HasKey(requiredKeyTag.ToString()))
                {
                    playerInventory.UseKey();
                    requiresKey = false;
                    OpenDoor();
                }
                else
                {
                    instructionText.text = instructionTextNoKey; // Update text to show the door is locked
                }
            }
            else
            {
                OpenDoor(); // Open door if no key is required
            }
        }
    }

    private void OpenDoor()
    {
        // Deactivate current instruction text
        //instructionText.gameObject.SetActive(false);
        instructionText.text = "";

        // Call PlayerManager to teleport to the chosen room
        PlayerManager playerManager = FindObjectOfType<PlayerManager>();
        if (playerManager != null)
        {
            switch (targetRoom)
            {
                case Room.Hallway:
                    playerManager.GotoHallway();
                    break;
                case Room.Bedroom:
                    playerManager.GotoBedroom();
                    break;
                case Room.LivingRoom:
                    playerManager.GotoLivingRoom();
                    break;
                case Room.Bathroom:
                    playerManager.GotoBathroom();
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearDoor = true;
            playerInventory = other.GetComponent<PlayerInventory>();

            // Determine which text to show based on key requirement and availability
            if (requiresKey)
            {
                if (playerInventory != null && playerInventory.HasKey(requiredKeyTag.ToString()))
                {
                    instructionText.text = instructionTextWithKey + targetRoom;
                }
                else
                {
                    instructionText.text = instructionTextNoKey;
                }
            }
            else
            {
                instructionText.text = instructionTextNoKeyRequired + targetRoom;
            }

            // Show the instruction text UI
            instructionText.gameObject.SetActive(true);
            instructionText.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearDoor = false;
            //instructionText.gameObject.SetActive(false); // Hide instruction text when player leaves the area
            instructionText.text = "";
        }
    }
}
