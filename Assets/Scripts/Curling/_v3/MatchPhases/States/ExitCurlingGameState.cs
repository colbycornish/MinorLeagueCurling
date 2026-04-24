using UnityEngine;
using UnityEngine.InputSystem;

namespace CurlingManagersV3.MatchPhaseStates
{
    public class ExitCurlingGameState : IMatchPhaseState
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
            Debug.Log("Go to Main Menu or Exit Game");
            MainCurlingManager.ChangePhase(matchPhaseType: CurlingMatchPhase.None);
        }

        /************************************************************************************************************************/

        public override CurlingMatchPhase StateMatchPhaseType => CurlingMatchPhase.ExitCurlingGame;

    }
}