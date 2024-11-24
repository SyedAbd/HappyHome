using System.Collections;
using UnityEngine;
using TMPro;

public class FearController : MonoBehaviour
{
    [Header("Fear Trigger Settings")]
    public PlayerController playerController;       // Reference to the PlayerController script
    public TextMeshProUGUI fearMessageText;         // UI Text to display the fear message
    public GameObject objectToDeactivate;           // Parent object to deactivate when flashlight points at it
    public SpriteRenderer objectSprite;             // Reference to the parent object's SpriteRenderer
    public float focusTimeToDeactivate = 2f;        // Time the flashlight needs to be pointed at the object
    public float blinkSpeed = 1f;                   // Speed of the blinking effect

    private bool isPlayerInFearArea = false;        // Tracks if the player is in the fear area
    private bool isFlashlightPointingAtObject = false; // Tracks if flashlight is pointing at the object
    private float flashlightFocusTimer = 0f;        // Timer to track flashlight focus time
    private Coroutine blinkCoroutine = null;

    void Start()
    {
        if (fearMessageText != null)
        {
            fearMessageText.text = ""; // Clear fear message at start
        }
    }

    void Update()
    {
        if (isPlayerInFearArea)
        {
            playerController.isInFear = true;

            if (isFlashlightPointingAtObject)
            {
                flashlightFocusTimer += Time.deltaTime;

                if (flashlightFocusTimer >= focusTimeToDeactivate)
                {
                    playerController.isInFear = false;
                    ResetFearState();
                    objectToDeactivate.SetActive(false);
                }
            }
            else
            {
                flashlightFocusTimer = 0f;
            }
        }
        else
        {
            playerController.isInFear = false;

            if (fearMessageText.text == "You are scared of the spider! Point the flashlight at it to make it go away.")
            {
                fearMessageText.text = "";
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInFearArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ResetFearState();
        }
    }

    private void ResetFearState()
    {
        isPlayerInFearArea = false;
        playerController.isInFear = false;
        flashlightFocusTimer = 0f;
        isFlashlightPointingAtObject = false;

        if (fearMessageText != null)
        {
            fearMessageText.text = "";
        }

        if (blinkCoroutine != null)
        {
            StopCoroutine(blinkCoroutine);
            blinkCoroutine = null;
        }

        if (objectSprite != null)
        {
            SetAlpha(1f); // Restore alpha to 100%
        }
    }

    public void SetFlashlightPointingAtObject(bool isPointing)
    {
        isFlashlightPointingAtObject = isPointing;

        if (isPointing && isPlayerInFearArea)
        {
            if (blinkCoroutine == null && objectSprite != null)
            {
                blinkCoroutine = StartCoroutine(BlinkSprite());
            }
        }
        else
        {
            if (blinkCoroutine != null)
            {
                StopCoroutine(blinkCoroutine);
                blinkCoroutine = null;
            }

            if (objectSprite != null)
            {
                SetAlpha(1f); // Restore alpha to 100%
            }
        }
    }

    private IEnumerator BlinkSprite()
    {
        while (true)
        {
            for (float alpha = 0.2f; alpha <= 0.95f; alpha += Time.deltaTime * blinkSpeed)
            {
                SetAlpha(alpha);
                yield return null;
            }

            for (float alpha = 0.95f; alpha >= 0.2f; alpha -= Time.deltaTime * blinkSpeed)
            {
                SetAlpha(alpha);
                yield return null;
            }
        }
    }

    private void SetAlpha(float alpha)
    {
        if (objectSprite != null)
        {
            Color color = objectSprite.color;
            color.a = alpha;
            objectSprite.color = color;
        }
    }
}
