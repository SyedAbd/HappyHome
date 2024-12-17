using UnityEngine;

public class KeyPressTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E key pressed!"); // This should appear in the console when you press 'E'
        }
    }
}
