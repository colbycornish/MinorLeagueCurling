
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

/// <summary>
/// Singleton GameManager that handles scene transitions, player spawn positioning, and persistent data.
/// </summary>
/// 

public enum CameraState
{
    Follow,
    Tripod,
    Dolly,
    Crane,
    Airiel,
    Free
}


public enum CameraShotType
{
    Insert,
    ExtremeCloseUp,
    CloseUp,
    ThirdPerson, // OverTheShoulder
    Medium,
    Wide,
    BirdsEye,
    Helicopter
}


public enum CameraMoveType
{
    DutchAngle,
    DollyIn,
    DollyOut,
    CraneDown,
    CraneUp,
    BlurOut,
    FocusIn,
    Refocus,
    ZoomIn,
    ZoomOut
}



public class CameraManager : MonoBehaviour
{
    public static CameraManager _instance;
    public event Action<CameraState> OnStateChanged;
    public CameraState currentState = CameraState.Follow;
    public bool isReady = false;

    void Awake()
    {
        if (_instance == null)
        {
            DontDestroyOnLoad(gameObject);
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        isReady = true;
    }

    public void SetState(CameraState newState)
    {
        Debug.Log($"[MatchPhase] {currentState} → {newState}");
        if (currentState == newState) return;

        currentState = newState;
        Debug.Log($"[MatchPhase] {currentState} → {newState}");
        OnStateChanged?.Invoke(newState);
    }
}