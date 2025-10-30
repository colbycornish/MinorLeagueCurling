// using System.Collections.Generic;
// using UnityEngine;
// using System;

// // [RequireComponent(typeof(Rigidbody))]
// public class CurlingStoneThrowControllerV2 : MonoBehaviour
// {
//     // public Action<GameObject> onRestCallback;

//     [Header("Other References")]
//     public PowerMeterUI powerMeter; // Assign PowerMeterUI script in Inspector
//     public GameObject powerMeterPromptUI;

//     [Header("Launch Settings")]
//     public float launchForce = 100f; // Base launch force (tweak as needed; adjust for distance--may want to bring force down if we shorten the distance)
//     public float spinStrength = 5f; // Tweak for how much spin affects trajectory (side force applied during slide)

//     [Header("Input Keys")]
//     public KeyCode actionKey = KeyCode.Space; // This is the key used to activate the power meter and launch the stone


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

//         if (currentPhase == CurlingMatchPhase.CurlingPowerMeterPhase)
//         {
//             ResetPowerMeter();
//             ActivatePowerMeter();
//         }
//     }

//     /// <summary>
//     /// Update
//     /// </summary>

//     void Update()
//     {
//         CurlingMatchPhase currentPhase = CurlingMatchPhaseManager.Instance.CurrentPhase;
//         // Step 2: Press Space again to select power and launch
//         if (powerMeter.isActive && Input.GetKeyDown(actionKey))
//         {
//             Debug.Log("[Stone Throw] Launch Power Selected");
//             LaunchPowerSelected();
//         }

//     }

//     /// <summary>
//     /// Power Meter Accessors
//     /// </summary>
//     public void ResetPowerMeter()
//     {
//         powerMeter.ResetMeter();
//     }

//     public void ActivatePowerMeter()
//     {
//         powerMeter.Activate();
//     }

    
//     public void LaunchPowerSelected(){
//         powerMeter.SelectPower();  // locks the power level
//         float power = powerMeter.GetPower();  // get selected power
//         LaunchStone(power);
        
//         if (powerMeterPromptUI != null){
//             powerMeterPromptUI.SetActive(false);
//         }
//     }

//     /// <summary>
//     /// Launch Stone
//     /// </summary>

//     public void LaunchStone(float power)
//     {

//         // Get relevant game objects;
//         CurlingStone currentStone = CurlingGameManagerV2.Instance.stoneManager.currentStone;
//         Vector3 launchDirection = CurlingGameManagerV2.Instance.aimController.directionPivot.forward;

//         // Fire the stone
//         currentStone.LaunchStone(
//             launchForceMultiplier: power, // power
//             launchForce: launchForce, // default power
//             launchDirection: launchDirection,
//             initialSpinDirection: currentStone.spinAmountInitial, // initial spin direction
//             // initialSpinDirection: CurlingGameManagerV2.Instance.aimController.spinAmountInitial, // initial spin direction
//             initialSpinStrength: spinStrength // initial spin strength
//         );
        
//         // Tell relevant parties that stone has been launched
//         currentStone.isSliding = true;
//         currentStone.isThrown = true;
//         currentStone.isInPlay = true;

//         // Add initial spin to the stone
//         // float curlAmountInitial = CurlingGameManagerV2.Instance.aimController.curlAmountInitial;
//         // rb.angularVelocity = Vector3.up * curlAmountInitial * spinStrength; // Add angular velocity for curling effect (purely visual spin)
//         // Debug.Log($"[Stone Throw] 🌀 Curl applied: angularVelocity = {rb.angularVelocity}");

//         // The trigger for moving onto the sweeping phase has been moved to a yellow line collider
//         // CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.CurlingStoneSweepingPhase);
//     }  


   
//     /// <summary>
//     /// Reset / Setup
//     /// </summary>
//     public void Reset()
//     {
//         ResetPowerMeter();
//     }
// }

