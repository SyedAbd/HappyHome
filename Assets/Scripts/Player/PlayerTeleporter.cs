using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Transform hallwaySpawnPoint;
    [SerializeField] private Transform bedroomSpawnPoint;
    [SerializeField] private Transform livingRoomSpawnPoint;
    [SerializeField] private Transform bathroomSpawnPoint;
    private Transform player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    public void GotoHallway()
    {
        TeleportPlayer(hallwaySpawnPoint);
    }

    public void GotoBedroom()
    {
        TeleportPlayer(bedroomSpawnPoint);
    }

    public void GotoLivingRoom()
    {
        TeleportPlayer(livingRoomSpawnPoint);
    }

    public void GotoBathroom()
    {
        TeleportPlayer(bathroomSpawnPoint);
    }

    private void TeleportPlayer(Transform targetSpawnPoint)
    {
        if (player != null && targetSpawnPoint != null)
        {
            player.position = targetSpawnPoint.position;
        }
        else
        {
            Debug.LogWarning("Player or target spawn point is missing!");
        }
    }
}
