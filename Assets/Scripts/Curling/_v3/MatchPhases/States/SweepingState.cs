using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace CurlingManagersV3.MatchPhaseStates
{
    public class SweepingState : IMatchPhaseState
    {
        private InputAction sweepLeftAction;
        private InputAction sweepRightAction;
        
        private void Awake()
        {
            sweepLeftAction = InputSystem.actions.FindAction("SweepLeft");
            sweepRightAction = InputSystem.actions.FindAction("SweepRight");
        }

        /************************************************************************************************************************/

        protected virtual void OnEnable()
        {
            sweepLeftAction.performed += OnSweepLeft;
            sweepLeftAction.Enable();

            sweepRightAction.performed += OnSweepRight;
            sweepRightAction.Enable();
            
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
            sweepLeftAction.Disable();
            sweepRightAction.Disable();
            
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
                MainCurlingManager.ChangePhase(matchPhaseType: CurlingMatchPhase.PostThrowResult);
            }

            CurlingManager._instance.Parameters.Sweeping.IsSweepingLeft = sweepLeftAction.IsPressed();
            CurlingManager._instance.Parameters.Sweeping.IsSweepingRight = sweepRightAction.IsPressed();
            
        }

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




        // private Coroutine _sweepLeftProcess;
        // private Coroutine _sweepRightProcess;
        // public void StartSweepLeft(InputAction.CallbackContext callbackContext)
        // {
        //     if (this._sweepLeftProcess != null)
        //         this.StopCoroutine(this._sweepLeftProcess);

        //     this._sweepLeftProcess = this.StartCoroutine(this.SweepLeftProcess(callbackContext));
        // }


        // private IEnumerator SweepLeftProcess(InputAction.CallbackContext callbackContext)
        // {
        //     double startTime = callbackContext.startTime;
        //     double time = callbackContext.time;
        //     while (time - startTime < 0.01f)
        //     {
        //         CurlingManager._instance.Parameters.Sweeping.IsSweepingLeft = true;
        //         // CurlingManager._instance.Parameters.Canvas.leftSweeperExhaustionBarController.IncreaseExhaustionLevel();

        //         yield return null;

        //         time = callbackContext.time;
        //     }

        //     CurlingManager._instance.Parameters.Sweeping.IsSweepingLeft = false;
        // }

        // private void OnSweepLeft(InputAction.CallbackContext obj)
        // {
        //     Debug.Log("Sweep Left action performed");
        //     StartSweepLeft(obj);
        //     CurlingManager._instance.Parameters.Sweeping.IsSweepingLeft = true;
        //     // CurlingManager._instance.Parameters.Canvas.leftSweeperExhaustionBarController.IncreaseExhaustionLevel();
        // }

        // private void OnSweepLeftCancelled(InputAction.CallbackContext obj)
        // {
        //     Debug.Log("Sweep Left action Cancelled");
        //     CurlingManager._instance.Parameters.Sweeping.IsSweepingLeft = false;
        // }

        // /************************************************************************************************************************/

        // public void StartSweepRight(InputAction.CallbackContext callbackContext)
        // {
        //     if (this._sweepRightProcess != null)
        //         this.StopCoroutine(this._sweepRightProcess);

        //     this._sweepRightProcess = this.StartCoroutine(this.SweepRightProcess(callbackContext));
        // }


        // private IEnumerator SweepRightProcess(InputAction.CallbackContext callbackContext)
        // {
        //     double startTime = callbackContext.startTime;
        //     double time = callbackContext.time;
        //     while (time - startTime < 0.01f)
        //     {
        //         CurlingManager._instance.Parameters.Sweeping.IsSweepingRight = true;
        //         // CurlingManager._instance.Parameters.Canvas.rightSweeperExhaustionBarController.IncreaseExhaustionLevel();

        //         yield return null;

        //         time = callbackContext.time;
        //     }

        //     CurlingManager._instance.Parameters.Sweeping.IsSweepingRight = false;
        // }

        // private void OnSweepRight(InputAction.CallbackContext obj)
        // {
        //     Debug.Log("Sweep Right action performed");
        //     CurlingManager._instance.Parameters.Sweeping.IsSweepingRight = true;
        //     StartSweepRight(obj);
        //     CurlingManager._instance.Parameters.Canvas.rightSweeperExhaustionBarController.IncreaseExhaustionLevel();
        // }

        // private void OnSweepRightCancelled(InputAction.CallbackContext obj)
        // {
        //     Debug.Log("Sweep Right action Cancelled");
        //     CurlingManager._instance.Parameters.Sweeping.IsSweepingRight = false;
        // }
