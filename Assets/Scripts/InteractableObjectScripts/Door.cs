using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Door : MonoBehaviour
{
    public string targetScene;
    public string instructionTextNoKey = "The door is locked.";
    public string instructionTextWithKey = "Press E to unlock.";
    public string instructionTextNoKeyRequired = "Press E to open the door.";
    public TextMeshProUGUI enterTextUI;
    public bool requiresKey = true;
    private bool isNearDoor = false;
    private PlayerInventory playerInventory;

    void Start()
    {
        enterTextUI.gameObject.SetActive(false);
        enterTextUI.enabled = true;
    }

    void Update()
    {
        if (isNearDoor && Input.GetKeyDown(KeyCode.E))
        {
            if (requiresKey)
            {
                if (playerInventory.HasKey())
                {
                    OpenDoor();
                }
            }
            else
            {
                OpenDoor();
            }
        }
    }

    void OpenDoor()
    {
        if (!string.IsNullOrEmpty(targetScene))
        {
            SceneManager.LoadScene(targetScene);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearDoor = true;
            playerInventory = other.GetComponent<PlayerInventory>();

            if (requiresKey)
            {
                if (playerInventory != null && playerInventory.HasKey())
                {
                    enterTextUI.text = instructionTextWithKey;
                }
                else
                {
                    enterTextUI.text = instructionTextNoKey;
                }
            }
            else
            {
                enterTextUI.text = instructionTextNoKeyRequired;
            }

            enterTextUI.gameObject.SetActive(true);
            enterTextUI.enabled = true; 
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isNearDoor = false;
            enterTextUI.gameObject.SetActive(false);
        }
    }
}
