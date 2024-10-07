using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StoryScript : MonoBehaviour
{
    public TextAsset inkJSON;
    private Story story;

    public Text textPrefab;
    public Button buttonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkJSON.text);

        refreshUI();

    }

    void refreshUI()
    {
        eraseUI();

        // Instantiate and display the current story chunk
        Text storyText = Instantiate(textPrefab) as Text;
        storyText.text = loadStoryChunk();
        storyText.transform.SetParent(this.transform, false);

        // Create a button for each available choice
        foreach (Choice choice in story.currentChoices)
        {
            // Instantiate a new button
            Button choiceButton = Instantiate(buttonPrefab) as Button;
            choiceButton.transform.SetParent(this.transform, false);

            // Find the TextMeshProUGUI component in the button prefab
            TextMeshProUGUI choiceText = choiceButton.GetComponentInChildren<TextMeshProUGUI>();

            if (choiceText != null)
            {
                // Set the button text to the corresponding Ink choice text
                choiceText.text = choice.text;
            }
            else
            {
                // Log an error if the TextMeshProUGUI component is missing
                Debug.LogError("TextMeshProUGUI component not found in button prefab.");
            }

            // Add a listener to handle the button click
            choiceButton.onClick.AddListener(delegate {
                chooseStoryChoice(choice);
            });
        }
    }

    void eraseUI()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    void chooseStoryChoice(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        refreshUI();
    }

    // Update is called once per frame
    void Update()
    {

    }

    string loadStoryChunk()
    {
        string text = "";

        if (story.canContinue)
        {
            text = story.ContinueMaximally();
        }

        return text;
    }
}