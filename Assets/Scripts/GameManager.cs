using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isInkActive = true;
    public Animator animator_canvas; // Canvas animator for fading animation control
    private string _roomName; // Private backing field for roomName
    private bool _isToMove; // Private backing field for isToMove

    private DialogueManager dialogueManager;
    private GameObject sceneController;
    public string roomName
    {
        get { return _roomName; }
        set { _roomName = value; }
    }

    public bool isToMove
    {
        get { return _isToMove; }
        set { _isToMove = value; }
    }
    private void Start()
    {
        GameObject sceneController = GameObject.Find("SceneController");
    }
    void Awake()
    {
        // Ensure this manager persists across scenes
        if (Instance == null)
        {
            Instance = this;
            LoadRoomScene();
            LoadTutorialScene();
            DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed on scene load
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate Updated upstream
        }
    }
    public void Update()
    {
        if(isInkActive)
        {
            SetActiveInRoomsScene(false);
            SetActiveInTutorial(false);
        }
    }

    public void ChnageSceneToRooms()
    {
        Play_Animation_Fade("from_black");

        isInkActive = false;
        SetActiveInRoomsScene(true);
        SetActiveInkScene(false);
    }

    public void ChnageSceneToTutorial()
    {
        Play_Animation_Fade("from_black");
        isInkActive = false;
        SetActiveInTutorial(true);
        SetActiveInRoomsScene(false);
        SetActiveInkScene(false);

    }
    public void ChnageSceneToInkFromTutorial()
    {
        //Play_Animation_Fade("from_black");
        isInkActive = true;
        SetActiveInTutorial(false);
        SetActiveInRoomsScene(false);
        UnloadTutorialScene();
        SetActiveInkScene(true);

    }
    public void ChangeSceneToink()
    {
        Play_Animation_Fade("from_black");

        isInkActive = true;
        SetActiveInRoomsScene(false);
        SetActiveInTutorial(false);
        SetActiveInkScene(true);

        //if (sceneController != null)
        //{
        //    dialogueManager = sceneController.GetComponent<DialogueManager>();
        //    dialogueManager.ContinueTheStory();

        //}

    }

    // Start a fading animation coroutine
    public void Play_Animation_Fade(string choice)
	{
        StartCoroutine(Animation_Fade(choice));
    }

    IEnumerator Animation_Fade(string choice)
	{
        // Trigger the fade animation
        switch(choice)
		{
            case "from_black":
                animator_canvas.SetTrigger("Fade_From_Black");
                break;
            case "to_black":
                animator_canvas.SetTrigger("Fade_To_Black");
                break;
            default:
                animator_canvas.SetTrigger("Fade_From_Black");
                break;
        }

        yield return null;
	}

    // Method to load the Rooms scene in the background
    public void LoadRoomScene()
    {
        if (!SceneManager.GetSceneByName("Rooms_Scene").isLoaded)
        {
            SceneManager.LoadSceneAsync("Rooms_Scene", LoadSceneMode.Additive);
            SetActiveInRoomsScene(false);
        }
    }
    public void LoadTutorialScene()
    {
        if (!SceneManager.GetSceneByName("Tutorial_Scene").isLoaded)
        {
            SceneManager.LoadSceneAsync("Tutorial_Scene", LoadSceneMode.Additive);
            SetActiveInTutorial(false);
        }
    }

    public void UnloadRoomScene()
    {
        if (SceneManager.GetSceneByName("Rooms_Scene").isLoaded)
        {
            SceneManager.UnloadSceneAsync("Rooms_Scene");
        }
    }
    public void UnloadTutorialScene()
    {
        if (SceneManager.GetSceneByName("Tutorial_Scene").isLoaded)
        {
            SceneManager.UnloadSceneAsync("Tutorial_Scene");
        }
    }
    public void SetActiveInRoomsScene(bool isActive)
    {
        Scene roomsScene = SceneManager.GetSceneByName("Rooms_Scene");

        if (roomsScene.isLoaded)
        {
            // Find the GameObject in the scene by tag or name
            GameObject targetObject = null;

            foreach (GameObject obj in roomsScene.GetRootGameObjects())
            {
                if (obj.CompareTag("ActiveOrInactive") || obj.name == "Active")
                {
                    targetObject = obj;
                    break;
                }
            }

            // Activate or deactivate the object
            if (targetObject != null)
            {
                targetObject.SetActive(isActive);
            }
            else
            {
                Debug.LogWarning("GameObject with tag 'ActiveorInactive' or name 'Active' not found in Rooms_Scene.");
            }
        }
        else
        {
            Debug.LogWarning("Rooms_Scene is not loaded.");
        }
    }

    public void SetActiveInTutorial(bool isActive)
    {
        Scene TutorialScene = SceneManager.GetSceneByName("Tutorial_Scene");

        if (TutorialScene.isLoaded)
        {
            Debug.Log("in the active obj in tutorial scene");
            // Find the GameObject in the scene by tag or name
            GameObject targetObject = null;

            foreach (GameObject obj in TutorialScene.GetRootGameObjects())
            {
                if (obj.CompareTag("ActiveOrInactive") || obj.name == "Active")
                {
                    Debug.Log("Found the active obj in tutorial scene");
                    targetObject = obj;
                    break;
                }
            }

            // Activate or deactivate the object
            if (targetObject != null)
            {
                targetObject.SetActive(isActive);
            }
            else
            {
                Debug.LogWarning("GameObject with tag 'ActiveorInactive' or name 'Active' not found in Rooms_Scene.");
            }
        }
        else
        {
            Debug.LogWarning("Tutorial_Scene is not loaded.");
        }
    }

    
    public void SetActiveInkScene(bool isActive)
    {
        Scene inkScene = SceneManager.GetSceneByName("Ink_Narrative_Scene");

        if (inkScene.isLoaded)
        {
            // Find the GameObject in the scene by tag or name
            GameObject targetObject = null;

            foreach (GameObject obj in inkScene.GetRootGameObjects())
            {
                if (obj.CompareTag("ActiveOrInactive") || obj.name == "Active")
                {
                    targetObject = obj;
                    break;
                }
            }

            // Activate or deactivate the object
            if (targetObject != null)
            {
                targetObject.SetActive(isActive);
            }
            else
            {
                Debug.LogWarning("GameObject with tag 'ActiveorInactive' or name 'Active' not found in Ink_Scene.");
            }
        }
        else
        {
            Debug.LogWarning("ink_Scene is not loaded.");
        }
    }
}
