using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class CurlingStoneSweepController : MonoBehaviour
{
    // public Action<GameObject> onRestCallback;
    // // private Rigidbody rb;
    // private bool hasBeenThrown = false;
    // private bool hasReportedRest = false;

    [Header("Launch Settings")]
    public float curlStrength = 5f;     // Tweak for how much spin affects trajectory (side force applied during slide)

    [Header("Sweeper Settings")]
    public float sweepBoostAmount = 1.5f; // how strong the speed boost is ** NEW SWEEPER CODE **
    public float sweepDecayRate = 2f;

    // State
    private Rigidbody currentStone;
    // private bool hasLaunched = false;
    // private bool isSliding = false;
    public float curlAmount = 0f;       // -1 = left curl, 0 = no curl, 1 = right curl

    // Sweeper State ** NEW SWEEPER CODE **
    private bool isSweepingLeft = false; // ** NEW SWEEPER CODE **
    private bool isSweepingRight = false; // ** NEW SWEEPER CODE **
    private float sweepBoostFactor = 0f; // ** NEW SWEEPER CODE **

    [Header("Input Keys")]
    public KeyCode resetKey = KeyCode.R;
    public KeyCode rightCurlKey = KeyCode.E;
    public KeyCode leftCurlKey = KeyCode.Q;
    public KeyCode rightSweeperKey = KeyCode.L; // Action button for right sweeper sweeping ** NEW SWEEPER CODE **
    public KeyCode leftSweeperKey = KeyCode.K; // Action button for left sweeper sweeping ** NEW SWEEPER CODE **
    public KeyCode actionKey = KeyCode.Space; // This is the key used to activate the power meter and launch the stone


    private void Awake()
    {
        // rb = GetComponent<Rigidbody>();
    }

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

        }

        if (currentPhase == CurlingMatchPhase.CurlingStoneSweepingPhase)
        {
            // hasLaunched = true;
            // isSliding = true;
        }
        if (currentPhase != CurlingMatchPhase.CurlingStoneSweepingPhase)
        {
            // hasLaunched = false;
            // isSliding = false;
        }
        
        
        
    }


    void Update()
    {
        CurlingMatchPhase currentPhase = CurlingMatchPhaseManager.Instance.CurrentPhase;
        if (currentPhase == CurlingMatchPhase.CurlingStoneSweepingPhase)
        {
            // Track sweeping input ** NEW SWEEPER CODE **
            isSweepingLeft = Input.GetKey(leftSweeperKey); // ** NEW SWEEPER CODE **
            isSweepingRight = Input.GetKey(rightSweeperKey); // ** NEW SWEEPER CODE **

            // 🔍 Debug logs: Is Unity registering these keys?
            if (Input.GetKeyDown(leftSweeperKey))
            {
                Debug.Log("✔️ K key pressed");
            }
            if (Input.GetKeyDown(rightSweeperKey))
            {
                Debug.Log("✔️ L key pressed");
            }
            return;
        }

        // Handle spin input before charging
        if (currentPhase == CurlingMatchPhase.CurlingAimControlsPhase)
        {
            if (Input.GetKeyDown(leftCurlKey))
            {
                curlAmount = -1f;
                Debug.Log("⤵️ Left curl selected");
            }

            if (Input.GetKeyDown(rightCurlKey))
            {
                curlAmount = 1f;
                Debug.Log("⤴️ Right curl selected");
            }
        }
        



    }

    void FixedUpdate()
    {
        CurlingStone currentStone = CurlingGameManagerV2.Instance.stoneManager.currentStone;

        // TODO: Move to the match flow
        if (currentStone.isSliding && currentStone.rb != null) // The following line used to be in immediately after != null and I think it was throwing things off: && Mathf.Abs(curlAmount) > 0.01f
        {
            // todo: move to match flow
            if (currentStone.rb != null && currentStone.rb.linearVelocity.sqrMagnitude < 0.01f && currentStone.rb.angularVelocity.sqrMagnitude < 0.01f)
            {
                currentStone.isSliding = false;
                CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.PostThrowResult);
            }
            // Only apply curl if stone is still moving
            //if (stoneRb.linearVelocity.magnitude < 0.05f) // Note the default here is 0.2f; tweaking just for testing purposes
            //{
            //    isSliding = false;
            //    return;
            //}

            ApplySpinForceToStone();

            // ** Apply sweeping boost ** NEW SWEEPER CODE
            bool sweeping = isSweepingLeft || isSweepingRight;
            if (sweeping)
            {
                ApplySweepingImpactToStone();
            }

            // Debug lines to track that sweeping is working correctly
            if (isSweepingLeft) Debug.Log("🧹 Sweeping LEFT (K key)");
            if (isSweepingRight) Debug.Log("🧹 Sweeping RIGHT (L key)");
        }
    }


    public void ApplySpinForceToStone()
    {
        Rigidbody cs = CurlingGameManagerV2.Instance.stoneManager.currentStone.rb; // currentStone
        Vector3 forward = cs.linearVelocity.normalized;
        Vector3 side = Vector3.Cross(Vector3.up, forward).normalized;
        cs.AddForce(side * curlAmount * 0.33f * curlStrength, ForceMode.Acceleration); // Added "* 0.33f" so the default curved trajectory is more mild (might need to make even more mild)
        // 🧪 Debug: draw movement and curl direction
        // Debug.DrawRay(currentStone.position, forward * 2f, Color.green);  // forward
        // Debug.DrawRay(currentStone.position, side * 2f, Color.red);       // curl direction
        // Debug.Log("Drawing curl debug rays!"); // FLAG: THIS IS NOT TRIGGERING SO CLEARLY SOMETHING IS WRONG
    }

    public void ApplySweepingImpactToStone()
    {
        Rigidbody cs = CurlingGameManagerV2.Instance.stoneManager.currentStone.rb; // currentStone
        Vector3 forward = cs.linearVelocity.normalized;
        Vector3 side = Vector3.Cross(Vector3.up, forward).normalized;
        sweepBoostFactor = Mathf.Clamp01(sweepBoostFactor + Time.fixedDeltaTime * sweepDecayRate);
        cs.AddForce(forward * sweepBoostFactor * sweepBoostAmount, ForceMode.Acceleration);

        // ** Modify curl direction slightly based on sweeping ** NEW SWEPER CODE
        if (isSweepingLeft && !isSweepingRight)
        {
            cs.AddForce(-side * curlStrength * 0.75f, ForceMode.Acceleration); // changed scaling from 0.2 to 0.75 to increase sweeping impact
        }

        else if (isSweepingRight && !isSweepingLeft)
        {
            cs.AddForce(side * curlStrength * 0.75f, ForceMode.Acceleration); // changed scaling from 0.2 to 0.75 to increase sweeping impact
        }

        else
        {
            sweepBoostFactor = Mathf.Clamp01(sweepBoostFactor - Time.fixedDeltaTime * sweepDecayRate);
        }
    }
    
    public void SetCurrentStone(CurlingStone stone)
    {
        // RigidBody cs = CurlingGameManagerV2.Instance.stoneManager.currentStone.rb; // currentStone
        if (currentStone != null)
        {
            currentStone.isKinematic = true; // Disable physics on the previous stone
        }
        currentStone = stone.rb;
    }
  

    public void Reset()
    {
        // hasLaunched = false;
        // isSliding = false;
        curlAmount = 0f;
    }
}


