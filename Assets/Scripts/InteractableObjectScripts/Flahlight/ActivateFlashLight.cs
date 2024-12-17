using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FlashlightToggleWithCooldown : MonoBehaviour
{
    [SerializeField] private GameObject flashlight;
    [SerializeField] private TextMeshProUGUI promptText;
    [SerializeField] private Image batteryFillImage;
    [SerializeField] private GameObject batteryPanel;

    [SerializeField] private float displayDuration = 3f;
    [SerializeField] private float maxUsageTime = 5f;
    [SerializeField] private float depletionRate = 0.1f;
    [SerializeField] private float rechargeRate = 0.05f;

    [SerializeField] private AudioClip toggleSound;  // Sound clip to play when toggling
    [SerializeField] private AudioSource audioSource; // Reference to the AudioSource

    private bool isFlashlightActive = true;
    private float displayTimer;

    void OnEnable()
    {
        displayTimer = displayDuration;

        batteryPanel.SetActive(true);
        batteryFillImage.fillAmount = 1f; // Start battery fully charged
    }

    void Update()
    {
        if (displayTimer > 0)
        {
            displayTimer -= Time.deltaTime;
            if (displayTimer <= 0)
            {
                promptText.text = ""; // Clear the initial message after the duration
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isFlashlightActive = !isFlashlightActive;
            flashlight.SetActive(isFlashlightActive);

            // Play the toggle sound
            if (toggleSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(toggleSound);
            }
        }

        if (isFlashlightActive)
        {
            batteryFillImage.fillAmount -= depletionRate * Time.deltaTime;

            if (batteryFillImage.fillAmount <= 0)
            {
                batteryFillImage.fillAmount = 0;
                flashlight.SetActive(false);
                isFlashlightActive = false;
            }
        }
        else
        {
            batteryFillImage.fillAmount += rechargeRate * Time.deltaTime;
            if (batteryFillImage.fillAmount >= 1f)
            {
                batteryFillImage.fillAmount = 1f;
            }
        }
    }
}

