using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;


public class CurlingPlayerController : MonoBehaviour
{
    // Rigidbody of the player.
    public GameObject leftSweeper;
    public GameObject rightSweeper;
    public GameObject centerPoint;
    public GameObject stone;
    public GameObject thrower;

    private Rigidbody centerPointRb;
    // Movement along X and Y axes.
    public float movementX;
    public float movementY;

    // Speed at which the player moves.
    public float speed = 0; 
    public float maxSpeed = 0; 

    private CurlingStone stone2 = new CurlingStone();
    private CurlingSweeper leftSweeperCS = new CurlingSweeper();
    private CurlingSweeper rightSweeperCS = new CurlingSweeper();
        
    //Set this value in the inspector
    public Vector3 targetPosition;
    
    // Start is called before the first frame update.
    void Start()
    {

        leftSweeperCS.init(
            gameObject: leftSweeper,
            sweeperRb: leftSweeper.GetComponent<Rigidbody>(),
            speed: 1,
            maxSpeed: 5,
            strength: 1,
            isLeftSweeper: true,
            isRightSweeper: false
        );
        leftSweeperCS.SetLinearDamping(1);


        rightSweeperCS.init(
            gameObject: rightSweeper,
            sweeperRb: rightSweeper.GetComponent<Rigidbody>(),
            speed: 1,
            maxSpeed: 5,
            strength: 1,
            isLeftSweeper: false,
            isRightSweeper: true
        );
        rightSweeperCS.SetLinearDamping(1);

        stone2.init(
            gameObject: stone,
            stoneRb: stone.transform.GetComponent<Rigidbody>(),
            speed: 5,
            maxSpeed: 5
        );
          
        centerPointRb = centerPoint.GetComponent<Rigidbody>();

        Debug.Log("SetPlayerName:: name = " + "1");

    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        
        // Store the X and Y components of the movement.
        speed = maxSpeed;
        movementX = movementVector.x; 
        movementY = movementVector.y; 

        leftSweeperCS.triggerSweep();

    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate() 
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

        // Apply force to the Rigidbody to move the player objects.
        leftSweeperCS.sweeperRb.AddForce(movement * speed); 
        rightSweeperCS.sweeperRb.AddForce(movement * speed); 
        centerPointRb.AddForce(movement * speed); 

        // force the stone to change it's direction
        targetPosition = centerPointRb.transform.position;
        stone2.ChangeDirection(
            targetPosition: targetPosition
        );

    }

}



