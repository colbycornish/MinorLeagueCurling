using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages inputs for the Curling game.
/// It's unclear if inputs should be handled here, or at a lower level.
/// 
/// TODO: Research Input handling in Unity and decide if this is the right place.
/// </summary>


public class CurlingInputManagerV2 : MonoBehaviour
{
    // public CurlingGameManagerV2 gameManager;
    // private CurlingStoneThrowControllerV2 currentThrower;
    private bool isInputEnabled = true;
    // public KeyCode resetKey = KeyCode.R;
    public KeyCode rightCurlKey = KeyCode.E;
    public KeyCode leftCurlKey = KeyCode.Q;
    public KeyCode rightSweeperKey = KeyCode.L; // Action button for right sweeper sweeping ** NEW SWEEPER CODE **
    public KeyCode leftSweeperKey = KeyCode.K; // Action button for left sweeper sweeping ** NEW SWEEPER CODE **
    public KeyCode actionKey = KeyCode.Space;
    public KeyCode confirmKey = KeyCode.X;
    public KeyCode beginKey = KeyCode.T;
    public KeyCode resetAllKey = KeyCode.R;


    private void Update()
    {
        if (!isInputEnabled) return;
        CurlingMatchPhase currentPhase = CurlingMatchPhaseManager.Instance.CurrentPhase;

        if (Input.GetKeyDown(KeyCode.G))
        {
            CurlingGameManagerV2.Instance.ExitCurlingGame();
        }

        switch (currentPhase)
        {
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
                // HandlePowerMeterInput();
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
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.TeamSplash);
        }
    }

    private void HandleTeamSplashInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.StoneSelection);
        }
    }

    private void HandleStoneSelectionInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.StoneSelectionConfirm);
        }
    }

    private void HandleStoneSelectionDetailsInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.StoneSelectionConfirm);
        }
    }

    private void HandleStoneSelectionConfirmInput()
    {
        if (Input.GetKeyDown(confirmKey))
        {
            CurlingGameManagerV2.Instance.OnStoneSelectionConfirmed();
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.CurlingAimControlsPhase);
        }
    }

    // ..... Handled by Aim Controller
    private void HandleCurlingAimControlsPhaseInput()
    {
        if (Input.GetKeyDown(confirmKey))
        {
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.CurlingPowerMeterPhase);
        }
        // Handle inputs specific to the Aim Controls phase
    }

    // ❌ Handled by Stone Throw Controller
    // private void HandlePowerMeterInput()
    // {
    //     // Handled by Stone Throw Controller
    //     // if (Input.GetMouseButtonDown(0))
    //     // {
    //     //     CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.CurlingStoneSweepingPhase);
    //     // }
    //     // Handle inputs specific to the Power Meter phase
    // }

    // ❌ Handled by Stone Sweep Controller
    // private void HandleCurlingStoneSweepingPhaseInput()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.PostThrowResult);
    //     }
    // }
    private void HandleCurlingTurnEndInput()
    {
        bool stoneIsMoving = CurlingGameManagerV2.Instance.stoneManager.IsCurrentStoneMoving();
        bool stoneIsMovingForward = CurlingGameManagerV2.Instance.stoneManager.IsCurrentStoneMovingForward();
        if (!stoneIsMoving || !stoneIsMovingForward)
        {
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.PostThrowResult);
        }
        // Handle inputs specific to the Curling Controls phase
    }

    private void HandleCurlingNoSweepZonePhaseInput()
    {
        bool stoneIsMoving = CurlingGameManagerV2.Instance.stoneManager.IsCurrentStoneMoving();
        bool stoneIsMovingForward = CurlingGameManagerV2.Instance.stoneManager.IsCurrentStoneMovingForward();
        if (!stoneIsMoving || !stoneIsMovingForward)
        {
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.PostThrowResult);
        }
        // Handle inputs specific to the Curling Controls phase
    }

    

    private void HandlePostThrowResultInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!CurlingGameManagerV2.Instance.gameData.settings.enableObstaclePlacementByPlayer &&
                !CurlingGameManagerV2.Instance.gameData.settings.enableObstaclePlacementByEnvironment
            )
            {
                HandleNextTurn();
            }
            else
            {
                CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.ObstacleSelection);
            }
        }
        // Handle inputs specific to the Post Throw Result phase
    }

    private void HandleObstacleSelectionInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.ObstaclePlacement);
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

        CurlingGameManagerV2.Instance.EndCurrentCurlingGame();
        /// RESTART?
        if (Input.GetMouseButtonDown(0))
        {
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.RoundSplash);
        }
    }

    /// <summary>
    /// Helpers
    /// </summary>

    private void HandleNextTurn()
    {
        bool gameIsOver = false;
        int currentTurnCount = CurlingGameManagerV2.Instance.gameData.turnCurrent;
        int maxTurnCount = CurlingGameManagerV2.Instance.turnsMax;
        if ((currentTurnCount + 1) == maxTurnCount)
        {
            CurlingGameManagerV2.Instance.EndCurrentCurlingGame(); // End Curling Game
        }
        else
        {
            CurlingGameManagerV2.Instance.NextTurn();
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.StoneSelection);
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
