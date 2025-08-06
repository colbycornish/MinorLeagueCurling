using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages inputs for the Curling game.
/// It's unclear if inputs should be handled here, or at a lower level.
/// 
/// TODO: Research Input handling in Unity and decide if this is the right place.
/// </summary>


public class PracticeStoneMovementController : MonoBehaviour
{
    public bool isEnabled = true;

    [Header("Control Settings")]
    public KeyCode sweepLeftKey = KeyCode.K;
    public KeyCode resetKey = KeyCode.R;
    public KeyCode sweepRightKey = KeyCode.L;
    public KeyCode launchStoneKey = KeyCode.Space;

    [Header("Game Objects")]
    public CurlingStone currentStone;
    public GameObject directionObject;
    public Transform launchPoint;

    // public bool isEnabled = true;

    [Header("Launch Settings")]
    public float sweepStrength = 5f;     // Tweak for how much spin affects trajectory (side force applied during slide)

    [Header("Spin Settings")]
    public float spinDirection = 0f;
    public float curlAmount = -1f;       // -1 = left curl, 0 = no curl, 1 = right curl

    [Header("Sweeper Settings")]
    public float sweepBoostAmount = 1.5f; // how strong the speed boost is ** NEW SWEEPER CODE **
    public float sweepDecayRate = 2f;
    private bool isSweepingLeft = false; // ** NEW SWEEPER CODE **
    private bool isSweepingRight = false; // ** NEW SWEEPER CODE **
    private float sweepBoostFactor = 0f; // ** NEW SWEEPER CODE **


    private void Update()
    {
        if (!isEnabled || currentStone == null) return;


        if (Input.GetKeyDown(resetKey))
        {
            ResetStone();
        }
        if (Input.GetKeyDown(launchStoneKey))
        {
            HandleLaunchStone();
        }
        isSweepingLeft = Input.GetKey(sweepLeftKey); // ** NEW SWEEPER CODE **
        isSweepingRight = Input.GetKey(sweepRightKey); // ** NEW SWEEPER CODE **

    }


    void FixedUpdate()
    {
        if (!isEnabled || currentStone == null) return;
        if (Input.GetKeyDown(sweepLeftKey))
        {
            HandleLeftSweep();
        }
        if (Input.GetKeyDown(sweepRightKey))
        {
            HandleRightSweep();
        }
        if (isSweepingLeft || isSweepingRight)
        {
            ApplySweepingImpactToStone();
        }

        currentStone.ApplySpinForceToStone(
            spinAmount: spinDirection,
            sweepStrength: sweepStrength,
            spinSpeedMultiplier: 1f // adjusting to spinMultiplier
        );

        // The stone does have a component of additional spin that can be applied visually,
        // but currently it does actually affect how the stone moves. We might need to adjust
        // the stone's game object to have a rigidbody (for movement and physics), and then a 
        // separate component for just visual looks.

        // TODO: Impliment this.
        // if (!isEnabled || currentStone == null) return;
        // currentStone.HandleVisualSpin();

    }

    /// <summary>
    /// Launching functions
    /// </summary>

    private void HandleLaunchStone()
    {
        currentStone.LaunchStone(
            launchForceMultiplier: 1f, // power
            launchForce: 30f, // default power
            launchDirection: directionObject.transform.forward
        );
    }

    /// <summary>
    /// Sweeping Functions
    /// </summary>

    private void HandleLeftSweep()
    {
        /// Sets spin settings in the stone.
        /// currently doesn't do anything.
        currentStone.HandleLeftSweepSpin();

        // this works though
        spinDirection = -1f;
        currentStone.ApplySpinForceToStone(
            spinAmount: spinDirection, // adjusting to spin multiplier
            sweepStrength: sweepStrength,
            spinSpeedMultiplier: 1f
        );
    }

    private void HandleRightSweep()
    {
        /// Sets spin settings in the stone.
        /// currently doesn't do anything.
        currentStone.HandleRightSweepSpin();

        // this works though
        spinDirection = 1f;
        currentStone.ApplySpinForceToStone(
            spinAmount: spinDirection, // adjusting to spin multiplier
            sweepStrength: sweepStrength,
            spinSpeedMultiplier: 1f
        );

    }

    public void ApplySweepingImpactToStone()
    {
        sweepBoostFactor = Mathf.Clamp01(sweepBoostFactor + Time.fixedDeltaTime * sweepDecayRate);

        currentStone.ApplySweepingImpactToStone(
            sweepStrength: sweepStrength,
            sweepBoostFactor: sweepBoostFactor,
            sweepBoostAmount: sweepBoostAmount,
            sweepDecayRate: sweepDecayRate,
            isSweepingLeft: isSweepingLeft,
            isSweepingRight: isSweepingRight
        );

        if (isSweepingLeft && isSweepingRight)
        {
            sweepBoostFactor = Mathf.Clamp01(sweepBoostFactor - Time.fixedDeltaTime * sweepDecayRate);
        }
    }
    
    /// <summary>
    /// Reset the stone to the launch point
    /// </summary>
    private void ResetStone()
    {
        currentStone.transform.position = launchPoint.position;
    }
}
