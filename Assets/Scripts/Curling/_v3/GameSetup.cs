using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using Curling.Rules;
using CurlingObjects;

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

            // Announcer Booth Setup
            CurlingManager._instance.Parameters.Course.announcerArea.Setup();
    
            // Camera Setup
            CurlingManager._instance.cameraController.Setup(
                stoneCamera: CurlingManager._instance.stoneCamera,
                ccStoneCamera: CurlingManager._instance.ccStoneCamera,
                targetZoneCamera: CurlingManager._instance.stoneCamera,
                throwerCamera: CurlingManager._instance.stoneCamera, 
                courseCamera: CurlingManager._instance.stoneCamera,
                announcersCamera: CurlingManager._instance.stoneCamera
            );
            AnnouncerBooth ab = courseData.announcerBooth;

            CurlingManager._instance.Parameters.Cinematics.ccDollyInAwayTeamCamera = courseData.ccDollyInTeamAway;
            CurlingManager._instance.Parameters.Cinematics.ccDollyInHomeTeamCamera = courseData.ccDollyInTeamHome;
            CurlingManager._instance.Parameters.Cinematics.ccDollyAnnouncerCamera = CurlingManager._instance.Parameters.Course.announcerArea.dollyTwoShotCamera;
            CurlingManager._instance.Parameters.Cinematics.ccProfileMickAnnouncerCamera = CurlingManager._instance.Parameters.Course.announcerArea.mickProfileCamera;
            CurlingManager._instance.Parameters.Cinematics.ccProfileBroomyAnnouncerCamera = CurlingManager._instance.Parameters.Course.announcerArea.broomyProfileCamera;

            // Stones
            CurlingManager._instance.stoneManager.Setup(
                courseData: courseData,
                teamHome: teamHome,
                teamAway: teamAway
            );

            // Players
            CurlingManager._instance.Players.Setup(
                curlingCourse: courseData,
                teamHome: teamHome,
                teamAway: teamAway
            );

            // End Game Setup
            CurlingManager._instance.gameEndManager.SetExitInfo(
                sceneName: exitSceneName,
                spawnId: exitSpawnId
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
            CurlingManager._instance.Parameters.Course.stoneBenchTeamHome = courseData.stoneBenchTeamHome;
            CurlingManager._instance.Parameters.Course.stoneBenchTeamAway = courseData.stoneBenchTeamAway;

            CurlingManager._instance.Parameters.Course.directionPivotObject = courseData.directionalPivot;
            CurlingManager._instance.Parameters.Course.directionPivot = courseData.directionalPivot.transform;
            
            CurlingManager._instance.Parameters.Course.targetZone = courseData.targetZone;

            CurlingManager._instance.Parameters.Course.launchPoint = courseData.launchPoint;

            CurlingManager._instance.Parameters.Course.announcerArea = courseData.announcerBooth;
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
            CurlingManager._instance.Parameters.Status.isReady = false;
            CurlingManager._instance.Parameters.Status.isLoading = true;

            Debug.Log("Game Setup: Curling Game is Loading...");
        }

        public void MarkAsReady(){
            CurlingManager._instance.Parameters.Status.isReady = true;
            CurlingManager._instance.Parameters.Status.isLoading = false;
            
            Debug.Log("Game Setup: Curling Game is Ready.");
            CurlingManager._instance.ChangePhase(CurlingMatchPhase.StartGameIntro);
        }
    }
}



// CurlingManager._instance.Sweeping.Setup();
            // CurlingManager._instance.rules = 
            // .Setup( 
            //     rulesData: rulesData,
            //     gameData: gameData,
            //     teamHome: teamHome,
            //     teamAway: teamAway
            // );
            //