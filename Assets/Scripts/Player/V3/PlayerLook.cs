using UnityEngine;
using Unity.Cinemachine;

namespace PlayerControls.v3
{
    public class PlayerLook : MonoBehaviour
    {
        public float sensitivity = 100f;
        public Transform playerBody; // The main player GameObject
        public Transform cameraHolder; // An empty GameObject child of the player, holding the camera

        private float xRotation = 0f;

        void Start()
        {
            playerBody = this.transform;
            Cursor.lockState = CursorLockMode.Locked; // Lock cursor to center of screen

            // if (cameraHolder != null)
            // {
            //     CinemachineCamera virtualCamera = cameraHolder.gameObject.GetComponent<CinemachineCamera>();
            //     var composer = virtualCamera.GetComponent<CinemachineComposer>();
            //     // RotationComposer
            //     if (composer != null)
            //     {
            //         Debug.Log("Found The Composer!");
            //     }
            // }
        }

        void FixedUpdate()
        {

            LookHoriztonally();
            LookVertically();
            // float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            // Rotate player body for horizontal look (yaw)
            // playerBody.Rotate(Vector3.up * mouseX);

            // // Rotate camera holder for vertical look (pitch)
            // if (cameraHolder != null)
            // {
            //     xRotation -= mouseY;
            //     xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Clamp vertical look angle
            //     playerBody.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            // }
        }


        void LookHoriztonally()
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            playerBody.Rotate(Vector3.up * mouseX);
        }

        void LookVertically()
        {
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
            // playerBody.Rotate(Vector3.up * mouseX);
            
        }
    }
}
  