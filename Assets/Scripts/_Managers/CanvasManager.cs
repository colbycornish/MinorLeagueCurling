using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;


public class CanvasManager : MonoBehaviour
{
    public GameObject CanvasMainMenu;
    public GameObject CanvasPauseMenu;
    public GameObject CanvasDialogue;
    public GameObject CanvasInventory;
    public GameObject CanvasSettings;
    public GameObject CanvasShop;
    public GameObject CanvasCredits;
    public GameObject CanvasCurlingMatch;

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