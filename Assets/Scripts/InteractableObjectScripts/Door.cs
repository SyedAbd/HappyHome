using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;  // For TextMeshProUGUI

public class Door : MonoBehaviour
{
    public string targetScene; 
    public string instructionText;
    public TextMeshProUGUI enterTextUI;   
    private bool isNearDoor = false; 

    void Start()
    {
        
        enterTextUI.gameObject.SetActive(false);
    }

    void Update()
    {
        
        if (isNearDoor && Input.GetKeyDown(KeyCode.E))
        {
            if (!string.IsNullOrEmpty(targetScene))
            {
                SceneManager.LoadScene(targetScene); 
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Something entered");
        if (other.CompareTag("Player"))
        {
            isNearDoor = true;
            enterTextUI.text = instructionText;
            enterTextUI.gameObject.SetActive(true); 
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            isNearDoor = false;
            if(!enterTextUI == null)
            enterTextUI.gameObject.SetActive(false); 
        }
    }
}
