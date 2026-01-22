using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        private float horizontalInput;
        private float verticalInput;

        void Start()
        {
            rb = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            orientation = this.transform;
        }

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


        void MoveCharacter()
        {
            float speed = moveSpeed;
            float vertical = verticalInput;
            float animSpeed = walkAnimationSpeed;

            if (Input.GetKey(KeyCode.LeftShift))
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