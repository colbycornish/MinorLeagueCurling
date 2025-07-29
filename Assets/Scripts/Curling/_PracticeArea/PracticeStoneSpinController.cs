using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages inputs for the Curling game.
/// It's unclear if inputs should be handled here, or at a lower level.
/// 
/// TODO: Research Input handling in Unity and decide if this is the right place.
/// </summary>


public class PracticeStoneSpinController : MonoBehaviour
{
    public KeyCode sweepLeftKey = KeyCode.K;
    public KeyCode sweepRightKey = KeyCode.L;
    public CurlingStone currentStone;
    public float currentSpinSpeed = 0f;
    public float targetSpinSpeed = 0f;
    public float spinSpeedStep = 0.2f;
    public float maxTargetSpinSpeed = 3f;
    public float maxTorque = 10f;
    public bool isEnabled = true;


    private void Update()
    {
        if (!isEnabled || currentStone == null) return;
        if (Input.GetKeyDown(sweepLeftKey))
        {
            currentStone.HandleLeftSweepSpin();
        }
        if (Input.GetKeyDown(sweepRightKey))
        {
            // HandleRightSweep();
            currentStone.HandleRightSweepSpin();
        }
        
    }

    void FixedUpdate()
    {
        if (!isEnabled || currentStone == null) return;
        currentStone.HandleVisualSpin();
        
    }

    private void HandleVelocity()
    {
        
    }

    private void HandleLeftSweep()
    {
        if (maxTargetSpinSpeed > targetSpinSpeed)
        {
            targetSpinSpeed -= spinSpeedStep;
        }
        HandleSweepStop();
    }

    private void HandleRightSweep()
    {
        if (-maxTargetSpinSpeed < targetSpinSpeed)
        {
            targetSpinSpeed += spinSpeedStep; 
        }
        HandleSweepStop();
    }

    public void HandleSweepStop()
    {
        if (targetSpinSpeed > 0f && targetSpinSpeed < spinSpeedStep)
        {
            targetSpinSpeed = 0f; // Prevent negative spin speed
        }
        if (targetSpinSpeed < 0f && targetSpinSpeed > -spinSpeedStep)
        {
            targetSpinSpeed = 0f; // Prevent negative spin speed
        }
    }

    private void HandleVisualSpin()
    {
        // Rigidbody rb = currentStone.GetComponent<Rigidbody>();
        float appliedSpinSpeed = targetSpinSpeed;
        if (currentSpinSpeed > maxTorque)
        {
            appliedSpinSpeed = 0;   
        }
        float torqueMagnitude = 1f * appliedSpinSpeed; // Adjust this value to control the strength of the spin
        currentStone.rb.AddRelativeTorque(transform.up * torqueMagnitude, ForceMode.Acceleration);
        LogCurrentSpinForce();

    }

    private void LogCurrentSpinForce()
    {
        currentSpinSpeed = currentStone.rb.angularVelocity.magnitude;
        Mathf.Clamp(currentSpinSpeed, -10f, 10f); // Clamp the spin speed to a reasonable range

        // rigidbody.velocity.normalized;

        // myTransform.forward = Vector3.Slerp(transform.forward, rigidbody.velocity.normalized, gravityRotate * Time.deltaTime);

    }

     


}
