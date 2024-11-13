using System;
using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static event Action<Story> OnCreateStory;
    public Story story;
    public float delay = 10f;

    public float letterDelay = 0.05f;
    private List<string> storyLines = new List<string>();

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
    }
    public void ToggleSkipButton()
    {

        if(skipText == true)
        {
            skipText = false;
        }
        else skipText = true;

    }
    void StartStory()
    {
        story = new Story(inkJSONAsset.text);
        OnCreateStory?.Invoke(story);
        RefreshView();
    }

    void RefreshView()
    {
        RemoveChildren();
        storyLines.Clear();

        while (story.canContinue)
        {
            string text = story.Continue().Trim();
            storyLines.Add(text);
        }

        StartCoroutine(ShowStorySequentially());
    }

    IEnumerator ShowStorySequentially()
    {
        foreach (var line in storyLines)
        {
            yield return StartCoroutine(CreateContentView(line));
            yield return new WaitForSeconds(0.5f);
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
            choice.onClick.AddListener(RestartStory);
        }
    }

    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);

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
        else if (choice.text.Contains("You enjoy the show") || choice.text.Contains("You spend some time alone"))
        {
            GameManager.Instance.roomName = "Hallway";
            GameManager.Instance.isToMove = true;
            GameManager.Instance.ChnageSceneToTutorial();
        }

        StartCoroutine(ContinueTheStory());
    }

    IEnumerator ContinueTheStory()
    {
        while (canvas != null && !isSceneActive.gameObject.activeSelf)
        {
            yield return null;
        }
        RefreshView();
    }

    void RestartStory()
    {
        StartStory();
    }

    IEnumerator CreateContentView(string text)
    {
        // Create and set up the TextMeshProUGUI object
        TextMeshProUGUI storyText = Instantiate(textPrefab);
        storyText.transform.SetParent(canvas.transform, false);
        storyText.text = text;

        // Add a ContentSizeFitter component to adjust the size dynamically
        ContentSizeFitter sizeFitter = storyText.gameObject.AddComponent<ContentSizeFitter>();
        sizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

        // Run the typewriter effect
        yield return StartCoroutine(TypeText(storyText, text));
    }

    IEnumerator TypeText(TextMeshProUGUI storyText, string text)
    {
        storyText.text = "";
        foreach (char letter in text.ToCharArray())
        {
            storyText.text += letter;
            if (!skipText)
                yield return new WaitForSeconds(letterDelay);
        }
    }

    Button CreateChoiceView(string text)
    {
        Button choice = Instantiate(buttonPrefab);
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

    [SerializeField] private TextAsset inkJSONAsset = null;
    [SerializeField] private Canvas canvas = null;
    [SerializeField] private TextMeshProUGUI textPrefab = null;
    [SerializeField] private Button buttonPrefab = null;
    [SerializeField] public bool skipText;
}
