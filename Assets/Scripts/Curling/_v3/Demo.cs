using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

/// <summary>
/// </summary>

namespace CurlingManagersV3
{
    public class Demo : MonoBehaviour
    {
        [Header("Use Demo Launcher")]
        public bool isEnabled = true;

        [Header("Critical Information")]
        public CurlingCourseData courseData;
        public CurlingTeam teamHome;
        public CurlingTeam teamAway;
        public CurlingTeamData teamDataA;
        public CurlingTeamData teamDataB;

        [Header("Settings")]
        public CurlingGameData gameData;

        [Header("End Game Location")]
        public string exitScene;
        public string exitSpawnId;


        [Header("Useful Tools")]
        public bool demoHasBeenLoaded = false;
        public KeyCode beginKey = KeyCode.T;
        public KeyCode resetAllKey = KeyCode.R;
        

        // base skin used for each team
        // This method will takes the exiting stone prefab, and instantiate 5 stones for each team at 
        // the specified spawn locations in the scene. 
        public void Start()
        {
            Debug.Log("Demo Manager Ready. Press " + beginKey.ToString() + " to begin the demo.");
        }

        public void Update()
        {
            if (!demoHasBeenLoaded && isEnabled && Input.GetKeyDown(beginKey))
            {
                StartDemo();
            }
            if (demoHasBeenLoaded && isEnabled && Input.GetKeyDown(resetAllKey))
            {
                // StartDemo();
            }
        }

        public void ResetDemo()
        {
            demoHasBeenLoaded = false;
        }

        public void StartDemo()
        {
            PrepDemoTeamData();
            PrepGameDataSettings();
            PrepExitLocation();
            CurlingRules rules = new CurlingRules();
            rules.SetDefaultRules();
            CurlingManager._instance.Setup.SetupAll(
                rulesData: rules,
                courseData: courseData,
                teamHome: teamHome,
                teamAway: teamAway,
                gameData: gameData,
                exitSceneName: exitScene,
                exitSpawnId: exitSpawnId
            );

            
            // CurlingManager._instance.InitSetupFromDemo(
            //     courseData,
            //     teamHome,
            //     teamAway,
            //     gameData,
            //     exitScene,
            //     exitSpawnId
            // );
            demoHasBeenLoaded = true;
        }

        public void PrepDemoTeamData()
        {
            teamHome.SetData();
            teamAway.SetData();

            teamDataA = teamHome.data;
            teamDataB = teamAway.data;
        }

        public void PrepGameDataSettings()
        {
            gameData.Reset();
            gameData.settings.SetSettings(
                roundsPerGame: 5, // roundsPerGame
                turnsPerTeamPerRound: 1, // turnsPerTeamPerRound
                maxStonesPerTeam: 5, // maxStonesPerTeam
                isPracticeMode: false, // isPracticeMode
                isTimedMode: false, // isTimedMode
                timeLimit: 0.0f, // timeLimit
                enableObstaclePlacementByPlayer: false, // enableObstaclePlacementByPlayer
                enableObstaclePlacementByEnvironment: false // enableObstaclePlacementByEnvironment
            );
        }

        public void PrepExitLocation()
        {
            exitScene = "Town";
            exitSpawnId = "Town_Curling_Sheet_Underground";
        }
    }
}