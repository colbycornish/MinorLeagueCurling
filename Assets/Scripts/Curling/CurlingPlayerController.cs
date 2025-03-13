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
    // private CurlingStone stone2 = new CurlingStone();
    


    public GameObject leftSweeper;
    public GameObject rightSweeper;
    public GameObject centerPoint;
    public GameObject stone;
    public GameObject thrower;

    private Rigidbody leftSweeperRb; 
    private Rigidbody rightSweeperRb;
    private Rigidbody centerPointRb;

    private CurlingSweeperController leftCurlingSweeper;
    // private CurlingSweeperController RightCurlingSweeper;

    // Movement along X and Y axes.
    public float movementX;
    public float movementY;

    // Speed at which the player moves.
    public float speed = 0; 
    public float maxSpeed = 0; 

    private CurlingStone stone2 = new CurlingStone();
    private CurlingSweeper leftSweeper2 = new CurlingSweeper();
    private CurlingSweeper rightSweeper2 = new CurlingSweeper();
    public GameObject playerGroup; 
        
    //Set this value in the inspector
    public Vector3 targetPosition;
    
    // Start is called before the first frame update.
    void Start()
    {

        leftSweeper2.init(
            gameObject: leftSweeper,
            sweeperRb: leftSweeper.GetComponent<Rigidbody>(),
            speed: 1,
            maxSpeed: 5,
            strength: 1,
            isLeftSweeper: true,
            isRightSweeper: false
        );

        rightSweeper2.init(
            gameObject: rightSweeper,
            sweeperRb: rightSweeper.GetComponent<Rigidbody>(),
            speed: 1,
            maxSpeed: 5,
            strength: 1,
            isLeftSweeper: false,
            isRightSweeper: true
        );

        stone2.init(
            gameObject: stone,
            stoneRb: stone.transform.GetComponent<Rigidbody>(),
            speed: 1,
            maxSpeed: 5
        );
          
        leftSweeperRb = leftSweeper.GetComponent<Rigidbody>();
        rightSweeperRb = rightSweeper.GetComponent<Rigidbody>();
        centerPointRb = centerPoint.GetComponent<Rigidbody>();

        Debug.Log("SetPlayerName:: name = " + "1");

    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // leftCurlingSweeper.OnMove(
        //     movementValue: movementValue
        // );
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();
        
        // Store the X and Y components of the movement.
        speed = maxSpeed;
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    private void FixedUpdate() 
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

        // Apply force to the Rigidbody to move the player objects.
        leftSweeperRb.AddForce(movement * speed); 
        rightSweeperRb.AddForce(movement * speed); 
        centerPointRb.AddForce(movement * speed); 

    }


    // void Update()
    // {
    //     targetPosition = playerGroup.transform.position;
    //     stone.ChangeDirection(targetPosition: targetPosition);
    // }











}



