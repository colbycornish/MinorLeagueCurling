using UnityEngine;

namespace CurlingManagersV3.MatchPhaseStates
{
    public class NoSweepZoneState : IMatchPhaseState
    {
        // private GameObject controlsCanvas;
        private MatchPhaseStateMachine matchPhaseStateMachine; // Reference to the controller

        public override void OnEnter()
        {
            UICanvasManager.v3.UICanvasManager.Instance.CloseAllCanvases();
            UICanvasManager.v3.UICanvasManager.Instance.CloseAllModals();
            // Add listeners to buttons, e.g., PlayButton.onClick.AddListener(() => uiStateMachine.ChangeState(new GamePlayState(...)));
        }

        public override void OnUpdate()
        {
            HandleCurlingTurnEndInput();
            // Handle input or logic while in this state
        }

        private void HandleCurlingTurnEndInput()
        {
            bool stoneIsMoving = CurlingManager._instance.stoneManager.IsCurrentStoneMoving();
            bool stoneIsMovingForward = CurlingManager._instance.stoneManager.IsCurrentStoneMovingForward();
            if (!stoneIsMoving || !stoneIsMovingForward)
            {
                MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.PostThrowResult);
            }
        }

        private void HandleCurlingNoSweepZonePhaseInput()
        {
            bool stoneIsMoving = CurlingManager._instance.stoneManager.IsCurrentStoneMoving();
            bool stoneIsMovingForward = CurlingManager._instance.stoneManager.IsCurrentStoneMovingForward();
            if (!stoneIsMoving || !stoneIsMovingForward)
            {
                MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.PostThrowResult);
            }
        }

        public override void OnExit()
        {
            // Remove listeners
        }

        /// <summary>
        /// Used to help the CanvasManager know which canvas to enable when this state is active
        /// </summary>
        public override CurlingMatchPhase StateMatchPhaseType => CurlingMatchPhase.CurlingNoSweepZone;

    }
}