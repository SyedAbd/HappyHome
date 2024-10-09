using UnityEngine;

public class DemoBox : MonoBehaviour
{
    public float delay = 3f;
    [SerializeField]private float timer = 0f;
    [SerializeField]private bool isInContact = false;

    void Update()
    {
        if (isInContact)
        {
            timer += Time.deltaTime;
            if (timer >= delay)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            timer = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Incontact");
        if (other.CompareTag("Flashlight"))
        {
            isInContact = true;
            timer = 0f;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Flashlight"))
        {
            isInContact = false;
            timer = 0f;
        }
    }
}
