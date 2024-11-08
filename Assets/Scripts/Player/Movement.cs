using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float movement;
    private Animator animator;
    private float scaleX;

    void Start()
    {
        animator = GetComponent<Animator>();
        scaleX = transform.localScale.x;
    }

    void Update()
    {
        movement = Input.GetAxis("Horizontal");

        // Move the player
        transform.Translate(Vector3.right * movement * moveSpeed * Time.deltaTime);

        // Set the walking animation
        animator.SetBool("IsWalking", movement != 0);

        // Flip the player's sprite based on movement direction
        if (movement > 0)
        {
            // Move to the right
            transform.localScale = new Vector3(scaleX, transform.localScale.y, transform.localScale.z);
        }
        else if (movement < 0)
        {
            // Move to the left (flip sprite)
            transform.localScale = new Vector3(-1*scaleX, transform.localScale.y, transform.localScale.z);
        }
    }
}
