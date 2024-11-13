using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    public float moveSpeed = 5f;
    private float movement;
    private Animator animator;
    private float scaleX;

    [Header("Fear Mechanic")]
    public bool isInFear = false;                     // Indicates if the player is in a fear state
    public float fearLevel = 0f;                      // Current fear level, ranges from 0 (no fear) to 1 (full fear)
    [SerializeField] private float fearIncreaseRate = 0.2f;  // Rate at which fear increases when in fear
    [SerializeField] private float fearDecreaseRate = 0.1f;  // Rate at which fear decreases when out of fear
    [SerializeField] private float fearThreshold = 0.7f;     // Fear level threshold above which the player is frozen
    [SerializeField] private Image fearFillImage;           // UI Image for fear level display
    [SerializeField] private TextMeshProUGUI fearPromptText; // Text to prompt the player to use the flashlight

    [Header("Audio Settings")]
    [SerializeField] private AudioClip walkSound;          // Sound for player walking
    private AudioSource audioSource;                       // AudioSource component to play sound

    private bool canMove = true;                           // Controls if the player can move

    void Start()
    {
        animator = GetComponent<Animator>();
        scaleX = transform.localScale.x;
        fearPromptText.text = ""; // Start with an empty prompt

        // Setup audio source for walking sound
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = walkSound;
        audioSource.loop = true;   // Loop sound while moving
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        // Update fear level based on fear state
        if (isInFear)
        {
            fearLevel += fearIncreaseRate * Time.deltaTime;
            fearLevel = Mathf.Clamp(fearLevel, 0, 1); // Keep fear level within range 0-1
        }
        else
        {
            fearLevel -= fearDecreaseRate * Time.deltaTime;
            fearLevel = Mathf.Clamp(fearLevel, 0, 1);
        }

        // Update fear level UI
        fearFillImage.fillAmount = fearLevel;

        // If fear level is above the threshold, freeze player and show prompt
        if (fearLevel > fearThreshold)
        {
            fearPromptText.text = "Get the FlashLight and point it at scary thing otherwise you'll pass out";
        }
        else
        {
            canMove = true;
            if (fearPromptText.text == "Point your flashlight at the object that you are scared of") fearPromptText.text = ""; // Clear prompt if below threshold
        }

        // Handle player movement if allowed
        if (canMove)
        {
            HandleMovement();
        }
        else
        {
            // Stop walking animation and sound if player is frozen by fear
            animator.SetBool("IsWalking", false);
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    private void HandleMovement()
    {
        movement = Input.GetAxis("Horizontal");

        // Move the player
        transform.Translate(Vector3.right * movement * moveSpeed * Time.deltaTime);

        // Set the walking animation and play sound based on movement
        bool isMoving = movement != 0;
        animator.SetBool("IsWalking", isMoving);

        if (isMoving && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            // Only play the sound when moving left (A) or right (D)
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // Stop the sound if not moving or not pressing A/D
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }

        // Flip the player's sprite based on movement direction
        if (movement > 0)
        {
            // Move to the right
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
        }
        else if (movement < 0)
        {
            // Move to the left (flip sprite)
            transform.localScale = new Vector3(-1 * scaleX, transform.localScale.y, transform.localScale.z);
        }
    }
}
