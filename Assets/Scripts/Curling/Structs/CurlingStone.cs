using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;


public class CurlingStone : MonoBehaviour
{
    [Header("Basic Data")]
    public string title = "Basic Stone";
    public string description = "Just your basic curling stone.";
    public Image avatarImage;

    [Header("Ids")]
    [HideInInspector] public int teamId_i;
    [HideInInspector] public string teamId;
    [HideInInspector] public int stoneIndex;

    [Header("Status")]
    [HideInInspector] public bool isThrown = false;
    [HideInInspector] public bool isInPlay = false;
    [HideInInspector] public bool isSliding = false;

    [Header("Stats")]
    [Header("Spin")]
    [HideInInspector] public float curlAmountCurrent = 0f;
    [HideInInspector] public float curlAmountInitial = 0f;
    [HideInInspector] public float spinAmountInitial = 0f;
    [HideInInspector] public float spinAmountCurrent = 0f;

    // [Header("Launch Force")]
    // [HideInInspector] public float launchForce = 0f;

    // [Header("Launch Force")]
    // [HideInInspector] public float primaryColor = 0f;

    [Header("Model")]
    public Rigidbody rb; // Rigidbody to apply force / detect motion
    public GameObject visual; // Optional: mesh or model 
    // 
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

    public bool IsStationary => rb != null && rb.linearVelocity.sqrMagnitude < 0.01f && rb.angularVelocity.sqrMagnitude < 0.01f;

    
    
    public void LaunchStone(
        float launchForceMultiplier, // power
        float launchForce, // default power
        Vector3 launchDirection
    )
    {        
        // Fire the stone
        rb.AddForce(launchDirection * launchForce * launchForceMultiplier, ForceMode.Impulse);
        Debug.Log("[Stone Throw] 🚀 Stone launched with power: " + launchForceMultiplier);

        // Add initial spin to the stone
        // float curlAmountInitial = CurlingGameManagerV2.Instance.aimController.curlAmountInitial;
        // rb.angularVelocity = Vector3.up * curlAmountInitial * spinStrength; // Add angular velocity for curling effect (purely visual spin)
        // Debug.Log($"[Stone Throw] 🌀 Curl applied: angularVelocity = {cs.angularVelocity}");

        isThrown = true;
        isInPlay = true;
        isSliding = true;
    }

    public void ApplySpinForceToStone(
        float spinAmount,
        float sweepStrength
    )
    {
        // Rigidbody rb = CurlingGameManagerV2.Instance.stoneManager.currentStone.rb; // currentStone
        Vector3 forward = rb.linearVelocity.normalized;
        Vector3 side = Vector3.Cross(Vector3.up, forward).normalized;
        rb.AddForce(side * spinAmount * 0.33f * sweepStrength, ForceMode.Acceleration); // Added "* 0.33f" so the default curved trajectory is more mild (might need to make even more mild)
        // 🧪 Debug: draw movement and curl direction
        // Debug.DrawRay(currentStone.position, forward * 2f, Color.green);  // forward
        // Debug.DrawRay(currentStone.position, side * 2f, Color.red);       // curl direction
        // Debug.Log("Drawing curl debug rays!"); // FLAG: THIS IS NOT TRIGGERING SO CLEARLY SOMETHING IS WRONG
    }
    
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
        rb.AddForce(forward * sweepBoostFactor * sweepBoostAmount, ForceMode.Acceleration);

        // ** Modify curl direction slightly based on sweeping ** NEW SWEPER CODE
        if (isSweepingLeft && !isSweepingRight)
        {
            rb.AddForce(-side * sweepStrength * 0.75f, ForceMode.Acceleration); // changed scaling from 0.2 to 0.75 to increase sweeping impact
        }

        else if (isSweepingRight && !isSweepingLeft)
        {
            rb.AddForce(side * sweepStrength * 0.75f, ForceMode.Acceleration); // changed scaling from 0.2 to 0.75 to increase sweeping impact
        }

        else
        {
            sweepBoostFactor = Mathf.Clamp01(sweepBoostFactor - Time.fixedDeltaTime * sweepDecayRate);
        }
    }
}