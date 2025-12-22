using UnityEngine;
using Unity.Cinemachine;
// using UnityEngine.InputSystem;

namespace PlayerControls.v3
{
    public class PlayerLook : MonoBehaviour
    {
        public float sensitivity = 100f;
        public Transform playerBody; // The main player GameObject
        public Transform cameraHolder; // An empty GameObject child of the player, holding the camera
        private float xRotation = 0f;

        // Camera Items
        private CinemachineThirdPersonFollow ccThirdPersonFollow; // An empty GameObject child of the player, holding the camera
        private float cc_tpf_y_start = 0;
        private float cc_tpf_y_cur = 0;
        private CinemachineRotationComposer ccRotationComposer; // An empty GameObject child of the player, holding the camera
        private float cc_rc_yOffset_start = 0;
        private float cc_rc_yOffset_cur = 0;

        void Start()
        {
            playerBody = this.transform;
            Cursor.lockState = CursorLockMode.Locked; // Lock cursor to center of screen

            ccThirdPersonFollow = cameraHolder.gameObject.GetComponent<CinemachineThirdPersonFollow>();
            ccRotationComposer = cameraHolder.gameObject.GetComponent<CinemachineRotationComposer>();
            cc_rc_yOffset_start = ccRotationComposer.TargetOffset.y;
            cc_rc_yOffset_cur = ccRotationComposer.TargetOffset.y;
            cc_tpf_y_start = ccThirdPersonFollow.ShoulderOffset.y;
            cc_tpf_y_cur = ccThirdPersonFollow.ShoulderOffset.y;
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

        // void OnSprint(InputValue value)
        // {
        //     Debug.Log("Sprint Input Detected");
        // }

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
            cameraHolder.gameObject.GetComponent<CinemachineRotationComposer>();

            Mathf.Clamp(cc_tpf_y_start, cc_tpf_y_start-2, cc_tpf_y_start+2);
            // playerBody.Rotate(Vector3.up * mouseX);
            
        }
    }
}
  