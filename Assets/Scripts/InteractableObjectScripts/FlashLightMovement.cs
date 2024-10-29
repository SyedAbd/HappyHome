using UnityEngine;

public class FlashLightMovement : MonoBehaviour
{
    public Camera mainCamera;  // Reference to the main camera

    void Update()
    {
        
        Vector3 mousePosition = Input.mousePosition;

        
        Vector3 worldMousePosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 0));

       
        Vector3 direction = worldMousePosition - transform.position;

        
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
