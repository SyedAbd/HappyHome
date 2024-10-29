using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private bool hasKey = false;
    private bool isSpaceAvailable = true;
    private string keyTag;
    private GameObject gameObj;

    // Call this method when the player picks up the key
    public void PickUpKey(GameObject obj,string tagOfTheKey)
    {
        hasKey = true;
        isSpaceAvailable = false;
        keyTag = tagOfTheKey;
        gameObj = obj;
    }
    public void DropKey()
    {
        hasKey= false;
        isSpaceAvailable = true;
        keyTag= null;
        gameObj= null;
    }
    // Check if the player has the key
    public bool HasKey(string tagOfTheKey)
    {
         if (tagOfTheKey == keyTag)return true;
         return false;
    }

    public void UseKey()
    {
        isSpaceAvailable= true;
        Destroy(gameObj);

    }
}
