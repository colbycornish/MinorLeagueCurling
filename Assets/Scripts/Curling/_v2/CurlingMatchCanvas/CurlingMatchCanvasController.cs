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
            case CurlingMatchPhase.AimControls:
                ActivateCurlingInGameCanvas(phase);
                break;
            case CurlingMatchPhase.PowerMeter:
                ActivateCurlingInGameCanvas(phase);
                break;
            case CurlingMatchPhase.CurlingControls:
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

    public void ActivateSplashRoundCanvas(CurlingMatchPhase phase)
    {
        // DisableNonActiveCanvases(); // Disable Other Canvases
        // Enable Splash Round Canvas
        splashRoundCanvas.SetActive(true);
        // splashRoundCanvasController.ShowSplashRound();
    }
    public void ActivateSplashTeamCanvas(CurlingMatchPhase phase)
    {
        // DisableNonActiveCanvases(); // Disable Other Canvases
        // Enable Splash Team Canvas
        splashTeamCanvas.SetActive(true);
        // splashTeamCanvasController.ShowSplashTeam();

    }
    public void ActivateStoneSelectionCanvas(CurlingMatchPhase phase)
    {
        // DisableNonActiveCanvases(); // Disable Other Canvases
        // Enable Stone Selection Canvas
        stoneSelectionCanvasController.ShowStoneSelection();
        stoneSelectionCanvas.SetActive(true);

    }

    public void ActivateCurlingInGameCanvas(CurlingMatchPhase phase)
    {
        // DisableNonActiveCanvases(); // Disable Other Canvases

        // Enable Curling In Game Canvas
        curlingInGameCanvas.SetActive(true);
        curlingInGameCanvasController.enableTeamDisplays();
        
        if (phase == CurlingMatchPhase.AimControls)
        {
            curlingInGameCanvasController.disableExhaustionBars();
            curlingInGameCanvasController.disablePowerMeter();
            curlingInGameCanvasController.enableAimDisplay();
            curlingInGameCanvasController.setResultText(
                "Aiming",
                "Select the direction to throw the stone"
            );
        }
        else if (phase == CurlingMatchPhase.PowerMeter)
        {
            curlingInGameCanvasController.disableAimDisplay();
            curlingInGameCanvasController.enablePowerMeter();
            curlingInGameCanvasController.enableExhaustionBars();
            curlingInGameCanvasController.setResultText(
                "Power",
                "Select the power to throw the stone"
            );
        }
        else if (phase == CurlingMatchPhase.CurlingControls)
        {
            curlingInGameCanvasController.disableAimDisplay();
            curlingInGameCanvasController.disablePowerMeter();
            curlingInGameCanvasController.enableExhaustionBars();
            curlingInGameCanvasController.setResultText(
                "Sweeping",
                "Sweep to influence the stone's path"
            );
        }
        else
        {
            Debug.LogError("Invalid phaseId");
        }
    }


    public void ActivatePostThrowResultCanvas(CurlingMatchPhase phase)
    {
        // DisableNonActiveCanvases(); // Disable Other Canvases

        // Enable Post Throw Result Canvas
        curlingInGameCanvas.SetActive(true);
        curlingInGameCanvasController.disableAimDisplay();
        curlingInGameCanvasController.disableExhaustionBars();

        curlingInGameCanvasController.enableTeamDisplays();
        curlingInGameCanvasController.setResultText(
            "Oh No!",
            "Out of Bounds"
        );
        curlingInGameCanvasController.enableResultText();
    }

    
    public void ActivateObstacleSelectionCanvas(CurlingMatchPhase phase)
    {
        // DisableNonActiveCanvases(); // Disable Other Canvases
        // Enable Post Throw Result Canvas
        obstacleSelectionCanvas.SetActive(true);
    }

    public void ActivateObstaclePlacementCanvas(CurlingMatchPhase phase)
    {
        // DisableNonActiveCanvases(); // Disable Other Canvases
        // Enable Post Throw Result Canvas
        obstaclePlacementCanvas.SetActive(true);
    }


    public void ActivateFinalResultsCanvas(CurlingMatchPhase phase)
    {
        // DisableNonActiveCanvases(); // Disable Other Canvases
        // Enable Post Throw Result Canvas
        finalResultsCanvas.SetActive(true);
    }

    public void DisableNonActiveCanvases()
    {
        CurlingMatchPhase currentPhase = CurlingMatchPhaseManager.Instance.CurrentPhase;

        // Disable all canvases except the active one
        if (currentPhase != CurlingMatchPhase.RoundSplash) { splashRoundCanvas.SetActive(false); }
        if (currentPhase != CurlingMatchPhase.TeamSplash) { splashTeamCanvas.SetActive(false); }
        if (currentPhase != CurlingMatchPhase.StoneSelection &&
            currentPhase != CurlingMatchPhase.StoneSelectionDetails &&
            currentPhase != CurlingMatchPhase.StoneSelectionConfirm)
        {
            stoneSelectionCanvas.SetActive(false);
        }
        if (currentPhase != CurlingMatchPhase.AimControls &&
            currentPhase != CurlingMatchPhase.PowerMeter &&
            currentPhase != CurlingMatchPhase.CurlingControls)
        {
            curlingInGameCanvas.SetActive(false);
        }
        if (currentPhase != CurlingMatchPhase.PostThrowResult) { postThrowResultCanvas.SetActive(false); }
        if (currentPhase != CurlingMatchPhase.ObstacleSelection) { obstacleSelectionCanvas.SetActive(false); }
        if (currentPhase != CurlingMatchPhase.ObstaclePlacement) { obstaclePlacementCanvas.SetActive(false); }
        if (currentPhase != CurlingMatchPhase.FinalResults) { finalResultsCanvas.SetActive(false); }

    }
}
