using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

interface ICurlingSweeper
{
    GameObject gameObject { get ; set;}
    Rigidbody sweeperRb { get; set; }
    float speed { get; set; }
    float maxSpeed { get; set; }
    bool isLeftSweeper {get; set;}
    bool isRightSweeper {get; set;}
    int strength { get; set; }

    void AdjustSpeed(int s);
    void SetLinearDamping(int ld);
    
} 


// CurlingStone "implements" the ICurlingStone interface
class CurlingSweeper : ICurlingSweeper
{
    public GameObject gameObject { get ; set; }
    public Rigidbody sweeperRb { get; set; }
    public float speed { get; set; }
    public float maxSpeed { get; set; }
    public int strength { get; set; }
    public bool isLeftSweeper {get; set;}
    public bool isRightSweeper {get; set;}
    public int linearDamping { get; set; }
    
    public void init(
        GameObject gameObject,
        Rigidbody sweeperRb,
        float speed,
        float maxSpeed,
        int strength,
        bool isLeftSweeper,
        bool isRightSweeper
    ){
        this.gameObject = gameObject;
        this.sweeperRb = sweeperRb;
        this.speed = speed;
        this.maxSpeed = maxSpeed;
        this.strength = strength;
        this.isLeftSweeper = isLeftSweeper;
        this.isRightSweeper = isRightSweeper;
    }
    
    public void AdjustSpeed(int speedChange)
    {
        speed += speedChange;
    }
    public void SetLinearDamping(int ld)
    {
        sweeperRb.linearDamping = ld;
    }

} 



public class CurlingSweeperController : MonoBehaviour
{
    
}





//     void OnTriggerEnter(
//         Collider other
//     ) {

//         /*
//             if the sweeper encounters one of the walled snow edges, 
//             the sweeper who runs into it should jump over the obstacle,
//             and be prevented from moving much futher beyond it
//         */
        
//        if (other.gameObject.CompareTag("wall_curling_snow")) 
//        {
//             // if moving left (this is the left wall)
//             if (movementX < 0){
//                 // check that the sweeper can still move left.
//                 // if they can, the sweeper should jump.
//                 if (canMoveLeft == true && canMoveRight == true){
//                     canMoveRight = true;
//                     canMoveLeft = false;
//                     triggerJump();  
//                 }
//                 // if they cannot, continue the current state
//                 if (canMoveLeft == false && canMoveRight == true){
//                     canMoveRight = true;
//                     canMoveLeft = false;
//                 }
//             }
//             // if moving right (this is the right wall)
//             else if (movementX > 0){
//                 // check that the sweeper can still move right.
//                 // if they can, the sweeper should jump.
//                 if (canMoveRight == true){
//                     canMoveLeft = true;
//                     canMoveRight = false;
//                     triggerJump();  
//                 }
//                 // if they cannot, continue the current state
//                 if (canMoveRight == false){
//                     canMoveLeft = true;
//                     canMoveRight = false;
//                 }
//             }
//        }
   
//    }