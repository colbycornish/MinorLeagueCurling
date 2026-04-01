using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// </summary>

namespace CurlingManagersV3
{
    public class Sweeping : MonoBehaviour
    {
        /// <summary>
        /// Setup
        /// </summary>
        public void Setup()
        {           
            // GameObject lsEB = CurlingManager._instance.Parameters.Canvas.leftSweeperExhaustionBar;
            // GameObject rsEB = CurlingManager._instance.Parameters.Canvas.rightSweeperExhaustionBar;

            // CurlingManager._instance.Parameters.Canvas.leftSweeperExhaustionBarController = 
            //     lsEB.GetComponent<SweeperExhaustionBarV2>();
            
            // CurlingManager._instance.Parameters.Canvas.rightSweeperExhaustionBarController = 
            //     rsEB.GetComponent<SweeperExhaustionBarV2>();

        }

        /// <summary>
        /// Update!
        /// </summary>
        // void Update()
        // {
        //     // return; // FLAG: DISABLE FIXED UPDATE FOR NOW TO TEST IF THIS IS CAUSING PROBLEMS WITH SWEEPING
        //     // CurlingMatchPhase currentPhase = MatchPhaseManager._instance.currentPhase;
        //     CurlingMatchPhase currentMatchPhase = CurlingManager._instance.StateMachine.CurrentState?.StateMatchPhaseType ?? CurlingMatchPhase.None;
        //     if (currentMatchPhase == CurlingMatchPhase.CurlingStoneSweepingPhase)
        //     {
                
        //         // Track sweeping input ** NEW SWEEPER CODE **
        //         // this does not work. It's unclear why. 
        //         // this.isSweepingLeft = Input.GetKeyDown(leftSweeperKey); // ** NEW SWEEPER CODE **
        //         // this.isSweepingRight = Input.GetKeyDown(rightSweeperKey); // ** NEW SWEEPER CODE **

        //         if (Input.GetKeyDown(KeyCode.LeftShift))
        //         {
        //             Debug.Log("Left Shift Key Down - Start Sweeping Left");
        //             CurlingManager._instance.Parameters.Sweeping.IsSweepingLeft = true;
        //             CurlingManager._instance.Parameters.Canvas.leftSweeperExhaustionBarController.IncreaseExhaustionLevel();
        //         } 
        //         if (Input.GetKeyDown(KeyCode.RightShift))
        //         {
        //             Debug.Log("Right Shift Key Down - Start Sweeping Right");
        //             CurlingManager._instance.Parameters.Sweeping.IsSweepingRight = true;
        //             CurlingManager._instance.Parameters.Canvas.rightSweeperExhaustionBarController.IncreaseExhaustionLevel();
        //         } 
        //         if (Input.GetKeyUp(KeyCode.LeftShift)){
        //             CurlingManager._instance.Parameters.Sweeping.IsSweepingLeft = false;
        //         }
        //         if (Input.GetKeyUp(KeyCode.RightShift)){
        //             CurlingManager._instance.Parameters.Sweeping.IsSweepingRight = false;
        //         }
        //         return;
        //     }

        //     if (currentMatchPhase == CurlingMatchPhase.CurlingNoSweepZone && 
        //         (CurlingManager._instance.Parameters.Sweeping.IsSweepingLeft == true ||
        //         CurlingManager._instance.Parameters.Sweeping.IsSweepingRight == true)
        //     )
        //     {
        //         CurlingManager._instance.Parameters.Sweeping.IsSweepingLeft = false;
        //         CurlingManager._instance.Parameters.Sweeping.IsSweepingRight = false;
        //     }
        // }

        // void FixedUpdate()
        // {
        //     // return; // FLAG: DISABLE FIXED UPDATE FOR NOW TO TEST IF THIS IS CAUSING PROBLEMS WITH SWEEPING
        //     CurlingStone currentStone = CurlingManager._instance.Parameters.Stones.currentStone; 
        //     if (currentStone == null || currentStone.rb == null) return;

        //     CurlingMatchPhase currentMatchPhase = CurlingManager._instance.StateMachine.CurrentState?.StateMatchPhaseType ?? CurlingMatchPhase.None;
        //     // CurlingMatchPhase currentPhase = MatchPhaseManager._instance.currentPhase;
        //     if (currentMatchPhase != CurlingMatchPhase.CurlingStoneSweepingPhase)
        //     { 
        //         return;
        //     }

        //     // TODO: Move to the match flow
        //     if (
        //         currentStone != null && 
        //         currentStone.Parameters.Status.IsSliding && 
        //         currentStone.rb != null
        //         // The following line used to be in immediately after != null,
        //         // and I think it was throwing things off: 
        //         // && Mathf.Abs(curlAmount) > 0.01f
        //     ) 
        //     {
                
        //         // todo: move to match flow
        //         if (
        //             currentStone.rb != null && 
        //             currentStone.rb.linearVelocity.sqrMagnitude < 0.01f && 
        //             currentStone.rb.angularVelocity.sqrMagnitude < 0.01f
        //         )
        //         {
        //             currentStone.Parameters.Status.IsSliding = false;
        //         }

                
        //         ApplySpinForceToStone(stone: currentStone);
                
        //         /// TODO: both of these are currently false
        //         bool sweeping = CurlingManager._instance.Parameters.Sweeping.IsSweepingLeft || CurlingManager._instance.Parameters.Sweeping.IsSweepingRight;
        //         if (sweeping)
        //         {
        //             ApplySweepingImpactToStone(stone: currentStone);
        //         }

                
        //     }
        // }

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