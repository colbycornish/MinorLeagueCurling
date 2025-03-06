using UnityEngine;


public class CurlingThrowerController : MonoBehaviour
{

    private Rigidbody throwerRb;
    private double currentThrowPower = 0;
    private double currentThrowAngle = 0;

    private bool canThrowStone = false;
    private bool hasThrownStone = false;
    private int numStonesThrown = 0;

    // private double currentThrowAngle = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }


    public void InitiateRockThrowProcess(){
        LaunchRockSelection();
        LaunchRockThrowPowerSelection();
        LaunchRockThrowAngleSelection();
        LaunchRock();

        // Enable the angle meter
        // - wait for input

        // Throw the rock
    }


    public void LaunchRockSelection(){
        // Enable rock selection
        // - wait for input
    }

    public void LaunchRockThrowPowerSelection(){
        // Enable rock selection
        // - wait for input
    }

    public void LaunchRockThrowAngleSelection(){
        // Enable the angle meter
        // - wait for input
        
    }

    public void LaunchRock(){
        // initiate animation
        hasThrownStone = true;
        numStonesThrown = numStonesThrown + 1;
    }

    public void Reset(){
        // initiate animation
        canThrowStone = false;
        hasThrownStone = false;
        currentThrowPower = 0;
        currentThrowAngle = 0;

    }

}
