using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum CanvasState
{
    None,
    Loading,
    MainMenu,
    PauseMenu,
    Dialogue,
    Inventory,
    // Settings,
    Shop,
    Credits,
    CurlingMatch
}

public class CanvasManager : MonoBehaviour
{
    public static CanvasManager _instance;
    [Header("Canvases")]
    public GameObject CanvasMainMenu;
    public GameObject CanvasPauseMenu;
    public GameObject CanvasDialogue;
    public GameObject CanvasInventory;
    // public GameObject CanvasSettings; 
    public GameObject CanvasShop;
    public GameObject CanvasCredits;
    public GameObject CanvasCurlingMatch;
    public GameObject CanvasLoading;

    [Header("Helper Areas")]
    public GameObject CanvasDisplayArea; // Place to put 3d models that can then be rendered into a UI Canvas
    public CanvasDisplayAreaController canvasDisplayAreaController;

    public CanvasState currentState = CanvasState.None;

    void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        // isReady = true;
    }

    /// <summary>
    /// Canvas Control Area
    /// </summary>
    /// <param name="newState"></param>
    public void OpenCanvas(CanvasState newState)
    {
        if (currentState == newState) return;

        // Disable all canvases first
        CloseAllCanvases();

        // Enable the selected canvas
        switch (newState)
        {
            case CanvasState.MainMenu:
                EnableCanvas(CanvasMainMenu);
                break;
            case CanvasState.PauseMenu:
                EnableCanvas(CanvasPauseMenu);
                break;
            case CanvasState.Dialogue:
                EnableCanvas(CanvasDialogue);
                break;
            case CanvasState.Inventory:
                EnableCanvas(CanvasInventory);
                break;
            // case CanvasState.Settings:
            //     EnableCanvas(CanvasSettings);
            //     break;
            case CanvasState.Shop:
                EnableCanvas(CanvasShop);
                break;
            case CanvasState.Credits:
                EnableCanvas(CanvasCredits);
                break;
            case CanvasState.CurlingMatch:
                EnableCanvas(CanvasCurlingMatch);
                break;
            case CanvasState.Loading:
                EnableCanvas(CanvasLoading);
                break;
            default:
                // No canvas to enable
                break;
        }

        currentState = newState;
    }

    public void CloseAllCanvases()
    {
        DisableCanvas(CanvasMainMenu);
        DisableCanvas(CanvasPauseMenu);
        DisableCanvas(CanvasDialogue);
        DisableCanvas(CanvasInventory);
        DisableCanvas(CanvasShop);
        DisableCanvas(CanvasCredits);
        DisableCanvas(CanvasCurlingMatch);
        DisableCanvas(CanvasLoading);
    }

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

    /// <summary>
    /// Canvas Display Area Control
    /// </summary>
    /// <param name="newState"></param>
}