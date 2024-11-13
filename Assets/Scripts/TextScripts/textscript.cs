using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class InkTypewriterEffect : MonoBehaviour
{
    public Text uiText;                 // UI Text component for displaying the story
    public TextAsset inkJSONAsset;       // Drag the Ink JSON file here
    public float letterDelay = 0.05f;    // Delay between each letter

    private Story story;
    private string currentText;

    void Start()
    {
        // Initialize the Ink story from the JSON asset
        story = new Story(inkJSONAsset.text);
        DisplayNextLine(); // Display the first line of the story
    }

    public void DisplayNextLine()
    {
        if (story.canContinue)
        {
            currentText = story.Continue(); // Get the next line from the Ink story
            uiText.text = ""; // Clear the text field
            StopAllCoroutines();
            StartCoroutine(TypeText()); // Start the typewriter effect coroutine
        }
    }

    IEnumerator TypeText()
    {
        for (int i = 0; i <= currentText.Length; i++)
        {
            uiText.text = currentText.Substring(0, i); // Update UI with the current substring
            yield return new WaitForSeconds(letterDelay); // Wait before showing the next character
        }
    }

    void Update()
    {
        // Example: Press the space key to show the next line of text
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DisplayNextLine();
        }
    }
}
