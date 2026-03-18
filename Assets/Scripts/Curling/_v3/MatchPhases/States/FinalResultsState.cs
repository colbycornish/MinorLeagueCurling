using UnityEngine;

namespace CurlingManagersV3.MatchPhaseStates
{
    public class FinalResultsState : IMatchPhaseState
    {
        // private GameObject controlsCanvas;
        private MatchPhaseStateMachine matchPhaseStateMachine; // Reference to the controller

        public override void OnEnter()
        {
            CurlingManager._instance.gameEndManager.EndCurlingGame();
            UICanvasManager.v3.UICanvasManager.Instance.OpenCurlingFinalResults();
            // Add listeners to buttons, e.g., PlayButton.onClick.AddListener(() => uiStateMachine.ChangeState(new GamePlayState(...)));
        }

        public override void OnUpdate()
        {
            HandleFinalResultsInput();
            // Handle input or logic while in this state
        }

        private void HandleFinalResultsInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.RoundSplash);
            }
        }

        public override void OnExit()
        {
            // Remove listeners
        }

        /// <summary>
        /// Used to help the CanvasManager know which canvas to enable when this state is active
        /// </summary>
        public override CurlingMatchPhase StateMatchPhaseType => CurlingMatchPhase.FinalResults;

    }
}