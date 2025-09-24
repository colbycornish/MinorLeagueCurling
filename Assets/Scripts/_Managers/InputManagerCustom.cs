
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

/// <summary>
/// Singleton GameManager that handles scene transitions, player spawn positioning, and persistent data.
/// </summary>




public class InputManagerCustom : MonoBehaviour
{
    /// Movement
    [Header("Movement")] // Left Thumbstick
    public KeyCode leftArrowKey = KeyCode.LeftArrow; // Movement
    // public event Action OnLeftArrowPress;
    // public event Action OnLeftArrowHold;
    // public event Action OnLeftArrowRelease;
    public KeyCode rightArrowKey = KeyCode.RightArrow; // D Pad Left
    // public event Action OnRightArrowPress;
    // public event Action OnRightArrowHold;
    // public event Action OnRightArrowRelease;
    public KeyCode upArrowKey = KeyCode.UpArrow; // D Pad Right
    // public event Action OnUpArrowPress;
    // public event Action OnUpArrowHold;
    // public event Action OnUpArrowRelease;
    public KeyCode downArrowKey = KeyCode.DownArrow; // D Pad Left
    // public event Action OnDownArrowPress;
    // public event Action OnDownArrowHold;
    // public event Action OnDownArrowRelease;
    
    [Header("View")] // Left Thumbstick
    public KeyCode leftDpadKey = KeyCode.A; // Movement
    // public event Action OnLeftDpadPress;
    // public event Action OnLeftDpadHold;
    // public event Action OnLeftDpadRelease;

    public KeyCode rightDpadKey = KeyCode.D; // D Pad Left
    // public event Action OnRightDpadPress;
    // public event Action OnRightDpadHold;
    // public event Action OnRightDpadRelease;

    public KeyCode upDpadKey = KeyCode.W; // D Pad Right
    // public event Action OnUpDpadPress;
    // public event Action OnUpDpadHold;
    // public event Action OnUpDpadRelease;

    public KeyCode downDpadKey = KeyCode.S; // D Pad Left
    // public event Action OnDownDpadPress;
    // public event Action OnDownDpadHold;
    // public event Action OnDownDpadRelease;

    [Header("View")] // Left Thumbstick
    public KeyCode leftTrigger = KeyCode.LeftShift; // L2 (Left Trigger)
    // public event Action OnLeftTriggerPress;
    // public event Action OnLeftTriggerHold;
    // public event Action OnLeftTriggerRelease;

    public KeyCode rightTrigger = KeyCode.RightShift; // R2 (Right Trigger)
    // public event Action OnRightTriggerPress;
    // public event Action OnRightTriggerHold;
    // public event Action OnRightTriggerRelease;

    /// Action Keys (In-Game)
    [Header("Action Keys")]
    public KeyCode actionKey = KeyCode.Space; // Controller X
    // public event Action OnKeyPress;
    // public event Action OnKeyHold;
    // public event Action OnKeyRelease;

    public KeyCode goBackKey = KeyCode.B; // Controller O
    // public event Action OnGoBackKeyPress;
    // public event Action OnGoBackKeyHold;
    // public event Action OnGoBackKeyRelease;

    public KeyCode infoKey = KeyCode.V; // Controller Triangle: View related information or use equipment. 
    // public event Action OnInfoKeyPress;
    // public event Action OnInfoKeyHold;
    // public event Action OnInfoKeyRelease;

    public KeyCode interactKey = KeyCode.X; // Controller Square: Context-sensitive actions or shortcuts. 
    // public event Action OnInteractKeyPress;
    // public event Action OnInteractKeyHold;
    // public event Action OnInteractKeyRelease;

    /// Special Keys
    [Header("Special Keys")]
    public KeyCode pauseKey = KeyCode.P; // Special Button (Right)
    public KeyCode inventoryKey = KeyCode.I; // Middle Button

    /// God Mode Keys
    [Header("God Mode Keys")]
    public KeyCode resetKey = KeyCode.R; // Special Button



    // void Awake()
    // {
    //     MainManager._instance.SetPhase(MainState.Loading);
    //     GameManager._instance.SetPhase(GameState.Loading);
    //     MainManager._instance.SetPhase(MainState.Playing);
    //     GameManager._instance.SetPhase(GameState.Curling);
    //     if (_instance == null)
    //     {
    //         DontDestroyOnLoad(gameObject);
    //         _instance = this;
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    //     isReady = true;
    // }

    // Check if the Left Shift key is currently held down
    public void ActionKeyControl(){
        if (Input.GetKey(leftArrowKey))
        {
            Debug.Log("Left Shift key is held down.");
        }

        // Check if the Left Shift key was pressed down in the current frame
        if (Input.GetKeyDown(leftArrowKey))
        {
            Debug.Log("Left Shift key was pressed down.");
        }

        // Check if the Left Shift key was released in the current frame
        if (Input.GetKeyUp(leftArrowKey))
        {
            Debug.Log("Left Shift key was released.");
        }
    }


    public void SelectionKeyControl(){
        // Cross (X): Select or interact. 
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Left Shift key is held down.");
        }

        // Check if the Left Shift key was pressed down in the current frame
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Left Shift key was pressed down.");
        }

        // Check if the Left Shift key was released in the current frame
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Debug.Log("Left Shift key was released.");
        }
    }

    public void CancelKeyControl(){
        // Circle (○): Cancel or go back. 
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Left Shift key is held down.");
        }

        // Check if the Left Shift key was pressed down in the current frame
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("Left Shift key was pressed down.");
        }

        // Check if the Left Shift key was released in the current frame
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Debug.Log("Left Shift key was released.");
        }
    }



}



// Main Buttons

// Circle (○): Cancel or go back. 
// Square (□): Context-sensitive actions or shortcuts. 
// Triangle (△): View related information or use equipment. 
// PS Button: Access the Control Center or go to the Home Screen. 
// Create Button: Display the create menu for taking screenshots or video clips. 
// Options Button: Access the options menu. 
// Analog Sticks

// Left Analog Stick: Used for movement and navigation. 
// Right Analog Stick: Used for camera control and movement. 
// L3 (Left Stick Button): Pressing down on the left analog stick. 
// R3 (Right Stick Button): Pressing down on the right analog stick. 

// Triggers and Bumpers
// L1/R1 (Left/Right Bumpers): Shoulder buttons. 
// L2/R2 (Left/Right Triggers): Pressure-sensitive triggers used for actions like aiming and firing. 