using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class DialogueManager : MonoBehaviour
{
    // Content
    public static DialogueManager _instance;
    
    // Resources
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject dialogueItem;
    private DialogueItem dialogueItemController;
    public bool isDialogueActive = false;
    // public KeyCode interactKey = KeyCode.X;

    public enum DefaultState { Minimized, Expanded }
    public enum AfterMinimize { Disable, Destroy }

    private void Awake()
    {
        // Ensure only one instance of UIManager exists
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        MinimizeDialogue();
        
    }

    private void Update()
    {
        // if (isDialogueActive && Input.GetKeyDown(interactKey))
        // {
        //     MinimizeDialogue();
        // }
    }

    public void NewDialogue(
        string speakerName,
        string text,
        string controlKey
    )
    {
        
        dialogueItemController = dialogueItem.GetComponent<DialogueItem>();
        dialogueItemController.UpdateText(speakerName, text, controlKey);
        ExpandNotification();
    }

    public void UpdateUI()
    {

    }



    public void AnimateNotification()
    {
        // ExpandNotification();
    }

    public void ExpandNotification()
    {
        isDialogueActive = true;
        dialogueItem.SetActive(true);
    }

    public void MinimizeDialogue()
    {
        isDialogueActive = false;
        dialogueItem.SetActive(false);
        
    }

    public void DestroyDialogue()
    {
        
    }

}