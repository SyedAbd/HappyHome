using System;
using Ink.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;
    public Story story;
    public float delay = 10f;

    public float letterDelay = 0.05f; // Delay between each letter for typewriter effect
    private string currentText;
    private List<string> storyLines = new List<string>(); // Store each line of story to show one by one

    private bool firstTimeContinuingStory = true;
    public GameObject isSceneActive;

    void Awake()
    {
        if (firstTimeContinuingStory)
        {
            Debug.Log("Awake first time");
            RemoveChildren();
            StartStory();
            firstTimeContinuingStory = false;
        }
        else {
            Debug.Log("Awake second time");
            //RefreshView(); 
        }
    }

    void StartStory()
    {
        // Start a new story from the beginning
        story = new Story(inkJSONAsset.text);

        // If we were saving the story state, we'd do it here:
        // if (PlayerPrefs.HasKey("InkStoryState"))
        // {
        //     string savedState = PlayerPrefs.GetString("InkStoryState", "");
        //     story.state.LoadJson(savedState); // Load saved state
        // }

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

        // Save the state before refreshing
        // SaveStoryState();

        if (choice.text.Contains("Living Room") || choice.text.Contains("Enjoy the show") || choice.text.Contains("What happened?"))
        {
            GameManager.Instance.roomName = "Livingroom";
            GameManager.Instance.isToMove = true;
            GameManager.Instance.ChnageSceneToRooms();
        }
        else if (choice.text.Contains("Bedroom") || choice.text.Contains("Go to bed"))
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

        /* NOTE! For testing purposes only
         * 
         * else if (choice.text.Contains("playhouse"))
        {
            Debug.Log("If condition of the Playhouse");
            GameManager.Instance.roomName = "Livingroom";
            GameManager.Instance.isToMove = true;
            GameManager.Instance.ChnageSceneToRooms();
        }
        */

        StartCoroutine(ContinueTheStory());
        

    }
    IEnumerator ContinueTheStory()
    {

        while (canvas != null && !isSceneActive.gameObject.activeSelf)
        {
            yield return null; // Wait for the next frame
        }
        RefreshView();
    }
    void RestartStory()
    {
        // Clear saved state to start from the beginning
        // PlayerPrefs.DeleteKey("InkStoryState");
        StartStory(); // Call StartStory to restart
    }

    // void SaveStoryState()
    // {
    //     string stateJson = story.state.ToJson();
    //     PlayerPrefs.SetString("InkStoryState", stateJson);
    //     PlayerPrefs.Save();
    // }

    IEnumerator CreateContentView(string text)
    {
        TMPro.TextMeshProUGUI storyText = Instantiate(textPrefab);
        storyText.transform.SetParent(canvas.transform, false);

        yield return StartCoroutine(TypeText(storyText, text)); // Wait for typewriter effect to complete
    }

    IEnumerator TypeText(TMPro.TextMeshProUGUI storyText, string text)
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
    private TMPro.TextMeshProUGUI textPrefab = null;
    [SerializeField]
    private Button buttonPrefab = null;
}
