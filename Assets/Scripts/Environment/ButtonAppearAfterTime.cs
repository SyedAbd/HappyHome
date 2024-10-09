using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadSceneOnClick : MonoBehaviour
{
    public Button yourButton; // Reference to the button in the UI
    public int sceneIndex;    // Index of the scene you want to load

    void Start()
    {
        // Assign the LoadNewScene method to the button's onClick event
        yourButton.onClick.AddListener(LoadNewScene);
    }

    void LoadNewScene()
    {
        // Load the specified scene by index
        SceneManager.LoadScene(sceneIndex);
    }
}
