using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skipscript : MonoBehaviour
{

    [SerializeField] private DialogueManager dialogueManager;
    public void onclick()
    {
        if (dialogueManager != null)
        {
            dialogueManager.ToggleSkipButton();
        }




    }        
        

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
