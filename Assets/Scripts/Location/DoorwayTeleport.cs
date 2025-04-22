using UnityEngine;
public class DoorwayTeleport : MonoBehaviour
{
    public string targetScene;
    public string targetSpawnID;
    public KeyCode interactKey = KeyCode.E;
    private bool playerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            UI_Prompt.Instance.ShowPrompt(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            UI_Prompt.Instance.ShowPrompt(false);
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