
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

/// <summary>
/// Singleton GameManager that handles scene transitions, player spawn positioning, and persistent data.
/// </summary>




public class InputManagerCustom : MonoBehaviour
{
    /// Movement
    [Header("Movement")]
    public KeyCode leftCurlKey = KeyCode.A; // D Pad Right
    public KeyCode rightCurlKey = KeyCode.D; // D Pad Left
    public KeyCode leftSweeperKey = KeyCode.K; // L2 (Left Trigger)
    public KeyCode rightSweeperKey = KeyCode.L; // R2 (Right Trigger)

    /// Action Keys (In-Game)
    [Header("Action Keys")]
    public KeyCode actionKey = KeyCode.Space; // Controller X
    public KeyCode previousKey = KeyCode.B; // Controller O
    public KeyCode infoKey = KeyCode.V; // Controller Triangle
    public KeyCode interactKey = KeyCode.X; // Controller Square

    /// Special Keys
    [Header("Special Keys")]
    public KeyCode pauseKey = KeyCode.P; // Special Button (Right)
    public KeyCode inventoryKey = KeyCode.I; // Middle Button

    /// God Mode Keys
    [Header("God Mode Keys")]
    public KeyCode resetKey = KeyCode.R; // Special Button

    // void Awake()
    // {
    //     MainManager._instance.SetPhase(MainState.Loading);
    //     GameManager._instance.SetPhase(GameState.Loading);
    //     MainManager._instance.SetPhase(MainState.Playing);
    //     GameManager._instance.SetPhase(GameState.Curling);
    //     if (_instance == null)
    //     {
    //         DontDestroyOnLoad(gameObject);
    //         _instance = this;
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    //     isReady = true;
    // }



}
