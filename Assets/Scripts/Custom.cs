using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Custom : Interactable
{
    public override void OnInteract()
    {
        // Custom interaction: Print a random number and open the door
        int randomNumber = Random.Range(1, 101);
        Debug.Log("Generated Random Number for Door: " + randomNumber);

        // Logic to open the door
        Debug.Log("Door is opening...");
    }
}
