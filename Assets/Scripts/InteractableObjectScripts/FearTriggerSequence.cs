using System.Collections;
using UnityEngine;
using TMPro;

public class FearTriggerSequence : MonoBehaviour
{
    public TextMeshProUGUI initialMessageText; // Initial message text object
    public float initialMessageDuration = 0.2f; // Duration to show the initial message
    public GameObject cutScenePanel; // First GameObject to activate
    public GameObject textBox; // Reusable GameObject (second screen)
    public TextMeshProUGUI textBoxText; // Text for the reusable screen
    public GameObject spiderToBeDestroid;
    public GameObject keyToBeEnabled;
    public GameObject flashLightToBeEnabled;
    public float letterDelay = 0.01f; // Delay between each letter for typewriter effect
    public string firstMsg = "Jamie has given you a flashlight. Point it towards things that make you feel scared to make them go away. You can control the light with your mouse. The meter at the corner of the screen shows how scared you are. If you get too scared, you will pass out."; // First message text
    public string secondMsg = "You can toggle the flashlight with F. Be careful to not let it overheat."; // Second message text

    private bool playerInTrigger = false;

    // References to CanvasGroup for fade transitions
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 0.5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !playerInTrigger) // Ensuring only one trigger action per entry
        {
            playerInTrigger = true;
            StartCoroutine(Sequence());
        }
    }

    private IEnumerator Sequence()
    {
        // Step 1: Fade out to prepare for cutscene
        yield return FadeOut();

        // Step 2: Pause the game
        Time.timeScale = 0;

        // Step 3: Show initial message letter by letter
        yield return ShowTextLetterByLetter(initialMessageText, "Aah! A spider!", initialMessageDuration);
        yield return new WaitForSecondsRealtime(0.8f);
        initialMessageText.text = "";

        // Step 4: Activate first screen and wait for it to be deactivated
        Time.timeScale = 1;
        cutScenePanel.SetActive(true);
        yield return new WaitUntil(() => !cutScenePanel.activeSelf);
        Time.timeScale = 0;
        flashLightToBeEnabled.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);

        // Step 5: Show the first message on the reusable screen letter by letter
        textBox.SetActive(true);
        yield return ShowTextLetterByLetter(textBoxText, firstMsg, letterDelay);

        yield return new WaitUntil(() => !textBox.activeSelf);
        textBoxText.text = "";

        // Step 6: Fade out before resuming game
        Time.timeScale = 1;
        yield return FadeOut();

        yield return new WaitUntil(() => !spiderToBeDestroid.activeSelf);

        // Enable the key after spider is destroyed
        keyToBeEnabled.SetActive(true);

        // Step 7: Fade in and show second message
        yield return FadeIn();
        yield return ShowTextLetterByLetter(initialMessageText, "See, everything’s fine, I’ve got you. You can keep the flashlight if you want", initialMessageDuration);
        yield return new WaitForSecondsRealtime(2f);
        if (initialMessageText.text == "See, everything’s fine, I’ve got you. You can keep the flashlight if you want") initialMessageText.text = "";

        textBox.SetActive(true);
        yield return ShowTextLetterByLetter(textBoxText, secondMsg, letterDelay);

        yield return new WaitUntil(() => !textBox.activeSelf);

        // Step 8: Final message and game resume
        yield return ShowTextLetterByLetter(initialMessageText, "I think I should head to bed now…", initialMessageDuration);
        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(3f);
        if (initialMessageText.text == "I think I should head to bed now…") initialMessageText.text = "";

        // Final Fade Out and resume game normally
        yield return FadeOut();
        Time.timeScale = 1;
    }

    private IEnumerator ShowTextLetterByLetter(TextMeshProUGUI textObject, string message, float letterDelay)
    {
        textObject.gameObject.SetActive(true);
        textObject.text = ""; // Clear existing text

        foreach (char letter in message)
        {
            textObject.text += letter; // Add each letter to the text
            yield return new WaitForSecondsRealtime(letterDelay); // Wait for a bit before adding the next letter
        }
    }

    private IEnumerator FadeIn()
    {
        // Fade in using the CanvasGroup alpha
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        fadeCanvasGroup.alpha = 1f;
    }

    private IEnumerator FadeOut()
    {
        // Fade out using the CanvasGroup alpha
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            fadeCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }
        fadeCanvasGroup.alpha = 0f;
    }
}
