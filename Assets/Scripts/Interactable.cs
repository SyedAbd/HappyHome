using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    // This method will be called when the player interacts with the object
    public virtual void OnInteract()
    {
        Debug.Log("Interacted with " + gameObject.name);
        // Implement interaction logic here (e.g., open a door, pick up an item)
    }
}
