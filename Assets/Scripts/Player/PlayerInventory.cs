using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private bool hasKey = false;

    // Call this method when the player picks up the key
    public void PickUpKey()
    {
        hasKey = true;
    }
    public void DropKey()
    {
        hasKey= false;
    }
    // Check if the player has the key
    public bool HasKey()
    {
        return hasKey;
    }
}
