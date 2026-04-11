using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace CurlingManagersV3.MatchPhaseStates
{
    public class LaunchStoneState : IMatchPhaseState
    {
        private InputAction goToNextPhaseAction;
        
        void Awake()
        {
            goToNextPhaseAction = InputSystem.actions.FindAction("GoToNextPhase", true);
        }

        /************************************************************************************************************************/
        protected virtual void OnEnable()
        {
            // CurlingGui is already tracking state changes, and the launch countdown will start automatically upon
            // entering this state.
            // goToNextPhaseAction.performed += OnGoToNextPhase;
            // goToNextPhaseAction.Enable();

            OnEnter();
        }

        public override void OnEnter()
        {
            // UICanvasManager.v3.UICanvasManager.Instance.OpenCurlingPostThrowResults();
            this.StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            // Debug.Log("Coroutine started, waiting for 6 seconds...");
            yield return new WaitForSeconds(3.00f);
            OnGoToNextPhase(new InputAction.CallbackContext());
        }

        /************************************************************************************************************************/

        protected virtual void OnDisable()
        {
            goToNextPhaseAction.performed -= OnGoToNextPhase;
            goToNextPhaseAction.Disable();
        }

        /************************************************************************************************************************/

        // Lauch the stone! 
        private void OnGoToNextPhase(InputAction.CallbackContext obj)
        {

            // Debug.Log("Go To Next Phase action performed, launching stone and changing phase");
            // MainCurlingManager.Throwing.UpdatePowerFromPowerMeterSelection();
            
            MainCurlingManager.Throwing.LaunchStone(
                stone: CurlingManager._instance.Parameters.Stones.currentStone, 
                launchDirection: CurlingManager._instance.Parameters.Course.directionPivot.forward,
                power: CurlingManager._instance.Parameters.Throwing.LaunchPower 
            );
        } 

        /************************************************************************************************************************/
        /// <summary>
        /// Used to help the CurlingManager know which canvas to enable when this state is active
        /// </summary>
        public override CurlingMatchPhase StateMatchPhaseType => CurlingMatchPhase.CurlingLaunchStonePhase;

    }
}