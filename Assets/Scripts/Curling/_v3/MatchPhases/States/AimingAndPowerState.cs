using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

namespace CurlingManagersV3.MatchPhaseStates
{
    public class AimingAndPowerState : IMatchPhaseState
    {
        private InputAction goToNextPhaseAction;
        private InputAction aimAction;
        private InputAction curlLeftAction;
        private InputAction curlRightAction;
        private InputAction increasePowerAction;
        private InputAction decreasePowerAction;

        private Coroutine _aimingProcess;
        
        private void Awake()
        {
            Debug.Log("AimingAndPowerState Awake: Finding Input Actions");
            aimAction = InputSystem.actions.FindAction("AimThrow", true);
            curlLeftAction = InputSystem.actions.FindAction("CurlLeft", true);
            curlRightAction = InputSystem.actions.FindAction("CurlRight", true);
            increasePowerAction = InputSystem.actions.FindAction("IncreasePower", true);
            decreasePowerAction = InputSystem.actions.FindAction("DecreasePower", true);
            goToNextPhaseAction = InputSystem.actions.FindAction("GoToNextPhase", true);
            
        }

        /************************************************************************************************************************/


        protected virtual void OnEnable()
        {
            // Adding listeners to input actions and enabling actions
            aimAction.started += OnAimThrow;
            aimAction.performed += OnAimThrow;
            aimAction.Enable();

            curlLeftAction.performed += OnCurlLeft;
            curlLeftAction.Enable();

            curlRightAction.performed += OnCurlRight;
            curlRightAction.Enable();

            increasePowerAction.performed += OnIncreasePower;
            increasePowerAction.Enable();

            decreasePowerAction.performed += OnDecreasePower;
            decreasePowerAction.Enable();

            goToNextPhaseAction.performed += OnGoToNextPhase;
            goToNextPhaseAction.Enable();
            
            OnEnter();
        }

        public override void OnEnter()
        {
            // Close any UI Modals, and open the InGame Curling HUD
            Debug.Log("AimingAndPowerState OnEnterState: Entering Aiming and Power State");
            UICanvasManager.v3.UICanvasManager.Instance.CloseAllModals();
            UICanvasManager.v3.UICanvasManager.Instance.OpenCurlingInGameHUD();

            // Reseting the power meter, so that the levels start in the middle
            MainCurlingManager.Throwing.ResetPowerMeter();
        }


        /************************************************************************************************************************/

        private void OnAimThrow(InputAction.CallbackContext obj)
        {
            Debug.Log("Aim Throw action performed");
            StartAiming(obj);   
        }
        
        // This method is called when the player performs the AimThrow action. It starts a coroutine that 
        // continuously updates the aiming direction based on the input value until the input value returns to zero 
        // (i.e., the player stops aiming).
        public void StartAiming(InputAction.CallbackContext callbackContext)
        {
            if (this._aimingProcess != null)
                this.StopCoroutine(this._aimingProcess);

            this._aimingProcess = this.StartCoroutine(this.AimingProcess(callbackContext));
        }


        private IEnumerator AimingProcess(InputAction.CallbackContext callbackContext)
        {
            float direction = callbackContext.ReadValue<float>();

            while (direction != 0)
            {
                CurlingManager._instance.Aiming.ChangeDirection(
                    input: callbackContext.ReadValue<float>()
                );

                yield return null;

                direction = callbackContext.ReadValue<float>();
            }
        }

        /************************************************************************************************************************/

        // Changes the initial curl amount
        private void OnCurlLeft(InputAction.CallbackContext obj)
        {
            Debug.Log("Curl Left action performed");
            CurlingManager._instance.Aiming.IncreaseLeftCurlAmount();
        }

        private void OnCurlRight(InputAction.CallbackContext obj)
        {
            Debug.Log("Curl Right action performed");
            CurlingManager._instance.Aiming.IncreaseRightCurlAmount();
        }

        /************************************************************************************************************************/

        // Increases or decreases the initial power level of the throw.
        private void OnIncreasePower(InputAction.CallbackContext obj)
        {
            Debug.Log("Increase Power action performed");
            CurlingManager._instance.Throwing.IncreaseThrowPower();
        }

        private void OnDecreasePower(InputAction.CallbackContext obj)
        {
            Debug.Log("Decrease Power action performed");
            CurlingManager._instance.Throwing.DecreaseThrowPower();
        }

        /************************************************************************************************************************/

        


        public void OnDisable()
        {
            // Removing listeners from input actions and disabling actions
            // Debug.Log("AimingAndPowerState OnDisable: Removing listeners from input actions and disabling actions");
            aimAction.started -= OnAimThrow;
            aimAction.performed -= OnAimThrow;
            aimAction.Disable();

            curlLeftAction.performed -= OnCurlLeft;
            curlLeftAction.Disable();

            curlRightAction.performed -= OnCurlRight;
            curlRightAction.Disable();

            increasePowerAction.performed -= OnIncreasePower;
            increasePowerAction.Disable();

            decreasePowerAction.performed -= OnDecreasePower;
            decreasePowerAction.Disable();

            goToNextPhaseAction.performed -= OnGoToNextPhase;
            goToNextPhaseAction.Disable();
        }

        /************************************************************************************************************************/

        // Lauch the stone! 
        private void OnGoToNextPhase(InputAction.CallbackContext obj)
        {

            Debug.Log("Go To Next Phase action performed, launching stone and changing phase");
            MainCurlingManager.Throwing.UpdatePowerFromPowerMeterSelection();
            
            MainCurlingManager.Throwing.LaunchStone(
                stone: CurlingManager._instance.Parameters.Stones.currentStone, 
                launchDirection: CurlingManager._instance.Parameters.Course.directionPivot.forward,
                power: CurlingManager._instance.Parameters.Throwing.LaunchPower 
            );
        } 


        /************************************************************************************************************************/

        /// <summary>
        /// Used to help the CurlingManager know which canvas to enable when this state is active
        /// </summary>
        public override CurlingMatchPhase StateMatchPhaseType => CurlingMatchPhase.AimingAndPowerPhase;

    }
}

