using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// These are the scripts used for the Main Menu Screen that starts the game
    /// </summary>

    public void LoadGame(){
        // Opens up the 'start new game' experience
        SceneManager.LoadScene(sceneName: "Scene_Curling");
    }


    public void LaunchContinueGame(){
        // Opens up the 'ContinueGame' experience
        // TODO: Create launcher script for a continue game experience
        SceneManager.LoadScene(sceneName: "Scene_Curling");
    }

    public void LaunchSettings(){
        // Opens up the settings screen
        // TODO: Create launcher script for a settings experience
        // TODO: Create a global settings file

        SceneManager.LoadScene(sceneName: "Scene_Curling");
    }

    public void LaunchMultiplayer(){
        // Opens up the multiplayer menu
        SceneManager.LoadScene(sceneName: "Scene_Curling");
    }

    public void LaunchNewGame(){
        // Opens up the 'start new game' experience
        SceneManager.LoadScene(sceneName: "Scene_Curling");
    }
}
