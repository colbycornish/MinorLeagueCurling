using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using Curling.Rules;

/// <summary>
/// </summary>

namespace CurlingManagersV3
{
    public class GameSetup : MonoBehaviour
    {
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
            // Setup Locations
            
            CurlingManager._instance.Parameters.Course.course = courseData; 
            SetupLocations(courseData: courseData);
            
            CurlingManager._instance.Parameters.Course.directionPivotObject = courseData.directionalPivot;
            CurlingManager._instance.Parameters.Course.directionPivot = courseData.directionalPivot.transform;
            
        
            CurlingManager._instance.cameraController.Setup(
                stoneCamera: CurlingManager._instance.stoneCamera,
                targetZoneCamera: CurlingManager._instance.stoneCamera,
                throwerCamera: CurlingManager._instance.stoneCamera, 
                courseCamera: CurlingManager._instance.stoneCamera,
                announcersCamera: CurlingManager._instance.stoneCamera
            );

            CurlingManager._instance.Sweeping.Setup();
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
            CurlingManager._instance.Players.Setup(
                curlingCourse: courseData,
                teamHome: teamHome,
                teamAway: teamAway
            );

            CurlingManager._instance.Parameters.CurrentGameScore.ResetScore();

            MarkAsReady();
        }

        public void SetupLocations(CurlingCourseData courseData)
        {
            CurlingManager._instance.Parameters.Course.idleLocationsTeamHome = courseData.idleLocationsTeamHome;
            CurlingManager._instance.Parameters.Course.idleLocationsTeamAway = courseData.idleLocationsTeamAway;

            CurlingManager._instance.Parameters.Course.throwerStartLocation = courseData.throwerStartLocation;
            CurlingManager._instance.Parameters.Course.sweeperLStartLocation = courseData.sweeperLStartLocation;
            CurlingManager._instance.Parameters.Course.sweeperRStartLocation = courseData.sweeperRStartLocation;

            CurlingManager._instance.Parameters.Course.stonesSpawnLocationsTeamHome = courseData.stonesSpawnLocationsTeamHome;
            CurlingManager._instance.Parameters.Course.stonesSpawnLocationsTeamAway = courseData.stonesSpawnLocationsTeamAway;
            
            CurlingManager._instance.Parameters.Course.targetZone = courseData.targetZone;

            CurlingManager._instance.Parameters.Course.launchPoint = courseData.launchPoint;
        }


        public void ResetAll(){
            CurlingManager._instance.Parameters.Status.isReady = false;
            CurlingManager._instance.Parameters.Status.isLoading = false;

            CurlingManager._instance.Aiming.Reset();
            // CurlingManager._instance.courseController.Reset();
            CurlingManager._instance.Parameters.Course.course = null;
            CurlingManager._instance.Parameters.Course.targetZone = null;

            CurlingManager._instance.Scoring.Reset();
            CurlingManager._instance.stoneManager.Reset();
            CurlingManager._instance.Players.Reset();
            CurlingManager._instance.TurnManager.Reset();
        }

        public void MarkAsLoading(){
            CanvasManager._instance.OpenCanvas(newState: CanvasState.CurlingMatch);

            CurlingManager._instance.Parameters.Status.isReady = false;
            CurlingManager._instance.Parameters.Status.isLoading = true;

            Debug.Log("Game Setup: Curling Game is Loading...");

            CurlingManagersV3.MatchPhaseManager._instance.SetPhase(
                newPhase: CurlingMatchPhase.Loading
            );
        }

        public void MarkAsReady(){
            CurlingManager._instance.Parameters.Status.isReady = true;
            CurlingManager._instance.Parameters.Status.isLoading = false;
            
            Debug.Log("Game Setup: Curling Game is Ready.");
            CurlingManager._instance.cameraController.PlayCourseIntroTimeline();
            CurlingManagersV3.MatchPhaseManager._instance.SetPhase(
                newPhase: CurlingMatchPhase.RoundSplash
            );
        }
    }
}