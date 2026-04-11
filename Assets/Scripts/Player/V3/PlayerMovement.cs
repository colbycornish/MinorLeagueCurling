using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


namespace PlayerControls.v3
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Speed Settings")]
        // public float walkSpeed = 8f;
        // public float runSpeed = 16f;
        public float moveSpeed = 8f;

        [Header("Character Objects")]
        public Transform orientation; // An empty GameObject child of the player, representing forward direction
        public Rigidbody rb;

        [Header("Animation Settings")]
        public Animator animator;
        public float walkAnimationSpeed = 2.2f;
        public float runAnimatonSpeed = 1f;
        private bool isSprinting = false;

        private float horizontalInput;
        private float verticalInput;

        [Header("Movement Inputs")]
        private InputAction movementAction;
        private InputAction sprintAction;
        // private InputAction interactAction;
        
        private Coroutine _movementProcess;

        void Awake()
        {
            sprintAction = InputSystem.actions.FindAction("Sprint", true);
            movementAction = InputSystem.actions.FindAction("Move", true);
            // interactAction = InputSystem.actions.FindAction("Interact", true);
        }

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            orientation = this.transform;
        }

        protected virtual void OnEnable()
        {
            movementAction.performed += MovePlayerAction;
            movementAction.Enable();

            sprintAction.performed += ToggleSprintAction;
            sprintAction.Enable();
        }

        protected virtual void OnDisable()
        {
            movementAction.performed -= MovePlayerAction;
            movementAction.Disable();

            sprintAction.performed -= ToggleSprintAction;
            sprintAction.Disable();
            
        }

        /************************************************************************************************************************/

        // private void OnMovePlayer(InputAction.CallbackContext obj)
        // {
        //     Debug.Log("Aim Throw action performed");
        //     StartMovement(obj);   
        // }
        
        // // This method is called when the player performs the AimThrow action. It starts a coroutine that 
        // // continuously updates the aiming direction based on the input value until the input value returns to zero 
        // // (i.e., the player stops aiming).
        // public void StartMovement(InputAction.CallbackContext callbackContext)
        // {
        //     if (this._movementProcess != null)
        //         this.StopCoroutine(this._movementProcess);

        //     this._movementProcess = this.StartCoroutine(this.MovementProcess(callbackContext));
        // }


        // private IEnumerator MovementProcess(InputAction.CallbackContext callbackContext)
        // {
        //     float direction = callbackContext.ReadValue<float>();

        //     while (direction != 0)
        //     {
        //         CurlingManager._instance.Aiming.ChangeDirection(
        //             input: callbackContext.ReadValue<float>()
        //         );

        //         yield return null;

        //         direction = callbackContext.ReadValue<float>();
        //     }
        // }

        void Update()
        {
            // Get input
            horizontalInput = Input.GetAxisRaw("Horizontal");
            verticalInput = Input.GetAxisRaw("Vertical");
            
        }

        void FixedUpdate()
        {
            MoveCharacter();
        }

        /************************************************************************************************************************/

        private void ToggleSprintAction(InputAction.CallbackContext obj)
        {
            isSprinting = !isSprinting;
        }

        private void MovePlayerAction(InputAction.CallbackContext obj)
        {
            StartMovement(obj);
        }

        public void StartMovement(InputAction.CallbackContext callbackContext)
        {
            if (this._movementProcess != null)
                this.StopCoroutine(this._movementProcess);

            this._movementProcess = this.StartCoroutine(this.MovementProcess(callbackContext));
        }


        private IEnumerator MovementProcess(InputAction.CallbackContext callbackContext)
        {
            Vector2 direction = callbackContext.ReadValue<Vector2>();

            while (direction.x != 0 || direction.y != 0)
            {
                Debug.Log("Player Input for movement direction has been registered!");
                // TODO: Adjust the MoveCharacter Function to work with this

                yield return null;

                direction = callbackContext.ReadValue<Vector2>();
            }
        }


        /************************************************************************************************************************/

        void MoveCharacter()
        {
            float speed = moveSpeed;
            float vertical = verticalInput;
            float animSpeed = walkAnimationSpeed;

            // if (Input.GetKey(KeyCode.LeftShift))
            if (isSprinting)
            {
                vertical *= 2f;
                speed *= 2f; // runSpeed
                animSpeed = runAnimatonSpeed;
            }

            Vector3 moveDirection = (orientation.forward * vertical + orientation.right * horizontalInput).normalized;
            Vector3 velocity = moveDirection * speed;

            // Apply velocity directly
            GetComponent<Rigidbody>().linearVelocity = new Vector3(velocity.x, GetComponent<Rigidbody>().linearVelocity.y, velocity.z); // Preserve vertical velocity for jumping

            // Animate Character
            animator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);
            animator.SetFloat("Horizontal", horizontalInput, 0.1f, Time.deltaTime);
            animator.SetFloat("WalkSpeed", animSpeed);
        }

        bool IsGrounded()
        {
            // Implement your ground check logic here (e.g., Raycast down)
            // For example:
            return Physics.Raycast(transform.position, Vector3.down, 1.1f);
        }

    }
}


// Jump input
// if (Input.GetButtonDown("Jump") && IsGrounded())
// {
//     rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
// }

// [Header("Speed Settings")]
// public float rotateSpeed;
// public float jumpForce;
// public float jumpForce = 10f;    

// Calculate movement direction relative to orientation
// Vector3 moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;
// moveDirection.Normalize(); // Ensure consistent speed in all directions

// Apply force
// rb.AddForce(moveDirection * moveSpeed * 10f, ForceMode.Force); // Multiply by 10 for better feel with ForceMode.Force