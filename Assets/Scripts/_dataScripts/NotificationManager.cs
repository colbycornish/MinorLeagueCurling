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
    [SerializeField] private Animator itemAnimator;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemControlText;
    [SerializeField] private TextMeshProUGUI itemText;

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
        itemText.GetComponent<TMPro.TextMeshProUGUI>().text = text;
        itemControlText.GetComponent<TMPro.TextMeshProUGUI>().text = controlKey;
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