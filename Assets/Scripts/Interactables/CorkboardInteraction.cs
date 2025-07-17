using UnityEngine;

/// <summary>
/// Teleports the player to a target scene and spawn ID, using a validated dropdown from a spawn database.
/// </summary>
public class CorkboardInteraction : MonoBehaviour
{
    public string notficationText;
    public KeyCode interactKey = KeyCode.X;
    private bool playerInRange = false;


    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            Debug.Log("Corkboard Interaction Triggered");
            // MenuManager._instance.ToggleCorkboardMenu();
            // Assuming you have a method to toggle the corkboard menu
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            OpenNotification();

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CloseNotification();
        }
    }

    private void OpenNotification()
    {
        string txt = notficationText;
        if (txt == null || txt == "")
        {
            txt = "view bulletin board ";
        }

        NotificationManager._instance.NewNotification(txt, "X");
        NotificationManager._instance.ExpandNotification();
    }
    private void CloseNotification()
    {
        NotificationManager._instance.MinimizeNotification();
        playerInRange = false;
    }

    
}