using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnableButtonAfterDelay : MonoBehaviour
{
    public Button targetButton;   // The button you want to enable after 10 seconds
    public float delay = 10f;     // Delay time (10 seconds by default)

    void Start()
    {
        // Disable the button at the start
        targetButton.gameObject.SetActive(false);

        // Start the coroutine to enable the button after a delay
        StartCoroutine(EnableButtonAfterDelayCoroutine());
    }

    IEnumerator EnableButtonAfterDelayCoroutine()
    {
        // Wait for the specified delay (10 seconds by default)
        yield return new WaitForSeconds(delay);

        // Enable the button after the delay
        targetButton.gameObject.SetActive(true);
    }
}
