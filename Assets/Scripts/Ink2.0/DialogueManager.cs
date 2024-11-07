using System;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;
    public Story story;
    public float delay = 10f;

    public float letterDelay = 0.05f; // Delay between each letter for typewriter effect
    private string currentText;
    private List<string> storyLines = new List<string>(); // Store each line of story to show one by one

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
        storyLines.Clear();

        while (story.canContinue)
        {
            string text = story.Continue();
            text = text.Trim();
            storyLines.Add(text); // Add each line to storyLines list
        }

        StartCoroutine(ShowStorySequentially());
    }

    IEnumerator ShowStorySequentially()
    {
        foreach (var line in storyLines)
        {
            yield return StartCoroutine(CreateContentView(line));
            yield return new WaitForSeconds(0.5f); // Optional delay between text boxes
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
        SaveStoryState();

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
        PlayerPrefs.DeleteKey("InkStoryState");
        StartStory();
    }

    void SaveStoryState()
    {
        string stateJson = story.state.ToJson();
        PlayerPrefs.SetString("InkStoryState", stateJson);
        PlayerPrefs.Save();
    }

    IEnumerator CreateContentView(string text)
    {
        Text storyText = Instantiate(textPrefab) as Text;
        storyText.transform.SetParent(canvas.transform, false);

        yield return StartCoroutine(TypeText(storyText, text)); // Wait for typewriter effect to complete
    }

    IEnumerator TypeText(Text storyText, string text)
    {
        storyText.text = ""; // Clear text initially
        foreach (char letter in text.ToCharArray())
        {
            storyText.text += letter;
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
