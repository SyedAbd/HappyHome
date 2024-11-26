using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneOnClick : MonoBehaviour
{
    public Button yourButton; // Reference to the button in the UI
    public int sceneIndex;    // Index of the scene you want to load
    public GameObject settings;
    public GameObject mainMenu;
    public GameObject credits;

    void Start()
    {
        // Assign the LoadNewScene method to the button's onClick event
        //yourButton.onClick.AddListener(LoadNewScene);
    }

    void LoadNewScene()
    {
        // Load the specified scene by index
        SceneManager.LoadScene(sceneIndex);
    }
    
    public void LoadSceneIndex()
    {
        SceneManager.LoadScene(sceneIndex);
    }
    public void LoadSettings()
    {
        mainMenu.SetActive(false);
        settings.SetActive(true);
        credits.SetActive(false);


    }
    public void LoadCredits()
    {
        mainMenu.SetActive(false);
        settings.SetActive(false);
        credits.SetActive(true);


    }

    public void LoadInkScence()
    {
        mainMenu.SetActive(false);
        settings.SetActive(false);
        credits.SetActive(false);


    }
    public void LoadMainMenu()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
        credits.SetActive(false);


    }
}
