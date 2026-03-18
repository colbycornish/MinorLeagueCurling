using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using Curling.Rules;
using CurlingManagersV3.Parameters;

/// <summary>
/// This is the high-level game manager for a curling game.
/// It orchestrates the flow of the game, including starting new ends,
/// managing player turns, and handling the end of the game.
/// </summary>


namespace CurlingManagersV3
{
    [RequireComponent(typeof(CurlingManagersV3.Aiming))]
    [RequireComponent(typeof(CurlingManagersV3.Players))]
    [RequireComponent(typeof(CurlingManagersV3.Scoring))]
    [RequireComponent(typeof(CurlingManagersV3.Stones))]
    [RequireComponent(typeof(CurlingManagersV3.Sweeping))]
    [RequireComponent(typeof(CurlingManagersV3.Throwing))]
    //
    [RequireComponent(typeof(CurlingManagersV3.CurlingCameraController))]
    [RequireComponent(typeof(CurlingManagersV3.CurlingInputManager))]
    [RequireComponent(typeof(CurlingManagersV3.GameTurn))]
    [RequireComponent(typeof(CurlingManagersV3.GameEnd))]
    [RequireComponent(typeof(CurlingManagersV3.GameSetup))]
    [RequireComponent(typeof(CurlingManagersV3.Demo))]

    [DefaultExecutionOrder(-10000)]// Initialize the StateMachine before anything uses it.
    public class CurlingManager : MonoBehaviour
    {

        [SerializeField]
        private MatchPhaseStateMachine _StateMachine = new MatchPhaseStateMachine();
        public MatchPhaseStateMachine StateMachine => _StateMachine;

        // [HideInInspector] 
        public static CurlingManager _instance { get; private set; }

        [SerializeField]
        private CurlingParameters _Parameters;
        public CurlingParameters Parameters => _Parameters;

        [SerializeField]
        private CurlingManagersV3.Aiming _Aiming;
        public CurlingManagersV3.Aiming Aiming => _Aiming;

        [SerializeField]
        private CurlingManagersV3.Players _Players;
        public CurlingManagersV3.Players Players => _Players;

        [SerializeField]
        private CurlingManagersV3.Scoring _Scoring;
        public CurlingManagersV3.Scoring Scoring => _Scoring;
        
        public CurlingManagersV3.Stones stoneManager;

        [SerializeField]
        private CurlingManagersV3.Sweeping _Sweeping;
        public CurlingManagersV3.Sweeping Sweeping => _Sweeping;

        [SerializeField]
        private CurlingManagersV3.Throwing _Throwing;
        public CurlingManagersV3.Throwing Throwing => _Throwing;
        

        // Helper Managers
        public CurlingManagersV3.Demo demo;

        // [SerializeField]
        // private CurlingManagersV3.CurlingCameraController _CameraController;
        // public CurlingManagersV3.CurlingCameraController CameraController => _CameraController;
        public CurlingManagersV3.CurlingCameraController cameraController;
        public CurlingManagersV3.CurlingInputManager curlingInputManager;

        [SerializeField]
        private CurlingManagersV3.GameSetup _Setup;
        public CurlingManagersV3.GameSetup Setup => _Setup;

        [SerializeField]
        private CurlingManagersV3.GameTurn _TurnManager;
        public CurlingManagersV3.GameTurn TurnManager => _TurnManager;
    
        public CurlingManagersV3.GameEnd gameEndManager;

        [Header("[Data] Course")]
        public Camera stoneCamera;
        
        [Header("[Data] Settings")]
        public CurlingGameData gameData;
        // public int turnsMax = 10;

#if UNITY_EDITOR
        protected void OnValidate()
        {
            _Aiming = GetComponent<CurlingManagersV3.Aiming>();
            _Players = GetComponent<CurlingManagersV3.Players>();
            _Scoring = GetComponent<CurlingManagersV3.Scoring>();
            stoneManager = GetComponent<CurlingManagersV3.Stones>();
            _Sweeping = GetComponent<CurlingManagersV3.Sweeping>();
            _Throwing = GetComponent<CurlingManagersV3.Throwing>();
            //
            cameraController = GetComponent<CurlingManagersV3.CurlingCameraController>();
            curlingInputManager = GetComponent<CurlingManagersV3.CurlingInputManager>();
            _TurnManager = GetComponent<CurlingManagersV3.GameTurn>();
            gameEndManager = GetComponent<CurlingManagersV3.GameEnd>();

            _Setup = GetComponent<CurlingManagersV3.GameSetup>();
            demo = GetComponent<CurlingManagersV3.Demo>();
        }
#endif

        // private void Awake()
        // {
        //     if (_instance != null)
        //     {
        //         Destroy(gameObject);
        //         Init();
        //         return;
        //     }
        //     _instance = this;
        //     Init();
        // }

        protected virtual void Awake()
        {
            _StateMachine.InitializeAfterDeserialize();

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
            if (_Aiming == null) _Aiming = GetComponent<CurlingManagersV3.Aiming>();
            // if (courseController == null) courseController = GetComponent<CurlingManagersV3.CourseController>();
            if (_Players == null) _Players = GetComponent<CurlingManagersV3.Players>();
            if (_Scoring == null) _Scoring = GetComponent<CurlingManagersV3.Scoring>();
            if (stoneManager == null) stoneManager = GetComponent<CurlingManagersV3.Stones>();
            if (_Sweeping == null) _Sweeping = GetComponent<CurlingManagersV3.Sweeping>();
            if (_Throwing == null) _Throwing = GetComponent<CurlingManagersV3.Throwing>();
            //
            if (cameraController == null) cameraController = GetComponent<CurlingManagersV3.CurlingCameraController>();
            if (curlingInputManager == null) curlingInputManager = GetComponent<CurlingManagersV3.CurlingInputManager>();
            if (_TurnManager == null) _TurnManager = GetComponent<CurlingManagersV3.GameTurn>();
            if (gameEndManager == null) gameEndManager = GetComponent<CurlingManagersV3.GameEnd>();

            if (_Setup == null) _Setup = GetComponent<CurlingManagersV3.GameSetup>();
            if (demo == null) demo = GetComponent<CurlingManagersV3.Demo>();
        }

    
        /// <summary>
        /// Turn Management
        /// </summary>
        public void HandleNextTurn()
        {
            if (!TurnManager.IsThereAnotherTurnAfterThisOne())
            {
                EndCurrentCurlingGame(); // End Curling Game
            }
            else
            {
                HandleEndOfTurn();
                HandleStartNextTurn();
                MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.StoneSelection);
            }
        }

        public void HandleEndOfTurn()
        {
            // Called when a stone finishes moving, and is now resting in the target zone.
            Scoring.CalculateScore();
            SaveCurlingGameResults();
            // HandleNextTurn();
        }

        public void HandleStartNextTurn()
        {
            if (!TurnManager.IsThereAnotherTurnAfterThisOne()){
                gameEndManager.EndCurrentCurlingGame();
                return;
            } else {
                TurnManager.NextTurn();
                Players.UpdateCurrentTeam();
                Players.RepositionCharacters();
            }
        }

        /// <summary>
        /// Stone Selection
        /// </summary>
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
                if (stone.Parameters.Status.IsInPlay == true){ 
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
            // aiming.Reset();
            Aiming.Reset();
            Sweeping.ResetSweeperExhaustionBars();

            // Update Player Tracking
            CurlingTeam activeTeam = 
                Parameters.Turn.CurrentTurn == CurlingGameTurnType.Home
                    ? Parameters.Teams.teamHome
                    : Parameters.Teams.teamAway;

            CurlingTeam inactiveTeam = 
                Parameters.Turn.CurrentTurn == CurlingGameTurnType.Home
                    ? Parameters.Teams.teamAway
                    : Parameters.Teams.teamHome;
 
            _Players.UpdateSweeperStoneTracking(
                team: activeTeam,
                stone: Parameters.Stones.currentStone
            );

            _Players.UpdateSweeperStoneTracking(
                team: inactiveTeam,
                stone: null
            );

            // _Players.PrepareSweepersToTrackActiveStone();
            MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.CurlingAimControlsPhase);
            

        }

        /// <summary>
        /// Aiming and Power
        /// </summary>
        public void OnAimingPhaseSelection(){
            // throwing.UpdatePowerFromPowerMeterSelection();
        }

        public void OnAimingPhaseComplete(){
            Throwing.ResetPowerMeter();
            MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.CurlingPowerMeterPhase);
        }

        public void OnPowerPhaseSelection(){
            Throwing.UpdatePowerFromPowerMeterSelection();
        }

        public void OnPowerPhaseComplete(){
            Throwing.LaunchStone(
                stone: CurlingManager._instance.Parameters.Stones.currentStone, 
                launchDirection: Parameters.Course.directionPivot.forward,
                power: Parameters.Throwing.LaunchPower 
            );
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
            Scoring.Reset();
            Players.Reset();
            stoneManager.Reset();
            TurnManager.Reset();
        }
        

    }
}
