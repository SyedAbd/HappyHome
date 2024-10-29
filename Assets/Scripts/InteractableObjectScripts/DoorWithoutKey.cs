using UnityEngine;
using TMPro;  // Required for TextMesh Pro

public class DoorWithoutKey : MonoBehaviour
{
    public enum Room
    {
        Hallway,
        Bedroom,
        LivingRoom,
        Bathroom
    }

    [SerializeField] private Room targetRoom;  // Dropdown for room selection
    [SerializeField] private PlayerManager playerManager;  // Drag the PlayerManager here in the Inspector
    [SerializeField] private float interactionDistance = 2f;  // Distance within which the player can interact with the door
    [SerializeField] private TextMeshProUGUI instructionText;  // TextMesh Pro text for instructions

    private Transform player;
    private bool playerInRange;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;  // Finds player based on "Player" tag
        instructionText.text = "";  // Clear any initial text
    }

    void Update()
    {
        // Check if player is close enough and presses "E"
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            TransportPlayer();
        }
    }

    private void TransportPlayer()
    {
        // Call the appropriate method on the PlayerManager based on the selected room
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
            default:
                Debug.LogWarning("Invalid room specified in DoorWithoutKey script");
                break;
        }

        // Hide the instruction text once transported
        instructionText.text = "";
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            instructionText.text = $"Press E to go to the {targetRoom}";  // Display room-specific instruction
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            instructionText.text = "";  // Clear instruction when player leaves range
        }
    }
}
