using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// </summary>

namespace CurlingManagersV3
{
    public class Throwing : MonoBehaviour
    {
        /// <summary>
        /// Power Meter Accessors
        /// </summary>
        public void ResetPowerMeter()
        {
            CurlingManager._instance.Parameters.Canvas.powerMeterController.SetPowerLimits(
                current: 0.3f,
                min: 0f,
                max: 1f
            );
        }

        public void IncreaseThrowPower()
        {
            CurlingManager._instance.Parameters.Canvas.powerMeterController.IncreasePower();
        }

        public void DecreaseThrowPower()
        {
            CurlingManager._instance.Parameters.Canvas.powerMeterController.DecreasePower();
        }

        public void UpdatePowerFromPowerMeterSelection()
        {
            float p = CurlingManager._instance.Parameters.Canvas.powerMeterController.GetPower();
            CurlingManager._instance.Parameters.Throwing.LaunchPower = p;
        }

        public float GetLaunchPower(){
            return CurlingManager._instance.Parameters.Throwing.LaunchPower;
        }

        public void LaunchPowerSelected(){
            float power = CurlingManager._instance.Parameters.Canvas.powerMeterController.currentPower;
            CurlingManager._instance.Parameters.Throwing.LaunchPower = power;

            LaunchStone(
                stone: CurlingManager._instance.Parameters.Stones.currentStone, //CurlingManagersV3.CurlingManager._instance.stoneManager.currentStone,
                launchDirection: CurlingManagersV3.CurlingManager._instance.Parameters.Course.directionPivot.forward,
                power: power
            );
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
                currentStone = CurlingManager._instance.Parameters.Stones.currentStone; //CurlingManager._instance.stoneManager.currentStone;
                return;
            }
            
            
            Vector3 direction = launchDirection;
            if (direction == null){
                direction = CurlingManager._instance.Parameters.Course.directionPivot.forward;
            }

            // Fire the stone
            currentStone.LaunchStone(
                launchForceMultiplier: CurlingManager._instance.Parameters.Throwing.LaunchPower,
                launchForce: CurlingManager._instance.Parameters.Throwing.LaunchForce, // default power
                launchDirection: direction,
                initialSpinDirection: currentStone.Parameters.Movement.SpinAmountInitial, // initial spin direction
                initialSpinStrength: CurlingManager._instance.Parameters.Throwing.SpinStrength // initial spin strength
            );
            
            

            // Tell relevant parties that stone has been launched
            currentStone.Parameters.Status.IsSliding = true;
            currentStone.Parameters.Status.IsThrown = true;
            currentStone.Parameters.Status.IsInPlay = true;

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