using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : UIController
{
    /// <summary>
    /// These are the scripts used for the Main Menu Screen that starts the game
    /// </summary>
    /// 
    private void Update()
    {
        MainState currentState = MainManager._instance.currentState;

        switch (currentState)
        {
            case MainState.MainMenu:
                // make things visible
                Show();
                break;
            default:
                Hide();
                // hide this canvas
                break;
        }
    }

    public void LaunchContinueGame(){
        // Opens up the 'ContinueGame' experience
        // TODO: Create launcher script for a continue game experience
        SceneManager.LoadScene(sceneName: "Scene_Curling");
    }

    public void LaunchLoadGame(){
        // Opens up the 'start new game' experience
        SceneManager.LoadScene(sceneName: "Scene_Menu_LoadGame");
    }

    public void LaunchSettings(){
        // Opens up the settings screen
        // TODO: Create launcher script for a settings experience
        // TODO: Create a global settings file

        SceneManager.LoadScene(sceneName: "Scene_Menu_Settings");
    }

    public void LaunchMultiplayer(){
        // Opens up the multiplayer menu
        SceneManager.LoadScene(sceneName: "Scene_Curling");
    }

    public void LaunchNewGame(){
        
    }

    public void LaunchMainMenu(){
        
    }
}
