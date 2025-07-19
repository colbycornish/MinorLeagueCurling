using System.Collections.Generic;
using UnityEngine;
using System;

// [RequireComponent(typeof(Rigidbody))]
public class CurlingStoneThrowControllerV2 : MonoBehaviour
{
    public Action<GameObject> onRestCallback;

    [Header("Other References")]
    public Transform directionPivot;         // The pivot (arrow) showing throw direction
    public PowerMeterUI powerMeter;          // Assign PowerMeterUI script in Inspector
    public GameObject powerMeterPromptUI;

    [Header("Launch Settings")]
    public float launchForce = 100f;        // Base launch force (tweak as needed; adjust for distance--may want to bring force down if we shorten the distance)
    public float spinStrength = 5f;     // Tweak for how much spin affects trajectory (side force applied during slide)

    // State
    // private Rigidbody currentStone;
    private bool hasLaunched = false;
    // public float curlAmount = 0f;       // -1 = left curl, 0 = no curl, 1 = right curl

    [Header("Input Keys")]
    public KeyCode resetKey = KeyCode.R;
    public KeyCode rightSweeperKey = KeyCode.L; // Action button for right sweeper sweeping ** NEW SWEEPER CODE **
    public KeyCode leftSweeperKey = KeyCode.K; // Action button for left sweeper sweeping ** NEW SWEEPER CODE **
    public KeyCode actionKey = KeyCode.Space; // This is the key used to activate the power meter and launch the stone


    private void Awake()
    {
        // rb = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Listen for curling phase changes
    /// </summary>

    private void OnEnable()
    {
        if (CurlingMatchPhaseManager.Instance == null) return;
        CurlingMatchPhaseManager.Instance.OnPhaseChanged += HandlePhase;
    }

    private void OnDisable()
    {
        if (CurlingMatchPhaseManager.Instance == null) return;
        CurlingMatchPhaseManager.Instance.OnPhaseChanged -= HandlePhase;
    }

    public void HandlePhase(CurlingMatchPhase phase)
    {
        CurlingMatchPhase currentPhase = CurlingMatchPhaseManager.Instance.CurrentPhase;

        if (currentPhase == CurlingMatchPhase.CurlingAimControlsPhase)
        {
            Reset();
        }

        if (currentPhase == CurlingMatchPhase.CurlingPowerMeterPhase)
        {
            powerMeter.ResetMeter();
            powerMeter.Activate();
        }
        
        if (currentPhase == CurlingMatchPhase.CurlingStoneSweepingPhase)
        {

        }
    }

    /// <summary>
    /// Update
    /// </summary>

    void Update()
    {
        CurlingMatchPhase currentPhase = CurlingMatchPhaseManager.Instance.CurrentPhase;
        // Step 2: Press Space again to select power and launch
        if (powerMeter.isActive && Input.GetKeyDown(actionKey))
        {
            Debug.Log("[Stone Throw] Launch Power Selected");
            LaunchPowerSelected();
        }

    }

    /// <summary>
    /// Power Meter Accessors
    /// </summary>

    public void ActivatePowerMeter()
    {
        powerMeter.Activate();
        Debug.Log("[Stone Throw] Power meter activated");
    }

    
    public void LaunchPowerSelected(){
        powerMeter.SelectPower();  // locks the power level
        float power = powerMeter.GetPower();  // get selected power
        LaunchStone(power);
        hasLaunched = true;
        // isSliding = true;
        if (powerMeterPromptUI != null){
            powerMeterPromptUI.SetActive(false);
        }
    }

    /// <summary>
    /// Launch Stone
    /// </summary>
    
    public void LaunchStone(float power)
    {
        // Get relevant game objects;
        Rigidbody cs = CurlingGameManagerV2.Instance.stoneManager.currentStone.rb; // currentStone
        Vector3 launchDirection = CurlingGameManagerV2.Instance.aimController.directionPivot.forward;
        float curlAmountInitial = CurlingGameManagerV2.Instance.aimController.curlAmountInitial;
        // Apply information to Stone
        CurlingGameManagerV2.Instance.stoneManager.currentStone.isThrown = true;
        CurlingGameManagerV2.Instance.stoneManager.currentStone.isInPlay = true;

        // Fire the stone
        cs.AddForce(launchDirection * launchForce * power, ForceMode.Impulse);
        Debug.Log("[Stone Throw] 🚀 Stone launched with power: " + power);


        // Add initial spin to the stone
        cs.angularVelocity = Vector3.up * curlAmountInitial * spinStrength; // Add angular velocity for curling effect (purely visual spin)
        Debug.Log($"[Stone Throw] 🌀 Curl applied: angularVelocity = {cs.angularVelocity}");
        

        // Tell relevant parties that stone has been launched
        CurlingGameManagerV2.Instance.stoneManager.currentStone.isSliding = true;
        CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.CurlingStoneSweepingPhase);
    }  


   
    /// <summary>
    /// Reset / Setup
    /// </summary>
    
    public void Reset()
    {
        // if (currentStone != null)
        // {
        //     currentStone.linearVelocity = Vector3.zero;
        //     currentStone.angularVelocity = Vector3.zero;
        // }

        // isCharging = false;
        hasLaunched = false;
        // isSliding = false;
        
        powerMeter.ResetMeter();
    }
}

