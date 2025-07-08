using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class DialogueManager : MonoBehaviour
{
    // Content
    public static DialogueManager _instance;
    public Sprite icon;

    // Resources
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject dialogueItem;
    [SerializeField] private DialogueItem dialogueItemController;
    public bool isDialogueActive = false;
    public KeyCode interactKey = KeyCode.X;
    // [SerializeField] private Animator itemAnimator;
    // [SerializeField] private Image itemIcon;
    // [SerializeField] private TextMeshProUGUI itemControlText;
    // [SerializeField] private TextMeshProUGUI itemText;

    // Settings
    [Range(0, 10)] public float minimizeAfter = 3;

    // Events
    public UnityEvent onDestroy;

    // Helpers
    bool isOn;

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
        // DontDestroyOnLoad(gameObject);
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
        isDialogueActive = true;
        dialogueItemController = dialogueItem.GetComponent<DialogueItem>();
        dialogueItemController.UpdateText(speakerName, text, controlKey);
        dialogueItem.SetActive(true);
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
        
    }

    public void MinimizeDialogue()
    {
        isDialogueActive = false;
        dialogueItem.SetActive(false);
        
    }

    public void DestroyDialogue()
    {
        
    }

    // IEnumerator DisableAnimator()
    // {
        
    // }

    // IEnumerator DisableItem()
    // {
        
    // }

    // IEnumerator MinimizeItem()
    // {
        
    // }
}