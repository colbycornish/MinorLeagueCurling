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
            CurlingMatchPhase currentPhase = MatchPhaseManager._instance.currentPhase;

            if (Input.GetKeyDown(KeyCode.N))
            {
                MatchPhaseManager._instance.TestNextPhase();
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                CurlingManager._instance.gameEndManager.ExitCurlingGame();
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
                MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.TeamSplash);
            }
        }

        private void HandleTeamSplashInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.StoneSelection);
            }
        }

        private void HandleStoneSelectionInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.StoneSelectionConfirm);
            }
        }

        private void HandleStoneSelectionDetailsInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.StoneSelectionConfirm);
            }
        }

        private void HandleStoneSelectionConfirmInput()
        {
            if (Input.GetKeyDown(confirmKey))
            {
                CurlingManager._instance.OnStoneSelectionConfirmed();
                MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.CurlingAimControlsPhase);
            }
        }

        // ..... Handled by Aim Controller
        private void HandleCurlingAimControlsPhaseInput()
        {
            /// Add Left Curl To Throw
            if (Input.GetKeyDown(leftCurlKey))
            {
                CurlingManager._instance.Aiming.IncreaseLeftCurlAmount();
            }

            /// Add Right Curl To Throw
            if (Input.GetKeyDown(rightCurlKey))
            {
                CurlingManager._instance.Aiming.IncreaseRightCurlAmount();
            }

            /// Move Aiming Direction Left/Right
            if (Input.GetAxis("Horizontal") != 0f)
            {
                float input = Input.GetAxis("Horizontal");
                
                CurlingManager._instance.Aiming.ChangeDirection(
                    input: input
                );
            }

            if (Input.GetKeyDown(confirmKey))
            {
                CurlingManager._instance.OnAimingPhaseComplete();
            }
        }

        // ❌ Handled by Stone Throw Controller
        private void HandlePowerMeterInput()
        {
            // Handled by Stone Throw Controller
            // Increase Power
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                CurlingManager._instance.Throwing.IncreaseThrowPower();
            }

            // Decrease Power
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                CurlingManager._instance.Throwing.DecreaseThrowPower();
            }

            // Confirm Power Selection and Launch Stone
            if (Input.GetKeyDown(confirmKey))
            {
                CurlingManager._instance.OnPowerPhaseSelection();
                CurlingManager._instance.OnPowerPhaseComplete();
            }
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
            bool stoneIsMoving = CurlingManager._instance.stoneManager.IsCurrentStoneMoving();
            bool stoneIsMovingForward = CurlingManager._instance.stoneManager.IsCurrentStoneMovingForward();
            if (!stoneIsMoving || !stoneIsMovingForward)
            {
                MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.PostThrowResult);
            }
        }

        private void HandleCurlingNoSweepZonePhaseInput()
        {
            bool stoneIsMoving = CurlingManager._instance.stoneManager.IsCurrentStoneMoving();
            bool stoneIsMovingForward = CurlingManager._instance.stoneManager.IsCurrentStoneMovingForward();
            if (!stoneIsMoving || !stoneIsMovingForward)
            {
                MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.PostThrowResult);
            }
        }

        

        private void HandlePostThrowResultInput()
        {
            // Handle inputs specific to the Post Throw Result phase
            if (Input.GetMouseButtonDown(0))
            {
                if (!CurlingManager._instance.gameData.settings.enableObstaclePlacementByPlayer &&
                    !CurlingManager._instance.gameData.settings.enableObstaclePlacementByEnvironment
                )
                {
                    HandleNextTurn();
                }
                else
                {
                    MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.ObstacleSelection);
                }
            }
        }

        private void HandleObstacleSelectionInput()
        {
            // Handle inputs specific to the Obstacle Placement phase
            if (Input.GetMouseButtonDown(0))
            {
                MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.ObstaclePlacement);
            }
        }
        private void HandleObstaclePlacementInput()
        {
            // Handle inputs specific to the Obstacle Placement phase
            if (Input.GetMouseButtonDown(0))
            {
                HandleNextTurn();
            }
        }

        private void HandleFinalResultsInput()
        {
            CurlingManager._instance.gameEndManager.EndCurlingGame();
            /// RESTART?
            if (Input.GetMouseButtonDown(0))
            {
                MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.RoundSplash);
            }
        }

        /// <summary>
        /// Helpers
        /// </summary>

        private void HandleNextTurn()
        {
            if (!CurlingManager._instance.TurnManager.IsThereAnotherTurnAfterThisOne())
            {
                CurlingManager._instance.EndCurrentCurlingGame(); // End Curling Game
            }
            else
            {
                CurlingManager._instance.HandleEndOfTurn();
                CurlingManager._instance.HandleStartNextTurn();
                MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.StoneSelection);
            }
        }

        private void HandleExitCurlingGameInput()
        {

        }
    }
}