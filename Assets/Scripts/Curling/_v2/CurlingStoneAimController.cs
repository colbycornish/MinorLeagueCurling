using System.Collections.Generic;
using UnityEngine;
using System;


/// Only active during the curling phase
public class CurlingStoneAimController : MonoBehaviour
{
    [Header("Aim Settings")]
    public float curlStrength = 5f;     // Tweak for how much spin affects trajectory (side force applied during slide)
    public float curlAmountInitial = 0f;       // -1 = left curl, 0 = no curl, 1 = right curl
                                               // State
    public GameObject directionPivotObject;
    public Transform directionPivot; // assign this in Inspector
    public float rotationSpeed = 100f;
    public float directionalLimit = 30f;
    public bool isActive = false;

    [Header("Input Keys")]
    public KeyCode resetKey = KeyCode.R;
    public KeyCode rightCurlKey = KeyCode.E;
    public KeyCode leftCurlKey = KeyCode.Q;
    public KeyCode rightAimKey = KeyCode.A;
    public KeyCode leftAimKey = KeyCode.D;


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

        if (currentPhase == CurlingMatchPhase.StoneSelection){ Reset(); }
        else if (currentPhase == CurlingMatchPhase.CurlingAimControlsPhase)
        {
            Enable();
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reset();
            }
        }
        else { Disable(); }
    }

    /// <summary>
    /// Update!
    /// </summary>


    void Update()
    {
        CurlingMatchPhase currentPhase = CurlingMatchPhaseManager.Instance.CurrentPhase;
        // Handle spin input before charging
        if (currentPhase == CurlingMatchPhase.CurlingAimControlsPhase)
        {
            if (Input.GetKeyDown(leftCurlKey))
            {
                ApplyIntialCurlAmount(-1f);
            }

            if (Input.GetKeyDown(rightCurlKey))
            {
                ApplyIntialCurlAmount(1f);
            }
            ChangeDirection();
        }
    }
    
    void FixedUpdate()
    {
        CurlingStone currentStone = CurlingGameManagerV2.Instance.stoneManager.currentStone;
    }

    /// <summary>
    /// Curl amount (Spin Amount): This governs how much initial spin is applied to the stone
    /// </summary>

    public void ApplyIntialCurlAmount(float amount)
    {
        CurlingStone currentStone = CurlingGameManagerV2.Instance.stoneManager.currentStone;
        curlAmountInitial = amount;
        currentStone.curlAmountInitial = amount;
    }

    
    /// <summary>
    /// Curl amount (Spin Amount): This governs how much initial spin is applied to the stone
    /// </summary>

    public void ApplySpinForceToStone()
    {
        Rigidbody cs = CurlingGameManagerV2.Instance.stoneManager.currentStone.rb; // currentStone
    }


    /// <summary>
    /// Launch Direction: Change the direction in which the stone is initially thrown
    /// </summary>
    public void ChangeDirection()
    {
        // Get the rotation direction from the input
        float input = Input.GetAxis("Horizontal"); // A/D or Left/Right arrows

        if (input != 0)
        {
            // Debug.Log($"Direction Rotation {input}");
            float rotationAmount = input * rotationSpeed * Time.deltaTime;

            // Debug.Log($"rotation amount {rotationAmount}");
            bool canRotate = CanUpdateDirection(rotationAmount);
            if (canRotate)
            {
                directionPivot.Rotate(0f, rotationAmount, 0f);
            }
            // CurlingStone currentStone = CurlingGameManagerV2.Instance.stoneManager.currentStone;
        }
    }


    public bool CanUpdateDirection(float rotationAmount)
    {
        float currentY = directionPivot.eulerAngles.y;
        float nextY = currentY + rotationAmount;
        // Rotation is 360 degrees. The arrow starts at zero. 
        // We're allowing for 30 degrees to the left, and 30 degrees to the right.

        // Left bound: The launch direction can only move [directionalLimit] to the left
        float leftLimit = 360f - directionalLimit;
        // Right bound:  The launch direction can only move [directionalLimit] to the left
        float rightLimit = 0 + directionalLimit;

        // Test that the next position is within the range
        // [leftBound....359, 360/0, 1....rightBound]
        if (nextY > leftLimit || nextY < rightLimit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }



    /// <summary>
    /// Setup / Reset
    /// </summary>
    /// 
    public void Setup(CurlingCourseData courseData)
    {
        directionPivotObject = courseData.directionalPivot;
        directionPivot = directionPivotObject.transform;
    }
    

    public void Enable()
    {
        directionPivotObject.SetActive(true);
    }

    public void Disable()
    {
        directionPivotObject.SetActive(false);
    }

    public void Reset()
    {

        // hasLaunched = false;
        // isSliding = false;
        // directionPivot.Rotate(0f, input * rotationSpeed * Time.deltaTime, 0f);
        if (directionPivot != null)
        {
            // directionPivot.transform.rotation(0f, 0f, 0f);
            directionPivot.rotation *= Quaternion.Euler(0, 0, 0);
        }
        curlAmountInitial = 0f;
    }
}


