using UnityEngine;

namespace UICanvasManager.v3
{
    public class CurlingRulesState : ICanvasState
    {
        private GameObject mainMenuCanvas;
        
        /************************************************************************************************************************/

        public override void OnEnter()
        {
            gameObject.SetActive(true); // Show the canvas
            // Add listeners to buttons, e.g., PlayButton.onClick.AddListener(() => uiStateMachine.ChangeState(new GamePlayState(...)));
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
        public override CanvasType StateCanvasType => CanvasType.CurlingRules;

        // useful functions
        public void OnUpdateRule()
        {
            
        }

        public void OnUpdateGameMode()
        {
            
        }

        public void OnUpdateDifficulty()
        {
            
        }

        public void OnUpdateNumberOfGames()
        {
            
        }

        public void OnUpdateScoring()
        {
            
        }

        public void OnUpdateThrowClock()
        {
            
        }

    }
}