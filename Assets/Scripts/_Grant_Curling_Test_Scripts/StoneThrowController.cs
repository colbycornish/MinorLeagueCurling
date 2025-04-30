using UnityEngine;

public class StoneThrowController : MonoBehaviour
{
    public Rigidbody stoneRb;                // Assign your stone's Rigidbody
    public Transform directionPivot;         // The pivot (arrow) showing throw direction
    public float launchForce = 4000f;        // Base launch force (tweak as needed)
    public PowerMeterUI powerMeter;          // Assign PowerMeterUI script in Inspector

    private bool isCharging = false;
    private bool hasLaunched = false;
    private Vector3 initialStonePosition; // used for resetting throw
    private Quaternion initialStoneRotation; // used for resetting throw
    public float curlAmount = 0f;       // -1 = left curl, 0 = no curl, 1 = right curl
    public float curlStrength = 5f;     // Tweak for how much spin affects trajectory

    void Start() // I think this tracks the starting location of the stone, to be used when resetting throw?
    {
        initialStonePosition = stoneRb.transform.position;
        initialStoneRotation = stoneRb.transform.rotation;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R)) // reset the throw at any time by pressing 'R'
        {
            ResetThrow();
        }

        if (hasLaunched) return;

        if (!hasLaunched && !isCharging)
        {   
            if (Input.GetKeyDown(KeyCode.Q)) 
            {   
                curlAmount = -1f;
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
            powerMeter.Activate();
            isCharging = true;
            Debug.Log("Power meter activated");
        }

        // Step 2: Press Space again to select power and launch
        else if (isCharging && Input.GetKeyDown(KeyCode.Space))
        {
            powerMeter.SelectPower();  // locks the power level
            float power = powerMeter.GetPower();  // get selected power
            LaunchStone(power);
            hasLaunched = true;
        }

    }

    void LaunchStone(float power)
    {
        Vector3 launchDirection = directionPivot.forward;
        stoneRb.AddForce(launchDirection * launchForce * power, ForceMode.Impulse);
        stoneRb.angularVelocity = Vector3.up * curlAmount * curlStrength; // Add angular velocity for curling effect
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
        curlAmount = 0f;

    Debug.Log("🔁 Stone reset and ready to throw again!");
    }
}
