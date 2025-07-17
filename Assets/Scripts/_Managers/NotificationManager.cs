using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;


public class NotificationManager : MonoBehaviour
{
    // Content
    public static NotificationManager _instance;

    // Resources
    [SerializeField] private GameObject notificationArea;
    [SerializeField] private GameObject notificationItem;
    private NotificationItem notificationItemController;
    public bool isNotificationActive = false;
    // public bool isReady = false;

    // Events
    public UnityEvent onDestroy;

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
        MinimizeNotification();
        // DontDestroyOnLoad(gameObject);
    }
    
    public void NewNotification(
        string text,
        string controlKey
    )
    {
        notificationItemController = notificationItem.GetComponent<NotificationItem>();
        notificationItemController.UpdateText(text, controlKey);
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
        isNotificationActive = true;
        notificationItem.SetActive(true);
    }

    public void MinimizeNotification()
    {
        isNotificationActive = false;
        notificationItem.SetActive(false);
    }

    public void DestroyNotification()
    {
        
    }
}