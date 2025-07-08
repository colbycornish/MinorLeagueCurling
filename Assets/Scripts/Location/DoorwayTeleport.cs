using UnityEngine;

/// <summary>
/// Teleports the player to a target scene and spawn ID, using a validated dropdown from a spawn database.
/// </summary>
public class DoorwayTeleport : MonoBehaviour
{
    public string targetScene;
    public string targetSpawnID;
    public string notficationText;
    public SpawnPointDatabase spawnDatabase;
    public KeyCode interactKey = KeyCode.X;
    private bool playerInRange = false;

    public SceneDatabase sceneDatabase;
    // public GameObject promptUI;

    // void Start()
    // {
    //     if (promptUI != null)
    //         promptUI.SetActive(false);
    // }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            string txt = notficationText;
            if (txt == null || txt == "")
            {
                txt = "enter " + targetScene;
                if (targetScene.Contains("Town"))
                {
                    txt = "exit to town";
                }
            }

            if (NotificationManager._instance != null)
            {
                NotificationManager._instance.NewNotification(txt, "X");
                NotificationManager._instance.ExpandNotification();
            }
            
            // if (promptUI != null)
            // {
            //     promptUI.SetActive(true);
            // }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (NotificationManager._instance != null)
            {
                NotificationManager._instance.MinimizeNotification();
            }
            playerInRange = false;
            // if (promptUI != null){
            //     promptUI.SetActive(false);
            // }
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            GameManager.Instance.TeleportToScene(targetScene, targetSpawnID);
        }
    }
}