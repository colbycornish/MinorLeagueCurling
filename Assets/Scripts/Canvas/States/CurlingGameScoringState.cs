using System.Collections.Generic;
using TMPro;
using UnityEngine;
using CurlingManagersV3;
using Unity.VisualScripting;

namespace UICanvasManager.v3
{
    public class CurlingGameScoringDisplayState : ICanvasState
    {
        private GameObject mainMenuCanvas;
        public List<TextMeshProUGUI> turnDisplayTextObjects;

        [Header("Team Name")]
        public List<TextMeshProUGUI> homeTeamNameDisplayTextObjects;
        public List<TextMeshProUGUI> awayTeamNameDisplayTextObjects;
        [Header("Team Score")]
        public List<TextMeshProUGUI> homeTeamScoreDisplayTextObjects;
        public List<TextMeshProUGUI> awayTeamScoreDisplayTextObjects;

        

        
        
        /************************************************************************************************************************/

        public override void OnEnter()
        { 
            // UpdateTextDisplays();
            gameObject.SetActive(true); // Show the canvas
            UpdateTextDisplays();
            // Add listeners to buttons, e.g., PlayButton.onClick.AddListener(() => uiStateMachine.ChangeState(new GamePlayState(...)));
        }

        public override void OnExit()
        {
            gameObject.SetActive(false); // Hide the canvas
            // Remove listeners
        }

        private void UpdateTextDisplays()
        {
            if (CurlingManagersV3.CurlingManager._instance != null)
            {
                CurlingManager cm = CurlingManagersV3.CurlingManager._instance;
                string homeTeamName = cm.Parameters.Teams.teamHome.name;
                string awayTeamName = cm.Parameters.Teams.teamAway.name;

                int homeTeamScore = cm.Parameters.CurrentGameScore.teamHomeScore;
                int awayTeamScore = cm.Parameters.CurrentGameScore.teamAwayScore;

                UpdateTextObjects(homeTeamNameDisplayTextObjects, homeTeamName);
                UpdateTextObjects(homeTeamScoreDisplayTextObjects, $"{homeTeamScore}");

                UpdateTextObjects(awayTeamNameDisplayTextObjects, awayTeamName);
                UpdateTextObjects(awayTeamScoreDisplayTextObjects, $"{awayTeamScore}");

            }
        }

        private void UpdateTextObjects(
            List<TextMeshProUGUI> listOfTextObjects,
            string text
        )
        {
            foreach (TextMeshProUGUI textObj in turnDisplayTextObjects)
            {
                textObj.text = text;
            }
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Used to help the CanvasManager know which canvas to enable when this state is active
        /// </summary>
        public override CanvasType StateCanvasType => CanvasType.CurlingGameScoringDisplayState;

    }
}