using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isInkActive = true;
    public bool isTutorialActive = false;
    public bool isRoomsActive = false;
    public Animator animator_canvas; // Canvas animator for fading animation control
    private string _roomName; // Private backing field for roomName
    private bool _isToMove; // Private backing field for isToMove

    private DialogueManager dialogueManager;
    private GameObject sceneController;

    // Music settings
    public AudioClip inkMusicClip; // Music for the Ink scene
    public AudioClip gameplayMusicClip; // Music for gameplay scenes
    private string inkSceneName = "Ink_Narrative_Scene"; // The name of the Ink scene
    private string roomsSceneName = "Rooms_Scene"; // The name of the gameplay scene

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
            Destroy(gameObject); // Destroy duplicate instance
        }
    }

    public void Update()
    {
        if (isInkActive)
        {
            SetActiveInRoomsScene(false);
            SetActiveInTutorial(false);
        }
        else if (isRoomsActive)
        {
            SetActiveInTutorial(false);
            SetActiveInkScene(false);
        }
        else if (isTutorialActive)
        {
            SetActiveInRoomsScene(false);
            SetActiveInkScene(false);
        }
    }

    public void ChnageSceneToRooms()
    {
        Play_Animation_Fade("from_black");

        isInkActive = false;
        isTutorialActive = false;
        isRoomsActive = true;
        SetActiveInRoomsScene(true);
        SetActiveInkScene(false);
        MusicManager.Instance.StopMusic(); // Stop music when switching to rooms
    }

    public void ChnageSceneToTutorial()
    {
        Play_Animation_Fade("from_black");
        isInkActive = false;
        isRoomsActive = false;
        isTutorialActive = true;
        SetActiveInTutorial(true);
        SetActiveInRoomsScene(false);
        SetActiveInkScene(false);
        MusicManager.Instance.StopMusic(); // Stop music when switching to tutorial
    }

    public void ChnageSceneToInkFromTutorial()
    {
        isTutorialActive = false;
        isRoomsActive = false;
        isInkActive = true;
        SetActiveInTutorial(false);
        SetActiveInRoomsScene(false);
        UnloadTutorialScene();
        SetActiveInkScene(true);
        MusicManager.Instance.PlayMusic(inkMusicClip); // Play music for Ink scene
    }

    public void ChangeSceneToink()
    {
        Play_Animation_Fade("from_black");
        isRoomsActive = false;
        isTutorialActive = false;
        isInkActive = true;
        SetActiveInRoomsScene(false);
        SetActiveInTutorial(false);
        SetActiveInkScene(true);
        MusicManager.Instance.PlayMusic(inkMusicClip); // Play music for Ink scene
    }

    public void ChangeBackgroundMusic(AudioClip newClip)
    {
        MusicManager.Instance.ChangeMusic(newClip); // Call the simple method to switch the music
    }

    // Start a fading animation coroutine
    public void Play_Animation_Fade(string choice)
    {
        StartCoroutine(Animation_Fade(choice));
    }

    IEnumerator Animation_Fade(string choice)
    {
        // Trigger the fade animation
        switch (choice)
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
            GameObject targetObject = null;

            foreach (GameObject obj in roomsScene.GetRootGameObjects())
            {
                if (obj.CompareTag("ActiveOrInactive") || obj.name == "Active")
                {
                    targetObject = obj;
                    break;
                }
            }

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
            GameObject targetObject = null;

            foreach (GameObject obj in TutorialScene.GetRootGameObjects())
            {
                if (obj.CompareTag("ActiveOrInactive") || obj.name == "Active")
                {
                    targetObject = obj;
                    break;
                }
            }

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
            GameObject targetObject = null;

            foreach (GameObject obj in inkScene.GetRootGameObjects())
            {
                if (obj.CompareTag("ActiveOrInactive") || obj.name == "Active")
                {
                    targetObject = obj;
                    break;
                }
            }

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
            Debug.LogWarning("Ink_Scene is not loaded.");
        }
    }
}
