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
   // public GameObject flashLightPanelToBeEnabled;
    public float letterDelay = 0.01f; // Delay between each letter for typewriter effect
    public string firstMsg = "Jamie has given you a flashlight. Point it towards things that make you feel scared to make them go away. You can control the light with your mouse. The meter at the corner of the screen shows how scared you are. If you get too scared, you will pass out."; // First message text
    public string secondMsg = "You can toggle the flashlight with F. Be careful to not let it overheat."; // Second message text

    private bool playerInTrigger = false;

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
        
        // Step 1: Pause the game
        Time.timeScale = 0;

        // Step 2: Show initial message letter by letter
        yield return ShowTextLetterByLetter(initialMessageText, "Aah! A spider!", initialMessageDuration);
        yield return new WaitForSecondsRealtime(0.8f);
        initialMessageText.text = "";




        // Step 3: Activate first screen and wait for it to be deactivated
        Time.timeScale = 1;
        cutScenePanel.SetActive(true);
        yield return new WaitUntil(() => !cutScenePanel.activeSelf);
        Time.timeScale = 0;
        flashLightToBeEnabled.SetActive(true);
        //flashLightPanelToBeEnabled.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);




        // Step 4: Show the first message on the reusable screen letter by letter
        textBox.SetActive(true);
        yield return ShowTextLetterByLetter(textBoxText, firstMsg, letterDelay);
        
        yield return new WaitUntil(() => !textBox.activeSelf);
        textBoxText.text = "";






        Time.timeScale = 1;
        
        yield return new WaitUntil(() => !spiderToBeDestroid.activeSelf);

        keyToBeEnabled.SetActive(true);
        Time.timeScale = 0;
        // Step 5: Show the second message on the reusable screen letter by letter
        yield return new WaitForSecondsRealtime(1f);


        yield return ShowTextLetterByLetter(initialMessageText, "See, everything’s fine, I’ve got you. You can keep the flashlight if you want", initialMessageDuration);
        yield return new WaitForSecondsRealtime(4f);
        if(initialMessageText.text == "See, everything’s fine, I’ve got you. You can keep the flashlight if you want")
        initialMessageText.text = "";




        textBox.SetActive(true);
        yield return ShowTextLetterByLetter(textBoxText, secondMsg, letterDelay);
        
        yield return new WaitUntil(() => !textBox.activeSelf);




        yield return ShowTextLetterByLetter(initialMessageText, "I think I should head to bed now…", initialMessageDuration);
        Time.timeScale = 1;
        yield return new WaitForSecondsRealtime(3f);
        if(initialMessageText.text == "I think I should head to bed now…") initialMessageText.text = "";
        // Step 6: Resume the game
        

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

       // yield return new WaitForSecondsRealtime(letterDelay * message.Length); // Keep text for duration based on length
        //textObject.gameObject.SetActive(false);
    }
}
