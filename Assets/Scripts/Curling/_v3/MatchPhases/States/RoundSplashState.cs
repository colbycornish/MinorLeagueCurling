using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace CurlingManagersV3.MatchPhaseStates
{
    // TODO: Impliment Round Spalsh Screen based on how many rounds are being played
    public class RoundSplashState : IMatchPhaseState
    {
        private InputAction goToNextPhaseAction;
        
        void Awake()
        {
            goToNextPhaseAction = InputSystem.actions.FindAction("GoToNextPhase", true);
        }

        /************************************************************************************************************************/

        protected virtual void OnEnable()
        {
            // Add listeners and enable action
            goToNextPhaseAction.performed += OnGoToNextPhase;
            goToNextPhaseAction.Enable();
            
            UICanvasManager.v3.UICanvasManager.Instance.OpenCurlingTurnSplash();
            OnEnter();
        }

        public override void OnEnter()
        {
            Debug.Log("Invoking GoToNextPhase after 4 seconds");
            this.StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            Debug.Log("Coroutine started, waiting for 4 seconds...");
            yield return new WaitForSeconds(4f);
            OnGoToNextPhase(new InputAction.CallbackContext());
        }

        /************************************************************************************************************************/

        protected virtual void OnDisable()
        {
            goToNextPhaseAction.performed -= OnGoToNextPhase;
            goToNextPhaseAction.Disable();
        }

        /************************************************************************************************************************/

        private void OnGoToNextPhase(InputAction.CallbackContext obj)
        {
            MainCurlingManager.ChangePhase(matchPhaseType: CurlingMatchPhase.TurnSplash);
        } 

        /************************************************************************************************************************/

        /// <summary>
        /// Used to help the CurlingManager know which canvas to enable when this state is active
        /// </summary>
        public override CurlingMatchPhase StateMatchPhaseType => CurlingMatchPhase.RoundSplash;

    }
}