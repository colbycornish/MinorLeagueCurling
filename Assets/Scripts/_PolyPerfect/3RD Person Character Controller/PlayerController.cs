using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Polyperfect.People
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        public float walkSpeed, runSpeed, rotateSpeed, jumpForce, walkAnimationSpeed, runAnimatonSpeed;

        public Animator animator;
        public new Rigidbody rigidbody;

        Vector3 offset;

        public float distToGround;

        public bool isGrounded;

        // Start is called before the first frame update
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            animator = GetComponent<Animator>();
            distToGround = GetComponent<Collider>().bounds.extents.y;
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

        void HandleRotation(){
            // Handle Rotation
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

// void OnCollisionEnter(Collision collision) 
// {
//         // if(collision.gameObject.name == "YourWallName")  // or if(gameObject.CompareTag("YourWallTag"))
//         // {
//         //     rigidbody.velocity = Vector3.zero;
//         // }
//     Debug.Log("OnCollisionEnter: " + collision.gameObject.name);
//     // rigidbody.linearVelocity = Vector3.zero;
// }



// CharacterController controller = player.GetComponent<CharacterController>();
// if (controller != null)
// {
//     controller.enabled = false;
//     player.transform.position = spawnPoint.transform.position;
//     controller.enabled = true;
// }



// // Update is called once per frame
// void Update()
// {
//     isGrounded = Grounded();
//     //Allow the player to move left and right
//     float horizontalMove = Input.GetAxisRaw("Horizontal");
//     //Allow the player to move forward and back
//     float vertical = Input.GetAxisRaw("Vertical");

//     float speed = walkSpeed;
//     float animSpeed = walkAnimationSpeed;

//     if (Input.GetKey(KeyCode.LeftShift))
//     {
//         vertical *= 2f;
//         speed = runSpeed;
//         animSpeed = runAnimatonSpeed;
//     }

//     var translation = transform.forward * (vertical * Time.deltaTime);
//     translation += transform.right * (horizontalMove * Time.deltaTime);
//     translation *= speed;
//     translation = rigidbody.position + translation;

//     isGrounded = Grounded();
//     if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
//     {
//         // Jump
//         rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
//     }

//     // // Rotate the player with the mouse
//     float horizontal = 0f; //Input.GetAxis("Mouse X") * Time.deltaTime;
//     Quaternion rotation = transform.rotation * Quaternion.Euler(0, 0 * 0, 0);
    
//     if (Input.GetKey(KeyCode.D))
//     {
//         horizontal = 1 * Time.deltaTime;
//         rotation = transform.rotation * Quaternion.Euler(0, horizontal * rotateSpeed, 0);
        
//     }


//     if (Input.GetKey(KeyCode.A))
//     {
//         horizontal = -1 * Time.deltaTime;
//         rotation = transform.rotation * Quaternion.Euler(0, horizontal * rotateSpeed, 0);
        
//     }


//     animator.SetFloat("Vertical", vertical, 0.1f, Time.deltaTime);
//     animator.SetFloat("Horizontal", horizontalMove, 0.1f, Time.deltaTime);
//     animator.SetFloat("WalkSpeed", animSpeed);

//     rigidbody.MovePosition(translation);
//     rigidbody.MoveRotation(rotation);
// }