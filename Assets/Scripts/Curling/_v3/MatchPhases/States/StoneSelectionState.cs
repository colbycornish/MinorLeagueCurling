using UnityEngine;
using UICanvasManager.v3;

namespace CurlingManagersV3.MatchPhaseStates
{
    public class StoneSelectionState : IMatchPhaseState
    {
        // private GameObject controlsCanvas;
        private MatchPhaseStateMachine matchPhaseStateMachine; // Reference to the controller

        public override void OnEnter()
        {
            UICanvasManager.v3.UICanvasManager.Instance.OpenStoneToUseSelectionModal();
        }

        public override void OnUpdate()
        {
            // Handle input or logic while in this state
            if (Input.GetMouseButtonDown(0))
            {
                MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.StoneSelectionConfirm);
            }
        }

        public override void OnExit()
        {
            // TODO: Go To AimAndPower
            // Remove listeners
        }

        /// <summary>
        /// Used to help the CanvasManager know which canvas to enable when this state is active
        /// </summary>
        public override CurlingMatchPhase StateMatchPhaseType => CurlingMatchPhase.StoneSelection;

    }
}