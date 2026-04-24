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
            goToNextPhaseAction.performed += OnGoToNextPhase;
            goToNextPhaseAction.Enable();

            UICanvasManager.v3.UICanvasManager.Instance.OpenCurlingFinalResults();
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

        public override CurlingMatchPhase StateMatchPhaseType => CurlingMatchPhase.FinalResults;

    }
}