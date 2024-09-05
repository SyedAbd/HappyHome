using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // This method will be called when the player interacts with the object
    public virtual void OnInteract()
    {
        // Generate and display random numbers in the console
        int randomNumber = Random.Range(1, 101); // Random number between 1 and 100
        Debug.Log("Generated Random Number: " + randomNumber);
    }
}
