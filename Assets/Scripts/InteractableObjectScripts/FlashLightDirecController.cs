using UnityEngine;

public class FlashLightDirecController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float movement;
    

    void Start()
    {
        
    }

    void Update()
    {
        movement = Input.GetAxis("Horizontal");

        // Move the player
        //transform.Translate(Vector3.right * movement * moveSpeed * Time.deltaTime);

      
        if (movement > 0)
        {
            // Player moving right, ensure sprite is facing right
            transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
        }
        else if (movement < 0)
        {
            // Player moving left, flip sprite to face left
            transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
        }
    }
}

