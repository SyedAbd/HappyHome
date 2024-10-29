using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private string roomName
    {
        get { return roomName; }
        set { roomName = value; }

    }
    private bool isToMove
    {
        get { return isToMove; }
        set { isToMove = value; }

    }
    // Variables to store the state of your items
    //public bool hasKey;
    //public bool isFlashlightPicked;

    void Awake()
    {
        // Ensure this manager persists across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevent this object from being destroyed on scene load
        }
        else
        {
            //Destroy(gameObject); // Destroy duplicate
        }
    }

    public void ResetState()
    {
        //hasKey = false;
        //isFlashlightPicked = false;
    }
}
