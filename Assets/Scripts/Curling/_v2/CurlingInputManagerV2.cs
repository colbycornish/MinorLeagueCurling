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

    private void Update()
    {
        if (!isInputEnabled) return;
        CurlingMatchPhase currentPhase = CurlingMatchPhaseManager.Instance.CurrentPhase;

        switch (currentPhase)
        {
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
            case CurlingMatchPhase.AimControls:
                HandleAimControlsInput();
                break;
            case CurlingMatchPhase.PowerMeter:
                HandlePowerMeterInput();
                break;
            case CurlingMatchPhase.CurlingControls:
                HandleCurlingControlsInput();
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
        // Handle inputs specific to the Team Splash phase
    }
    private void HandleStoneSelectionInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.StoneSelectionConfirm);
        }
        // Handle inputs specific to the Stone Selection phase
    }
    private void HandleStoneSelectionDetailsInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.StoneSelectionConfirm);
        }
        // Handle inputs specific to the Stone Selection Details phase
    }
    private void HandleStoneSelectionConfirmInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CurlingGameManagerV2.Instance.OnStoneSelectionConfirmed();
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.AimControls);
        }
        // Handle inputs specific to the Stone Selection Confirm phase
    }
    private void HandleAimControlsInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.PowerMeter);
        }
        // Handle inputs specific to the Aim Controls phase
    }
    private void HandlePowerMeterInput()
    {
        // if (Input.GetMouseButtonDown(0))
        // {
        //     CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.CurlingControls);
        // }
        // Handle inputs specific to the Power Meter phase
    }
    private void HandleCurlingControlsInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.PostThrowResult);
        }
        // Handle inputs specific to the Curling Controls phase
    }
    private void HandlePostThrowResultInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.ObstacleSelection);
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
            if (CurlingGameManagerV2.Instance.playerManager.totalStonesPerEnd == CurlingGameManagerV2.Instance.playerManager.currentStoneIndex)
            {
                CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.FinalResults);
                return;
            }
            else
            {
                CurlingGameManagerV2.Instance.NextTurn();
                CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.RoundSplash);
            }
            
            
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
    // public void SetCurrentThrower(CurlingStoneThrowControllerV2 thrower)
    // {
    //     // currentThrower = thrower;
    // }
}
