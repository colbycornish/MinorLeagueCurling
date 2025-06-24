

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostThrowResultCanvasController : MonoBehaviour
{

    /// <summary>
    /// Public Variables of Global Settings
    /// </summary>
    public GameObject content;
    public GameObject shadow;

    public void Start(){
        
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

    public void OnEnable() {
        // This method is called when the canvas is enabled
        Debug.Log("Post Throw Result Canvas Enabled");
        ShowPostThrowResult("GOOD THROW!");
    }
    public void OnDisable() {
        // This method is called when the canvas is disabled
        Debug.Log("Post Throw Result Canvas Disabled");
        HidePostThrowResult();
    }




    public void ShowPostThrowResult(string resultMessage) {
        // Display the result message on the canvas
        content.SetActive(true);
        shadow.SetActive(true);
        Debug.Log("Post Throw Result: " + resultMessage);
    }
    public void HidePostThrowResult() {
        // Hide the result message and reset the canvas
        content.SetActive(false);
        shadow.SetActive(false);
        Debug.Log("Post Throw Result Canvas Hidden");
    }

}
