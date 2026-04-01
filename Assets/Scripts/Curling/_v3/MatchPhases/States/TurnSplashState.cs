using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace CurlingManagersV3.MatchPhaseStates
{
    public class TurnSplashState : IMatchPhaseState
    {
        private InputAction goToNextPhaseAction;
        
        void Awake()
        {
            goToNextPhaseAction = InputSystem.actions.FindAction("GoToNextPhase", true);
        }

        /************************************************************************************************************************/

        protected virtual void OnEnable()
        {
            CurlingManager._instance.Parameters.Stones.currentStone = null; 
            goToNextPhaseAction.performed += OnGoToNextPhase;
            goToNextPhaseAction.Enable();
            
            OnEnter();
        }

        

        public override void OnEnter()
        {
            UICanvasManager.v3.UICanvasManager.Instance.OpenCurlingGameSplashTurnDisplaySection();


            this.StartCoroutine(Wait());
            Debug.Log("Invoking GoToNextPhase after 4 seconds");

            OnGoToNextPhase(new InputAction.CallbackContext());

        }

        private IEnumerator Wait()
        {
            Debug.Log("Coroutine started, waiting for 4 seconds...");
            yield return new WaitForSeconds(4f);
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
            MainCurlingManager.ChangePhase(matchPhaseType: CurlingMatchPhase.StoneSelection);
        } 

        /************************************************************************************************************************/
        /// <summary>
        /// Used to help the CurlingManager know which canvas to enable when this state is active
        /// </summary>
        public override CurlingMatchPhase StateMatchPhaseType => CurlingMatchPhase.TurnSplash;

    }
}