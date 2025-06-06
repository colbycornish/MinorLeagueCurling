// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEngine;
// using UnityEngine.InputSystem;
// using TMPro;

// //  - Sweeper Structure
// //             - GameObject: Sweeper Character
// //             - Int: Exhaustion Level
// //             - Int: Exhaustion Level Max
// //             - Int: Exhaustion Level Min
// //             - Int: Exhaustion Level Current
// //             - Int: Strength Level
// //             - Int: Strength Level Max
// //             - Int: Strength Level Min
// //             - Int: Strength Level Current
// //             - Int: Stamina Level
// //             - Int: Stamina Level Max
// //             - Int: Stamina Level Min
// //             - Int: Stamina Level Current
// //             - Int: Speed Level
// //             - Int: Speed Level Max
// //             - Int: Speed Level Min
// //             - Int: Speed Level Current
// //             - bool isSweeping
// //             - bool isExhausted
// //             - hasAdditionalBenefits
// //             - SWEEPERBENEFITS
// //             - ANIMATIONS


// interface ICurlingSweeper
// {
//     GameObject gameObject { get ; set;}
//     Rigidbody sweeperRb { get; set; }
//     float speed { get; set; }
//     float maxSpeed { get; set; }
//     bool isLeftSweeper {get; set;}
//     bool isRightSweeper {get; set;}
//     int strength { get; set; }
//     // int exhaustionLevel { get; set; }
//     // int exhaustionLevelMax { get; set; }
//     // int exhaustionLevelMin { get; set; }
//     // int exhaustionLevelCur { get; set; }
//     // int strengthLevel { get; set; }
//     // int strengthLevelMin { get; set; }
//     // int strengthLevelMax { get; set; }
//     // int strengthLevelCur { get; set; }
//     // int staminaLevel { get; set; }
//     // int staminaLevelMin { get; set; }
//     // int staminaLevelMax { get; set; }
//     // int staminaLevelCur { get; set; }
//     // int speedLevel { get; set; }
//     // int speedLevelMin { get; set; }
//     // int speedLevelMax { get; set; }
//     // int speedLevelCur { get; set; }
//     // bool isSweeping {get; set;}
//     // bool isExhausted {get; set;}


//     void AdjustSpeed(int s);
//     void SetLinearDamping(int ld);

//     void triggerSweep();
    
// } 


// // CurlingStone "implements" the ICurlingStone interface
// class CurlingSweeper : ICurlingSweeper
// {
//     public GameObject gameObject { get ; set; }
//     public Rigidbody sweeperRb { get; set; }
//     public float speed { get; set; }
//     public float maxSpeed { get; set; }
//     public int strength { get; set; }
//     public bool isLeftSweeper  {get; set;}
//     public bool isRightSweeper {get; set;}
//     public int linearDamping { get; set; }
    
//     public void init(
//         GameObject gameObject,
//         Rigidbody sweeperRb,
//         float speed,
//         float maxSpeed,
//         int strength,
//         bool isLeftSweeper,
//         bool isRightSweeper
//     ){
//         this.gameObject = gameObject;
//         this.sweeperRb = sweeperRb;
//         this.speed = speed;
//         this.maxSpeed = maxSpeed;
//         this.strength = strength;
//         this.isLeftSweeper = isLeftSweeper;
//         this.isRightSweeper = isRightSweeper;
//     }
    
//     public void AdjustSpeed(int speedChange)
//     {
//         speed += speedChange;
//     }
//     public void SetLinearDamping(int ld)
//     {
//         sweeperRb.linearDamping = ld;
//     }


//     public void triggerSweep(){
//         // get the settings
//         Color newColor = new Color(1.0f, 1.0f, 0.0f);
//         Color oldColor = gameObject.GetComponent<Renderer>().material.color;
//         Color highLightMat = Color.Lerp(oldColor, newColor, Mathf.PingPong(Time.time, 1.0f));

//         // gameObject.GetComponent<Renderer>().material.color = newColor;
//         gameObject.GetComponent<Renderer>().material.color = highLightMat;
//     }

// } 



// public class CurlingSweeperController : MonoBehaviour
// {
    
// }


// // private Color startColor = new Color(255f,255f,255f);
// //     private Color endColor = new Color(255f, 0f, 0f);
// //     public float duration = 1.0f;

// //     void Update()
// //     {
// //         highLightMat.color = Color.Lerp(endColor, startColor, Mathf.PingPong(Time.time, duration));
// //     }


// //     void OnTriggerEnter(
// //         Collider other
// //     ) {

// //         /*
// //             if the sweeper encounters one of the walled snow edges, 
// //             the sweeper who runs into it should jump over the obstacle,
// //             and be prevented from moving much futher beyond it
// //         */
        
// //        if (other.gameObject.CompareTag("wall_curling_snow")) 
// //        {
// //             // if moving left (this is the left wall)
// //             if (movementX < 0){
// //                 // check that the sweeper can still move left.
// //                 // if they can, the sweeper should jump.
// //                 if (canMoveLeft == true && canMoveRight == true){
// //                     canMoveRight = true;
// //                     canMoveLeft = false;
// //                     triggerJump();  
// //                 }
// //                 // if they cannot, continue the current state
// //                 if (canMoveLeft == false && canMoveRight == true){
// //                     canMoveRight = true;
// //                     canMoveLeft = false;
// //                 }
// //             }
// //             // if moving right (this is the right wall)
// //             else if (movementX > 0){
// //                 // check that the sweeper can still move right.
// //                 // if they can, the sweeper should jump.
// //                 if (canMoveRight == true){
// //                     canMoveLeft = true;
// //                     canMoveRight = false;
// //                     triggerJump();  
// //                 }
// //                 // if they cannot, continue the current state
// //                 if (canMoveRight == false){
// //                     canMoveLeft = true;
// //                     canMoveRight = false;
// //                 }
// //             }
// //        }
   
// //    }