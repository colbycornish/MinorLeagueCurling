using UnityEngine;

public class StoneThrowController : MonoBehaviour
{
    [Header("References")]
    public Rigidbody stoneRb;                // Assign your stone's Rigidbody
    public Transform directionPivot;         // The pivot (arrow) showing throw direction
    public PowerMeterUI powerMeter;          // Assign PowerMeterUI script in Inspector
    public GameObject powerMeterPromptUI;

    [Header("Launch Settings")]
    public float launchForce = 110f;        // Base launch force (tweak as needed; adjust for distance)
    public float curlStrength = 5f;     // Tweak for how much spin affects trajectory (side force applied during slide)

    [Header("Reset Settings")]
    public KeyCode resetKey = KeyCode.R;
    private Vector3 initialStonePosition; // used for resetting throw
    private Quaternion initialStoneRotation; // used for resetting throw


    // State
    private bool isCharging = false;
    private bool hasLaunched = false;
    private bool isSliding = false;
    public float curlAmount = 0f;       // -1 = left curl, 0 = no curl, 1 = right curl

    void Start() // I think this tracks the starting location of the stone, to be used when resetting throw?
    {
        initialStonePosition = stoneRb.transform.position;
        initialStoneRotation = stoneRb.transform.rotation;
    }

    void Update()
    {
        // Always allow manual reset; reset the throw at any time by pressing 'R'
        if (Input.GetKeyDown(resetKey)) 
        {
            ResetThrow();
            return;
        }

        if (hasLaunched){
            return;
        } 

        // Handle spin input before charging
        if (!hasLaunched && !isCharging)
        {   
            if (Input.GetKeyDown(KeyCode.Q)) 
            {   
                curlAmount = -0.5f;
                Debug.Log("⤵️ Left curl selected");
            }

            if (Input.GetKeyDown(KeyCode.E)) 
            {
                curlAmount = 1f;
                Debug.Log("⤴️ Right curl selected");
            }
        }

        // Step 1: Press Space to activate power meter
        if (!isCharging && Input.GetKeyDown(KeyCode.Space))
        {
            ActivatePowerMeter();
        }

        // Step 2: Press Space again to select power and launch
        else if (isCharging && Input.GetKeyDown(KeyCode.Space))
        {
            LaunchPowerSelected();
        }

    }

    void FixedUpdate()
    {
        if (isSliding && Mathf.Abs(curlAmount) > 0.01f)
        {
            // Only apply curl if stone is still moving
            //if (stoneRb.linearVelocity.magnitude < 0.05f) // Note the default here is 0.2f; tweaking just for testing purposes
            //{
            //    isSliding = false;
            //    return;
            //}

            Vector3 forward = stoneRb.linearVelocity.normalized;
            Vector3 side = Vector3.Cross(Vector3.up, forward).normalized;
            stoneRb.AddForce(side * curlAmount * curlStrength, ForceMode.Acceleration);
            Debug.Log("Side Force = " + side);
            Debug.Log("Curl Amount = " + curlAmount);
            Debug.Log("Curl Strength = " + curlStrength);

            // 🧪 Debug: draw movement and curl direction
            Debug.DrawRay(stoneRb.position, forward * 2f, Color.green);  // forward
            Debug.DrawRay(stoneRb.position, side * 2f, Color.red);       // curl direction
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
        stoneRb.AddForce(launchDirection * launchForce * power, ForceMode.Impulse);
        stoneRb.angularVelocity = Vector3.up * curlAmount * curlStrength; // Add angular velocity for curling effect (purely visual spin)
        Debug.Log($"🌀 Curl applied: angularVelocity = {stoneRb.angularVelocity}");
        Debug.Log("🚀 Stone launched with power: " + power);
    }



    void ResetThrow()
    {
        stoneRb.linearVelocity = Vector3.zero;
        stoneRb.angularVelocity = Vector3.zero;

        stoneRb.transform.position = initialStonePosition;
        stoneRb.transform.rotation = initialStoneRotation;

        powerMeter.ResetMeter();

        isCharging = false;
        hasLaunched = false;
        isSliding = false;
        curlAmount = 0f;

        if (powerMeterPromptUI != null){
            powerMeterPromptUI.SetActive(true);
        }

        Debug.Log("🔁 Stone reset and ready to throw again!");
    }
}
