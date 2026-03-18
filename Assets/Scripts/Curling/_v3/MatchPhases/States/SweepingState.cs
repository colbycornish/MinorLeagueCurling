using UnityEngine;

namespace CurlingManagersV3.MatchPhaseStates
{
    public class SweepingState : IMatchPhaseState
    {
        // private GameObject controlsCanvas;
        private MatchPhaseStateMachine matchPhaseStateMachine; // Reference to the controller

        public override void OnEnter()
        {
            UICanvasManager.v3.UICanvasManager.Instance.OpenCurlingInGameHUD();
            // Add listeners to buttons, e.g., PlayButton.onClick.AddListener(() => uiStateMachine.ChangeState(new GamePlayState(...)));
        }

        

        public override void OnUpdate()
        {
            // Handle input or logic while in this state
        }

        private void HandleCurlingStoneSweepingPhaseInput()
        {
            // bool isSweepingLeft = Input.GetKey(leftSweeperKey);
            // bool isSweepingRight = Input.GetKey(rightSweeperKey);
            // if (Input.GetKey(rightSweeperKey))
            // {
                
            // }
            // if (Input.GetKey(leftSweeperKey))
            // {
                
            // }
        }

        public override void OnExit()
        {
            // Remove listeners
        }

        /// <summary>
        /// Used to help the CanvasManager know which canvas to enable when this state is active
        /// </summary>
        public override CurlingMatchPhase StateMatchPhaseType => CurlingMatchPhase.CurlingStoneSweepingPhase;

    }
}