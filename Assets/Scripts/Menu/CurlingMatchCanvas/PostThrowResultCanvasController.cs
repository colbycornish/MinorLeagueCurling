

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PostThrowResultCanvasController : MonoBehaviour
{

    /// <summary>
    /// Public Variables of Global Settings
    /// </summary>
    private string teamName_1 = "BLUE TEAM";
    private string teamName_2 = "RED TEAM";
    public GameObject content;

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

}
