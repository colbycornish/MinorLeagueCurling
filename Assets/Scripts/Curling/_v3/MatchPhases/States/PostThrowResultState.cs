
using UnityEngine;

namespace CurlingManagersV3.MatchPhaseStates
{
    public class PostThrowResultState : IMatchPhaseState
    {
        // private GameObject controlsCanvas;
        private MatchPhaseStateMachine matchPhaseStateMachine; // Reference to the controller

        public override void OnEnter()
        {
            UICanvasManager.v3.UICanvasManager.Instance.OpenCurlingPostThrowResults();
            // Add listeners to buttons, e.g., PlayButton.onClick.AddListener(() => uiStateMachine.ChangeState(new GamePlayState(...)));
        }

        public override void OnUpdate()
        {
            // Handle input or logic while in this state
        }

        // private void HandlePostThrowResultInput()
        // {
        //     // Handle inputs specific to the Post Throw Result phase
        //     if (Input.GetMouseButtonDown(0))
        //     {
        //         if (!CurlingManager._instance.gameData.settings.enableObstaclePlacementByPlayer &&
        //             !CurlingManager._instance.gameData.settings.enableObstaclePlacementByEnvironment
        //         )
        //         {
        //             CurlingManager._instance.HandleNextTurn();
        //         }
        //         else
        //         {
        //             MatchPhaseManager._instance.SetPhase(CurlingMatchPhase.ObstacleSelection);
        //         }
        //     }
        // }

        public override void OnExit()
        {
            // Remove listeners
        }

        /// <summary>
        /// Used to help the CanvasManager know which canvas to enable when this state is active
        /// </summary>
        public override CurlingMatchPhase StateMatchPhaseType => CurlingMatchPhase.PostThrowResult;

    }
}