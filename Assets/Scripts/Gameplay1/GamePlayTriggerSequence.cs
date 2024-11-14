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
    private bool hasGameStarted = false;


    // Start is called before the first frame update
    void Start()
    {
        if (!hasGameStarted) hasGameStarted = true;
        else
        StartCoroutine(SequenceForGamePlay());


    }

    private IEnumerator WaitForGameToStart()
    {
        yield return new WaitForSeconds(2f);
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
        objectsToBeEnabledInOrder[5].SetActive(true);
        Time.timeScale = 1;

        objectsToBeEnabledInOrder[1].SetActive(false);
        yield return new WaitUntil(() => objectsToBeEnabledInOrder[1].activeSelf);

        objectsToBeEnabledInOrder[6].SetActive(true);

        yield return new WaitUntil(() => !objectsToBeEnabledInOrder[6].activeSelf);

        //put fad in and out here 




        yield return new WaitUntil(() => objectsToBeEnabledInOrder[7].activeSelf);



        InstructionTextToDisable.SetActive(false);
        Time.timeScale = 0;
        

        textBox.SetActive(true);
        yield return ShowTextLetterByLetter(textBoxText, msgsInOrder[10], letterDelay);

        //Put Fad in and out here

        objectsToBeEnabledInOrder[2].SetActive(false);
        

        yield return new WaitUntil(() => !textBox.activeSelf);

        InstructionTextToDisable.SetActive(true);
        //objectsToBeEnabledInOrder[0].SetActive(true);


        Time.timeScale = 1;




        objectsToBeEnabledInOrder[1].SetActive(false);

        yield return new WaitUntil(()=> objectsToBeEnabledInOrder[1].activeSelf);



        Time.timeScale = 0;
        InstructionTextToDisable.SetActive(false);

        textBox.SetActive(true);
        yield return ShowTextLetterByLetter(textBoxText, msgsInOrder[11], letterDelay);
        yield return new WaitUntil(() => !textBox.activeSelf);

        textBox.SetActive(true);
        yield return ShowTextLetterByLetter(textBoxText, msgsInOrder[12], letterDelay);
        yield return new WaitUntil(() => !textBox.activeSelf);
        InstructionTextToDisable.SetActive(true);
        objectsToBeEnabledInOrder[8].SetActive(true);


        Time.timeScale = 1;



        yield return new WaitUntil(() => objectsToBeEnabledInOrder[9].activeSelf);

        //Put Fade in and out here

        Time.timeScale = 0;
        InstructionTextToDisable.SetActive(false);
        textBox.SetActive(true);
        yield return ShowTextLetterByLetter(textBoxText, msgsInOrder[14], letterDelay);
        yield return new WaitUntil(() => !textBox.activeSelf);
        InstructionTextToDisable.SetActive(true);
        objectsToBeEnabledInOrder[10].SetActive(true);


        Time.timeScale = 1;



        yield return new WaitUntil(() => objectsToBeEnabledInOrder[11].activeSelf);

        textBoxText.text = "";
        //Time.timeScale = 0;
        InstructionTextToDisable.SetActive(false);

        textBox.SetActive(true);
        yield return ShowTextLetterByLetter(textBoxText, msgsInOrder[15], letterDelay);
        //yield return new WaitUntil(() => !textBox.activeSelf);



        InstructionTextToDisable.SetActive(true);
        objectsToBeEnabledInOrder[1].SetActive(false);
        objectsToBeEnabledInOrder[10].SetActive(false);
        objectsToBeEnabledInOrder[11].SetActive(false);

        Time.timeScale = 1;






        yield return new WaitUntil(() => objectsToBeEnabledInOrder[1].activeSelf);


        Time.timeScale = 0;
        InstructionTextToDisable.SetActive(false);

        textBox.SetActive(true);
        yield return ShowTextLetterByLetter(textBoxText, msgsInOrder[16], letterDelay);
        yield return new WaitUntil(() => !textBox.activeSelf);
        // fade in and out
        objectsToBeEnabledInOrder[0].SetActive(false);


        textBox.SetActive(true);
        yield return ShowTextLetterByLetter(textBoxText, msgsInOrder[17], letterDelay);
        yield return new WaitUntil(() => !textBox.activeSelf);

        //Fad in and out

        objectsToBeEnabledInOrder[12].SetActive(true);
        objectsToBeEnabledInOrder[8].SetActive(false);
        //objectsToBeEnabledInOrder[12].SetActive(true);

        yield return new WaitUntil(() => !objectsToBeEnabledInOrder[12].activeSelf);



        //yield return new WaitForSeconds(1);
        Time.timeScale = 1;

        


        textBox.SetActive(true);
        yield return ShowTextLetterByLetter(textBoxText, msgsInOrder[19], letterDelay);
        yield return new WaitUntil(() => !textBox.activeSelf);


        InstructionTextToDisable.SetActive(true);
        //objectsToBeEnabledInOrder[1].SetActive(false);

        objectsToBeEnabledInOrder[10].SetActive(true) ;
        objectsToBeEnabledInOrder[11].SetActive(false);

        Time.timeScale = 1;

        yield return new WaitUntil(() => objectsToBeEnabledInOrder[11].activeSelf);

        InstructionTextToDisable.SetActive(false);

        textBox.SetActive(true);
        yield return ShowTextLetterByLetter(textBoxText, msgsInOrder[20], letterDelay);
        //yield return new WaitUntil(() => !textBox.activeSelf);
        yield return new WaitForSeconds(1f);

        textBox.SetActive(false);

        InstructionTextToDisable.SetActive(true);

        yield return new WaitForSeconds(0.5f);


        objectsToBeEnabledInOrder[13].SetActive(true);



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
