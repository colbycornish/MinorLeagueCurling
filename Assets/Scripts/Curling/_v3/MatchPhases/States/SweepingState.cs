using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace CurlingManagersV3.MatchPhaseStates
{
    public class SweepingState : IMatchPhaseState
    {
        private InputAction sweepLeftAction;
        private InputAction sweepRightAction;

        private Coroutine _stoneStoppedCoroutine;
        
        private void Awake()
        {
            sweepLeftAction = InputSystem.actions.FindAction("SweepLeft");
            sweepRightAction = InputSystem.actions.FindAction("SweepRight");
        }

        /************************************************************************************************************************/

        private void EnableActions(){
            sweepLeftAction.performed += OnSweepLeft;
            sweepLeftAction.Enable();

            sweepRightAction.performed += OnSweepRight;
            sweepRightAction.Enable();
        }

        private void DisableActions(){
            sweepLeftAction.performed -= OnSweepLeft;
            sweepLeftAction.Disable();
            
            sweepRightAction.performed -= OnSweepRight;
            sweepRightAction.Disable();
        }

        /************************************************************************************************************************/

        protected virtual void OnEnable()
        {
            this._stoneStoppedCoroutine = null;
            MainCurlingManager.cameraController.UpdateOrbitStoneCameraTarget();
            EnableActions();
            OnEnter();
        }

        public override void OnEnter()
        {
            UICanvasManager.v3.UICanvasManager.Instance.OpenCurlingInGameHUD();
            MainCurlingManager.Sweeping.ResetSweeperExhaustionBars();
        }

        /************************************************************************************************************************/

        protected virtual void OnDisable()
        {
            this._stoneStoppedCoroutine = null;
            DisableActions();
            OnExit();
        }

        public override void OnExit()
        {
            CurlingManager._instance.Parameters.Sweeping.IsSweepingLeft = false;
            CurlingManager._instance.Parameters.Sweeping.IsSweepingRight = false;
        }

        /************************************************************************************************************************/
        public void Update()
        {
            bool stoneIsMoving = CurlingManager._instance.stoneManager.IsCurrentStoneMoving();
            bool stoneIsMovingForward = CurlingManager._instance.stoneManager.IsCurrentStoneMovingForward();

            if (!stoneIsMoving || !stoneIsMovingForward)
            {
                if (this._stoneStoppedCoroutine == null){
                    DisableActions();
                    StoneHasStopped();
                }
            } else
            {
                CurlingManager._instance.Parameters.Sweeping.IsSweepingLeft = sweepLeftAction.IsPressed();
                CurlingManager._instance.Parameters.Sweeping.IsSweepingRight = sweepRightAction.IsPressed();
            }

        }

        public void StoneHasStopped()
        {
            if (this._stoneStoppedCoroutine == null)
                this._stoneStoppedCoroutine = this.StartCoroutine(this.Wait());
                
        }

        
        

        private IEnumerator Wait()
        {
            Debug.Log("Stone has stopped, waiting for 3 seconds...");
            MainCurlingManager.cameraController.SwitchToOrbitStoneCamera();
            yield return new WaitForSeconds(3.00f);
            MainCurlingManager.ChangePhase(matchPhaseType: CurlingMatchPhase.PostThrowResult);
        }

        /************************************************************************************************************************/

        public void FixedUpdate()
        {
            // Decrease Sweeper Exhaustion Levels over time
            CurlingManager._instance.Parameters.Canvas.leftSweeperExhaustionBarController.DecreaseExhaustionLevel();
            CurlingManager._instance.Parameters.Canvas.rightSweeperExhaustionBarController.DecreaseExhaustionLevel();

            CurlingStone currentStone = CurlingManager._instance.Parameters.Stones.currentStone; 
            // TODO: Move to the match flow
            if (
                currentStone != null && 
                currentStone.Parameters.Status.IsSliding && 
                currentStone.rb != null
                // The following line used to be in immediately after != null,
                // and I think it was throwing things off: 
                // && Mathf.Abs(curlAmount) > 0.01f
            ) 
            {
                
                // todo: move to match flow
                if (
                    currentStone.rb.linearVelocity.sqrMagnitude < 0.01f && 
                    currentStone.rb.angularVelocity.sqrMagnitude < 0.01f
                )
                {
                    currentStone.Parameters.Status.IsSliding = false;
                }

                
                MainCurlingManager.Sweeping.ApplySpinForceToStone(stone: currentStone);
                
                bool sweeping = CurlingManager._instance.Parameters.Sweeping.IsSweepingLeft || CurlingManager._instance.Parameters.Sweeping.IsSweepingRight;
                if (sweeping)
                {
                    MainCurlingManager.Sweeping.ApplySweepingImpactToStone(stone: currentStone);
                }
            }
        }

        /************************************************************************************************************************/

        private void OnSweepLeft(InputAction.CallbackContext obj)
        {
            Debug.Log("Sweep Left action performed");
            CurlingManager._instance.Parameters.Sweeping.IsSweepingLeft = true;
            CurlingManager._instance.Parameters.Canvas.leftSweeperExhaustionBarController.IncreaseExhaustionLevel();
        }

        private void OnSweepRight(InputAction.CallbackContext obj)
        {
            Debug.Log("Sweep Right action performed");
            CurlingManager._instance.Parameters.Sweeping.IsSweepingRight = true;
            CurlingManager._instance.Parameters.Canvas.rightSweeperExhaustionBarController.IncreaseExhaustionLevel();
        }
        /************************************************************************************************************************/

        /// <summary>
        /// Used to help the CurlingManager know which canvas to enable when this state is active
        /// </summary>
        public override CurlingMatchPhase StateMatchPhaseType => CurlingMatchPhase.CurlingStoneSweepingPhase;

    }
}

