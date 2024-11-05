using System;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;
    public Story story;
    public float delay = 10f;

    public float letterDelay = 0.05f; // Delay between each letter for typewriter effect

    private string currentText;

    void Awake()
    {
        // Remove the default message
        RemoveChildren();
        StartStory();
    }

    void StartStory()
    {
        // Check if we're restarting or starting fresh
        if (PlayerPrefs.HasKey("InkStoryState"))
        {
            // If we have a saved state, load it
            string savedState = PlayerPrefs.GetString("InkStoryState", "");
            story = new Story(inkJSONAsset.text);
            story.state.LoadJson(savedState); // Load saved state
        }
        else
        {
            // Start a new story from the beginning
            story = new Story(inkJSONAsset.text);
        }

        OnCreateStory?.Invoke(story);
        RefreshView();
    }

    void RefreshView()
    {
        RemoveChildren();

        while (story.canContinue)
        {
            string text = story.Continue();
            text = text.Trim();
            CreateContentView(text);
        }

        if (story.currentChoices.Count > 0)
        {
            for (int i = 0; i < story.currentChoices.Count; i++)
            {
                Choice choice = story.currentChoices[i];
                Button button = CreateChoiceView(choice.text.Trim());
                button.onClick.AddListener(delegate { OnClickChoiceButton(choice); });
            }
        }
        else
        {
            Button choice = CreateChoiceView("End of story.\nRestart?");
            choice.onClick.AddListener(delegate { RestartStory(); });
        }
    }

    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        SaveStoryState(); // Save the state before refreshing

        if (choice.text.Contains("Living Room"))
        {
            GameManager.Instance.roomName = "Livingroom";
            GameManager.Instance.isToMove = true;
            GameManager.Instance.ChnageSceneToRooms();

        }
        else if (choice.text.Contains("Bedroom"))
        {
            GameManager.Instance.roomName = "Bedroom";
            GameManager.Instance.isToMove = true;
            GameManager.Instance.ChnageSceneToRooms();

        }
        else if (choice.text.Contains("Bathroom"))
        {
            GameManager.Instance.roomName = "Bathroom";
            GameManager.Instance.isToMove = true;
            GameManager.Instance.ChnageSceneToRooms();

        }
        else if (choice.text.Contains("Hallway"))
        {
            GameManager.Instance.roomName = "Hallway";
            GameManager.Instance.isToMove = true;
            GameManager.Instance.ChnageSceneToRooms();

        }
        else if (choice.text.Contains("playhouse"))
        {
            Debug.Log("If condition of the Playhouse");
            GameManager.Instance.roomName = "Livingroom";
            GameManager.Instance.isToMove = true;
            GameManager.Instance.ChnageSceneToRooms();
        }

        RefreshView();
    }

    void RestartStory()
    {
        // Clear saved state to start from the beginning
        PlayerPrefs.DeleteKey("InkStoryState");
        StartStory(); // Call StartStory to restart
    }

    void SaveStoryState()
    {
        string stateJson = story.state.ToJson();
        PlayerPrefs.SetString("InkStoryState", stateJson);
        PlayerPrefs.Save();
    }

    void CreateContentView(string text)
    {
        Text storyText = Instantiate(textPrefab) as Text;
        storyText.transform.SetParent(canvas.transform, false);

        // Start coroutine for the typewriter effect
        StartCoroutine(TypeText(storyText, text));
    }

    // Coroutine to display text slowly (Typewriter Effect)
    IEnumerator TypeText(Text storyText, string text)
    {
        storyText.text = ""; // Clear text initially
        foreach (char letter in text.ToCharArray())
        {
            storyText.text += letter; // Add one letter at a time
            yield return new WaitForSeconds(letterDelay); // Wait between letters
        }
    }

    Button CreateChoiceView(string text)
    {
        Button choice = Instantiate(buttonPrefab) as Button;
        choice.transform.SetParent(canvas.transform, false);
        Text choiceText = choice.GetComponentInChildren<Text>();
        choiceText.text = text;
        HorizontalLayoutGroup layoutGroup = choice.GetComponent<HorizontalLayoutGroup>();
        layoutGroup.childForceExpandHeight = false;

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
