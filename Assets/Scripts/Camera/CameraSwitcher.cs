using UnityEngine;
using Unity.Cinemachine; // Use Unity.Cinemachine for 3.0+
using UnityEngine.InputSystem;
using System.Collections.Generic;


public class CameraSwitcher : MonoBehaviour {
    
    
    public enum CameraType
    {
        Exploration,
        Curling,
        ImageCapture
    }

    [Header("Mode")]
    public CameraType currentCameraSwitcher = CameraType.Exploration;

    [Header("Cinebrain")]
    private CinemachineBrain _Cinebrain;

    [Header("Exploration Cameras")]
    public GameObject explorationCameras;
    public List<CinemachineCamera> _ListOfExplorationCameras;
    private int currentExplorationCameraIndex = 0;

    [Header("Curling Cameras")]
    public GameObject curlingCameras;
    public List<CinemachineCamera> _ListOfCurlingCameras;
    private int currentCurlingCameraIndex = 0;

    [Header("Image Capture Cameras")]
    public GameObject imageCaptureCameras;
    public List<CinemachineCamera> _ListOfImageCaptureCameras;
    private int currentCameraIndex = 0;

    [Header("Input Actions")]
    private InputAction switchCameraAction;


    #if UNITY_EDITOR
    protected void OnValidate()
    {
        _Cinebrain = GetComponentInChildren<CinemachineBrain>();

        if (explorationCameras != null){
            CinemachineCamera[] listOfExplorationCameras = explorationCameras.GetComponentsInChildren<CinemachineCamera>(true);
            _ListOfExplorationCameras = new List<CinemachineCamera>(listOfExplorationCameras);
        }
        if (curlingCameras != null){
            CinemachineCamera[] listOfExplorationCameras = curlingCameras.GetComponentsInChildren<CinemachineCamera>(true);
            _ListOfCurlingCameras = new List<CinemachineCamera>(listOfExplorationCameras);
        }
        if (imageCaptureCameras != null){
            CinemachineCamera[] listOfExplorationCameras = imageCaptureCameras.GetComponentsInChildren<CinemachineCamera>(true);
            _ListOfImageCaptureCameras = new List<CinemachineCamera>(listOfExplorationCameras);
        }

    }
    #endif

    void Awake()
    {
        switchCameraAction = InputSystem.actions.FindAction("DevSwitchCamera", true);

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

    private void OnSwitchCamera(InputAction.CallbackContext obj)
    {
        int nextCameraIndex = GetNextCameraIndex();
        switch (currentCameraSwitcher)
        {
            case CameraType.Exploration:
                _ListOfExplorationCameras[nextCameraIndex].Prioritize();
                currentExplorationCameraIndex = nextCameraIndex;
                break;
            case CameraType.Curling:
                _ListOfCurlingCameras[nextCameraIndex].Prioritize();
                currentCurlingCameraIndex = nextCameraIndex;
                break;
            default:
                break;
        }
        // GetCurrentCameraIndex();
        // int newIndex = currentCameraIndex + 1;
        // CinemachineCamera newCamera = _ListOfExplorationCameras[newIndex];
        // newCamera.Prioritize();
    } 

    private int GetNextCameraIndex()
    {
        int currentCameraIndex = currentCameraSwitcher == CameraType.Exploration
            ? currentExplorationCameraIndex
            : currentCameraSwitcher == CameraType.Curling
                ? currentCurlingCameraIndex
                : 0;

        int maxCameraIndex = currentCameraSwitcher == CameraType.Exploration
            ? _ListOfExplorationCameras.Count
            : currentCameraSwitcher == CameraType.Curling
                ? _ListOfCurlingCameras.Count
                : 0;

        if (currentCameraIndex == maxCameraIndex) return 0;
        else return currentCameraIndex + 1;
    }

    private void GetCurrentCameraIndex()
    {
        int index = 0;
        foreach (CinemachineCamera c in _ListOfExplorationCameras)
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

    
}



 

        
