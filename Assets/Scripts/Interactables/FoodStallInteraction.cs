using UnityEngine;

/// <summary>
/// Teleports the player to a target scene and spawn ID, using a validated dropdown from a spawn database.
/// </summary>
public class FoodStallInteraction : MonoBehaviour
{
    public string notficationText;
    public KeyCode interactKey = KeyCode.X;
    private bool playerInRange = false;

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            string txt = notficationText;
            if (txt == null || txt == "")
            {
                txt = "view bulletin board ";
            }
            
            NotificationManager._instance.NewNotification(txt, "X");
            NotificationManager._instance.ExpandNotification();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NotificationManager._instance.MinimizeNotification();
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(interactKey))
        {
            Debug.Log("FoodStall Interaction Triggered");
            // MenuManager._instance.ToggleCorkboardMenu();
            // Assuming you have a method to toggle the corkboard menu
        }
    }
}