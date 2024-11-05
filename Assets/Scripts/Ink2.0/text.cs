using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

[System.Serializable]
public class TextData
{
    public string text;
}

public class TypewriterEffectFromJSON : MonoBehaviour
{
    public Text uiText; // For standard UI Text component, change to TextMeshProUGUI if using TextMeshPro
    public string jsonFileName = "TextData"; // Name of JSON file in Resources folder, without extension
    public float delay = 0.1f; // Delay between each character

    private string fullText;
    private string currentText = "";

    void Start()
    {
        LoadTextFromJSON();
        StartCoroutine(ShowText());
    }

    void LoadTextFromJSON()
    {
        TextData textData = new TextData();

        // Load the JSON file from Resources
        TextAsset jsonTextFile = Resources.Load<TextAsset>(jsonFileName);

        if (jsonTextFile != null)
        {
            textData = JsonUtility.FromJson<TextData>(jsonTextFile.text);
            fullText = textData.text; // Store the full text from JSON
            uiText.text = ""; // Clear the text field initially
        }
        else
        {
            Debug.LogError("JSON file not found in Resources!");
        }
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i <= fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i);
            uiText.text = currentText;
            yield return new WaitForSeconds(delay); // Wait before showing the next character
        }
    }
}