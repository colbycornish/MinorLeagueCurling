// using System.Collections.Generic;
// using UnityEngine;
// using System;

// public class CurlingStoneSweepController : MonoBehaviour
// {
//     [Header("Launch Settings")]
//     public float sweepStrength = 5f;     // Tweak for how much spin affects trajectory (side force applied during slide)

//     [Header("Sweeper Settings")]
//     public float sweepBoostAmount = 1.5f; // how strong the speed boost is ** NEW SWEEPER CODE **
//     public float sweepDecayRate = 2f;
//     public float curlAmount = 0f;       // -1 = left curl, 0 = no curl, 1 = right curl

//     // Sweeper State ** NEW SWEEPER CODE **
//     private bool isSweepingLeft = false; // ** NEW SWEEPER CODE **
//     private bool isSweepingRight = false; // ** NEW SWEEPER CODE **
//     private float sweepBoostFactor = 0f; // ** NEW SWEEPER CODE **

//     [Header("Input Keys")]
//     public KeyCode resetKey = KeyCode.R;
//     public KeyCode rightSweeperKey = KeyCode.L; // Action button for right sweeper sweeping ** NEW SWEEPER CODE **
//     public KeyCode leftSweeperKey = KeyCode.K; // Action button for left sweeper sweeping ** NEW SWEEPER CODE **


//     private void Awake()
//     {
//         // rb = GetComponent<Rigidbody>();
//     }

//     /// <summary>
//     /// Listen for curling phase changes
//     /// </summary>

//     private void OnEnable()
//     {
//         if (CurlingMatchPhaseManager.Instance == null) return;
//         CurlingMatchPhaseManager.Instance.OnPhaseChanged += HandlePhase;
//     }

//     private void OnDisable()
//     {
//         if (CurlingMatchPhaseManager.Instance == null) return;
//         CurlingMatchPhaseManager.Instance.OnPhaseChanged -= HandlePhase;
//     }

//     public void HandlePhase(CurlingMatchPhase phase)
//     {
//         CurlingMatchPhase currentPhase = CurlingMatchPhaseManager.Instance.CurrentPhase;
//         if (currentPhase == CurlingMatchPhase.CurlingAimControlsPhase)
//         {
//             Reset();
//         }
//         if (currentPhase == CurlingMatchPhase.CurlingStoneSweepingPhase){
//             // hasLaunched = true;
//             // isSliding = true;
//         }
//     }

//     /// <summary>
//     /// Update!
//     /// </summary>
//     void Update()
//     {
//         CurlingMatchPhase currentPhase = CurlingMatchPhaseManager.Instance.CurrentPhase;
//         if (currentPhase == CurlingMatchPhase.CurlingStoneSweepingPhase)
//         {
//             // Track sweeping input ** NEW SWEEPER CODE **
//             isSweepingLeft = Input.GetKey(leftSweeperKey); // ** NEW SWEEPER CODE **
//             isSweepingRight = Input.GetKey(rightSweeperKey); // ** NEW SWEEPER CODE **
//             return;
//         }

//     }

//     void FixedUpdate()
//     {
//         CurlingStone currentStone = CurlingGameManagerV2.Instance.stoneManager.currentStone;
//         if (currentStone == null || currentStone.rb == null) return;

//         // TODO: Move to the match flow
//         if (currentStone != null && currentStone.isSliding && currentStone.rb != null) // The following line used to be in immediately after != null and I think it was throwing things off: && Mathf.Abs(curlAmount) > 0.01f
//         {
//             // todo: move to match flow
//             if (currentStone.rb != null && currentStone.rb.linearVelocity.sqrMagnitude < 0.01f && currentStone.rb.angularVelocity.sqrMagnitude < 0.01f)
//             {
//                 currentStone.isSliding = false;
//                 // CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.PostThrowResult);
//             }
//             ApplySpinForceToStone();

//             // ** Apply sweeping boost ** NEW SWEEPER CODE
//             bool sweeping = isSweepingLeft || isSweepingRight;
//             if (sweeping)
//             {
//                 ApplySweepingImpactToStone();
//             }

//             // Debug lines to track that sweeping is working correctly
//             if (isSweepingLeft) Debug.Log("🧹 Sweeping LEFT (K key)");
//             if (isSweepingRight) Debug.Log("🧹 Sweeping RIGHT (L key)");
//         }
//     }

//     /// <summary>
//     /// Apply Spin
//     /// </summary>


//     public void ApplySpinForceToStone()
//     {
//         CurlingStone currentStone = CurlingGameManagerV2.Instance.stoneManager.currentStone;
//         // Fire the stone
//         currentStone.ApplySpinForceToStone(
//             spinAmount: curlAmount, // curlAmount
//             sweepStrength: sweepStrength
//         );
        
//         // 🧪 Debug: draw movement and curl direction
//         // Debug.DrawRay(currentStone.position, forward * 2f, Color.green);  // forward
//         // Debug.DrawRay(currentStone.position, side * 2f, Color.red);       // curl direction
//         // Debug.Log("Drawing curl debug rays!"); // FLAG: THIS IS NOT TRIGGERING SO CLEARLY SOMETHING IS WRONG
//     }

//     /// <summary>
//     /// Sweeping Impact
//     /// </summary>

//     public void ApplySweepingImpactToStone()
//     {
//         CurlingStone currentStone = CurlingGameManagerV2.Instance.stoneManager.currentStone;
//         Rigidbody rb = CurlingGameManagerV2.Instance.stoneManager.currentStone.rb; // currentStone
//         Vector3 forward = rb.linearVelocity.normalized;
//         Vector3 side = Vector3.Cross(Vector3.up, forward).normalized;
//         sweepBoostFactor = Mathf.Clamp01(sweepBoostFactor + Time.fixedDeltaTime * sweepDecayRate);

//         currentStone.ApplySweepingImpactToStone(
//             sweepStrength: sweepStrength,
//             sweepBoostFactor: sweepBoostFactor,
//             sweepBoostAmount: sweepBoostAmount,
//             sweepDecayRate: sweepDecayRate,
//             isSweepingLeft: isSweepingLeft,
//             isSweepingRight: isSweepingRight
//         );

//         if (isSweepingLeft && isSweepingRight)
//         {
//             sweepBoostFactor = Mathf.Clamp01(sweepBoostFactor - Time.fixedDeltaTime * sweepDecayRate);
//         }
//     }

    
  
//     /// <summary>
//     /// Reset
//     /// </summary>
//     public void Reset()
//     {
//         // hasLaunched = false;
//         // isSliding = false;
//         curlAmount = 0f;
//     }
// }


