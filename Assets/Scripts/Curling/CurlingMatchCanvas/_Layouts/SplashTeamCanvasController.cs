

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashTeamCanvasController : MonoBehaviour
{

    /// <summary>
    /// Public Variables of Global Settings
    /// </summary>
    private string canvasId = "CANVAS_SPLASH_TEAM";
    private string teamName_1 = "BLUE TEAM";
    private string teamName_2 = "RED TEAM";
    public GameObject content;
    public GameObject shadow;

    public void Start()
    {

    }

    public void LoadExistingSettings()
    {
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

    public string GetCanvasId()
    {
        return canvasId;
    }
    public void SetCanvasId(string id)
    {
        canvasId = id;
    }
    public void OnEnable()
    {
        // This method is called when the canvas is enabled
        ShowSplashTeam();
    }
    public void OnDisable()
    {
        // This method is called when the canvas is disabled
        HideSplashTeam();
    }
    public void ShowSplashTeam()
    {
        // Display the splash team on the canvas
        content.SetActive(true);
        shadow.SetActive(true);
        Debug.Log("Splash Team Displayed: " + teamName_1 + " vs " + teamName_2);
    }
    public void HideSplashTeam()
    {
        // Hide the splash team on the canvas
        content.SetActive(false);
        shadow.SetActive(false);
        Debug.Log("Splash Team Hidden");
    }
    public void SetTeamNames(string team1, string team2)
    {
        teamName_1 = team1;
        teamName_2 = team2;
        Debug.Log("Teams set: " + teamName_1 + " vs " + teamName_2);
    }

}
