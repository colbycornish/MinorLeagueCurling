using UnityEngine;
using CurlingGameDataNamespace;

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

namespace CurlingGameDataNamespace {
    public class CurlingGameManager : MonoBehaviour
    {
        // Start() and Update() methods deleted - we don't need them right now

        public static CurlingGameManager Instance;
       
        //
        public CurlingMatchCanvasController curlingMatchCanvasController;
        public StoneThrowController stoneThrowController;
        /// <summary>
        /// defines markers for the current turn
        /// </summary>
        
        private int turnCount = 0;
        // private int roundCount = 1;
        // private int maxRounds = 3;

        private bool hasGameEnded = false;
        private bool hasGameStarted = false;
        private bool hasGamePaused = false;
        
        /// <summary>
        /// Array of current stone placements (logged after throw)
        /// </summary>

        void Update()
        {
            // Check for user input to switch between canvases
            if (Input.GetKeyDown(KeyCode.N))
            {
                curlingMatchCanvasController.GoToNextCanvas();
            }

            if (Input.GetKeyDown(KeyCode.B))
            {
                curlingMatchCanvasController.GoToPreviousCanvas();
            }
            if (curlingMatchCanvasController.activeCanvas == "CANVAS_CURLING_IN_GAME_POWER" && 
                Input.GetKeyDown(KeyCode.Space)
            )
            {
                curlingMatchCanvasController.GoToNextCanvas();
            }
            

        }

        void SetTeams () {

        }

        void SaveStonePositions () {

        }
 
        void AdvanceTurn () {
            turnCount =+ 1;
        }

        public void StartGame(){
            hasGameStarted = true;
        }

        public void EndGame(){
            hasGameEnded = true;
        }

        public bool getHasGameStarted(){
            return hasGameStarted;
        }

        public bool getHasGameEnded(){
            return hasGameEnded;
        }

        void Reset() {
            turnCount = 0;
            hasGameEnded = false;
            hasGameStarted = false;
            hasGamePaused = false;
        }
        


    }
}