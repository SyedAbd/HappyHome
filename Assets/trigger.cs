using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PhraseCheck : MonoBehaviour
{
    [SerializeField] private InputField inputField;  
    [SerializeField] private Button submitButton;    
    [SerializeField] private string correctPhrase = "open sesame"; 
    [SerializeField] private int targetSceneIndex; 

    void Start()
    {
        submitButton.onClick.AddListener(CheckPhrase);
    }

    private void CheckPhrase()
    {
        if (inputField.text.Equals(correctPhrase, System.StringComparison.OrdinalIgnoreCase))
        {
            SceneManager.LoadScene(targetSceneIndex);
        }
        else
        {
            Debug.Log("Incorrect phrase! Try again.");
        }
    }
}
