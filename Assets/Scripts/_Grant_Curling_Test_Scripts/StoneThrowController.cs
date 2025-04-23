using UnityEngine;

public class StoneThrowController : MonoBehaviour
{
    public Rigidbody stoneRb;                      // Assign in Inspector
    public Transform directionPivot;               // Assign the same pivot you're rotating
    public float launchForce = 4000f;                // Adjust as needed
    private bool hasLaunched = false;
    private bool isCharging = false;
    public PowerMeterUI powerMeter;

void Update()
{
    if (!hasLaunched)
    {
        // bool isPowerSelected = powerMeter.IsPowerSelected();
        if (!isCharging && Input.GetKeyDown(KeyCode.Space))
        {
            powerMeter.ResetMeter();
            isCharging = true;
        }
        else if (isCharging  && powerMeter.IsPowerSelected())
        {
            float power = powerMeter.GetPower();
            LaunchStone(power);
            hasLaunched = true;
        }
    }
}

void LaunchStone(float power)
{
    Vector3 launchDirection = directionPivot.forward;
    stoneRb.AddForce(launchDirection * launchForce * power, ForceMode.Impulse);
    Debug.Log("🚀 Stone launched with power: " + power);
}
}

