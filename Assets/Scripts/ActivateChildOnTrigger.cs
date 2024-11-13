using System.Collections;
using UnityEngine;

public class ActivateChildOnTrigger : MonoBehaviour
{
    public GameObject childObject; // Assign the child object in the inspector

    private Coroutine activationCoroutine;

    private void Start()
    {
        // Ensure the child object is initially inactive
        if (childObject != null)
        {
            childObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Start the coroutine to activate the child after 2 seconds
            if (childObject != null)
            {
                Debug.Log("Activiting icon");
                childObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            if (childObject != null)
            {
                childObject.SetActive(false);
            }
        }
    }

}
