using UnityEngine;
using System.Collections.Generic;
using CurlingManagersV3;

namespace UICanvasManager.v3
{
    public class CurlingGameFinalResultDisplay : ICanvasState
    {
        public GameObject loseCanvas;
        public GameObject winCanvas;
        public GameObject drawCanvas;

        /************************************************************************************************************************/

        public override void OnEnter()
        {
            winCanvas.SetActive(false);
            loseCanvas.SetActive(false);
            drawCanvas.SetActive(false);
            //
            gameObject.SetActive(true); // Show the canvas
            SelectFinalScreenToShow();
            // Add listeners to buttons, e.g., PlayButton.onClick.AddListener(() => uiStateMachine.ChangeState(new GamePlayState(...)));
        }

        public override void OnExit()
        {
            winCanvas.SetActive(false);
            loseCanvas.SetActive(false);
            drawCanvas.SetActive(false);
            //
            gameObject.SetActive(false); // Hide the canvas
            // Remove listeners
        }

        /************************************************************************************************************************/

        private void SelectFinalScreenToShow()
        {
            CurlingManager cm = CurlingManager._instance;
            int homeScore = cm.Parameters.CurrentGameScore.teamHomeScore;
            int awayScore = cm.Parameters.CurrentGameScore.teamAwayScore;


            if (homeScore > awayScore)
            {
                Debug.Log("Home team wins! Showing win canvas.");
                winCanvas.SetActive(true);
                return;
            }
            else if (awayScore > homeScore)
            {
                Debug.Log("Away team wins! Showing lose canvas.");
                loseCanvas.SetActive(true);
                return;
            }
            else
            {
                Debug.Log("It's a draw! Showing draw canvas.");
                drawCanvas.SetActive(true);
                return;
            }
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Used to help the CanvasManager know which canvas to enable when this state is active
        /// </summary>
        public override CanvasType StateCanvasType => CanvasType.CurlingGameFinalResultDisplay;

    }
}