
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Singleton GameManager that handles scene transitions, player spawn positioning, and persistent data.
/// </summary>
public class UIManager : MonoBehaviour
{

    private static UIManager instance;
    public static CanvasManager canvas;
    // public static NotificationManager notifications;
    
    // public static UIManager Instance
    // {
    //     get
    //     {
    //         if (instance == null)
    //         {
    //             instance = FindObjectByType<UIManager>();
    //             if (instance == null)
    //             {
    //                 GameObject obj = new GameObject("UIManager");
    //                 instance = obj.AddComponent<UIManager>();
    //             }
    //         }
    //         return instance;
    //     }
    // }
    private void Awake()
    {
        // Ensure only one instance of UIManager exists
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

