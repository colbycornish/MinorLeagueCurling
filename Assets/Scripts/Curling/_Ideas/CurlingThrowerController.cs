using UnityEngine;


interface ICurlingThrower
{
    GameObject gameObject { get ; }
    Rigidbody throwerRb { get; set; }
    float power { get; set; }
    float maxPower { get; set; }
    bool canThrowStone {get; set;}
    bool hasThrownStone {get; set;}
    int numStonesThrown {get; set;}
    int strength { get; set; }
    // float linearDamping { get; set; }
    void AdjustPower(int s);
} 


// CurlingStone "implements" the ICurlingStone interface
class CurlingThrower : ICurlingThrower
{
    public GameObject gameObject { get ; }
    public Rigidbody throwerRb { get; set; }
    public float power { get; set; }
    public float maxPower { get; set; }
    public bool canThrowStone {get; set;}
    public bool hasThrownStone {get; set;}
    public int numStonesThrown { get; set; }
    public int strength { get; set; }
    
    // float linearDamping { get; set; }
    public void AdjustPower(int powerChange)
    {
        power = powerChange;
    }
    

} 


public class CurlingThrowerController : MonoBehaviour
{

    private CurlingThrower thrower = new CurlingThrower();
    private Rigidbody throwerRb;
    // private double currentThrowPower = 0;
    // private double currentThrowAngle = 0;

    // private bool canThrowStone = false;
    // private bool hasThrownStone = false;
    // private int numStonesThrown = 0;

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
        // hasThrownStone = true;
        // numStonesThrown = numStonesThrown + 1;
    }

    public void Reset(){
        // initiate animation
        // canThrowStone = false;
        // hasThrownStone = false;
        // currentThrowPower = 0;
        // currentThrowAngle = 0;

    }

}
