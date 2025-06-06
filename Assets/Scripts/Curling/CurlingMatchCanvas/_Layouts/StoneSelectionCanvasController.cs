

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoneSelectionCanvasController : MonoBehaviour
{

    /// <summary>
    /// Public Variables of Global Settings
    /// </summary>
    private string canvasId = "CANVAS_STONE_SELECTION";
    public GameObject content;
    public GameObject shadow;

    public void Start(){
        LoadExistingSettings();
    }

    public void LoadExistingSettings(){
        // Loads the existing User settings file, that determines things like:
        // - Audio levels
        // - Displays
        //  - Brightness
        //  - Gamma
        // - Controls
        //  - Controller
        // - Platform
        // - Difficulty
        // - Dev Settings & Features
    }

    public string GetCanvasId() {
        return canvasId;
    }
    public void SetCanvasId(string id) {
        canvasId = id;
    }

    public void OnEnable() {
        // This method is called when the canvas is enabled
        Debug.Log("Stone Selection Canvas Enabled");
        ShowStoneSelection();
    }
    public void OnDisable() {
        // This method is called when the canvas is disabled
        Debug.Log("Stone Selection Canvas Disabled");
        HideStoneSelection();
    }



    public void ShowStoneSelection() {
        // Display the stone selection on the canvas
        content.SetActive(true);
        shadow.SetActive(true);
        Debug.Log("Stone Selection Displayed");
    }
    public void HideStoneSelection() {
        // Hide the stone selection on the canvas
        content.SetActive(false);
        shadow.SetActive(false);
        Debug.Log("Stone Selection Hidden");
    }



    public void SelectStone(int stoneIndex) {
        // Logic to handle stone selection
        Debug.Log("Stone Selected: " + stoneIndex);
        // You can add more logic here to update the game state or UI
    }
    public void ConfirmSelection() {
        // Logic to confirm the stone selection
        Debug.Log("Stone Selection Confirmed");
        // You can add more logic here to proceed to the next step in the game
    }
    public void CancelSelection() {
        // Logic to cancel the stone selection
        Debug.Log("Stone Selection Canceled");
        // You can add more logic here to revert any changes made during selection
    }

}
