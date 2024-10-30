using UnityEngine;

public class SaveProgres : MonoBehaviour
{
    public Vector3 playerPosition;
    public int score;

    void Start()
    {
        LoadProgress();
    }

    public void SaveProgress()
    {
        // Save position
        PlayerPrefs.SetFloat("PosX", playerPosition.x);
        PlayerPrefs.SetFloat("PosY", playerPosition.y);
        PlayerPrefs.SetFloat("PosZ", playerPosition.z);

        // Save score
        PlayerPrefs.SetInt("Score", score);

        PlayerPrefs.Save(); // Explicitly save all PlayerPrefs data
    }

    public void LoadProgress()
    {
        if (PlayerPrefs.HasKey("PosX") && PlayerPrefs.HasKey("Score"))
        {
            // Load position
            float x = PlayerPrefs.GetFloat("PosX");
            float y = PlayerPrefs.GetFloat("PosY");
            float z = PlayerPrefs.GetFloat("PosZ");
            playerPosition = new Vector3(x, y, z);

            // Load score
            score = PlayerPrefs.GetInt("Score");
        }
    }
}
