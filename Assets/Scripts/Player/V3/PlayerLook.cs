using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.InputSystem;

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

        [Header("Movement Inputs")]
        private InputAction lookAction;
        private Coroutine _lookProcess;

        void Awake()
        {
            lookAction = InputSystem.actions.FindAction("Look", true);
        }

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

        protected virtual void OnEnable()
        {
            // lookAction.performed += OnChangeLookDirection;
            // lookAction.Enable();
        }

        protected virtual void OnDisable()
        {
            // lookAction.performed -= OnChangeLookDirection;
            // lookAction.Disable();
        }

        /************************************************************************************************************************/

        // private void OnChangeLookDirection(InputAction.CallbackContext obj)
        // {
        //     Debug.Log("Aim Throw action performed");
        //     StartChangeLookDirection(obj);   
        // }
        
        // // This method is called when the player performs the AimThrow action. It starts a coroutine that 
        // // continuously updates the aiming direction based on the input value until the input value returns to zero 
        // // (i.e., the player stops aiming).
        // public void StartChangeLookDirection(InputAction.CallbackContext callbackContext)
        // {
        //     if (this._lookProcess != null)
        //         this.StopCoroutine(this._lookProcess);

        //     this._lookProcess = this.StartCoroutine(this.ChangeLookDirectionProcess(callbackContext));
        // }


        // private IEnumerator ChangeLookDirectionProcess(InputAction.CallbackContext callbackContext)
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

        /************************************************************************************************************************/

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

            Mathf.Clamp(cc_tpf_y_cur, cc_tpf_y_start-2, cc_tpf_y_start+2);
            
            // playerBody.Rotate(Vector3.up * mouseX);
            
        }
    }
}
  