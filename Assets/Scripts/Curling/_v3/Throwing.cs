using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the high-level game manager for a curling game.
/// It orchestrates the flow of the game, including starting new ends,
/// managing player turns, and handling the end of the game.
/// </summary>

namespace CurlingManagersV3
{
    public class Throwing : MonoBehaviour
    {
        [Header("Other References")]
        // public PowerMeterUI powerMeter; // Assign PowerMeterUI script in Inspector
        // public GameObject powerMeterPromptUI;
        public PowerMeterController powerMeterController;
        public bool isActive = false;
        public bool isReady = false;

        [Header("Launch Settings")]
        public float launchForce = 100f; // Base launch force (tweak as needed; adjust for distance--may want to bring force down if we shorten the distance)
        public float spinStrength = 5f; // Tweak for how much spin affects trajectory (side force applied during slide)
        public float launchPower = 0f;

        // [Header("Input Keys")]
        // public KeyCode actionKey = KeyCode.Space; // This is the key used to activate the power meter and launch the stone

    
        /// <summary>
        /// Update
        /// </summary>

        // void Update()
        // {
        //     CurlingMatchPhase currentPhase = CurlingManagersV3.MatchPhaseManager._instance.currentPhase;
        //     // Step 2: Press Space again to select power and launch
        //     if (powerMeter.isActive && Input.GetKeyDown(actionKey))
        //     {
        //         Debug.Log("[Stone Throw] Launch Power Selected");
        //         LaunchPowerSelected();
        //     }

        // }

        /// <summary>
        /// Power Meter Accessors
        /// </summary>
        public void Setup(
            PowerMeterController powerMeter
        ){
            this.powerMeterController = powerMeter;
        }

        public void ResetPowerMeter()
        {
            powerMeterController.SetPowerLimits(
                current: 0.3f,
                min: 0f,
                max: 1f
            );
            // powerMeter.ResetMeter();
        }

        public void IncreaseThrowPower()
        {
            // powerMeter.Activate();
            powerMeterController.IncreasePower();
        }

        public void DecreaseThrowPower()
        {
            // powerMeter.Activate();
            powerMeterController.DecreasePower();
        }

        public void ActivatePowerMeter()
        {
            // powerMeter.Activate();
        }

        public void UpdatePowerFromPowerMeterSelection()
        {
            float p = powerMeterController.GetPower();
            launchPower = p;
        }

        public float GetLaunchPower(){
            // LaunchPowerSelected();
            return launchPower;
        }

        public void LaunchPowerSelected(){
            float power = powerMeterController.currentPower;
            launchPower = power;
            LaunchStone(
                stone: CurlingManagersV3.CurlingManager._instance.stoneManager.currentStone,
                launchDirection: CurlingManagersV3.CurlingManager._instance.aiming.directionPivot.forward,
                power: power
            );
            
            // if (powerMeterPromptUI != null){
            //     powerMeterPromptUI.SetActive(false);
            // }
        }

        /// <summary>
        /// Launch Stone
        /// </summary>

        public void LaunchStone(
            CurlingStone stone,
            Vector3 launchDirection,
            float power
        )
        {
            CurlingStone currentStone = stone;
            if (currentStone == null){
                currentStone = CurlingManager._instance.stoneManager.currentStone;
                return;
            }
            
            
            Vector3 direction = launchDirection;
            if (direction == null){
                direction = CurlingManager._instance.aiming.directionPivot.forward;
            }

            // Fire the stone
            currentStone.LaunchStone(
                launchForceMultiplier: power, // power
                launchForce: launchForce, // default power
                launchDirection: direction,
                initialSpinDirection: currentStone.spinAmountInitial, // initial spin direction
                // initialSpinDirection: CurlingGameManagerV2.Instance.aimController.spinAmountInitial, // initial spin direction
                initialSpinStrength: spinStrength // initial spin strength
            );
            
            // Tell relevant parties that stone has been launched
            currentStone.isSliding = true;
            currentStone.isThrown = true;
            currentStone.isInPlay = true;

            // Add initial spin to the stone
            // float curlAmountInitial = CurlingGameManagerV2.Instance.aimController.curlAmountInitial;
            // rb.angularVelocity = Vector3.up * curlAmountInitial * spinStrength; // Add angular velocity for curling effect (purely visual spin)
            // Debug.Log($"[Stone Throw] 🌀 Curl applied: angularVelocity = {rb.angularVelocity}");

            // The trigger for moving onto the sweeping phase has been moved to a yellow line collider
            // CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.CurlingStoneSweepingPhase);
        }  


    
        /// <summary>
        /// Reset / Setup
        /// </summary>
        public void Reset()
        {
            ResetPowerMeter();
        }

    }
}





        // /// <summary>
        // /// Listen for curling phase changes
        // /// </summary>

        // private void OnEnable()
        // {
        //     if (CurlingMatchPhaseManager.Instance == null) return;
        //     CurlingMatchPhaseManager.Instance.OnPhaseChanged += HandlePhase;
        // }

        // private void OnDisable()
        // {
        //     if (CurlingMatchPhaseManager.Instance == null) return;
        //     CurlingMatchPhaseManager.Instance.OnPhaseChanged -= HandlePhase;
        // }

        // public void HandlePhase(CurlingMatchPhase phase)
        // {
        //     CurlingMatchPhase currentPhase = CurlingMatchPhaseManager.Instance.CurrentPhase;
        //     if (currentPhase == CurlingMatchPhase.CurlingAimControlsPhase)
        //     {
        //         Reset();
        //     }

        //     if (currentPhase == CurlingMatchPhase.CurlingPowerMeterPhase)
        //     {
        //         ResetPowerMeter();
        //         ActivatePowerMeter();
        //     }
        // }