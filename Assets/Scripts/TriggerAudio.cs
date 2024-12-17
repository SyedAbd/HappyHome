using UnityEngine;

public class TriggerAudio : MonoBehaviour
{
    public AudioSource audioSource; // Reference to the AudioSource on the other object

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object has the AudioSource
        if (collision.gameObject == audioSource.gameObject)
        {
            audioSource.Play(); // Play the audio
        }
    }
}