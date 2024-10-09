using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI; // Use Unity's UI.Text for legacy text
using System.Collections;

public class InkTextHandler : MonoBehaviour
{
    public Text textBox;  // Reference to the Legacy Text component
    public TextAsset inkJSONAsset;  // The Ink story file
    public float typingSpeed = 0.05f; // Typing speed (adjust as needed)

    private Story story;   // Story object from Ink
    private string currentText = ""; // Text that is being progressively displayed

    void Start()
    {
        // Initialize the Ink story
        story = new Story(inkJSONAsset.text);

        // Get the first line of text from the Ink story
        string inkText = story.Continue();

        // Display the text using the typing effect
        StartCoroutine(ShowTypingEffect(inkText));
    }

    // Coroutine to display the text with the typing effect
    IEnumerator ShowTypingEffect(string fullText)
    {
        textBox.text = ""; // Clear the text at the start

        // Loop through each character and display it progressively
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            textBox.text = currentText;
            yield return new WaitForSeconds(typingSpeed); // Wait for the typing speed
        }

        // If the Ink story can continue, get the next line and show it
        if (story.canContinue)
        {
            string nextLine = story.Continue();
            StartCoroutine(ShowTypingEffect(nextLine));
        }
    }

    // Optionally, add a method to fetch the next line of text from the Ink story (for custom use)
    public string GetNextLine()
    {
        if (story.canContinue)
        {
            return story.Continue();
        }
        return null;
    }
}
