// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public enum CanvasMainMenuState
// {
//     None,
//     Loading,
//     MainMenu,
//     NewGame,
//     LoadGame,
//     Settings,
//     Controls,
//     QuickCurl,
//     Multiplayer,
//     ExitGame
// }

// public class MainMenuCanvasController : UIController
// {
//     /// <summary>
//     /// These are the scripts used for the Main Menu Screen that starts the game
//     /// </summary>

//     public GameObject CanvasMainMenu;
//     public GameObject CanvasNewGame;
//     public GameObject CanvasLoadGame;
//     public GameObject CanvasSettings;
//     public GameObject CanvasQuickCurl;
//     public GameObject CanvasControls;
//     public GameObject CanvasMultiplayer;
//     public GameObject CanvasExitGame;

//     public CanvasMainMenuState currentState = CanvasMainMenuState.None;

//     // private void Update()
//     // {
//     //     MainState currentMainState = MainManager._instance.currentState;

//     //     switch (currentMainState)
//     //     {
//     //         case MainState.MainMenu:
//     //             OpenSection(CanvasMainMenuState.MainMenu);
//     //             // make things visible
//     //             Show();
//     //             break;
//     //         default:
//     //             Hide();
//     //             // hide this canvas
//     //             break;
//     //     }
//     // }
    
    

//     public void ContinueGame()
//     {
//         MainManager._instance.SetState(MainState.Playing);
//     }
    
//     public void ShowMainMenu()
//     {
//         OpenSection(CanvasMainMenuState.MainMenu);
//     }

//     public void ShowNewGameCanvas()
//     {
//         OpenSection(CanvasMainMenuState.NewGame);
//     }

//     public void ShowLoadGameCanvas(){
//         OpenSection(CanvasMainMenuState.LoadGame);
//     }

//     public void ShowSettingsCanvas(){
//         // Opens up the settings screen
//         OpenSection(CanvasMainMenuState.Settings);
//     }

//     public void ShowControlsCanvas(){
//         // Opens up the settings screen
//         OpenSection(CanvasMainMenuState.Controls);
//     }

//     public void ShowMultiplayerCanvas(){
//         OpenSection(CanvasMainMenuState.Multiplayer);
//     }

//     public void ShowQuickCurlCanvas(){
//         OpenSection(CanvasMainMenuState.QuickCurl);
//     }



//     public void OpenSection(CanvasMainMenuState newState)
//     {
//         Debug.Log("Opening section: " + newState.ToString());
//         if (currentState == newState) return;

//         // Disable all canvases first
//         CloseAllSections();

//         // Enable the selected canvas
//         switch (newState)
//         {
//             case CanvasMainMenuState.MainMenu:
//                 EnableCanvas(CanvasMainMenu);
//                 break;
//             case CanvasMainMenuState.NewGame:
//                 EnableCanvas(CanvasNewGame);
//                 break;
//             case CanvasMainMenuState.LoadGame:
//                 EnableCanvas(CanvasLoadGame);
//                 break;
//             case CanvasMainMenuState.Settings:
//                 EnableCanvas(CanvasSettings);
//                 break;
//             case CanvasMainMenuState.QuickCurl:
//                 EnableCanvas(CanvasQuickCurl);
//                 break;
//             case CanvasMainMenuState.Controls:
//                 EnableCanvas(CanvasControls);
//                 break;
//             case CanvasMainMenuState.Multiplayer:
//                 EnableCanvas(CanvasMultiplayer);
//                 break;
//             case CanvasMainMenuState.ExitGame:
//                 EnableCanvas(CanvasExitGame);
//                 break;
//             case CanvasMainMenuState.Loading:
//             default:
//                 // No canvas to enable
//                 break;
//         }

//         currentState = newState;
//     }
    
//     public void CloseAllSections()
//     {
//         DisableCanvas(CanvasMainMenu);
//         DisableCanvas(CanvasNewGame);
//         DisableCanvas(CanvasLoadGame);
//         DisableCanvas(CanvasSettings);
//         DisableCanvas(CanvasControls);
//         DisableCanvas(CanvasMultiplayer);
//         DisableCanvas(CanvasExitGame);
//     }

//     // Method to enable a specific canvas
//     public void EnableCanvas(GameObject canvas){
//         canvas.SetActive(true);
//     }


//     // Method to disable a specific canvas
//     public void DisableCanvas(GameObject canvas){
//         canvas.SetActive(false);
//     }
// }
