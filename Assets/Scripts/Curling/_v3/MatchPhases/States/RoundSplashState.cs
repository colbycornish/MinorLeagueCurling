using UnityEngine;

namespace CurlingManagersV3.MatchPhaseStates
{
    public class RoundSplashState : IMatchPhaseState
    {
        // private GameObject controlsCanvas;
        private MatchPhaseStateMachine matchPhaseStateMachine; // Reference to the controller

        public override void OnEnter()
        {
            UICanvasManager.v3.UICanvasManager.Instance.OpenCurlingGameSplashTurnDisplaySection();
            // Add listeners to buttons, e.g., PlayButton.onClick.AddListener(() => uiStateMachine.ChangeState(new GamePlayState(...)));
        }

        public override void OnUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnExit();
                // MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.TeamSplash);
            }
            // Handle input or logic while in this state
        }

        public override void OnExit()
        {
            //TODO: Go To Stone Selection
            // Remove listeners
        }

        /// <summary>
        /// Used to help the CanvasManager know which canvas to enable when this state is active
        /// </summary>
        public override CurlingMatchPhase StateMatchPhaseType => CurlingMatchPhase.RoundSplash;

    }
}