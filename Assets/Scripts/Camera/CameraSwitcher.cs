using UnityEngine;
using Unity.Cinemachine; // Use Unity.Cinemachine for 3.0+
using UnityEngine.InputSystem;
using System.Collections.Generic;


public class CameraSwitcher : MonoBehaviour {
    
    private InputAction switchCameraAction;
    public List<CinemachineCamera> _ListOfCameras;
    private int currentCameraIndex = 0;


    #if UNITY_EDITOR
    protected void OnValidate()
    {
        CinemachineCamera[] listOfCameras = GetComponentsInChildren<CinemachineCamera>(true);
        _ListOfCameras = new List<CinemachineCamera>(listOfCameras);

    }
    #endif

    void Awake()
    {
        switchCameraAction = InputSystem.actions.FindAction("DevSwitchToNextCamera", true);

    }

    protected virtual void OnEnable()
    {
        // Add Listeners
        switchCameraAction.performed += OnSwitchCamera;
        switchCameraAction.Enable();
        
    }

    /************************************************************************************************************************/

    protected virtual void OnDisable()
    {
        // Remove listeners
        switchCameraAction.performed -= OnSwitchCamera;
        switchCameraAction.Disable();
    }

    private void GetCurrentCameraIndex()
    {
        int index = 0;
        foreach (CinemachineCamera c in _ListOfCameras)
        {
            if (c.IsLive)
            {
                currentCameraIndex = index;
                return;
            }
            else
            {
                index += 1;
            }
        }
    }

    private void OnSwitchCamera(InputAction.CallbackContext obj)
    {
        GetCurrentCameraIndex();
        int newIndex = currentCameraIndex + 1;
        CinemachineCamera newCamera = _ListOfCameras[newIndex];
        newCamera.Prioritize();
    } 
}



 

        
