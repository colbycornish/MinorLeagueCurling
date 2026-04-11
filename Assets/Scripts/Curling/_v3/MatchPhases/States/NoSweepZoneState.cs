using UnityEngine;
using UnityEngine.InputSystem;
 
namespace CurlingManagersV3.MatchPhaseStates
{
    public class NoSweepZoneState : IMatchPhaseState
    {
        private InputAction goToNextPhaseAction;
        
        void Awake()
        {
            goToNextPhaseAction = InputSystem.actions.FindAction("GoToNextPhase", true);
        }

        /************************************************************************************************************************/

        protected virtual void OnEnable()
        {
            // Add Listeners
            goToNextPhaseAction.performed += OnGoToNextPhase;
            goToNextPhaseAction.Enable();
            OnEnter();
        }

        public override void OnEnter()
        {
            UICanvasManager.v3.UICanvasManager.Instance.CloseAllCanvases();
            UICanvasManager.v3.UICanvasManager.Instance.CloseAllModals();
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
            MainCurlingManager.ChangePhase(matchPhaseType: CurlingMatchPhase.PostThrowResult);
        }

        /************************************************************************************************************************/

        public void Update()
        {
            HandleCurlingNoSweepZonePhaseInput();
        }

        /************************************************************************************************************************/

        private void HandleCurlingNoSweepZonePhaseInput()
        {
            // Debug.Log("NoSweepZoneState OnUpdate: Checking if stone has stopped moving or is no longer moving forward");
            bool stoneIsMoving = CurlingManager._instance.stoneManager.IsCurrentStoneMoving();
            bool stoneIsMovingForward = CurlingManager._instance.stoneManager.IsCurrentStoneMovingForward();
            if (!stoneIsMoving || !stoneIsMovingForward)
            {
                Debug.Log("Stone stopped moving or is no longer moving forward, transitioning to PostThrowResult phase");
                MainCurlingManager.ChangePhase(matchPhaseType: CurlingMatchPhase.PostThrowResult);
            }
        }

        private void OnGoToNextPhase(InputAction.CallbackContext obj)
        {
            MainCurlingManager.ChangePhase(matchPhaseType: CurlingMatchPhase.PostThrowResult);
        } 

        /************************************************************************************************************************/

        /// <summary>
        /// Used to help the CurlingManager know which canvas to enable when this state is active
        /// </summary>
        public override CurlingMatchPhase StateMatchPhaseType => CurlingMatchPhase.CurlingNoSweepZone;

    }
}