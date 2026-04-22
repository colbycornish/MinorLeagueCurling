using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace CurlingManagersV3.MatchPhaseStates
{
    public class ScoringState : IMatchPhaseState
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
            UICanvasManager.v3.UICanvasManager.Instance.OpenCurlingScoringDisplayState();
            this.StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            Debug.Log("......Coroutine started, waiting for 6 seconds...");
            yield return new WaitForSeconds(5.00f);
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
            // Debug.Log("Initiate Next Turn or Go to End Game Results");
            if (!MainCurlingManager.gameData.settings.enableObstaclePlacementByPlayer &&
                !MainCurlingManager.gameData.settings.enableObstaclePlacementByEnvironment
            )
            {
                MainCurlingManager.HandleNextTurn();
            } else
            {
                MainCurlingManager.HandleNextTurn();
            }
        } 

        /************************************************************************************************************************/
        
        public override CurlingMatchPhase StateMatchPhaseType => CurlingMatchPhase.ScoringPhase;

    }
}