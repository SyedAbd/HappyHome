using System;
using System.Collections;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;
    public Story story;
    public float letterDelay = 0.05f; // Delay between each letter for typewriter effect

    private string currentText;
    private Text storyText; // Single story text box

    void Awake()
    {
        RemoveChildren();
        StartStory();
    }

    void StartStory()
    {
        if (PlayerPrefs.HasKey("InkStoryState"))
        {
            string savedState = PlayerPrefs.GetString("InkStoryState", "");
            story = new Story(inkJSONAsset.text);
            story.state.LoadJson(savedState); // Load saved state
        }
        else
        {
            story = new Story(inkJSONAsset.text);
        }

        OnCreateStory?.Invoke(story);
        RefreshView();
    }

    void RefreshView()
    {
        RemoveChildren();

        // Create and set up the single text box for displaying story content
        storyText = Instantiate(textPrefab) as Text;
        storyText.transform.SetParent(canvas.transform, false);
        storyText.text = ""; // Start with an empty text box

        StartCoroutine(DisplayStorySection());
    }

    IEnumerator DisplayStorySection()
    {
        storyText.text = ""; // Clear previous section's text

        // Continue story to get the next section and reveal it line-by-line
        while (story.canContinue)
        {
            string nextLine = story.Continue().Trim(); // Get the next line of the current section
            yield return StartCoroutine(RevealText(nextLine));

            // Add a line break between story lines to enhance readability
            storyText.text += "\n";
            yield return new WaitForSeconds(0.3f); // Pause briefly between lines
        }

        // Display choices if there are any at the end of the story section
        if (story.currentChoices.Count > 0)
        {
            foreach (Choice choice in story.currentChoices)
            {
                Button choiceButton = CreateChoiceView(choice.text.Trim());
                choiceButton.onClick.AddListener(delegate { OnClickChoiceButton(choice); });
            }
        }
        else
        {
            Button restartButton = CreateChoiceView("End of story.\nRestart?");
            restartButton.onClick.AddListener(delegate { RestartStory(); });
        }
    }

    IEnumerator RevealText(string line)
    {
        foreach (char letter in line)
        {
            storyText.text += letter; // Append one letter at a time
            yield return new WaitForSeconds(letterDelay); // Wait for the set delay
        }
    }

    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        SaveStoryState();
        RefreshView();
    }

    void RestartStory()
    {
        PlayerPrefs.DeleteKey("InkStoryState");
        StartStory();
    }

    void SaveStoryState()
    {
        string stateJson = story.state.ToJson();
        PlayerPrefs.SetString("InkStoryState", stateJson);
        PlayerPrefs.Save();
    }

    Button CreateChoiceView(string text)
    {
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(canvas.transform, false);
        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;
        return choice;
    }

    void RemoveChildren()
    {
        int childCount = canvas.transform.childCount;
        for (int i = childCount - 1; i >= 0; --i)
        {
            Destroy(canvas.transform.GetChild(i).gameObject);
        }
    }

    [SerializeField]
    private TextAsset inkJSONAsset = null;
    [SerializeField]
    private Canvas canvas = null;
    [SerializeField]
    private Text textPrefab = null;
    [SerializeField]
    private Button buttonPrefab = null;
}
