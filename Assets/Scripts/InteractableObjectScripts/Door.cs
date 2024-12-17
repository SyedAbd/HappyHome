using UnityEngine;
using TMPro;
using System.Collections;

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

    [Header("Audio")]
    [SerializeField] private AudioSource teleportSound; // AudioSource for the teleport sound

    private bool isNearDoor = false;
    private PlayerInventory playerInventory;

    private void Start()
    {
        instructionText.enabled = true;
        instructionText.text = "";
    }

    private void Update()
    {
        if (isNearDoor && Input.GetKeyDown(KeyCode.E))
        {
            if (requiresKey)
            {
                if (playerInventory != null && playerInventory.HasKey(requiredKeyTag.ToString()))
                {
                    playerInventory.UseKey();
                    requiresKey = false;
                    OpenDoor();
                }
                else
                {
                    instructionText.text = instructionTextNoKey;
                }
            }
            else
            {
                OpenDoor();
            }
        }
    }

    private IEnumerator PlaySoundAndFade()
    {
        ScreenFader screenFader = FindObjectOfType<ScreenFader>();

        if (screenFader != null)
        {
            // Start fade and sound simultaneously
            StartCoroutine(screenFader.FadeToBlack());
        }

        if (teleportSound != null)
        {
            teleportSound.Play();
            Debug.Log("Teleport sound played");
        }

        // Wait for the sound to finish or fade duration, whichever is longer
        float waitTime = teleportSound != null ? teleportSound.clip.length : 0;
        yield return new WaitForSeconds(waitTime);

        // Perform the room transition
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

        // Immediately fade back to clear after the transition
        if (screenFader != null)
        {
            yield return screenFader.FadeToClear();
        }
    }

    private void OpenDoor()
    {
        instructionText.text = "";
        StartCoroutine(PlaySoundAndFade());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearDoor = true;
            playerInventory = other.GetComponent<PlayerInventory>();

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

            instructionText.gameObject.SetActive(true);
            instructionText.enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearDoor = false;
            instructionText.text = "";
        }
    }
}


