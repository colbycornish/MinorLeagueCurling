using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerControls.v1
{
    public class PlayerControllerV2 : MonoBehaviour
    {
        public float walkSpeed, runSpeed, rotateSpeed, jumpForce, walkAnimationSpeed, runAnimatonSpeed;

        public Animator animator;
        public new Rigidbody rigidbody;

        Vector3 offset;

        public float distToGround;

        public bool isGrounded;

        private SnapRotation snapRotation;
        private bool useSnapRotation = true;
        public float mouseSensitivity = 3f;
        public Transform playerBody;
        float xRotation = 0f;

        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            playerBody = GetComponent<Transform>();
            distToGround = GetComponent<Collider>().bounds.extents.y;
            snapRotation = new SnapRotation(0.5f);
        }

        void Update()
        {
            if (!snapRotation.IsTurning)
            {
                if (Input.GetKeyDown(KeyCode.D))
                    snapRotation.StartTurn(transform.rotation, 1);
                if (Input.GetKeyDown(KeyCode.A))
                    snapRotation.StartTurn(transform.rotation, -1);
            }
        }


        void FixedUpdate()
        {
            HandleMovement();
            HandleRotation();
        }

        void HandleMovement()
        {
            float horizontalMove = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            float speed = walkSpeed;
            float animSpeed = walkAnimationSpeed;

            if (Input.GetKey(KeyCode.LeftShift))
            {
                vertical *= 2f;
                speed = runSpeed;
                animSpeed = runAnimatonSpeed;
            }

            Vector3 moveDirection = (transform.forward * vertical + transform.right * horizontalMove).normalized;
            Vector3 velocity = moveDirection * speed;

            // Apply velocity directly
            rigidbody.linearVelocity = new Vector3(velocity.x, rigidbody.linearVelocity.y, velocity.z); // Preserve vertical velocity for jumping

            // Handle Animation
            animator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);
            animator.SetFloat("Horizontal", horizontalMove, 0.1f, Time.deltaTime);
            animator.SetFloat("WalkSpeed", animSpeed);
        }

        void HandleRotation()
        {
            // Handle Rotation
            if (!!useSnapRotation)
            {
                // This uses snap rotation, constraining the Player to 90 degree turns
                rigidbody.MoveRotation(snapRotation.UpdateRotation(rigidbody.rotation, Time.fixedDeltaTime));
            }
            else
            {
                // Otherwise, player has full control of the rotation
                float rotationInput = Input.GetAxisRaw("Horizontal");

                if (rotationInput != 0)
                {
                    rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                    Quaternion deltaRotation = Quaternion.Euler(0, rotationInput * rotateSpeed * Time.fixedDeltaTime, 0);
                    rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
                }
                else
                {
                    // Freeze Y rotation when not rotating to stop drift
                    rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
                }




                // Otherwise, player has full control of the rotation
                // float rotationInput = Input.GetAxisRaw("Horizontal");
                // float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
                // float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

                // xRotation -= mouseY;
                // xRotation = Mathf.Clamp(xRotation, -90f, 90f);

                // if (xRotation != 0)
                // {
                //     rigidbody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                //     Quaternion deltaRotation = Quaternion.Euler(0, xRotation * rotateSpeed * Time.fixedDeltaTime, 0);
                //     rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
                // }
                // else
                // {
                //     // Freeze Y rotation when not rotating to stop drift
                //     rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
                // }

                // transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
                // playerBody.Rotate(Vector3.up * mouseX);
            }

            // // void Update()
            // // {
            //     float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            //     float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            //     xRotation -= mouseY;
            //     xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            //     transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            //     playerBody.Rotate(Vector3.up * mouseX);
            // // }

            

            // Rotate the camera vertically
            // xRotation -= mouseY;
            // xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp vertical look
            // transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // // Rotate the player horizontally
            // playerBody.Rotate(Vector3.up * mouseX);
            
            

        }


        void Jump()
        {
            isGrounded = Grounded();
            rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        bool Grounded()
        {
            return Physics.Raycast(transform.position, -Vector3.up, distToGround, 9);
        }

    }
}
