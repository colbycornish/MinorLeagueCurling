

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashRoundCanvasController : MonoBehaviour
{

    /// <summary>
    /// Public Variables of Global Settings
    /// </summary>
    private string canvasId = "CANVAS_SPLASH_ROUND";
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
        Debug.Log("Splash Round Canvas Enabled");
        ShowSplashRound();
    }
    public void OnDisable() {
        // This method is called when the canvas is disabled
        Debug.Log("Splash Round Canvas Disabled");
        HideSplashRound();
    }

    
    public void ShowSplashRound() {
        // Display the splash round on the canvas
        content.SetActive(true);
        shadow.SetActive(true);
        Debug.Log("Splash Round Displayed");
    }
    public void HideSplashRound() {
        // Hide the splash round from the canvas
        content.SetActive(false);
        shadow.SetActive(false);
        Debug.Log("Splash Round Hidden");
    }

}
