using UnityEngine;
using UnityEngine.UI;

public class LetterDelaySlider : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private float minDelay = 0.00f;
    [SerializeField] private float maxDelay = 0.02f;

    private void Start()
    {
        slider.minValue = minDelay;
        slider.maxValue = maxDelay;
        slider.value = maxDelay - dialogueManager.letterDelay;
        slider.onValueChanged.AddListener(UpdateLetterDelay);
    }

    private void UpdateLetterDelay(float value)
    {
        if (dialogueManager != null)
        {
            dialogueManager.letterDelay = maxDelay - value;
        }
    }
}
