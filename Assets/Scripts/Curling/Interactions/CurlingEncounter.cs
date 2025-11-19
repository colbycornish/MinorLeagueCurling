using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

/// <summary>
/// This is the high-level game manager for a curling game.
/// It orchestrates the flow of the game, including starting new ends,
/// managing player turns, and handling the end of the game.
/// </summary>

namespace CurlingManagersV3
{
    public class CurlingEncounter : MonoBehaviour
    {
        [Header("Enable Encounter Launcher")]
        public bool isEnabled = true;

        [Header("Critical Information")]
        public CurlingCourseData courseData;
        public CurlingTeam teamHome;
        public CurlingTeam teamAway;
        public CurlingTeamData teamDataA;
        public CurlingTeamData teamDataB;

        [Header("Settings")]
        public CurlingGameData gameData;

        [Header("Timelines")]
        public PlayableDirector courseIntroTimeline;

        [Header("End Game Location")]
        public string exitScene;
        public string exitSpawnId;


        [Header("Useful Tools")]
        public bool demoHasBeenLoaded = false;
        public KeyCode beginKey = KeyCode.T;
        public KeyCode resetAllKey = KeyCode.R;


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
            CurlingManager._instance.setup.SetupAll(
                rulesData: rules,
                courseData: courseData,
                teamHome: teamHome,
                teamAway: teamAway,
                gameData: gameData,
                exitSceneName: exitScene,
                exitSpawnId: exitSpawnId
            );
            demoHasBeenLoaded = true;
        }


        /// <summary>
        /// Play Timelines
        /// </summary>

        public void PlayCourseIntroTimeline()
        {
            // timeline = GetComponent<PlayableDirector>();
            courseIntroTimeline.Play();
        }

        /// <summary>
        /// Prep Demo Data
        /// </summary>

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
                5, // roundsPerGame
                1, // turnsPerTeamPerRound
                5, // maxStonesPerTeam
                false, // isPracticeMode
                false, // isTimedMode
                0.0f, // timeLimit
                false, // enableObstaclePlacementByPlayer
                false // enableObstaclePlacementByEnvironment
            );
        }

        public void PrepExitLocation()
        {
            exitScene = "Town";
            exitSpawnId = "Town_Curling_Sheet_Underground";
        }

    }
}


