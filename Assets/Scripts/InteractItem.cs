using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractItem : MonoBehaviour
{
    [SerializeField] GameObject player; 
    [SerializeField] TextMeshProUGUI interactionText; 
    [SerializeField] GameObject keyPrefab; 
    private bool isPickedUp;
    private bool isInRange;
    private Vector2 speed;
    public float smoothTime;
    private Vector3 initialPosition;
    private Vector3 offset = new Vector3(0, 1.5f, 0);
    void Start()
    {
        isPickedUp = false;
        isInRange = false;
        interactionText.enabled = false; 
        initialPosition = transform.position; 
    }

    void Update()
    {
        if (isInRange && !isPickedUp)
        {
            interactionText.text = "Press E to pick up the key";
            interactionText.enabled = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                isPickedUp = true;
                interactionText.enabled = false; 
            }
        }

        if (isPickedUp)
        {
            
            
            transform.position = Vector2.SmoothDamp(transform.position, player.transform.position + offset, ref speed, smoothTime);

            
            if (Input.GetKeyDown(KeyCode.R))
            {
                DropItem(); 
            }
        }
    }

    private void DropItem()
    {
        
        Instantiate(keyPrefab, transform.position - offset, Quaternion.identity);

        
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isPickedUp)
        {
            isInRange = true; 
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !isPickedUp)
        {
            isInRange = false; 
            interactionText.enabled = false; 
        }
    }
}
