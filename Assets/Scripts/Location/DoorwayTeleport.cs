using UnityEngine;

/// <summary>
/// Teleports the player to a target scene and spawn ID, using a validated dropdown from a spawn database.
/// </summary>
public class DoorwayTeleport : MonoBehaviour
{
    public string targetScene;
    public string targetSpawnID;
    public SpawnPointDatabase spawnDatabase;
    public KeyCode interactKey = KeyCode.E;
    private bool playerInRange = false;

    public SceneDatabase sceneDatabase;
    public GameObject promptUI;

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
            if (promptUI != null){
                promptUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (promptUI != null){
                promptUI.SetActive(false);
            }
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