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
            if (CurlingManagersV3.CurlingManager._instance != null)
            {
                CurlingManager cm = CurlingManagersV3.CurlingManager._instance;
                int turnNumber = cm.Parameters.Turn.CurrentTurnCount;
            }
            gameObject.SetActive(true); // Show the canvas
            // Add listeners to buttons, e.g., PlayButton.onClick.AddListener(() => uiStateMachine.ChangeState(new GamePlayState(...)));
        }

        public override void OnUpdate()
        {
            // Handle input or logic while in this state
        }

        public override void OnExit()
        {
            gameObject.SetActive(false); // Hide the canvas
            // Remove listeners
        }

        /************************************************************************************************************************/

        /// <summary>
        /// Used to help the CanvasManager know which canvas to enable when this state is active
        /// </summary>
        public override CanvasType StateCanvasType => CanvasType.CurlingGameSplashTurnDisplay;

    }
}