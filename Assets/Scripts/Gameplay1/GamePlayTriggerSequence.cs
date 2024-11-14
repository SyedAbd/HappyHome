using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlayTriggerSequence : MonoBehaviour
{
    // Dont judge me by this scrip i have to present the demo in few hours 
    [SerializeField] private GameObject textBox;
    [SerializeField] private TextMeshProUGUI textBoxText;
    [SerializeField] private GameObject InstructionTextToDisable;
    [SerializeField] private GameObject[] objectsToBeEnabledInOrder;
    [SerializeField] private float letterDelay = 0.01f;
    [SerializeField] private string[] msgsInOrder;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SequenceForGamePlay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator SequenceForGamePlay()
    {
        Time.timeScale = 0;
       
        InstructionTextToDisable.SetActive(false);


        textBox.SetActive(true);
        yield return ShowTextLetterByLetter(textBoxText, msgsInOrder[0], letterDelay);
        yield return new WaitUntil(() => !textBox.activeSelf);

        textBox.SetActive(true);
        yield return ShowTextLetterByLetter(textBoxText, msgsInOrder[1], letterDelay);
        yield return new WaitUntil(() => !textBox.activeSelf);

        textBox.SetActive(true);
        yield return ShowTextLetterByLetter(textBoxText, msgsInOrder[2], letterDelay);
        yield return new WaitUntil(() => !textBox.activeSelf);
        InstructionTextToDisable.SetActive(true);


        objectsToBeEnabledInOrder[0].SetActive(true);


        Time.timeScale = 1;

        yield return new WaitUntil(() => objectsToBeEnabledInOrder[1].activeSelf);


        Time.timeScale = 0;
        InstructionTextToDisable.SetActive(false);
        textBox.SetActive(true);
        yield return ShowTextLetterByLetter(textBoxText, msgsInOrder[3], letterDelay);
        yield return new WaitUntil(() => !textBox.activeSelf);

        textBox.SetActive(true);
        yield return ShowTextLetterByLetter(textBoxText, msgsInOrder[4], letterDelay);
        yield return new WaitUntil(() => !textBox.activeSelf);

        textBox.SetActive(true);
        yield return ShowTextLetterByLetter(textBoxText, msgsInOrder[5], letterDelay);
        yield return new WaitUntil(() => !textBox.activeSelf);

        textBox.SetActive(true);
        yield return ShowTextLetterByLetter(textBoxText, "..........", letterDelay);
        yield return new WaitUntil(() => !textBox.activeSelf);

        textBox.SetActive(true);
        yield return ShowTextLetterByLetter(textBoxText, msgsInOrder[6], letterDelay);
        yield return new WaitUntil(() => !textBox.activeSelf);

        objectsToBeEnabledInOrder[2].SetActive(true);

        InstructionTextToDisable.SetActive(true);
        Time.timeScale = 1;



        yield return new WaitUntil(() => objectsToBeEnabledInOrder[3].activeSelf);


        Time.timeScale = 0;

        InstructionTextToDisable.SetActive(false);
        textBox.SetActive(true);
        yield return ShowTextLetterByLetter(textBoxText, msgsInOrder[7], letterDelay);
        yield return new WaitUntil(() => !textBox.activeSelf);

        textBox.SetActive(true);
        yield return ShowTextLetterByLetter(textBoxText, msgsInOrder[8], letterDelay);
        yield return new WaitUntil(() => !textBox.activeSelf);

        InstructionTextToDisable.SetActive(true);
        objectsToBeEnabledInOrder[4].SetActive(true);
        Time.timeScale = 1;





        yield return new WaitForSeconds(letterDelay);
    }


    private IEnumerator ShowTextLetterByLetter(TextMeshProUGUI textObject, string message, float letterDelay)
    {
        textObject.gameObject.SetActive(true);
        textObject.text = ""; // Clear existing text

        foreach (char letter in message)
        {
            textObject.text += letter; // Add each letter to the text
            yield return new WaitForSecondsRealtime(letterDelay); // Wait for a bit before adding the next letter
        }
    }
    private void ShowText(TextMeshProUGUI textObject, string message)
    {
        textObject.gameObject.SetActive(true);
        textObject.text = message;



    }

}
