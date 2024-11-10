using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightTrigger : MonoBehaviour
{
    private bool isPointingAtScarySpider = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider belongs to an object tagged as "ScarySpider"
        if (other.CompareTag("ScarySpider"))
        {
            // Attempt to get the child object with the script
            var childScript = other.GetComponentInChildren<FearController>();

            if (childScript != null)
            {
                // Notify the child script that the flashlight is pointing at the object
                childScript.SetFlashlightPointingAtObject(true);
                isPointingAtScarySpider = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the collider belongs to an object tagged as "ScarySpider"
        if (other.CompareTag("ScarySpider") && isPointingAtScarySpider)
        {
            // Attempt to get the child object with the script
            var childScript = other.GetComponentInChildren<FearController>();

            if (childScript != null)
            {
                // Notify the child script that the flashlight is no longer pointing at the object
                childScript.SetFlashlightPointingAtObject(false);
                isPointingAtScarySpider = false;
            }
        }
    }
}
