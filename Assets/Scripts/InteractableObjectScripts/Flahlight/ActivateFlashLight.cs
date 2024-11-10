using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FlashlightToggleWithCooldown : MonoBehaviour
{
    [SerializeField] private GameObject flashlight;          
    [SerializeField] private TextMeshProUGUI promptText;     
    [SerializeField] private Image batteryFillImage;         

    [SerializeField] private float displayDuration = 3f;     
    [SerializeField] private float maxUsageTime = 5f;        
    [SerializeField] private float depletionRate = 0.1f;     
    [SerializeField] private float rechargeRate = 0.05f;     

    private bool isFlashlightActive = false;                 
    private float displayTimer;                              

    void OnEnable()
    {
        // Reset timers and show the initial message
        displayTimer = displayDuration;
        promptText.text = "Press F to activate/deactivate flashlight";
        batteryFillImage.fillAmount = 1f; // Start battery fully charged
    }

    void Update()
    {
        // Handle the initial prompt message display
        if (displayTimer > 0)
        {
            displayTimer -= Time.deltaTime;
            if (displayTimer <= 0)
            {
                promptText.text = ""; // Clear the initial message after the duration
            }
        }

        // Toggle flashlight on/off with F key
        if (Input.GetKeyDown(KeyCode.F))
        {
            isFlashlightActive = !isFlashlightActive;
            flashlight.SetActive(isFlashlightActive);
        }

        // Adjust battery fill based on flashlight state
        if (isFlashlightActive)
        {
            // Deplete battery while flashlight is active
            batteryFillImage.fillAmount -= depletionRate * Time.deltaTime;

            // Turn off flashlight if battery is depleted
            if (batteryFillImage.fillAmount <= 0)
            {
                batteryFillImage.fillAmount = 0;
                flashlight.SetActive(false);
                isFlashlightActive = false;
                promptText.text = "Battery empty, recharging...";
            }
        }
        else
        {
            // Recharge battery when flashlight is inactive
            batteryFillImage.fillAmount += rechargeRate * Time.deltaTime;
            if (batteryFillImage.fillAmount >= 1f)
            {
                batteryFillImage.fillAmount = 1f;
            }
        }
    }
}
