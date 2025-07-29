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
    public KeyCode sweepLeftKey = KeyCode.K;
    public KeyCode resetKey = KeyCode.R;
    public KeyCode sweepRightKey = KeyCode.L;
    public KeyCode launchStoneKey = KeyCode.Space;
    public CurlingStone currentStone;
    public float currentSpeed = 0f;
    public GameObject directionObject;
    public Transform launchPoint;
    public float targetSpeed = 0f;
    public float maxTargetSpeed = 3f;
    public float maxSpeed = 10f;
    public bool isEnabled = true;

    [Header("Launch Settings")]
    public float sweepStrength = 5f;     // Tweak for how much spin affects trajectory (side force applied during slide)

    [Header("Sweeper Settings")]
    public float sweepBoostAmount = 1.5f; // how strong the speed boost is ** NEW SWEEPER CODE **
    public float sweepDecayRate = 2f;

    // State
    // private Rigidbody currentStone; // [HideInInspector] 
    // private bool hasLaunched = false;
    // private bool isSliding = false;
    public float curlAmount = -1f;       // -1 = left curl, 0 = no curl, 1 = right curl

    // Sweeper State ** NEW SWEEPER CODE **
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
    }

    private void HandleLaunchStone()
    {
        currentStone.LaunchStone(
            launchForceMultiplier: 1f, // power
            launchForce: 30f, // default power
            launchDirection: directionObject.transform.forward
        );
    }

    private void ResetStone()
    {
        currentStone.transform.position = launchPoint.position;
    }

    private void HandleLeftSweep()
    {
        currentStone.ApplySpinForceToStone(
            spinAmount: curlAmount, // curlAmount
            sweepStrength: sweepStrength
        );

        // currentStone.ApplySweepingImpactToStone(
        //     sweepStrength: sweepStrength,
        //     sweepBoostFactor: sweepBoostFactor,
        //     sweepBoostAmount: sweepBoostAmount,
        //     sweepDecayRate: sweepDecayRate,
        //     isSweepingLeft: isSweepingLeft,
        //     isSweepingRight: isSweepingRight
        // );
    }

    private void HandleRightSweep()
    {
        currentStone.ApplySpinForceToStone(
            spinAmount: curlAmount, // curlAmount
            sweepStrength: sweepStrength
        );

        // currentStone.ApplySweepingImpactToStone(
        //     sweepStrength: sweepStrength,
        //     sweepBoostFactor: sweepBoostFactor,
        //     sweepBoostAmount: sweepBoostAmount,
        //     sweepDecayRate: sweepDecayRate,
        //     isSweepingLeft: isSweepingLeft,
        //     isSweepingRight: isSweepingRight
        // );
    }

    public void HandleSweepStop()
    {
        // if (targetSpinSpeed > 0f && targetSpinSpeed < 0.1f)
        // {
        //     targetSpinSpeed = 0f; // Prevent negative spin speed
        // }
        // if (targetSpinSpeed < 0f && targetSpinSpeed > -0.1f)
        // {
        //     targetSpinSpeed = 0f; // Prevent negative spin speed
        // }
    }

    private void HandleVisualSpin()
    {
        // Rigidbody rb = currentStone.GetComponent<Rigidbody>();
        // float appliedSpinSpeed = targetSpinSpeed;
        // if (currentSpinSpeed > maxTorque)
        // {
        //     appliedSpinSpeed = 0;   
        // }
        // float torqueMagnitude = 1f * appliedSpinSpeed; // Adjust this value to control the strength of the spin
        // rb.AddRelativeTorque(transform.up * torqueMagnitude, ForceMode.Acceleration);
        // LogCurrentSpinForce();

    }

    private void LogCurrentSpinForce()
    {
        // currentSpinSpeed = currentStone.rb.angularVelocity.magnitude;
        // Mathf.Clamp(currentSpinSpeed, -10f, 10f); // Clamp the spin speed to a reasonable range

        // rigidbody.velocity.normalized;

        // myTransform.forward = Vector3.Slerp(transform.forward, rigidbody.velocity.normalized, gravityRotate * Time.deltaTime);

    }

     


}
