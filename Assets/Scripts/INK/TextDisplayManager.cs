using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextDisplayManager : MonoBehaviour
{
    public Text uiText; // Reference to the Text component
    public string[] dialogueLines; // Array of dialogue lines
    private int currentLine;
    public float typingSpeed = 0.05f; // Time between each letter

    private CheckpointManager checkpointManager;

    void Start()
    {
        // Load the saved progress
        checkpointManager = FindObjectOfType<CheckpointManager>();
        currentLine = checkpointManager.textProgressIndex;
        StartCoroutine(TypeTextFrom(currentLine));
    }

    IEnumerator TypeTextFrom(int lineIndex)
    {
        foreach (char letter in dialogueLines[lineIndex].ToCharArray())
        {
            uiText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextLine()
    {
        if (currentLine < dialogueLines.Length - 1)
        {
            currentLine++;
            uiText.text = "";
            checkpointManager.UpdateTextProgress(currentLine);
            StartCoroutine(TypeTextFrom(currentLine));
        }
    }
}
