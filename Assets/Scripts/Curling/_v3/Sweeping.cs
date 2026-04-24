using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// </summary>

namespace CurlingManagersV3
{
    public class Sweeping : MonoBehaviour
    {
        /// <summary>
        /// Apply Spin
        /// </summary>
        public void ApplySpinForceToStone(CurlingStone stone)
        {
            // Debug.Log("Applying Spin Force to Stone");
            CurlingStone currentStone = stone;
            if (currentStone == null){
                currentStone = CurlingManager._instance.Parameters.Stones.currentStone; 
            }
            if (currentStone == null || currentStone.rb == null) return;
            // Fire the stone
            currentStone.ApplySpinForceToStone(
                spinAmount: CurlingManager._instance.Parameters.Sweeping.CurlAmount, // curlAmount
                sweepStrength: CurlingManager._instance.Parameters.Sweeping.SweepStrength
            );
            
            // 🧪 Debug: draw movement and curl direction
            // Debug.DrawRay(currentStone.position, forward * 2f, Color.green);  // forward
            // Debug.DrawRay(currentStone.position, side * 2f, Color.red);       // curl direction
            // Debug.Log("Drawing curl debug rays!"); // FLAG: THIS IS NOT TRIGGERING SO CLEARLY SOMETHING IS WRONG
        }

        /// <summary>
        /// Sweeping Impact
        /// </summary>

        public void ApplySweepingImpactToStone(CurlingStone stone)
        {
            // Debug.Log("....Attempting to apply impact");
            CurlingStone currentStone = stone;
            if (currentStone == null){
                currentStone = CurlingManager._instance.Parameters.Stones.currentStone; 
            }
            if (currentStone == null || currentStone.rb == null) return;
            
            Rigidbody rb = currentStone.rb; // currentStone
            Vector3 forward = rb.linearVelocity.normalized;
            Vector3 side = Vector3.Cross(Vector3.up, forward).normalized;

            CurlingManager._instance.Parameters.Sweeping.SweepBoostFactor = Mathf.Clamp01(
                CurlingManager._instance.Parameters.Sweeping.SweepBoostFactor 
                    + Time.fixedDeltaTime 
                    * CurlingManager._instance.Parameters.Sweeping.SweepDecayRate
            );

            currentStone.ApplySweepingImpactToStone(
                sweepStrength: CurlingManager._instance.Parameters.Sweeping.SweepStrength,
                sweepBoostFactor: CurlingManager._instance.Parameters.Sweeping.SweepBoostFactor,
                sweepBoostAmount: CurlingManager._instance.Parameters.Sweeping.SweepBoostAmount,
                sweepDecayRate: CurlingManager._instance.Parameters.Sweeping.SweepDecayRate,
                isSweepingLeft: CurlingManager._instance.Parameters.Sweeping.IsSweepingLeft,
                isSweepingRight: CurlingManager._instance.Parameters.Sweeping.IsSweepingRight
            );

            if (
                CurlingManager._instance.Parameters.Sweeping.IsSweepingLeft && 
                CurlingManager._instance.Parameters.Sweeping.IsSweepingRight
            )
            {
                CurlingManager._instance.Parameters.Sweeping.SweepBoostFactor = Mathf.Clamp01(
                    CurlingManager._instance.Parameters.Sweeping.SweepBoostFactor 
                        - Time.fixedDeltaTime 
                        * CurlingManager._instance.Parameters.Sweeping.SweepDecayRate
                );
            }
        }

        
        public void ResetSweeperExhaustionBars()
        {
            /// leftSweeperBar
            CurlingManager._instance.Parameters.Canvas.leftSweeperExhaustionBarController.SetExhaustionLevelsFromCharacterStats(
                exhaustionLevel: 0.0f, 
                maxExhaustionLevel: 1.0f, 
                minExhaustionLevel: 0.0f,
                exhaustionRate: 0.06f,
                recoveryRate: 0.04f,
                exhaustionThreshold: 0.85f
            );
            CurlingManager._instance.Parameters.Canvas.leftSweeperExhaustionBarController.InitializeDisplay();
            
            /// rightSweeperBar
            CurlingManager._instance.Parameters.Canvas.rightSweeperExhaustionBarController.SetExhaustionLevelsFromCharacterStats(
                exhaustionLevel: 0.0f, 
                maxExhaustionLevel: 1.0f, 
                minExhaustionLevel: 0.0f,
                exhaustionRate: 0.06f,
                recoveryRate: 0.04f,
                exhaustionThreshold: 0.85f
            );
            CurlingManager._instance.Parameters.Canvas.rightSweeperExhaustionBarController.InitializeDisplay();

        }
    
        /// <summary>
        /// Reset
        /// </summary>
        public void Reset()
        {
            CurlingManager._instance.Parameters.Sweeping.CurlAmount = 0f;
        }

    }
}

