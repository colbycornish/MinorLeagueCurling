using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class CurlingSweeperController : MonoBehaviour
{
    // Rigidbody of the player.
    private Rigidbody sweeperRb;
    public bool isLeftSweeper;
    public bool isRightSweeper;

    //
    private bool canMoveLeft = true;
    private bool canMoveRight = true;

    // Movement along X, Y, and Z axes.
    private float movementX;
    private float movementY;
    private float movementZ;

    // Speed at which the player moves.
    public float speed = 0; 



    // Start is called before the first frame update.
    void Start()
    {
    // Get and store the Rigidbody component attached to the player.
        sweeperRb = transform.GetComponent<Rigidbody>();
        
    }
 
    // This function is called when a move input is detected.
    void OnMove(
        InputValue movementValue
    ) {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate() 
    {

    }

    /// <summary>
    /// Character Selection  
    /// </summary>

    public void SetSweeperCharacter(){

    } 





    /// <summary>
    /// Movement & Collision Detection
    /// </summary>

    void triggerJump(
        // add direction variable
    ) {
        Vector3 movement = new Vector3 (movementX, 5.0f, movementY);

        // Apply force to the Rigidbody to move the player.
        sweeperRb.AddForce(movement * speed); 

    }

    public void SetSweeperSpeed(){

    } 


    void OnTriggerEnter(
        Collider other
    ) {

        /*
            if the sweeper encounters one of the walled snow edges, 
            the sweeper who runs into it should jump over the obstacle,
            and be prevented from moving much futher beyond it
        */
        
       if (other.gameObject.CompareTag("wall_curling_snow")) 
       {
            // if moving left (this is the left wall)
            if (movementX < 0){
                // check that the sweeper can still move left.
                // if they can, the sweeper should jump.
                if (canMoveLeft == true && canMoveRight == true){
                    canMoveRight = true;
                    canMoveLeft = false;
                    triggerJump();  
                }
                // if they cannot, continue the current state
                if (canMoveLeft == false && canMoveRight == true){
                    canMoveRight = true;
                    canMoveLeft = false;
                }
            }
            // if moving right (this is the right wall)
            else if (movementX > 0){
                // check that the sweeper can still move right.
                // if they can, the sweeper should jump.
                if (canMoveRight == true){
                    canMoveLeft = true;
                    canMoveRight = false;
                    triggerJump();  
                }
                // if they cannot, continue the current state
                if (canMoveRight == false){
                    canMoveLeft = true;
                    canMoveRight = false;
                }
            }
       }
   
   }

}