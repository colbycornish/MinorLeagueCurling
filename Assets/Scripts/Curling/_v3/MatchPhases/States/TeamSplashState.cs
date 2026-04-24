using UnityEngine;
using UnityEngine.InputSystem;

namespace CurlingManagersV3.MatchPhaseStates
{
    public class TeamSplashState : IMatchPhaseState
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
            OnEnter();
        }

        public override void OnEnter()
        {
            UICanvasManager.v3.UICanvasManager.Instance.OpenCurlingTeamSplash();
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
        
        public override CurlingMatchPhase StateMatchPhaseType => CurlingMatchPhase.TeamSplash;

    }
}