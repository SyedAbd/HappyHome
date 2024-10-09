using Ink.Runtime;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
// using TMPro;

public class StoryScript : MonoBehaviour
{
    public TextAsset inkJSON;
    private Story story;

    public Text textPrefab;
    public Button buttonPrefab;

    private Text storyText;

    // Start is called before the first frame update
    void Start()
    {
        story = new Story(inkJSON.text);
        //storyText = Instantiate(textPrefab) as Text;
        refreshUI();

    }

    void refreshUI()
    {
        eraseUI();

        // Instantiate and display the current story chunk
        storyText = Instantiate(textPrefab) as Text;
        //storyText.text = loadStoryChunk();
        storyText.text = "";
        string text = loadStoryChunk();
        StartCoroutine(WriteTextSlowly(text));
        
        
        storyText.transform.SetParent(this.transform, false);

        
    }
    void CreateButtons()
    {

        // Create a button for each available choice
        foreach (Choice choice in story.currentChoices)
        {
            // Instantiate a new button
            Button choiceButton = Instantiate(buttonPrefab) as Button;
            choiceButton.transform.SetParent(this.transform, false);

            // Find the TextMeshProUGUI component in the button prefab
            Text choiceText = choiceButton.GetComponentInChildren<Text>();

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
    IEnumerator WriteTextSlowly(string text)
    {
        for(int i = 0;i < text.Length; i++)
        {
            storyText.text += text[i];
            yield return new WaitForSeconds(0.005f);
        }

        CreateButtons();

    }
}