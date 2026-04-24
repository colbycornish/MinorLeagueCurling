// using System.Collections;
// using UnityEngine;
// using UnityEngine.AddressableAssets;
// using UnityEngine.ResourceManagement.AsyncOperations;

// public enum CanvasPauseMenuState
// {
//     None,
//     Loading,
//     PauseMenu,
//     SaveGame,
//     LoadGame,
//     Settings,
//     Controls,
//     ExitGame
// }

// public class PauseMenuController : UIController
// {
//     public GameObject CanvasPauseMenu;
//     public GameObject CanvasLoadGame;
//     public GameObject CanvasSaveGame;
//     public GameObject CanvasSettings;
//     public GameObject CanvasControls;
//     public GameObject CanvasExitGame;

//     public CanvasPauseMenuState currentState = CanvasPauseMenuState.None;

//     private void Update()
//     {
//         MainState currentMainState = MainManager._instance.currentState;

//         switch (currentMainState)
//         {
//             case MainState.Paused:
//                 // make things visible
//                 Show();
//                 break;
//             default:
//                 Hide();
//                 // hide this canvas
//                 break;
//         }
//     }

//     public void ContinueGame()
//     {
//         MainManager._instance.SetState(MainState.Playing);
//     }
    
//     public void ShowPauseMenu()
//     {
//         OpenSection(CanvasPauseMenuState.PauseMenu);
//     }
 
//     public void ShowSaveGameCanvas()
//     {
//         OpenSection(CanvasPauseMenuState.SaveGame);
//     }

//     public void ShowLoadGameCanvas(){
//         OpenSection(CanvasPauseMenuState.LoadGame);
//     }

//     public void ShowSettingsCanvas(){
//         OpenSection(CanvasPauseMenuState.Settings);
//     }

//     public void ShowControlsCanvas(){
//         OpenSection(CanvasPauseMenuState.Controls);
//     }

//     public void ShowExitGameCanvas(){
//         OpenSection(CanvasPauseMenuState.ExitGame);
//     }


//     public void OpenSection(CanvasPauseMenuState newState)
//     {
//         if (currentState == newState) return;

//         // Disable all canvases first
//         CloseAllSections();

//         // Enable the selected canvas
//         switch (newState)
//         {
//             case CanvasPauseMenuState.PauseMenu:
//                 EnableCanvas(CanvasPauseMenu);
//                 break;
//             case CanvasPauseMenuState.SaveGame:
//                 EnableCanvas(CanvasSaveGame);
//                 break;
//             case CanvasPauseMenuState.LoadGame:
//                 EnableCanvas(CanvasLoadGame);
//                 break;
//             case CanvasPauseMenuState.Settings:
//                 EnableCanvas(CanvasSettings);
//                 break;
//             case CanvasPauseMenuState.Controls:
//                 EnableCanvas(CanvasControls);
//                 break;
//             case CanvasPauseMenuState.ExitGame:
//                 EnableCanvas(CanvasExitGame);
//                 break;
//             case CanvasPauseMenuState.Loading:
//             default:
//                 // No canvas to enable
//                 break;
//         }

//         currentState = newState;
//     }
    
//     public void CloseAllSections()
//     {
//         DisableCanvas(CanvasPauseMenu);
//         DisableCanvas(CanvasSaveGame);
//         DisableCanvas(CanvasLoadGame);
//         DisableCanvas(CanvasSettings);
//         DisableCanvas(CanvasControls);
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