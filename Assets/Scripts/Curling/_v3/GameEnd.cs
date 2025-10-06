using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the high-level game manager for a curling game.
/// It orchestrates the flow of the game, including starting new ends,
/// managing player turns, and handling the end of the game.
/// </summary>

namespace CurlingManagersV3
{
    public class GameEnd : MonoBehaviour
    {
        [Header("Setup Settings")]
        public bool isGameStarted = false;
        public bool isGamePaused = false;
        public bool isGameEnded = false;

        [Header("[Data] Exit Information")]
        public string exitToScene;
        public string exitSpawnId;

        
        // base skin used for each team
        // This method will takes the exiting stone prefab, and instantiate 5 stones for each team at 
        // the specified spawn locations in the scene. 
        public void SetExitInfo(
            string sceneName,
            string spawnId
        ){
            exitToScene = sceneName;
            exitSpawnId = spawnId;
        }

        public void ExitGame(){
            
        }
    
        
        public bool ShouldGameStart(){
            return true;
        }

        public bool ShouldGameEnd(){
            return false; //CurlingManager._instance.turnManager.currentTurnCount >= CurlingManager._instance.turnManager.maxTurnCount;

        }

        public void EndCurrentCurlingGame(){

        }

        public void EndCurlingGame(){

        }

        public void ExitCurlingGame()
        {
            // Clear Existing Stones
            // Clear Team People
            // Reset All Necessary Things 
            // GameManager._instance.TeleportToScene(
            //     exitScene,
            //     exitSpawnId
            // );
        }


    }
}