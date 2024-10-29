using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Public method to load a scene based on its name
    public void LoadSceneByName(string sceneName)
    {
        // Check if the scene name is valid (scene must be added in build settings)
        if (!string.IsNullOrEmpty(sceneName))
        {
            // Load the scene with the given name
            SceneManager.LoadScene("chapter1Prologue");
        }
        else
        {
            Debug.LogError("Scene name is empty or null! Make sure the scene name is valid.");
        }
    }
}
