using UnityEngine;

namespace CurlingManagersV3.MatchPhaseStates
{
    public class AimingAndPowerState : IMatchPhaseState
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

        // private void HandleCurlingAimControlsPhaseInput()
        // {
        //     /// Add Left Curl To Throw
        //     if (Input.GetKeyDown(leftCurlKey))
        //     {
        //         CurlingManager._instance.Aiming.IncreaseLeftCurlAmount();
        //     }

        //     /// Add Right Curl To Throw
        //     if (Input.GetKeyDown(rightCurlKey))
        //     {
        //         CurlingManager._instance.Aiming.IncreaseRightCurlAmount();
        //     }

        //     /// Move Aiming Direction Left/Right
        //     if (Input.GetAxis("Horizontal") != 0f)
        //     {
        //         float input = Input.GetAxis("Horizontal");
                
        //         CurlingManager._instance.Aiming.ChangeDirection(
        //             input: input
        //         );
        //     }

        //     if (Input.GetKeyDown(confirmKey))
        //     {
        //         CurlingManager._instance.OnAimingPhaseComplete();
        //     }
        // }


        // private void HandlePowerMeterInput()
        // {
        //     // Handled by Stone Throw Controller
        //     // Increase Power
        //     if (Input.GetKeyDown(KeyCode.UpArrow))
        //     {
        //         CurlingManager._instance.Throwing.IncreaseThrowPower();
        //     }

        //     // Decrease Power
        //     if (Input.GetKeyDown(KeyCode.DownArrow))
        //     {
        //         CurlingManager._instance.Throwing.DecreaseThrowPower();
        //     }

        //     // Confirm Power Selection and Launch Stone
        //     if (Input.GetKeyDown(confirmKey))
        //     {
        //         CurlingManager._instance.OnPowerPhaseSelection();
        //         CurlingManager._instance.OnPowerPhaseComplete();
        //     }
        // }

        public override void OnExit()
        {
            // Remove listeners
        }

        /// <summary>
        /// Used to help the CanvasManager know which canvas to enable when this state is active
        /// </summary>
        public override CurlingMatchPhase StateMatchPhaseType => CurlingMatchPhase.AimingAndPowerPhase;

    }
}