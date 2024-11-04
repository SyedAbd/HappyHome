using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float movement;
    private Animator animator;

    void Start()
    {
        
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        movement = Input.GetAxis("Horizontal");

        
        transform.Translate(Vector3.right * movement * moveSpeed * Time.deltaTime);

        
        animator.SetBool("IsWalking", movement != 0);

    }
}
