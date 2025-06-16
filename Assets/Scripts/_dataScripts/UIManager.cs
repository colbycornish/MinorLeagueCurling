
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// Singleton GameManager that handles scene transitions, player spawn positioning, and persistent data.
/// </summary>
public class UIManager : MonoBehaviour
{

    private static UIManager instance;
    
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

public class CanvasManager : MonoBehaviour
{
    public GameObject CanvasMainMenu;
    public GameObject CanvasPauseMenu;
    public GameObject CanvasDialogue;
    public GameObject CanvasInventory;
    public GameObject CanvasSettings;
    public GameObject CanvasShop;
    public GameObject CanvasCredits;

    private void Awake()
    {
        // Ensure only one instance of CanvasManager exists

        DontDestroyOnLoad(gameObject);
    }

    // enable CanvasMainMenu
    // enable CanvasPauseMenu
    // enable CanvasDialogue
    // enable CanvasInventory
    // enable CanvasSettings
    // enable CanvasShop
    // enable CanvasCredits

    // Method to enable a specific canvas
    public void EnableCanvas(GameObject canvas)
    {
        canvas.SetActive(true);
    }
    

    // Method to disable a specific canvas
    public void DisableCanvas(GameObject canvas)
    {
        canvas.SetActive(false);
    }
}