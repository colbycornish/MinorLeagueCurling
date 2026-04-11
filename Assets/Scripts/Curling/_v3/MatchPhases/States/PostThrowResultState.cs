
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace CurlingManagersV3.MatchPhaseStates
{
    public class PostThrowResultState : IMatchPhaseState
    {
        private InputAction goToNextPhaseAction;
        
        void Awake()
        {
            goToNextPhaseAction = InputSystem.actions.FindAction("GoToNextPhase", true);
        }

        /************************************************************************************************************************/

        protected virtual void OnEnable()
        {
            goToNextPhaseAction.performed += OnGoToNextPhase;
            goToNextPhaseAction.Enable();

            MainCurlingManager.cameraController.SwitchToTargetZoneCamera();

            OnEnter();
        }

        public override void OnEnter()
        {
            UICanvasManager.v3.UICanvasManager.Instance.OpenCurlingPostThrowResults();
            this.StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            Debug.Log("Coroutine started, waiting for 6 seconds...");
            yield return new WaitForSeconds(5.00f);
            OnGoToNextPhase(new InputAction.CallbackContext());
        }
        

        /************************************************************************************************************************/

        protected virtual void OnDisable()
        {
            goToNextPhaseAction.performed -= OnGoToNextPhase;
            goToNextPhaseAction.Disable();
        }

        public override void OnExit()
        {
            
        }

        /************************************************************************************************************************/
 

        private void OnGoToNextPhase(InputAction.CallbackContext obj)
        {
            MainCurlingManager.ChangePhase(matchPhaseType: CurlingMatchPhase.ScoringPhase);
        } 

        /************************************************************************************************************************/

        /// <summary>
        /// Used to help the CurlingManager know which canvas to enable when this state is active
        /// </summary>
        public override CurlingMatchPhase StateMatchPhaseType => CurlingMatchPhase.PostThrowResult;

    }
}