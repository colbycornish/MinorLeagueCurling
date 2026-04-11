using UnityEngine;
using UnityEngine.InputSystem;

namespace CurlingManagersV3.MatchPhaseStates
{
    public class FinalResultsState : IMatchPhaseState
    {
        private InputAction goToNextPhaseAction;
        
        void Awake()
        {
            goToNextPhaseAction = InputSystem.actions.FindAction("GoToNextPhase", true);
        }

        /************************************************************************************************************************/

        protected virtual void OnEnable()
        {
            // Add listeners
            goToNextPhaseAction.performed += OnGoToNextPhase;
            goToNextPhaseAction.Enable();

            UICanvasManager.v3.UICanvasManager.Instance.OpenCurlingFinalResults();

            // OnEnter();
        }

        public override void OnEnter()
        {
            // This is likely already triggered?
            CurlingManager._instance.gameEndManager.EndCurlingGame();
        }

        /************************************************************************************************************************/

        protected virtual void OnDisable()
        {
            // Remove listeners
            goToNextPhaseAction.performed -= OnGoToNextPhase;
            goToNextPhaseAction.Disable();
        }

        public override void OnExit()
        {
            MainCurlingManager.ChangePhase(matchPhaseType: CurlingMatchPhase.ExitCurlingGame);
        }

        /************************************************************************************************************************/

        private void OnGoToNextPhase(InputAction.CallbackContext obj)
        {
            MainCurlingManager.ChangePhase(matchPhaseType: CurlingMatchPhase.ExitCurlingGame);
        } 

        /************************************************************************************************************************/

        /// <summary>
        /// Used to help the CurlingManager know which canvas to enable when this state is active
        /// </summary>
        public override CurlingMatchPhase StateMatchPhaseType => CurlingMatchPhase.FinalResults;

    }
}