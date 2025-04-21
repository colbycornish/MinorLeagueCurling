using UnityEngine;

public class DoorwayTeleport : MonoBehaviour
{
    public Transform teleportDestination;
    public KeyCode interactKey = KeyCode.E;
    private bool playerInRange = false;
    private Transform player;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            player = other.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            player = null;
        }
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            FadeManager.Instance.FadeOutThenIn(() => {
                player.position = teleportDestination.position;
            });
        }
    }
}