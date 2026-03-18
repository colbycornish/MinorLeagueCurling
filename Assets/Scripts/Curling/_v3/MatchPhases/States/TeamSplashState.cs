using UnityEngine;

namespace CurlingManagersV3.MatchPhaseStates
{
    public class TeamSplashState : IMatchPhaseState
    {
        // private GameObject controlsCanvas;
        private MatchPhaseStateMachine matchPhaseStateMachine; // Reference to the controller

        public override void OnEnter()
        {
            UICanvasManager.v3.UICanvasManager.Instance.OpenCurlingTeamSplash();
            // Add listeners to buttons, e.g., PlayButton.onClick.AddListener(() => uiStateMachine.ChangeState(new GamePlayState(...)));
        }

        public override void OnUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.RoundSplash);
            }
        }

        public override void OnExit()
        {
            // Remove listeners
        }

        /// <summary>
        /// Used to help the CanvasManager know which canvas to enable when this state is active
        /// </summary>
        public override CurlingMatchPhase StateMatchPhaseType => CurlingMatchPhase.TeamSplash;

    }
}