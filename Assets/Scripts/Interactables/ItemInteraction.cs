using UnityEngine;
using System.Linq;

/// <summary>
/// Teleports the player to a target scene and spawn ID, using a validated dropdown from a spawn database.
/// </summary>
public class ItemInteraction : MonoBehaviour
{
    public string notficationText;
    public KeyCode interactKey = KeyCode.X;
    public InteractableItemDatabase itemDatabase;
    public string selectedItemId;
    private bool playerInRange = false;



    private void Update()
    {
        if (playerInRange)
        {
            
            bool isDialogueActive = DialogueManager._instance.isDialogueActive;
            bool isNotificationActive = NotificationManager._instance.isNotificationActive;

            if (isNotificationActive == false && isDialogueActive == false)
            {
                CloseDialogue();
                OpenNotification();
            }
            if (Input.GetKeyDown(interactKey))
            {
                if (isDialogueActive == true)
                {
                    Debug.LogWarning("💻 Update Close Dialogue");
                    CloseDialogue();
                    OpenNotification();
                }
                else if (isDialogueActive == false)
                {
                    Debug.LogWarning("💻 Update Open Dialogue");
                    OpenDialogue();
                    CloseNotification();
                }
            }
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
            Debug.LogWarning("💻 Player exited interaction range.");
            playerInRange = false;
            CloseNotification();
            CloseDialogue();
        }
    }

    /// utility
    public ItemData GetSelectedItemData()
    {
        if (itemDatabase == null || string.IsNullOrEmpty(selectedItemId)) return null;

        return itemDatabase.items.FirstOrDefault(item => item.id == selectedItemId);
    }

    /// Notification Controls
    public void OpenNotification()
    {
        Debug.LogWarning("💻 Open Notification");
        ItemData item = GetSelectedItemData();
        string txt = notficationText;
        if (txt == null || txt == "")
        {
            txt = $"{item.name}";
        }

        NotificationManager._instance.NewNotification(txt, "X");
        NotificationManager._instance.ExpandNotification();
    }

    public void CloseNotification()
    {
        Debug.LogWarning("💻 Close Notification");
        NotificationManager._instance.MinimizeNotification();
        // DialogueManager._instance.MinimizeDialogue();
    }
    
    /// Dialogue Controls
    public void OpenDialogue()
    {
        Debug.LogWarning("💻 Open Dialogue called.");
        ItemData item = GetSelectedItemData();
        if (item != null)
        {
            Debug.Log($"Interacted with: {item.name} ({item.rarity})");
            DialogueManager._instance.NewDialogue(
                item.name,
                item.description,
                "X"
            );
            NotificationManager._instance.MinimizeNotification();
        }
        else
        {
            Debug.LogWarning("No item selected or item not found in database.");
        }
    }

    public void CloseDialogue()
    {
        DialogueManager._instance.MinimizeDialogue();
    }
}