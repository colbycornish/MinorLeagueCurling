using UnityEngine;
// using CurlingGameDataNamespace;

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
///    
/// 
/// "CANVAS_SPLASH_ROUND",
// "CANVAS_SPLASH_TEAM",
// "CANVAS_STONE_SELECTION",
// "CANVAS_CURLING_IN_GAME_AIM",
// "CANVAS_CURLING_IN_GAME_POWER",
// "CANVAS_CURLING_IN_GAME_SWEEP",
// "CANVAS_POST_THROW_RESULT"
/// </summary>


public class CurlingGameManager : MonoBehaviour
{
    public CurlingMatchCanvasController curlingMatchCanvasController;
    public StoneThrowController stoneThrowController;
    public CurlingGameData gameData = new CurlingGameData();
    private bool hasGameEnded = false;
    private bool hasGameStarted = false;
    private bool hasGamePaused = false;
    // private string phaseID = "START";
    private float gameTime = 0f;
    private float canvasDisplayTimeMarker = 0f;
    private bool canvasDisplayTimeMarkerIsActive = false;

    /// <summary>
    /// Array of current stone placements (logged after throw)
    /// </summary>
    /// 
    private void Start()
    {
        // InitiateDemo();
    }

    void Update()
    {
        // Time management
        // if (canvasDisplayTimeMarkerIsActive)
        // {
        //     canvasDisplayTimeMarker += Time.deltaTime;
            
        // }
        // // Check for user input to switch between canvases
        // if (Input.GetKeyDown(KeyCode.N))
        // {
        //     ResetCanvasTimer();
        //     canvasDisplayTimeMarkerIsActive = true;
        //     curlingMatchCanvasController.GoToNextCanvas();
        // }

        // if (Input.GetKeyDown(KeyCode.B))
        // {
        //     ResetCanvasTimer();
        //     canvasDisplayTimeMarkerIsActive = true;
        //     curlingMatchCanvasController.GoToPreviousCanvas();
        // }
        // if (curlingMatchCanvasController.activeCanvas == "CANVAS_CURLING_IN_GAME_POWER" &&
        //     Input.GetKeyDown(KeyCode.Space)
        // )
        // {
        //     ResetCanvasTimer();
        //     canvasDisplayTimeMarkerIsActive = true;
        //     curlingMatchCanvasController.GoToNextCanvas();
        // }
    }

    void SetTeams()
    {

    }

    void SaveStonePositions()
    {

    }

    void AdvanceTurn()
    {
        gameData.turnCurrent += 1;
    }

    public void StartGame()
    {
        hasGameStarted = true;
        gameData.hasStarted = true;
    }

    public void EndGame()
    {
        hasGameEnded = true;
        gameData.hasEnded = true;
    }
    public bool getHasGameStarted() { return hasGameStarted; }
    public bool getHasGameEnded() { return hasGameEnded; }
    public bool ShouldEndMatch()
    {
        if (gameData.turnCurrent > gameData.settings.totalTurnsPerGame)
        {
            EndGame();
            gameData.hasEnded = true;
            return true;
        }
        return false;
    }

    void Reset()
    {
        gameData.turnCurrent = 0;
        
        hasGameEnded = false;
        hasGameStarted = false;
        hasGamePaused = false;
    }

    void ResetCanvasTimer()
    {
        canvasDisplayTimeMarker = 0f;
    }

    public void DisplaySplashRound()
    {
        hasGamePaused = true;
        // curlingMatchCanvasController.SetActiveCanvasById("CANVAS_SPLASH_ROUND");
    }
    // curlingMatchCanvasController.SetActiveCanvasById("CANVAS_SPLASH_TEAM");
    // curlingMatchCanvasController.SetActiveCanvasById("CANVAS_STONE_SELECTION");
    // curlingMatchCanvasController.SetActiveCanvasById("CANVAS_CURLING_IN_GAME_AIM");
    // curlingMatchCanvasController.SetActiveCanvasById("CANVAS_CURLING_IN_GAME_POWER");
    // curlingMatchCanvasController.SetActiveCanvasById("CANVAS_CURLING_IN_GAME_SWEEP");
    // curlingMatchCanvasController.SetActiveCanvasById("CANVAS_POST_THROW_RESULT");
    
}
