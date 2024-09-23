using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import TextMeshPro namespace
using UnityEngine.UI;
using static DialogueObject;
using UnityEngine.Events;
using System;
using System.Runtime.InteropServices;
using UnityEditor.Experimental.GraphView;

public class DialogueViewer : MonoBehaviour
{
    [SerializeField] Transform parentOfResponses;
    [SerializeField] Button prefab_btnResponse;
    [SerializeField] TextMeshProUGUI txtNodeDisplay; // Changed from UnityEngine.UI.Text to TextMeshProUGUI
    [SerializeField] DialogueController dialogueController;
    DialogueController controller;

    [DllImport("__Internal")]
    private static extern void openPage(string url);

    private void Start()
    {
        controller = dialogueController;
        controller.onEnteredNode += OnNodeEntered;
        controller.InitializeDialogue();

        // Start the dialogue
        var curNode = controller.GetCurrentNode();
    }

    public static void KillAllChildren(UnityEngine.Transform parent)
    {
        UnityEngine.Assertions.Assert.IsNotNull(parent);
        for (int childIndex = parent.childCount - 1; childIndex >= 0; childIndex--)
        {
            UnityEngine.Object.Destroy(parent.GetChild(childIndex).gameObject);
        }
    }

    private void OnNodeSelected(int indexChosen)
    {
        Debug.Log("Chose: " + indexChosen);
        controller.ChooseResponse(indexChosen);
    }

    private void OnNodeEntered(DialogueObject.Node newNode)
    {
        Debug.Log("Entering node: " + newNode.title);
        txtNodeDisplay.text = newNode.text; // Using TextMeshProUGUI for displaying node text

        KillAllChildren(parentOfResponses);
        for (int i = newNode.responses.Count - 1; i >= 0; i--)
        {
            int currentChoiceIndex = i;
            var response = newNode.responses[i];
            var responseButton = Instantiate(prefab_btnResponse, parentOfResponses);

            // Changed from UnityEngine.UI.Text to TextMeshProUGUI
            responseButton.GetComponentInChildren<TextMeshProUGUI>().text = response.displayText;

            responseButton.onClick.AddListener(delegate { OnNodeSelected(currentChoiceIndex); });
        }

        if (newNode.tags.Contains("END"))
        {
            Debug.Log("End!");
        }
    }
}
