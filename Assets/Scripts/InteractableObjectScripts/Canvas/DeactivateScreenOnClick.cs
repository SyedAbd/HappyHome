using UnityEngine;

public class DeactivateScreenOnClick : MonoBehaviour
{
    public void DeactivateParent()
    {
        if (transform.parent != null) // Check if the GameObject has a parent
        {
            transform.parent.gameObject.SetActive(false); // Deactivate the parent GameObject
        }
    }
}
