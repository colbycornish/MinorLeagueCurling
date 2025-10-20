using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class CurlingStone : MonoBehaviour
{
    [Header("Basic Data")]
    public string title = "Basic Stone";
    public string description = "Just your basic curling stone.";
    public string id = "";
    public Image avatarImage;

    [Header("Ids")]
    [HideInInspector] public int teamId_i;
    [HideInInspector] public string teamId;
    [HideInInspector] public int stoneIndex;

    [Header("Status")]
    [HideInInspector] public bool isThrown = false;
    [HideInInspector] public bool isInPlay = false;
    [HideInInspector] public bool isSliding = false;
    [HideInInspector] public bool isInScoringZone = false;
    
    [Header("Stats")]
    public float distanceFromTarget = 1000f;

    [Header("Spin")]
    /// <summary>
    /// spin can go in either the left (negative) or right (positive) direction
    /// </summary>
    public float spinSpeedInitial = 0f;
    public float spinSpeedCurrent = 0f;
    public float spinSpeedTarget = 0f;
    public float spinSpeedStep = 0.25f;
    public float spinSpeedDecay = 0.05f;
    public float spinSpeedTargetMax = 1f;
    public float maxTorque = 1f;
    /// <summary>
    /// Todo: remove?
    /// </summary>
    [HideInInspector] public float curlAmountCurrent = 0f;
    [HideInInspector] public float curlAmountInitial = 0f;
    [HideInInspector] public float spinAmountInitial = 0f;
    [HideInInspector] public float spinAmountCurrent = 0f;
    
    
    [Header("Speed")]
    [HideInInspector] public float speedCurrent = 0f;


    [Header("Model")]
    public Rigidbody rb; // Rigidbody to apply force / detect motion
    public GameObject visual; // Optional: mesh or model 

    [Header("Special Abilities")]
    [HideInInspector] public bool hasSpecialAbility = false;

    
    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    public void ResetStone(Vector3 position, Quaternion rotation)
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = position;
        transform.rotation = rotation;
    }

    /// <summary>
    /// Launch Functions
    /// </summary>
    
    public void LaunchStone(
        float launchForceMultiplier, // power
        float launchForce, // default power
        Vector3 launchDirection,
        float initialSpinDirection = 0f,
        float initialSpinStrength = 5f
    )
    {        
        // Fire the stone
        rb.AddForce(launchDirection * launchForce * launchForceMultiplier, ForceMode.Impulse);
        Debug.Log("[Stone Throw] 🚀 Stone launched with power: " + launchForceMultiplier);

        // Add initial spin to the stone
        if (initialSpinDirection != 0f)
        {
            rb.angularVelocity = Vector3.up * initialSpinDirection * initialSpinStrength; // Add angular velocity for curling effect (purely visual spin)
            Debug.Log($"[Stone Throw] 🌀 Initial spin applied: angularVelocity = {rb.angularVelocity}");
        }

        isThrown = true;
        isInPlay = true;
        isSliding = true;
    }

    /// <summary>
    /// Spin functions
    /// </summary>

    public void ApplySpinForceToStone(
        float spinAmount,   // TODO: adjust to spin multiplier
        float sweepStrength,
        float spinSpeedMultiplier = 1f
    )
    {
        Vector3 forward = rb.linearVelocity.normalized;
        Vector3 side = Vector3.Cross(Vector3.up, forward).normalized;

        float temp_spinMultiplier = 0.33f; // Added "* 0.33f" so the default curved trajectory is more mild (might need to make even more mild)
        rb.AddForce(side * spinAmount * temp_spinMultiplier * sweepStrength, ForceMode.Acceleration); 
    }

    
    public void HandleLeftSweepSpin()
    {
        spinSpeedTarget -= spinSpeedStep;
        spinSpeedTarget = Mathf.Clamp(spinSpeedTarget, -spinSpeedTargetMax, spinSpeedTargetMax);
        HandleSpinStop();
    }

    public void HandleRightSweepSpin()
    {
        spinSpeedTarget = spinSpeedTarget + spinSpeedStep;
        spinSpeedTarget = Mathf.Clamp(spinSpeedTarget, -spinSpeedTargetMax, spinSpeedTargetMax);
        HandleSpinStop();
    }

    public void HandleSpinStop()
    {
        if (spinSpeedTarget > 0f && spinSpeedTarget < spinSpeedStep)
        {
            spinSpeedTarget = 0f; // Prevent negative spin speed
        }
        if (spinSpeedTarget < 0f && spinSpeedTarget > -spinSpeedStep)
        {
            spinSpeedTarget = 0f; // Prevent negative spin speed
        }
    }

    // not currently used, since this affects movement as well.
    // it was intended to provide better visual feedback only.
    public void HandleVisualSpin()
    {
        // Rigidbody rb = currentStone.GetComponent<Rigidbody>();
        float appliedSpinSpeed = spinSpeedTarget;
        if (spinSpeedCurrent > maxTorque)
        {
            // float spinDiff = spinSpeedCurrent - maxTorque;
            appliedSpinSpeed = 0;// - spinDiff;
            Debug.Log($"[Stone Spin] 🌀 Spin speed clamped: {spinSpeedCurrent} -> {appliedSpinSpeed}");
        }
        float torqueMagnitude = 1f * appliedSpinSpeed; // Adjust this value to control the strength of the spin
        rb.AddRelativeTorque(transform.up * torqueMagnitude, ForceMode.Acceleration);
        LogCurrentSpinForce();

    }

    private void LogCurrentSpinForce()
    {
        spinSpeedCurrent = rb.angularVelocity.magnitude;
        Mathf.Clamp(spinSpeedCurrent, -10f, 10f); // Clamp the spin speed to a reasonable rang
    }

    /// <summary>
    /// Sweeping Functions
    /// TODO: Change apply sweeping to intake a vector and a force3.x
    /// </summary>

    public void ApplySweepingImpactToStone(
        float sweepStrength,
        float sweepBoostFactor,
        float sweepBoostAmount,
        float sweepDecayRate,
        bool isSweepingLeft,
        bool isSweepingRight
    )
    {
        Vector3 forward = rb.linearVelocity.normalized;
        Vector3 side = Vector3.Cross(Vector3.up, forward).normalized;
        sweepBoostFactor = Mathf.Clamp01(sweepBoostFactor + Time.fixedDeltaTime * sweepDecayRate);
        // applies a standard amount of force in the forward direction
        rb.AddForce(forward * sweepBoostFactor * sweepBoostAmount, ForceMode.Acceleration);

        // ** Modify curl direction slightly based on sweeping ** NEW SWEPER CODE
        if (isSweepingLeft && !isSweepingRight)
        {
            Debug.Log("Left Sweep Only Applied");
            // applies reduced force in the left direction
            rb.AddForce(-side * sweepStrength * 0.75f, ForceMode.Acceleration); // changed scaling from 0.2 to 0.75 to increase sweeping impact
        }

        else if (!isSweepingLeft && isSweepingRight)
        {
            Debug.Log("Right Sweep Only Applied");
            // applies reduced force in the right direction
            rb.AddForce(side * sweepStrength * 0.75f, ForceMode.Acceleration); // changed scaling from 0.2 to 0.75 to increase sweeping impact
        }
    }

    public bool IsStationary => rb != null && rb.linearVelocity.sqrMagnitude < 0.01f && rb.angularVelocity.sqrMagnitude < 0.01f;
}