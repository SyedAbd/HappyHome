using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public int textProgressIndex;

    void Start()
    {
        // Load progress from PlayerPrefs
        textProgressIndex = PlayerPrefs.GetInt("TextProgress", 0);

        // Load text from the saved point
        ContinueTextProgress(textProgressIndex);
    }

    public void UpdateTextProgress(int newProgressIndex)
    {
        textProgressIndex = newProgressIndex;
        PlayerPrefs.SetInt("TextProgress", textProgressIndex);
        PlayerPrefs.Save();
    }

    private void ContinueTextProgress(int progress)
    {
        Debug.Log("Resuming text from index: " + progress);
        // Custom logic for continuing text
    }
}
