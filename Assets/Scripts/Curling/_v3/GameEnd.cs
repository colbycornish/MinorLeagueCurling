using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// </summary>

namespace CurlingManagersV3
{
    public class GameEnd : MonoBehaviour
    {
        // [Header("Setup Settings")]
        // public bool isGameStarted = false;
        // public bool isGamePaused = false;
        // public bool isGameEnded = false;

        [Header("[Data] Exit Information")]
        public string exitToScene;
        public string exitSpawnId;

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