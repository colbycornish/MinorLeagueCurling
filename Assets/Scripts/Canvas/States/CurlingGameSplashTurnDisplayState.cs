using System.Collections.Generic;
using TMPro;
using UnityEngine;
using CurlingManagersV3;
using Unity.VisualScripting;

namespace UICanvasManager.v3
{
    public class CurlingGameSplashTurnDisplayState : ICanvasState
    {
        private GameObject mainMenuCanvas;
        public List<TextMeshProUGUI> turnDisplayTextObjects;
        
        /************************************************************************************************************************/

        public override void OnEnter()
        {
            UpdateTextDisplays();
            gameObject.SetActive(true); // Show the canvas
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
                int turnNumber = cm.Parameters.Turn.CurrentTurnCount;
                int turnDisplayNumber = turnNumber + 1; // Assuming you want to display "Turn 1" for the first turn (which is index 0)
                // string teamName = cm.Parameters.Turn.CurrentTurn == CurlingGameTurnType.Home ? "Home" : "Away";
            
                string textToDisplay = $"Turn {turnDisplayNumber}";

                foreach (TextMeshProUGUI textObj in turnDisplayTextObjects)
                {
                    textObj.text = textToDisplay;
                }
            }
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Used to help the CanvasManager know which canvas to enable when this state is active
        /// </summary>
        public override CanvasType StateCanvasType => CanvasType.CurlingGameSplashTurnDisplay;

    }
}