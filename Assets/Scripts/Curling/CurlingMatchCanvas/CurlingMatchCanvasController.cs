using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurlingMatchCanvasController : MonoBehaviour
{

    /// <summary>
    /// Public Variables of Global Settings
    /// </summary>
    private SplashRoundCanvasController splashRoundCanvasController;
    private SplashTeamCanvasController splashTeamCanvasController;
    private StoneSelectionCanvasController stoneSelectionCanvasController;
    private CurlingInGameCanvasController curlingInGameCanvasController;
    private PostThrowResultCanvasController postThrowResultCanvasController;

    public GameObject splashRoundCanvas; // ID = CANVAS_SPLASH_ROUND
    public GameObject splashTeamCanvas; // ID = CANVAS_SPLASH_TEAM
    public GameObject stoneSelectionCanvas; // ID = CANVAS_STONE_SELECTION
    public GameObject curlingInGameCanvas; // ID = CANVAS_CURLING_IN_GAME
    public GameObject postThrowResultCanvas; // ID = CANVAS_POST_THROW_RESULT

    private List<string> canvasOrderByIds = new List<string>
    {
        "CANVAS_SPLASH_ROUND",
        "CANVAS_SPLASH_TEAM",
        "CANVAS_STONE_SELECTION",
        "CANVAS_CURLING_IN_GAME_AIM",
        "CANVAS_CURLING_IN_GAME_POWER",
        "CANVAS_CURLING_IN_GAME_SWEEP",
        "CANVAS_POST_THROW_RESULT"
    };

    private List<KeyValuePair<string, GameObject>> canvasListById = new List<KeyValuePair<string, GameObject>>();
    public string activeCanvas = "CANVAS_SPLASH_ROUND";

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

    

    public void Start(){
        splashRoundCanvasController = splashRoundCanvas.GetComponent<SplashRoundCanvasController>();
        splashTeamCanvasController = splashTeamCanvas.GetComponent<SplashTeamCanvasController>();
        stoneSelectionCanvasController = stoneSelectionCanvas.GetComponent<StoneSelectionCanvasController>();
        curlingInGameCanvasController = curlingInGameCanvas.GetComponent<CurlingInGameCanvasController>();
        postThrowResultCanvasController = postThrowResultCanvas.GetComponent<PostThrowResultCanvasController>();

        splashRoundCanvasController.SetCanvasId("CANVAS_SPLASH_ROUND");
        splashTeamCanvasController.SetCanvasId("CANVAS_SPLASH_TEAM");
        stoneSelectionCanvasController.SetCanvasId("CANVAS_STONE_SELECTION");
        curlingInGameCanvasController.SetCanvasId("CANVAS_CURLING_IN_GAME");
        postThrowResultCanvasController.SetCanvasId("CANVAS_POST_THROW_RESULT");
        
        splashRoundCanvas.SetActive(true);
        splashTeamCanvas.SetActive(false);
        stoneSelectionCanvas.SetActive(false);
        curlingInGameCanvas.SetActive(false);
    }

    // void Update()
    // {
    //     // Check for user input to switch between canvases
    //     if (Input.GetKeyDown(KeyCode.N))
    //     {
    //         GoToNextCanvas();
    //     }

    //     if (Input.GetKeyDown(KeyCode.B))
    //     {
    //         GoToPreviousCanvas();
    //     }
    //     if (activeCanvas == "CANVAS_CURLING_IN_GAME_POWER" && Input.GetKeyDown(KeyCode.Space))
    //     {
    //         GoToNextCanvas();
    //     }
        

    // }

    public void GoToNextCanvas(){
        int currentIndex = canvasOrderByIds.IndexOf(activeCanvas);
        int nextIndex = (currentIndex + 1) % canvasOrderByIds.Count;
        activeCanvas = canvasOrderByIds[nextIndex];
        ActivateCanvas();
    }

    public void GoToPreviousCanvas(){
        int currentIndex = canvasOrderByIds.IndexOf(activeCanvas);
        int prevIndex = (currentIndex - 1 + canvasOrderByIds.Count) % canvasOrderByIds.Count;
        activeCanvas = canvasOrderByIds[prevIndex];
        ActivateCanvas();
    }

    public void SetActiveCanvasById(string canvasName)
    {
        // Set the active canvas based on the provided name
        activeCanvas = canvasName;
        ActivateCanvas();
    }

    public void ActivateCanvas(){
        // splashRoundCanvas.SetActive(false);
        // splashTeamCanvas.SetActive(false);
        // stoneSelectionCanvas.SetActive(false);
        // curlingInGameCanvas.SetActive(false);
        // Set the active canvas based on the current active canvas
        switch (activeCanvas)
        {
            case "CANVAS_SPLASH_ROUND":
                ActivateSplashRoundCanvas();
                break;
            case "CANVAS_SPLASH_TEAM":
                ActivateSplashTeamCanvas();
                break;
            case "CANVAS_STONE_SELECTION":
                ActivateStoneSelectionCanvas();
                break;
            case "CANVAS_CURLING_IN_GAME_AIM":
                ActivateCurlingInGameCanvas("AIM");
                break;
            case "CANVAS_CURLING_IN_GAME_POWER":
                ActivateCurlingInGameCanvas("POWER");
                break;
            case "CANVAS_CURLING_IN_GAME_SWEEP":
                ActivateCurlingInGameCanvas("SWEEP");
                break;
            case "CANVAS_POST_THROW_RESULT":
                ActivatePostThrowResultCanvas();
                break;
            default:
                Debug.Log("Invalid canvas name");
                splashRoundCanvas.SetActive(true);
                break;
        }
        return;
    }

    public void ActivateSplashRoundCanvas()
    {
        activeCanvas = "CANVAS_SPLASH_ROUND";
        // Disable Other Canvases
        // splashTeamCanvas.SetActive(false);
        // stoneSelectionCanvas.SetActive(false);
        // curlingInGameCanvas.SetActive(false);
        // postThrowResultCanvas.SetActive(false);
        DisableNonActiveCanvases();
        
        // Enable Splash Round Canvas
        splashRoundCanvas.SetActive(true);
        
        splashRoundCanvasController.ShowSplashRound();
    }
    public void ActivateSplashTeamCanvas()
    {
        activeCanvas = "CANVAS_SPLASH_TEAM";
        // Disable Other Canvases
        // splashRoundCanvas.SetActive(false);
        // stoneSelectionCanvas.SetActive(false);
        // curlingInGameCanvas.SetActive(false);
        // postThrowResultCanvas.SetActive(false);
        DisableNonActiveCanvases();
        
        // Enable Splash Team Canvas
        splashTeamCanvas.SetActive(true);
        splashTeamCanvasController.ShowSplashTeam();        
        
    }
    public void ActivateStoneSelectionCanvas()
    {
        activeCanvas = "CANVAS_STONE_SELECTION";
        DisableNonActiveCanvases();
        // Disable Other Canvases
        // splashRoundCanvas.SetActive(false);
        // splashTeamCanvas.SetActive(false);
        // curlingInGameCanvas.SetActive(false);
        // postThrowResultCanvas.SetActive(false);

        // Enable Stone Selection Canvas
        stoneSelectionCanvasController.ShowStoneSelection();
        stoneSelectionCanvas.SetActive(true);
        
    }

    public void ActivateCurlingInGameCanvas(string phaseId)
    {
        // Disable Other Canvases
        // splashRoundCanvas.SetActive(false);
        // splashTeamCanvas.SetActive(false);
        // stoneSelectionCanvas.SetActive(false);
        // postThrowResultCanvas.SetActive(false);
        activeCanvas = "CANVAS_CURLING_IN_GAME_" + phaseId;
        DisableNonActiveCanvases();

        // Enable Curling In Game Canvas
        curlingInGameCanvas.SetActive(true);
        curlingInGameCanvasController.enableTeamDisplays();
        

        if (phaseId == "AIM"){
            curlingInGameCanvasController.disableExhaustionBars();
            curlingInGameCanvasController.disablePowerMeter();
            curlingInGameCanvasController.enableAimDisplay();
            curlingInGameCanvasController.setResultText(
                "Aiming",
                "Select the direction to throw the stone"
            );
        } else if (phaseId == "POWER") {
            curlingInGameCanvasController.disableAimDisplay();
            curlingInGameCanvasController.enablePowerMeter();
            curlingInGameCanvasController.enableExhaustionBars();
            curlingInGameCanvasController.setResultText(
                "Power",
                "Select the power to throw the stone"
            );
        } else if (phaseId == "SWEEP") {
            curlingInGameCanvasController.disableAimDisplay();
            curlingInGameCanvasController.disablePowerMeter();
            curlingInGameCanvasController.enableExhaustionBars();
            curlingInGameCanvasController.setResultText(
                "Sweeping",
                "Sweep to influence the stone's path"
            );
        } else {
            Debug.LogError("Invalid phaseId: " + phaseId);
        } 
    }


    public void ActivatePostThrowResultCanvas()
    {
        activeCanvas = "CANVAS_POST_THROW_RESULT";
        // Disable Other Canvases
        DisableNonActiveCanvases();
        // splashRoundCanvas.SetActive(false);
        // splashTeamCanvas.SetActive(false);
        // stoneSelectionCanvas.SetActive(false);
        // curlingInGameCanvas.SetActive(false);

        // Enable Post Throw Result Canvas
        // postThrowResultCanvas.SetActive(true);
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

    public void DisableNonActiveCanvases()
    {
        // Disable all canvases except the active one
        if (activeCanvas != "CANVAS_SPLASH_ROUND")
        {
            splashRoundCanvas.SetActive(false);
        }
        
        if (activeCanvas != "CANVAS_SPLASH_TEAM")
        {
            splashTeamCanvas.SetActive(false);
        }
        
        if (activeCanvas != "CANVAS_STONE_SELECTION")
        {
            stoneSelectionCanvas.SetActive(false);
        }
        
        if (activeCanvas != "CANVAS_CURLING_IN_GAME_AIM" &&
                 activeCanvas != "CANVAS_CURLING_IN_GAME_POWER" &&
                 activeCanvas != "CANVAS_CURLING_IN_GAME_SWEEP")
        {
            curlingInGameCanvas.SetActive(false);
        }
        
        if (activeCanvas != "CANVAS_POST_THROW_RESULT")
        {
            postThrowResultCanvas.SetActive(false);
        }
        
    }


    



}
