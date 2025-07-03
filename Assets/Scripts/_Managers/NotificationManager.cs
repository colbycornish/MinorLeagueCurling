using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class NotificationManager : MonoBehaviour
{
    // Content
    public static NotificationManager _instance;
    public Sprite icon;
    [TextArea] public string notificationText = "Quest text here";

    // Resources
    [SerializeField] private GameObject notificationArea;
    [SerializeField] private GameObject notificationItem;
    [SerializeField] private NotificationItem notificationItemController;
    [SerializeField] private Animator itemAnimator;
    [SerializeField] private Image itemIcon;
    public bool isNotificationActive = false;

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
    
    public void NewNotification(
        string text,
        string controlKey
    )
    {
        notificationItemController = notificationItem.GetComponent<NotificationItem>();
        notificationItemController.UpdateText(text, controlKey);
        isNotificationActive = true;
        notificationItem.SetActive(true);
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

    public void MinimizeNotification()
    {
        isNotificationActive = false;
        notificationItem.SetActive(false);
    }

    public void DestroyNotification()
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