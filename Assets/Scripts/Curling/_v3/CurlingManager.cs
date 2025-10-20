using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
/// <summary>
/// This is the high-level game manager for a curling game.
/// It orchestrates the flow of the game, including starting new ends,
/// managing player turns, and handling the end of the game.
/// </summary>




namespace CurlingManagersV3
{
    [RequireComponent(typeof(CurlingManagersV3.Aiming))]
    [RequireComponent(typeof(CurlingManagersV3.CourseController))]
    [RequireComponent(typeof(CurlingManagersV3.Players))]
    [RequireComponent(typeof(CurlingManagersV3.Scoring))]
    [RequireComponent(typeof(CurlingManagersV3.Stones))]
    [RequireComponent(typeof(CurlingManagersV3.Sweeping))]
    [RequireComponent(typeof(CurlingManagersV3.Throwing))]
    //
    [RequireComponent(typeof(CurlingManagersV3.Demo))]
    [RequireComponent(typeof(CurlingManagersV3.CurlingCameraController))]
    [RequireComponent(typeof(CurlingManagersV3.CurlingInputManager))]
    [RequireComponent(typeof(CurlingManagersV3.GameSetup))]
    [RequireComponent(typeof(CurlingManagersV3.GameTurn))]
    [RequireComponent(typeof(CurlingManagersV3.GameEnd))]

    public class CurlingManager : MonoBehaviour
    {

        // [HideInInspector] 
        public static CurlingManager _instance { get; private set; }
        public CurlingManagersV3.Aiming aiming;
        public CurlingManagersV3.CourseController courseController;
        public CurlingManagersV3.Players players;
        public CurlingManagersV3.Scoring scoring;
        public CurlingManagersV3.Stones stoneManager;
        public CurlingManagersV3.Sweeping sweeping;
        public CurlingManagersV3.Throwing throwing;
        

        // Helper Managers
        public CurlingManagersV3.Demo demo;
        public CurlingManagersV3.CurlingCameraController cameraController;
        public CurlingManagersV3.CurlingInputManager curlingInputManager;
        public CurlingManagersV3.GameSetup setup;
        public CurlingManagersV3.GameTurn turnManager;
        public CurlingManagersV3.GameEnd gameEndManager;

        [Header("Teams")]
        public CurlingTeam teamHome;
        public CurlingTeam teamAway;

        [Header("[Data] Player")]
        CurlingTeam teamHome_tmp; // temporary for storage
        CurlingTeam teamAway_tmp; // temporary for storage

        [Header("Course")]
        public CurlingCourseData course; // temporary for storage
        CurlingCourseData courseData_tmp; // temporary for storage

        [Header("Rules & Settings")]
        public CurlingRules rules;
        // public CurlingRules settings;

        [Header("Canvas Objects")]
        public PowerMeterController powerMeterController;
        public GameObject leftSweeperExhaustionBar;
        public GameObject rightSweeperExhaustionBar;

        [Header("[Data] Course")]
        public Camera stoneCamera;
        
        // [Header("[Data] Exit Information")]
        // public string exitScene;
        // public string exitSpawnId;

        [Header("[Data] Settings")]
        public CurlingGameData gameData;
        public int turnsMax = 10;


        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                Init();
                return;
            }
            _instance = this;
            Init();
        }

        public void Init(){
            if (aiming == null) aiming = GetComponent<CurlingManagersV3.Aiming>();
            if (courseController == null) courseController = GetComponent<CurlingManagersV3.CourseController>();
            if (players == null) players = GetComponent<CurlingManagersV3.Players>();
            if (scoring == null) scoring = GetComponent<CurlingManagersV3.Scoring>();
            if (stoneManager == null) stoneManager = GetComponent<CurlingManagersV3.Stones>();
            if (sweeping == null) sweeping = GetComponent<CurlingManagersV3.Sweeping>();
            if (throwing == null) throwing = GetComponent<CurlingManagersV3.Throwing>();
            //
            if (demo == null) demo = GetComponent<CurlingManagersV3.Demo>();
            if (cameraController == null) cameraController = GetComponent<CurlingManagersV3.CurlingCameraController>();
            if (curlingInputManager == null) curlingInputManager = GetComponent<CurlingManagersV3.CurlingInputManager>();
            if (setup == null) setup = GetComponent<CurlingManagersV3.GameSetup>();
            if (turnManager == null) turnManager = GetComponent<CurlingManagersV3.GameTurn>();
            if (gameEndManager == null) gameEndManager = GetComponent<CurlingManagersV3.GameEnd>();
        }

    
        /// <summary>
        /// Setup data
        /// </summary>

      


        public void LoadInitialData()
        {
            // TODO: Adjust to grab information from a singlular storage spot
            // CurlingCourseData courseData = dataManager._instance.curlingGame.courseData;
            // CurlingTeam teamHome = dataManager._instance.curlingGame.teamHome;
            // CurlingTeam teamAway = dataManager._instance.curlingGame.teamAway;

            CurlingCourseData courseData = courseData_tmp;
            CurlingTeam teamHome = teamHome_tmp;
            CurlingTeam teamAway = teamAway_tmp;

            // aiming.Setup(courseData);
            // players.Setup(
            //     courseData,
            //     teamHome,
            //     teamAway
            // );

            // scoring.SetTargetZone(courseData.targetZone);

            // stoneManager.Setup(
            //     courseData,
            //     teamHome,
            //     teamAway
            // );

            StartCurlingGame();
            // CurlingManager._instance.StartCurlingGame();
        }

        /// <summary>
        /// Start Game
        /// </summary>

        // Starts the curling game.
        private void StartCurlingGame()
        {
            // CurlingMatchPhaseManager._instance.SetPhase(CurlingMatchPhase.Loading);
            // gameData.turnCurrent = 0;

            // // Resets the current game layout, and removes the existing stones
            // scoring.ResetCurlingGame();

            // // Sets up the stone objects for use in the game
            // stoneManager.SetupStones();

            // //Sets up the players
            // players.PreparePlayers();

            // // Sets the initial phase of the match
            // CurlingMatchPhaseManager._instance.SetPhase(CurlingMatchPhase.RoundSplash);
            // // OnStoneSelectionConfirmed();
        }

        public void HandleEndOfTurn()
        {
            // Called when a stone finishes moving, and is now resting in the target zone.
            scoring.CalculateScore();
            // players.UpdateScore(scoring.GetScore());
            // CurlingMatchPhaseManager._instance.SetPhase(CurlingMatchPhase.EndOfTurn);
            SaveCurlingGameResults();
            // HandleNextTurn();
        }


        /// <summary>
        /// Next Turn
        /// </summary>
        public void HandleStartNextTurn()
        {
            if (!turnManager.IsThereAnotherTurnAfterThisOne()){
                gameEndManager.EndCurrentCurlingGame();
                return;
            } else {
                turnManager.NextTurn();
                players.UpdateCurrentTeam();
                players.RepositionCharacters();
            }
        }

        public void OnStoneSelection(){
            List<CurlingStone> stoneOptions = stoneManager.GetStonesForCurrentTeam();

        }

        public void OnStoneSelectionConfirmed()
        {
            List<CurlingStone> stoneOptions = stoneManager.GetStonesForCurrentTeam();
            CurlingStone selectedStone = stoneOptions[0];
            for (int i = 0; i < stoneOptions.Count; i++)
            {
                CurlingStone stone = stoneOptions[i];
                if (stone.isInPlay == true){ 
                    continue; // Skip stones that are already in play
                }
                else {
                    selectedStone = stone; //stoneManager.UpdateCurrentStone(stone);
                    break;
                }
            }

            stoneManager.UpdateCurrentStone(
                selectedStone: selectedStone
            );
            stoneManager.PlaceCurrentStoneInLaunchPosition();
            // change camera
            cameraController.SwitchToStoneCamera();
            aiming.Reset();
            sweeping.ResetSweeperExhaustionBars();
            CurlingManagersV3.MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.CurlingAimControlsPhase);


        }

        public void OnAimingPhaseSelection(){
            // throwing.UpdatePowerFromPowerMeterSelection();
        }

        public void OnAimingPhaseComplete(){
            throwing.ResetPowerMeter();
            CurlingManagersV3.MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.CurlingPowerMeterPhase);
        }

        public void OnPowerPhaseSelection(){
            throwing.UpdatePowerFromPowerMeterSelection();
        }

        public void OnPowerPhaseComplete(){
            throwing.LaunchStone(
                stone: stoneManager.currentStone,
                launchDirection: aiming.directionPivot.forward,
                power: throwing.launchPower
            );
            // CurlingManagersV3.MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.CurlingStoneSweepingPhase);
        }


        


        /// <summary>
        /// End Game
        /// </summary>
        public void EndCurrentCurlingGame()
        {
            // scoring.CalculateScore();
            // // players.UpdateScore(scoring.GetScore());
            // CurlingMatchPhaseManager._instance.SetPhase(CurlingMatchPhase.FinalResults);
            // SaveCurlingGameResults();
        }

        public void SaveCurlingGameResults()
        {
            
        }


        public void ResetCurlingGame()
        {
            scoring.Reset();
            players.Reset();
            stoneManager.Reset();
            turnManager.Reset();
            // gameData.turnCurrent = 0;
            // turn.ResetTurn();
            // CurlingMatchPhaseManager._instance.SetPhase(CurlingMatchPhase.RoundSplash);
        }
        

    }
}



        // public void ExitCurlingGame()
        // {
        //     // Clear Existing Stones
        //     // Clear Team People
        //     // Reset All Necessary Things 
        //     // GameManager._instance.TeleportToScene(
        //     //     exitScene,
        //     //     exitSpawnId
        //     // );
        // }


        // public void InitSetupFromDemo(
        //     CurlingCourseData courseData,
        //     CurlingTeam teamHome,
        //     CurlingTeam teamAway,
        //     CurlingGameData data,
        //     string exitSceneName,
        //     string exitSpawnIdName
        // )
        // {
        //     // aiming.AdjustPowers();
        //     // sweeping.AdjustPowers();
        //     courseData_tmp = courseData;
        //     teamHome_tmp = teamHome;
        //     teamAway_tmp = teamAway;
        //     gameData = data;
        //     exitScene = exitSceneName;
        //     exitSpawnId = exitSpawnIdName;
        //     LoadInitialData();
        // }

              //     bool gameIsOver = false;
        //     int currentTurnCount = CurlingGameManagerV2._instance.gameData.currentTurn;
        //     int maxTurnCount = CurlingGameManagerV2._instance.gameData.turnsMax;
        //     if ((currentTurnCount + 1) == maxTurnCount)
        //     {
        //         CurlingGameManagerV2._instance.EndCurrentCurlingGame(); // End Curling Game
        //     }
        //     else
        //     {
        //         CurlingGameManagerV2._instance.NextTurn();
        //         CurlingMatchPhaseManager._instance.SetPhase(CurlingMatchPhase.StoneSelection);
        //     }
        // }

        // public void NextTurn()
        // {
        //     // players.NextPlayer();
        //     // gameData.turnCurrent++;
        //     // // turnCount++;
        //     // CurlingMatchPhaseManager._instance.SetPhase(CurlingMatchPhase.StoneSelection);
            
        // }
