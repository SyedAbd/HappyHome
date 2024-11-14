using UnityEngine;

public class StoolInBathroomTrigger : MonoBehaviour
{
    [SerializeField] private GameObject referenceObject; // The object to compare positions with
    [SerializeField] private GameObject objectToActivate; // The object to activate when conditions are met
    [SerializeField] private float maxDistanceBelow = 1.0f; // Maximum distance below reference object for activation (y-axis)
    [SerializeField] private float maxDistanceX = 0.5f; // Maximum horizontal distance from reference object for activation (x-axis)

    void Update()
    {
        // Calculate the vertical and horizontal distances between the current object and the reference object
        float distanceBelow = referenceObject.transform.position.y - transform.position.y;
        float distanceX = Mathf.Abs(referenceObject.transform.position.x - transform.position.x);

        // Check if the current object is within the specified range below and horizontally close to the reference object
        if (distanceBelow > 0 && distanceBelow <= maxDistanceBelow && distanceX <= maxDistanceX)
        {
            objectToActivate.SetActive(true); // Activate the specified object
        }
        else
        {
            objectToActivate.SetActive(false); // Deactivate the specified object
        }
    }
}
