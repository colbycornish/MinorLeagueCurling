
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurlingGameCanvasController : MonoBehaviour
{

    /// <summary>
    /// Public Variables of Global Settings
    /// </summary>

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

}
