using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using CurlingStones;
using MoreMountains.Feedbacks;

public class CurlingStone : MonoBehaviour
{

    [Header("Basic Data")]
    public CurlingStoneSO stoneDataSO;
    public string title = "Basic Stone";
    public string description = "Just your basic curling stone.";
    public string id = "";
    public Texture avatarImage;
    // private RawImage faceRenderTexture;

    [Header("Ids")]
    [HideInInspector] public int teamId_i;
    [HideInInspector] public string teamId;
    [HideInInspector] public int stoneIndex;


    [Header("Model")]
    public Rigidbody rb; // Rigidbody to apply force / detect motion
    public GameObject visual; // Optional: mesh or model 

    [Header("Special Abilities")]
    [HideInInspector] public bool hasSpecialAbility = false;

    [SerializeField]
    private CurlingStoneParameters _Parameters;
    public CurlingStoneParameters Parameters => _Parameters;

    [Header("FX")]
    public GameObject fxArea;
    public MMF_Player slidingSoundFx;
    public GameObject sweepLeftFx;
    public GameObject sweepRightFx;
    public GameObject movementFx;

    
    private void Awake()
    {
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        if (stoneDataSO != null)
        {
            title = stoneDataSO.Name;
            description = stoneDataSO.Description;
        }
    }

    public void ResetStone(Vector3 position, Quaternion rotation)
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = position;
        transform.rotation = rotation;
    }

    /************************************************************************************************************************/

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
        rb.AddForce(
            launchDirection * launchForce * launchForceMultiplier, 
            ForceMode.Impulse
        );
        Debug.Log("[Stone Throw] 🚀 Stone launched with power: " + launchForceMultiplier);

        // Add initial spin to the stone
        if (initialSpinDirection != 0f)
        {
            rb.angularVelocity = Vector3.up * initialSpinDirection * initialSpinStrength; // Add angular velocity for curling effect (purely visual spin)
            Debug.Log($"[Stone Throw] 🌀 Initial spin applied: angularVelocity = {rb.angularVelocity}");
        }
        if (movementFx != null) movementFx.SetActive(true);
        if (slidingSoundFx != null) {
            slidingSoundFx.gameObject.SetActive(true);
            slidingSoundFx.PlayFeedbacks();
        }
    
        Parameters.Status.IsThrown = true;
        Parameters.Status.IsInPlay = true;
        Parameters.Status.IsSliding = true;
        
    }

    /************************************************************************************************************************/

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
        Parameters.Movement.SpinSpeedTarget -= Parameters.Movement.SpinSpeedStep;
        Parameters.Movement.SpinSpeedTarget = Mathf.Clamp(
            Parameters.Movement.SpinSpeedTarget, 
            -Parameters.Movement.SpinSpeedTargetMax, 
            Parameters.Movement.SpinSpeedTargetMax
        );
        HandleSpinStop();
    }

    public void HandleRightSweepSpin()
    {
        Parameters.Movement.SpinSpeedTarget +=  Parameters.Movement.SpinSpeedStep;
        Parameters.Movement.SpinSpeedTarget = Mathf.Clamp(
            Parameters.Movement.SpinSpeedTarget, 
            -Parameters.Movement.SpinSpeedTargetMax, 
            Parameters.Movement.SpinSpeedTargetMax
        );
        HandleSpinStop();
    }

    public void HandleSpinStop()
    {
        if (Parameters.Movement.SpinSpeedTarget > 0f && 
            Parameters.Movement.SpinSpeedTarget < Parameters.Movement.SpinSpeedStep)
        {
            Parameters.Movement.SpinSpeedTarget = 0f; // Prevent negative spin speed
        }
        if (Parameters.Movement.SpinSpeedTarget < 0f && 
            Parameters.Movement.SpinSpeedTarget > -Parameters.Movement.SpinSpeedStep)
        {
            Parameters.Movement.SpinSpeedTarget = 0f; // Prevent negative spin speed
        }
    }

    // not currently used, since this affects movement as well.
    // it was intended to provide better visual feedback only.
    public void HandleVisualSpin()
    {
        float appliedSpinSpeed = Parameters.Movement.SpinSpeedTarget;
        if (Parameters.Movement.SpinSpeedCurrent > Parameters.Movement.MaxTorque)
        {
            // float spinDiff = spinSpeedCurrent - maxTorque;
            appliedSpinSpeed = 0;// - spinDiff;
            Debug.Log($"[Stone Spin] 🌀 Spin speed clamped: {Parameters.Movement.SpinSpeedCurrent} -> {appliedSpinSpeed}");
        }
        float torqueMagnitude = 1f * appliedSpinSpeed; // Adjust this value to control the strength of the spin
        rb.AddRelativeTorque(transform.up * torqueMagnitude, ForceMode.Acceleration);
        LogCurrentSpinForce();

    }

    private void LogCurrentSpinForce()
    {
        Parameters.Movement.SpinSpeedCurrent = rb.angularVelocity.magnitude;
        Mathf.Clamp(Parameters.Movement.SpinSpeedCurrent, -10f, 10f); // Clamp the spin speed to a reasonable rang
    }

    /************************************************************************************************************************/

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
        rb.AddForce(
            forward * sweepBoostFactor * sweepBoostAmount, 
            ForceMode.Acceleration
        );
        
        // ** Modify curl direction slightly based on sweeping ** NEW SWEPER CODE
        if (isSweepingLeft && !isSweepingRight)
        {
            // Debug.Log("Left Sweep Only Applied");
            // applies reduced force in the left direction
            if (sweepLeftFx != null) sweepLeftFx.SetActive(true);
            if (sweepRightFx != null) sweepRightFx.SetActive(false);
            rb.AddForce(
                -side * sweepStrength * 0.75f, 
                ForceMode.Acceleration
            ); // changed scaling from 0.2 to 0.75 to increase sweeping impact
        }

        else if (!isSweepingLeft && isSweepingRight)
        {
            if (sweepLeftFx != null) sweepLeftFx.SetActive(false);
            if (sweepRightFx != null) sweepRightFx.SetActive(true);
            // Debug.Log("Right Sweep Only Applied");
            // applies reduced force in the right direction
            rb.AddForce(
                side * sweepStrength * 0.75f, 
                ForceMode.Acceleration
            ); // changed scaling from 0.2 to 0.75 to increase sweeping impact
        }
    }

    /************************************************************************************************************************/

    public void UpdateDistanceFromTarget(GameObject targetZone)
    {
        Parameters.Movement.DistanceFromTarget = Vector3.Distance(
            transform.position, 
            targetZone.transform.position
        );
    }

    /************************************************************************************************************************/

    public void UpdateMovementStatus()
    {
        
    }

    public bool IsStoneMoving()
    {
        float threshold = 0.2f;

        // Debug.Log($"[Stone Velocity] {rb.linearVelocity.magnitude}");
        if (rb.linearVelocity == Vector3.zero)
        {
            Debug.Log("Velocity is zero (direct comparison)");
            return false;
            // Do something when velocity is zero
        }

        // Method 2: Checking the magnitude
        if (rb.linearVelocity.magnitude < threshold)
        {
            Debug.Log("Velocity is near zero (magnitude)");
            return false;
            // Do something when velocity is near zero
        }

        // Method 3: Checking the square of the magnitude (slightly faster than magnitude)
        if (rb.linearVelocity.sqrMagnitude < threshold * threshold)
        {
            Debug.Log("Velocity is near zero (squared magnitude)");
            return false;
            // Do something when velocity is near zero
        }

        // Method 4: Using IsSleeping() (for more reliable check if object is at rest)
        if (rb.IsSleeping())
        {
            Debug.Log("Rigidbody is sleeping (at rest)");
            return false;
            // Do something when the rigidbody is sleeping
        }

        return true;
    }

    public bool IsStoneMovingTowardsTarget(GameObject targetZone)
    {
        Vector3 toTarget = (targetZone.transform.position - transform.position).normalized;
        float approachDot = Vector3.Dot(rb.linearVelocity.normalized, toTarget);

        return approachDot > 0.5f; // Adjust threshold as needed
    }

    public bool IsStationary => rb != null && rb.linearVelocity.sqrMagnitude < 0.01f && rb.angularVelocity.sqrMagnitude < 0.01f;

    
    
}