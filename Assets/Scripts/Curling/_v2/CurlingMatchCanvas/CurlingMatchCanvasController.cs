using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/// <summary>
/// Phase 1: Show Round Number
/// Phase 2: Show which team's turn it is
/// -> Team Turn
///     Phase 3: Show stone selection canvas
///         -> Phase 3a: User Selects something and canvas updates detail information
///         -> Phase 3b: Confirm Selection or Return to 2a
///     Phase 4: Show in-game canvas
///         -> Stone selection canvas disappears
///         -> Phase 4a: Show Aiming controls
///                -> User confirms aiming direction
///         -> Phase 4b: Show Power Meter
///                -> Aim Controls disappear
///                -> User confirms power
///         -> Phase 4c: Show Curling controls
///                -> Power Meter Controls disappear
///                -> Stone is thrown
///                -> Canvas responds to sweeping
///         -> Phase 4d: Stone passed "no sweeping" line
///                -> Sweeper controls disappear
///                -> Camera angle changes
///         -> Phase 4e: Show Result Text 
///                -> ("Good Job!", "Bad Job!", "Out of Bounds!", etc.)
///                -> Update Scoring Progress
///     Phase 5: Show Obstacle Placement Selection
///         -> Phase 5a: Select Section or Obstacle
///         -> Phase 5b: Show Obstacle Placement
/// -> Switch Throwing Team
/// Repeat Until each team has thrown all their stones    
/// Phase 6: Show Final Result Canvas
///    -> Show Final Score
/// </summary>

public class CurlingMatchCanvasController : MonoBehaviour
{

    private SplashRoundCanvasController splashRoundCanvasController;
    private SplashTeamCanvasController splashTeamCanvasController;
    private StoneSelectionCanvasController stoneSelectionCanvasController;
    private CurlingInGameCanvasController curlingInGameCanvasController;
    private PostThrowResultCanvasController postThrowResultCanvasController;

    public GameObject loadingCanvas;
    public GameObject splashRoundCanvas; // ID = CANVAS_SPLASH_ROUND
    public GameObject splashTeamCanvas; // ID = CANVAS_SPLASH_TEAM
    public GameObject stoneSelectionCanvas; // ID = CANVAS_STONE_SELECTION
    public GameObject curlingInGameCanvas; // ID = CANVAS_CURLING_IN_GAME
    public GameObject postThrowResultCanvas;
    public GameObject obstacleSelectionCanvas;
    public GameObject obstaclePlacementCanvas;
    public GameObject finalResultsCanvas;

    public void Start()
    {
        splashRoundCanvasController = splashRoundCanvas.GetComponent<SplashRoundCanvasController>();
        splashTeamCanvasController = splashTeamCanvas.GetComponent<SplashTeamCanvasController>();
        stoneSelectionCanvasController = stoneSelectionCanvas.GetComponent<StoneSelectionCanvasController>();
        curlingInGameCanvasController = curlingInGameCanvas.GetComponent<CurlingInGameCanvasController>();
        postThrowResultCanvasController = postThrowResultCanvas.GetComponent<PostThrowResultCanvasController>();
        DisableNonActiveCanvases();
        ActivateLoadingCanvas(CurlingMatchPhase.Loading);
    }

    /// <summary>
    /// Subscribes to the phase change event from the CurlingMatchPhaseManager,
    /// which tells us which canvas should be displayed and active.
    /// </summary>
    private void OnEnable()
    {
        if (CurlingMatchPhaseManager.Instance == null) return;
        CurlingMatchPhaseManager.Instance.OnPhaseChanged += ActivateCanvasesByPhase;
    }

    private void OnDisable()
    {
        if (CurlingMatchPhaseManager.Instance == null) return;
        CurlingMatchPhaseManager.Instance.OnPhaseChanged -= ActivateCanvasesByPhase;
    }

    // CurlingMatchPhase phase
    public void ActivateCanvasesByPhase(CurlingMatchPhase phase)
    {

        DisableNonActiveCanvases(); 
        switch (phase)
        {
            case CurlingMatchPhase.Loading:
                ActivateLoadingCanvas(phase);
                break;
            case CurlingMatchPhase.RoundSplash:
                ActivateSplashRoundCanvas(phase);
                break;
            case CurlingMatchPhase.TeamSplash:
                ActivateSplashTeamCanvas(phase);
                break;
            case CurlingMatchPhase.StoneSelection:
                ActivateStoneSelectionCanvas(phase);
                break;
            case CurlingMatchPhase.StoneSelectionDetails:
                ActivateStoneSelectionCanvas(phase);
                break;
            case CurlingMatchPhase.StoneSelectionConfirm:
                ActivateStoneSelectionCanvas(phase);
                break;
            case CurlingMatchPhase.CurlingAimControlsPhase:
                ActivateCurlingInGameCanvas(phase);
                break;
            case CurlingMatchPhase.CurlingPowerMeterPhase:
                ActivateCurlingInGameCanvas(phase);
                break;
            case CurlingMatchPhase.CurlingStoneSweepingPhase:
                ActivateCurlingInGameCanvas(phase);
                break;
            case CurlingMatchPhase.CurlingNoSweepZone:
                ActivateCurlingInGameCanvas(phase);
                break;
            case CurlingMatchPhase.PostThrowResult:
                ActivatePostThrowResultCanvas(phase);
                break;
            case CurlingMatchPhase.ObstacleSelection:
                ActivateObstacleSelectionCanvas(phase);
                break;
            case CurlingMatchPhase.ObstaclePlacement:
                ActivateObstaclePlacementCanvas(phase);
                break;
            case CurlingMatchPhase.FinalResults:
                ActivateFinalResultsCanvas(phase);
                break;
            default:
                Debug.Log("Invalid canvas name");
                ActivateSplashRoundCanvas(phase);
                break;
        }

        // DisableNonActiveCanvases(); 
        return;
        
    }
    public void ActivateLoadingCanvas(CurlingMatchPhase phase)
    {
        loadingCanvas.SetActive(true);
    }

    public void ActivateSplashRoundCanvas(CurlingMatchPhase phase)
    {
        splashRoundCanvas.SetActive(true);
    }
    public void ActivateSplashTeamCanvas(CurlingMatchPhase phase)
    {
        splashTeamCanvas.SetActive(true);
    }
    public void ActivateStoneSelectionCanvas(CurlingMatchPhase phase)
    {
        stoneSelectionCanvasController.ShowStoneSelection();
        stoneSelectionCanvas.SetActive(true);
    }

    public void ActivateCurlingInGameCanvas(CurlingMatchPhase phase)
    {
        // Enable Curling In Game Canvas
        curlingInGameCanvas.SetActive(true);
        curlingInGameCanvasController.EnableTeamDisplays();
        
        if (phase == CurlingMatchPhase.CurlingAimControlsPhase)
        {
            curlingInGameCanvasController.DisableExhaustionBars();
            curlingInGameCanvasController.DisablePowerMeter();
            curlingInGameCanvasController.DisableResultText();
            curlingInGameCanvasController.EnableAimDisplay();
            // curlingInGameCanvasController.SetResultText(
            //     "Aiming",
            //     "Select the direction to throw the stone"
            // );
        }
        else if (phase == CurlingMatchPhase.CurlingPowerMeterPhase)
        {
            curlingInGameCanvasController.DisableAimDisplay();
            curlingInGameCanvasController.EnablePowerMeter();
            curlingInGameCanvasController.DisableResultText();
            curlingInGameCanvasController.EnableExhaustionBars();
            // curlingInGameCanvasController.SetResultText(
            //     "Power",
            //     "Select the power to throw the stone"
            // );
        }
        else if (phase == CurlingMatchPhase.CurlingStoneSweepingPhase)
        {
            curlingInGameCanvasController.DisableAimDisplay();
            curlingInGameCanvasController.DisableResultText();
            curlingInGameCanvasController.DisablePowerMeter();
            curlingInGameCanvasController.EnableExhaustionBars();
            // curlingInGameCanvasController.SetResultText(
            //     "Sweeping",
            //     "Sweep to influence the stone's path"
            // );
        }
        else if (phase == CurlingMatchPhase.CurlingNoSweepZone)
        {
            curlingInGameCanvasController.DisableAimDisplay();
            curlingInGameCanvasController.DisableResultText();
            curlingInGameCanvasController.DisablePowerMeter();
            curlingInGameCanvasController.EnableExhaustionBars();
            // curlingInGameCanvasController.SetResultText(
            //     "Sweeping",
            //     "Sweep to influence the stone's path"
            // );
        }

        
        else
        {
            Debug.LogError("Invalid phaseId");
        }
    }


    public void ActivatePostThrowResultCanvas(CurlingMatchPhase phase)
    {
        // Enable Post Throw Result Canvas
        curlingInGameCanvas.SetActive(true);
        curlingInGameCanvasController.DisableAimDisplay();
        curlingInGameCanvasController.DisableExhaustionBars();

        curlingInGameCanvasController.EnableTeamDisplays();
        
        curlingInGameCanvasController.SetResultText(
            "Oh No!",
            "Out of Bounds"
        );
        curlingInGameCanvasController.EnableResultText();
    }

    
    public void ActivateObstacleSelectionCanvas(CurlingMatchPhase phase)
    {
        obstacleSelectionCanvas.SetActive(true);
    }

    public void ActivateObstaclePlacementCanvas(CurlingMatchPhase phase)
    {
        obstaclePlacementCanvas.SetActive(true);
    }


    public void ActivateFinalResultsCanvas(CurlingMatchPhase phase)
    {
        finalResultsCanvas.SetActive(true);
    }

    public void DisableNonActiveCanvases()
    {
        CurlingMatchPhase currentPhase = CurlingMatchPhaseManager.Instance.CurrentPhase;

        // Disable all canvases except the active one
        if (currentPhase != CurlingMatchPhase.Loading) { loadingCanvas.SetActive(false); }
        if (currentPhase != CurlingMatchPhase.RoundSplash) { splashRoundCanvas.SetActive(false); }
        if (currentPhase != CurlingMatchPhase.TeamSplash) { splashTeamCanvas.SetActive(false); }
        if (currentPhase != CurlingMatchPhase.StoneSelection &&
            currentPhase != CurlingMatchPhase.StoneSelectionDetails &&
            currentPhase != CurlingMatchPhase.StoneSelectionConfirm)
        {
            stoneSelectionCanvas.SetActive(false);
        }
        if (currentPhase != CurlingMatchPhase.CurlingAimControlsPhase &&
            currentPhase != CurlingMatchPhase.CurlingPowerMeterPhase &&
            currentPhase != CurlingMatchPhase.CurlingStoneSweepingPhase && 
            currentPhase != CurlingMatchPhase.CurlingNoSweepZone)
        {
            curlingInGameCanvas.SetActive(false);
        }
        if (currentPhase != CurlingMatchPhase.PostThrowResult) { postThrowResultCanvas.SetActive(false); }
        if (currentPhase != CurlingMatchPhase.ObstacleSelection) { obstacleSelectionCanvas.SetActive(false); }
        if (currentPhase != CurlingMatchPhase.ObstaclePlacement) { obstaclePlacementCanvas.SetActive(false); }
        if (currentPhase != CurlingMatchPhase.FinalResults) { finalResultsCanvas.SetActive(false); }

    }
}
