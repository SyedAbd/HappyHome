using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Spawn Points")]
    [SerializeField] private Transform hallwaySpawnPoint;
    [SerializeField] private Transform bedroomSpawnPoint;
    [SerializeField] private Transform livingRoomSpawnPoint;
    [SerializeField] private Transform bathroomSpawnPoint;

    [Header("Rooms")]
    [SerializeField] private GameObject hallway;
    [SerializeField] private GameObject bedroom;
    [SerializeField] private GameObject livingRoom;
    [SerializeField] private GameObject bathroom;

    private Transform player;

    //private GameManager gameManager;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        //gameManager = GetComponent<GameManager>();
        GotoHallway();

    }
    public void Update()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.isToMove)
            {
                GameManager.Instance.isToMove = false;
                switch (GameManager.Instance.roomName)
                {
                    case "Bedroom":
                        GotoBedroom();
                        break;
                    case "Livingroom":
                        GotoLivingRoom();
                        break;
                    case "Bathroom":
                        GotoBathroom();
                        break;
                    default:
                        GotoHallway();
                        break;

                }
            }
        }
    }
    public void GotoHallway()
    {
        TeleportAndActivateRoom(hallwaySpawnPoint, hallway);
    }

    public void GotoBedroom()
    {
        TeleportAndActivateRoom(bedroomSpawnPoint, bedroom);
    }

    public void GotoLivingRoom()
    {
        TeleportAndActivateRoom(livingRoomSpawnPoint, livingRoom);
    }

    public void GotoBathroom()
    {
        TeleportAndActivateRoom(bathroomSpawnPoint, bathroom);
    }

    private void TeleportAndActivateRoom(Transform targetSpawnPoint, GameObject targetRoom)
    {
        // Teleport player to the target spawn point
        if (player != null && targetSpawnPoint != null)
        {
            player.position = targetSpawnPoint.position;
        }
        else
        {
            Debug.LogWarning("Player or target spawn point is missing!");
        }

        // Deactivate all rooms, then activate the target room
        hallway.SetActive(false);
        bedroom.SetActive(false);
        livingRoom.SetActive(false);
        bathroom.SetActive(false);

        targetRoom.SetActive(true);
    }
}
