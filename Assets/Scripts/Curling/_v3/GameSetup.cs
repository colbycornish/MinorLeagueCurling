using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using Curling.Rules;

/// <summary>
/// This is the high-level game manager for a curling game.
/// It orchestrates the flow of the game, including starting new ends,
/// managing player turns, and handling the end of the game.
/// </summary>

namespace CurlingManagersV3
{
    public class GameSetup : MonoBehaviour
    {
        [Header("Setup Settings")]
        public bool isReady = false;
        public bool isLoading = false;
        
        // base skin used for each team
        // This method will takes the exiting stone prefab, and instantiate 5 stones for each team at 
        // the specified spawn locations in the scene. 
        public void SetupAll(
            CurlingRules rulesData,
            CurlingCourseData courseData,
            CurlingTeam teamHome,
            CurlingTeam teamAway,
            CurlingGameData gameData,
            string exitSceneName,
            string exitSpawnId
        )
        {
            ResetAll();
            Debug.Log("Game Setup: Initializing Curling Game...");
            MarkAsLoading();
            CurlingManager._instance.aiming.Setup(
                courseData: courseData
            );
            CurlingManager._instance.courseController.Setup(
                courseData: courseData
            );
            CurlingManager._instance.scoring.Setup(
                courseData: courseData
            );
            CurlingManager._instance.throwing.Setup(
                powerMeter: CurlingManager._instance.powerMeterController
            );
            CurlingManager._instance.cameraController.Setup(
                stoneCamera: CurlingManager._instance.stoneCamera,
                targetZoneCamera: CurlingManager._instance.stoneCamera,
                throwerCamera: CurlingManager._instance.stoneCamera,
                courseCamera: CurlingManager._instance.stoneCamera,
                announcersCamera: CurlingManager._instance.stoneCamera
            );
            CurlingManager._instance.sweeping.Setup();
            // CurlingManager._instance.rules = 
            // .Setup(
            //     rulesData: rulesData,
            //     gameData: gameData,
            //     teamHome: teamHome,
            //     teamAway: teamAway
            // );
            //


            CurlingManager._instance.stoneManager.Setup(
                courseData: courseData,
                teamHome: teamHome,
                teamAway: teamAway
            );
            CurlingManager._instance.gameEndManager.SetExitInfo(
                sceneName: exitSceneName,
                spawnId: exitSpawnId
            );
            CurlingManager._instance.players.Setup(
                curlingCourse: courseData,
                teamHome: teamHome,
                teamAway: teamAway
            );
            MarkAsReady();
        }

        public void ResetAll(){
            isReady = false;
            isLoading = false;
            CurlingManager._instance.aiming.Reset();
            CurlingManager._instance.courseController.Reset();
            CurlingManager._instance.scoring.Reset();
            CurlingManager._instance.stoneManager.Reset();
            CurlingManager._instance.players.Reset();
            CurlingManager._instance.turnManager.Reset();
        }

        public void MarkAsLoading(){
            CanvasManager._instance.OpenCanvas(newState: CanvasState.CurlingMatch);
            isReady = false;
            isLoading = true;
            Debug.Log("Game Setup: Curling Game is Loading...");
            CurlingManagersV3.MatchPhaseManager._instance.SetPhase(
                newPhase: CurlingManagersV3.CurlingMatchPhase.Loading
            );
        }

        public void MarkAsReady(){
            isReady = true;
            isLoading = false;
            Debug.Log("Game Setup: Curling Game is Ready.");
            CurlingManagersV3.MatchPhaseManager._instance.SetPhase(
                newPhase: CurlingManagersV3.CurlingMatchPhase.RoundSplash
            );
        }

        


    }
}