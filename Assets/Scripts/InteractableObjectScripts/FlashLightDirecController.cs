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

