using UnityEngine;
using System.Collections.Generic; // Loops in generic collections in C#; I need it for the "List" bit below

public class StoneThrowController : MonoBehaviour
{

    [Header("Launch Setup")]
    public Transform launchPoint;
    
    [Header("Stone References")] // Manual list of stones for now - the Unity Inspector is weird with lists
    public Rigidbody stone1;
    public Rigidbody stone2;
    public Rigidbody stone3;
    public Rigidbody stone4;
    public Rigidbody stone5;

    [Header("Other References")]
    public Transform directionPivot;         // The pivot (arrow) showing throw direction
    public PowerMeterUI powerMeter;          // Assign PowerMeterUI script in Inspector
    public GameObject powerMeterPromptUI;

    [Header("Launch Settings")]
    public float launchForce = 75f;        // Base launch force (tweak as needed; adjust for distance)
    public float curlStrength = 5f;     // Tweak for how much spin affects trajectory (side force applied during slide)

    [Header("Sweeper Settings")]
    public float sweepBoostAmount = 1.5f; // how strong the speed boost is ** NEW SWEEPER CODE **
    public float sweepDecayRate = 2f;     // how fast the boost fades when not sweeping ** NEW SWEEPER CODE **

    [Header("Input Keys")]
    public KeyCode resetKey = KeyCode.R;
    public KeyCode rightCurlKey = KeyCode.E;
    public KeyCode leftCurlKey = KeyCode.Q;
    public KeyCode rightSweeperKey = KeyCode.L; // Action button for right sweeper sweeping ** NEW SWEEPER CODE **
    public KeyCode leftSweeperKey = KeyCode.K; // Action button for left sweeper sweeping ** NEW SWEEPER CODE **
    public KeyCode actionKey = KeyCode.Space; // This is the key used to activate the power meter and launch the stone
    // private Vector3 initialStonePosition; // used for resetting throw NOTE NOW THAT WE ARE THROWING MULTIPLE STONES I DON'T THINK I NEED THIS
    // private Quaternion initialStoneRotation; // used for resetting throw NOTE NOW THAT WE ARE THROWING MULTIPLE STONES I DON'T THINK I NEED THIS


    // State
    private int currentStoneIndex = 0;
    private Rigidbody currentStone;
    private bool isCharging = false;
    private bool hasLaunched = false;
    private bool isSliding = false;
    public float curlAmount = 0f;       // -1 = left curl, 0 = no curl, 1 = right curl

    // Sweeper State ** NEW SWEEPER CODE **
    private bool isSweepingLeft = false; // ** NEW SWEEPER CODE **
    private bool isSweepingRight = false; // ** NEW SWEEPER CODE **
    private float sweepBoostFactor = 0f; // ** NEW SWEEPER CODE **

    private List<Rigidbody> stones = new List<Rigidbody>(); // Empty list for stones
    void Start() // I think this tracks the starting location of the stone, to be used when resetting throw?
    {
        
        // Manually add stones to the list in order
        stones.Add(stone1);
        stones.Add(stone2);
        stones.Add(stone3);
        stones.Add(stone4);
        stones.Add(stone5);

        if (stones.Count > 0)
        {
            currentStone = stones[currentStoneIndex];
            PrepareCurrentStone(); // move stone to launch position at start (I'm using the same exact launch position, at least for now)
        }

        else
        {
            Debug.LogError("No stones assigned to the Throw Controller!");
        }
    }

    void Update()
    {
        // Always allow manual reset; reset the throw at any time by pressing 'R' NOTE: Code has been updated; R now advances to the next stone instead of resetting
        if (Input.GetKeyDown(resetKey)) 
        {
            ResetThrow();
            return;
        }

        if (hasLaunched){

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
        if (!hasLaunched && !isCharging)
        {   
            if (Input.GetKeyDown(leftCurlKey)) 
            {   
                curlAmount = -0.5f;
                Debug.Log("⤵️ Left curl selected");
            }

            if (Input.GetKeyDown(rightCurlKey)) 
            {
                curlAmount = 1f;
                Debug.Log("⤴️ Right curl selected");
            }
        }

        // Step 1: Press Space to activate power meter
        if (!isCharging && Input.GetKeyDown(actionKey))
        {
            ActivatePowerMeter();
        }

        // Step 2: Press Space again to select power and launch
        else if (isCharging && Input.GetKeyDown(actionKey))
        {
            LaunchPowerSelected();
        }

    }

    void FixedUpdate()
    {
        if (isSliding && currentStone != null) // The following line used to be in immediately after != null and I think it was throwing things off: && Mathf.Abs(curlAmount) > 0.01f
        {
            // Only apply curl if stone is still moving
            //if (stoneRb.linearVelocity.magnitude < 0.05f) // Note the default here is 0.2f; tweaking just for testing purposes
            //{
            //    isSliding = false;
            //    return;
            //}

            Vector3 forward = currentStone.linearVelocity.normalized;
            Vector3 side = Vector3.Cross(Vector3.up, forward).normalized;
            currentStone.AddForce(side * curlAmount * curlStrength, ForceMode.Acceleration);

            // ** Apply sweeping boost ** NEW SWEEPER CODE
            bool sweeping = isSweepingLeft || isSweepingRight;
            if (sweeping)
            {
                sweepBoostFactor = Mathf.Clamp01(sweepBoostFactor + Time.fixedDeltaTime * sweepDecayRate);
                currentStone.AddForce(forward * sweepBoostFactor * sweepBoostAmount, ForceMode.Acceleration);

                // ** Modify curl direction slightly based on sweeping ** NEW SWEPER CODE
                if (isSweepingLeft && !isSweepingRight)
                {
                    currentStone.AddForce(-side * curlStrength * 0.2f, ForceMode.Acceleration);
                }

                else if (isSweepingRight && !isSweepingLeft)
                {
                    currentStone.AddForce(side * curlStrength * 0.2f, ForceMode.Acceleration);
                }

                else
                {
                    sweepBoostFactor = Mathf.Clamp01(sweepBoostFactor - Time.fixedDeltaTime * sweepDecayRate);
                }
            }

            // Debug lines to track that sweeping is working correctly
            if (isSweepingLeft) Debug.Log("🧹 Sweeping LEFT (K key)");
            if (isSweepingRight) Debug.Log("🧹 Sweeping RIGHT (L key)");
            if (!isSweepingLeft && !isSweepingRight) Debug.Log("🧊 No sweeping input");
            // Debug lines to track various values
            //Debug.Log("Side Force = " + side); // Commenting out for now
            //Debug.Log("Curl Amount = " + curlAmount); // Commenting out for now
            //Debug.Log("Curl Strength = " + curlStrength); // Commenting out for now

            // 🧪 Debug: draw movement and curl direction
            Debug.DrawRay(currentStone.position, forward * 2f, Color.green);  // forward
            Debug.DrawRay(currentStone.position, side * 2f, Color.red);       // curl direction
            Debug.Log("Drawing curl debug rays!"); // FLAG: THIS IS NOT TRIGGERING SO CLEARLY SOMETHING IS WRONG
        }
    }

    void ActivatePowerMeter(){
        powerMeter.Activate();
        isCharging = true;
        Debug.Log("Power meter activated");
    }

    
    void LaunchPowerSelected(){
        powerMeter.SelectPower();  // locks the power level
        float power = powerMeter.GetPower();  // get selected power
        LaunchStone(power);
        hasLaunched = true;
        isSliding = true;

        if (powerMeterPromptUI != null){
            powerMeterPromptUI.SetActive(false);
        }
    }

    void LaunchStone(float power)
    {
        Vector3 launchDirection = directionPivot.forward;
        currentStone.AddForce(launchDirection * launchForce * power, ForceMode.Impulse);
        currentStone.angularVelocity = Vector3.up * curlAmount * curlStrength; // Add angular velocity for curling effect (purely visual spin)
        Debug.Log($"🌀 Curl applied: angularVelocity = {currentStone.angularVelocity}");
        Debug.Log("🚀 Stone launched with power: " + power);
    }



    void ResetThrow()
    {
        if (currentStone != null)
        {
            currentStone.linearVelocity = Vector3.zero;
            currentStone.angularVelocity = Vector3.zero;
        }

        isCharging = false;
        hasLaunched = false;
        isSliding = false;
        curlAmount = 0f;

        powerMeter.ResetMeter();

        currentStoneIndex++;

        if (currentStoneIndex < stones.Count)
        {
            currentStone = stones[currentStoneIndex];
            PrepareCurrentStone();
            Debug.Log("🔁 Next stone ready: " + currentStone.name);

            if (powerMeterPromptUI != null)
            {
                powerMeterPromptUI.SetActive(true);
            }
        } 

        else
        {
            Debug.Log("✅ All stones thrown!");
            currentStone = null;
        }

    }

    void PrepareCurrentStone()
    {
        if (launchPoint == null || currentStone == null) return;

        currentStone.transform.position = launchPoint.position;
        currentStone.transform.rotation = launchPoint.rotation;
        currentStone.linearVelocity = Vector3.zero;
        currentStone.angularVelocity = Vector3.zero;
    }
        
}

