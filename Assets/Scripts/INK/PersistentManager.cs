using UnityEngine;

public class PersistentManager : MonoBehaviour
{
    private static PersistentManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object alive
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
