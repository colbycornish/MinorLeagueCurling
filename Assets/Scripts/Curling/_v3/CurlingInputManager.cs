using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the high-level game manager for a curling game.
/// It orchestrates the flow of the game, including starting new ends,
/// managing player turns, and handling the end of the game.
/// </summary>

namespace CurlingManagersV3
{
    public class CurlingInputManager : MonoBehaviour
    {
        private bool isInputEnabled = true;
        public KeyCode rightCurlKey = KeyCode.E;
        public KeyCode leftCurlKey = KeyCode.Q;
        public KeyCode rightSweeperKey = KeyCode.RightShift; // Action button for right sweeper sweeping ** NEW SWEEPER CODE **
        public KeyCode leftSweeperKey = KeyCode.LeftShift; // Action button for left sweeper sweeping ** NEW SWEEPER CODE **
        public KeyCode actionKey = KeyCode.Space;
        public KeyCode confirmKey = KeyCode.X;
        public KeyCode beginKey = KeyCode.T;
        public KeyCode resetAllKey = KeyCode.R;


        private void Update()
        {
            if (!isInputEnabled) return;
            CurlingMatchPhase currentPhase = CurlingManagersV3.MatchPhaseManager._instance.currentPhase;

            if (Input.GetKeyDown(KeyCode.N))
            {
                CurlingManagersV3.MatchPhaseManager._instance.TestNextPhase();
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                CurlingManagersV3.CurlingManager._instance.gameEndManager.ExitCurlingGame();
            }

            switch (currentPhase)
            {
                case CurlingMatchPhase.None:
                    break;
                case CurlingMatchPhase.Loading:
                    break;
                case CurlingMatchPhase.RoundSplash:
                    HandleRoundSplashInput();
                    break;
                case CurlingMatchPhase.TeamSplash:
                    HandleTeamSplashInput();
                    break;
                case CurlingMatchPhase.StoneSelection:
                    HandleStoneSelectionInput();
                    break;
                case CurlingMatchPhase.StoneSelectionDetails:
                    HandleStoneSelectionDetailsInput();
                    break;
                case CurlingMatchPhase.StoneSelectionConfirm:
                    HandleStoneSelectionConfirmInput();
                    break;
                case CurlingMatchPhase.CurlingAimControlsPhase:
                    HandleCurlingAimControlsPhaseInput();
                    break;
                case CurlingMatchPhase.CurlingPowerMeterPhase:
                    HandlePowerMeterInput();
                    break;
                case CurlingMatchPhase.CurlingStoneSweepingPhase:
                    HandleCurlingTurnEndInput();
                    // HandleCurlingStoneSweepingPhaseInput();
                    break;
                case CurlingMatchPhase.CurlingNoSweepZone:
                    HandleCurlingNoSweepZonePhaseInput();
                    break;
                case CurlingMatchPhase.PostThrowResult:
                    HandlePostThrowResultInput();
                    break;
                case CurlingMatchPhase.ObstacleSelection:
                    HandleObstacleSelectionInput(); // Not implemented yet
                    break;
                case CurlingMatchPhase.ObstaclePlacement:
                    HandleObstaclePlacementInput();
                    break;
                case CurlingMatchPhase.FinalResults:
                    HandleFinalResultsInput();
                    break;

                case CurlingMatchPhase.ExitCurlingGame:
                    HandleExitCurlingGameInput();
                    break;
                default:
                    Debug.LogWarning($"Unhandled phase: {currentPhase}");
                    break;
            }
        }

        private void HandleRoundSplashInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CurlingManagersV3.MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.TeamSplash);
            }
        }

        private void HandleTeamSplashInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CurlingManagersV3.MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.StoneSelection);
            }
        }

        private void HandleStoneSelectionInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                
                CurlingManagersV3.MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.StoneSelectionConfirm);
            }
        }

        private void HandleStoneSelectionDetailsInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CurlingManagersV3.MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.StoneSelectionConfirm);
            }
        }

        private void HandleStoneSelectionConfirmInput()
        {
            if (Input.GetKeyDown(confirmKey))
            {
                // CurlingManagersV3.MatchPhaseManager._instance.OnStoneSelectionConfirmed();
                CurlingManagersV3.CurlingManager._instance.OnStoneSelectionConfirmed();
                CurlingManagersV3.MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.CurlingAimControlsPhase);
            }
        }

        // ..... Handled by Aim Controller
        private void HandleCurlingAimControlsPhaseInput()
        {
            /// Add Left Curl To Throw
            if (Input.GetKeyDown(leftCurlKey))
            {
                CurlingManagersV3.CurlingManager._instance.aiming.IncreaseLeftCurlAmount();
            }

            /// Add Right Curl To Throw
            if (Input.GetKeyDown(rightCurlKey))
            {
                CurlingManagersV3.CurlingManager._instance.aiming.IncreaseRightCurlAmount();
            }

            /// Move Aiming Direction Left/Right
            if (Input.GetAxis("Horizontal") != 0f)
            {
                float input = Input.GetAxis("Horizontal");
                CurlingManagersV3.CurlingManager._instance.aiming.ChangeDirection(
                    input: input
                );
            }

            if (Input.GetKeyDown(confirmKey))
            {
                CurlingManagersV3.CurlingManager._instance.OnAimingPhaseComplete();
            }
            // Handle inputs specific to the Aim Controls phase
        }

        // ❌ Handled by Stone Throw Controller
        private void HandlePowerMeterInput()
        {
            // Handled by Stone Throw Controller
            // Increase Power
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                CurlingManagersV3.CurlingManager._instance.throwing.IncreaseThrowPower();
            }

            // Decrease Power
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                CurlingManagersV3.CurlingManager._instance.throwing.DecreaseThrowPower();
            }

            // Confirm Power Selection and Launch Stone
            if (Input.GetKeyDown(confirmKey))
            {
                CurlingManagersV3.CurlingManager._instance.OnPowerPhaseSelection();
                CurlingManagersV3.CurlingManager._instance.OnPowerPhaseComplete();
                // CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.CurlingStoneSweepingPhase);
            }
            // Handle inputs specific to the Power Meter phase
        }

        // ❌ Handled by Stone Sweep Controller
        private void HandleCurlingStoneSweepingPhaseInput()
        {
            bool isSweepingLeft = Input.GetKey(leftSweeperKey);
            bool isSweepingRight = Input.GetKey(rightSweeperKey);
            if (Input.GetKey(rightSweeperKey))
            {
                
            }
        }

        private void HandleCurlingTurnEndInput()
        {
            // Debug.Log("Handling Curling Sweeper Phase");
            bool stoneIsMoving = CurlingManagersV3.CurlingManager._instance.stoneManager.IsCurrentStoneMoving();
            bool stoneIsMovingForward = CurlingManagersV3.CurlingManager._instance.stoneManager.IsCurrentStoneMovingForward();
            if (!stoneIsMoving || !stoneIsMovingForward)
            {
                CurlingManagersV3.MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.PostThrowResult);
            }
            // Handle inputs specific to the Curling Controls phase
        }

        private void HandleCurlingNoSweepZonePhaseInput()
        {
            bool stoneIsMoving = CurlingManagersV3.CurlingManager._instance.stoneManager.IsCurrentStoneMoving();
            bool stoneIsMovingForward = CurlingManagersV3.CurlingManager._instance.stoneManager.IsCurrentStoneMovingForward();
            if (!stoneIsMoving || !stoneIsMovingForward)
            {
                CurlingManagersV3.MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.PostThrowResult);
            }
            // Handle inputs specific to the Curling Controls phase
        }

        

        private void HandlePostThrowResultInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!CurlingManagersV3.CurlingManager._instance.gameData.settings.enableObstaclePlacementByPlayer &&
                    !CurlingManagersV3.CurlingManager._instance.gameData.settings.enableObstaclePlacementByEnvironment
                )
                {
                    HandleNextTurn();
                }
                else
                {
                    CurlingManagersV3.MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.ObstacleSelection);
                }
            }
            // Handle inputs specific to the Post Throw Result phase
        }

        private void HandleObstacleSelectionInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                CurlingManagersV3.MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.ObstaclePlacement);
            }
            // Handle inputs specific to the Obstacle Placement phase
        }
        private void HandleObstaclePlacementInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // CurlingGameManagerV2.Instance.NextTurn();
                HandleNextTurn();
                // CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.RoundSplash);
            }
            // Handle inputs specific to the Obstacle Placement phase
        }
        private void HandleFinalResultsInput()
        {

            CurlingManagersV3.CurlingManager._instance.gameEndManager.EndCurlingGame();
            /// RESTART?
            if (Input.GetMouseButtonDown(0))
            {
                CurlingManagersV3.MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.RoundSplash);
            }
        }

        /// <summary>
        /// Helpers
        /// </summary>

        private void HandleNextTurn()
        {
            // bool gameIsOver = false;
            // int currentTurnCount = CurlingManagersV3.CurlingManager._instance.turnCurrent;
            
            if (!CurlingManagersV3.CurlingManager._instance.turnManager.IsThereAnotherTurnAfterThisOne())
            {
                CurlingManagersV3.CurlingManager._instance.EndCurrentCurlingGame(); // End Curling Game
            }
            else
            {
                CurlingManagersV3.CurlingManager._instance.HandleEndOfTurn();
                CurlingManagersV3.CurlingManager._instance.HandleStartNextTurn();
                CurlingManagersV3.MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.StoneSelection);
            }
        }

        private void HandleExitCurlingGameInput()
        {

        }
        // public void SetCurrentThrower(CurlingStoneThrowControllerV2 thrower)
        // {
        //     // currentThrower = thrower;
        // }


    }
}