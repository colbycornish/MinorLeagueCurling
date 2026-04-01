
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
            Debug.Log("StartGameIntroState Awake: Finding SkipCinematicsAction");
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
            UICanvasManager.v3.UICanvasManager.Instance.OpenCurlingPostThrowResults();
            this.StartCoroutine(Wait());
        }

        private IEnumerator Wait()
        {
            Debug.Log("Coroutine started, waiting for 6 seconds...");
            yield return new WaitForSeconds(7.00f);
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
            Debug.Log("Initiate Next Turn or Go to End Game Results");
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

        /// <summary>
        /// Used to help the CurlingManager know which canvas to enable when this state is active
        /// </summary>
        public override CurlingMatchPhase StateMatchPhaseType => CurlingMatchPhase.PostThrowResult;

    }
}