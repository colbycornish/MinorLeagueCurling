using UnityEngine;
using System.Collections;


interface ICurlingStone
{
    GameObject gameObject { get; set; }
    Rigidbody stoneRb { get; set; }
    int speed { get; set; }
    int maxSpeed { get; set; }

    void AdjustSpeed(int s);
    
    void ChangeDirection(
        Vector3 targetPosition
    );
} 


// CurlingStone "implements" the ICurlingStone interface
public class CurlingStone : ICurlingStone
{
    public GameObject gameObject { get; set; }
    public Rigidbody stoneRb { get; set; }
    public int speed { get; set; }
    public int maxSpeed { get; set; }

    // instantiate the stone object
    public void init(
        GameObject gameObject,
        Rigidbody stoneRb,
        int speed,
        int maxSpeed
    )
    {
        this.gameObject = gameObject;
        this.stoneRb = stoneRb;
        this.speed = speed;
        this.maxSpeed = maxSpeed;
    }

    // adjust the speed of the stone
    public void AdjustSpeed(int speedChange)
    {
        if (speed < maxSpeed){
            speed += speedChange;
        } 
    }

    // Change the target destination of the stone
    public void ChangeDirection(
        Vector3 targetPosition
    )
    {
        Vector3 currentPosition = gameObject.transform.position;
        var step =  speed * Time.deltaTime; // calculate distance to move
        gameObject.transform.position = Vector3.MoveTowards(currentPosition, targetPosition, step);
    }
} 


public class CurlingStoneController : MonoBehaviour
{

    private CurlingStone stone = new CurlingStone();
    public GameObject playerGroup; 
        
    //Set this value in the inspector
    public Vector3 targetPosition;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stone.init(
            gameObject: gameObject,
            stoneRb: transform.GetComponent<Rigidbody>(),
            speed: 1,
            maxSpeed: 5
        );
    }


    void Update()
    {
        targetPosition = playerGroup.transform.position;
        stone.ChangeDirection(targetPosition: targetPosition);
    }
}




// void OnTriggerEnter(Collider other) 
//    {

//         /*
//             if the sweeper encounters one of the walled snow edges, 
//             the sweeper who runs into it should jump over the obstacle,
//             and be prevented from moving much futher beyond it
//         */
        
//        if (other.gameObject.CompareTag("wall_curling_snow")) 
//        {
//             // The stone has hit a wall, and it should slow down
//             // AdjustSpeed();
//             // AdjustAngle();
            
//        }
//        else if (other.gameObject.CompareTag("curling_landing_area")) 
//        {
//             // The stone has entered the landing area
//             // AdjustSpeed();
            
            
//        }
//        else if (other.gameObject.CompareTag("curling_obstacle_oil")) 
//        {
//             // The stone has encountered an oil spill
//             // AdjustSpeed();
//             // AdjustAngle();
            
//        }
//        else if (other.gameObject.CompareTag("curling_obstacle_trash")) 
//        {
//             // The stone has encountered a trash obstacle

//             // AdjustSpeed();
//             // AdjustAngle();
            
//        }
//        else if (other.gameObject.CompareTag("curling_obstacle_stone")) 
//        {
//             // The stone has encountered a stone obstacle
            
//        }
//        else if (other.gameObject.CompareTag("curling_obstacle_ice")) 
//        {
//             // The stone has encountered an ice obstacle
            
//        }
//        else if (other.gameObject.CompareTag("curling_obstacle_snow")) 
//        {
//             // The stone has encountered a snow obstacle
            
//        }
//        else if (other.gameObject.CompareTag("curling_obstacle_trash"))
//        {
//             // The stone has encountered a trash obstacle
            
//        }
//        else { 

//        }
   
//    }