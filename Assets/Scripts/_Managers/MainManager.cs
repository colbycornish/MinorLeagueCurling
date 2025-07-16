using UnityEngine;
using System;

public enum MainState
{
    Loading,
    MainMenu,
    Playing,
    Paused,
    Saving,
    GameOver,
    Quiting
}

public class MainManager : MonoBehaviour
{
    public static MainManager _instance;
    public event Action<MainState> OnStateChanged;
    public MainState currentState = MainState.MainMenu;

    // A public stat propert to allow other classes to get the reference, but not set it.
    // public static MainManager Instance
    // {
    //     get
    //     {
    //         if (_instance == null)
    //         {
    //             _instance = FindObjectOfType<MainManager>();
    //             if (_instance == null)
    //             {
    //                 GameObject obj = new GameObject("MainManager");
    //                 _instance = obj.AddComponent<MainManager>();
    //             }
    //         }
    //         return _instance;
    //     }
    // }

    // Set the Instance reference at the soonest opportunity
    // This is a singleton pattern to ensure only one instance of MainManager exists
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        // end of new code

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Sets the state for thie Main Manager.
    // This should be used for high level states only (like paused, main menu, loading, )
    public void SetState(MainState newState)
    {
        Debug.Log($"[MatchPhase] {currentState} → {newState}");
        if (currentState == newState) return;

        currentState = newState;
        Debug.Log($"[MatchPhase] {currentState} → {newState}");
        OnStateChanged?.Invoke(newState);
    }

    public void Play()
    {
        // currentState = MainState.Playing;
        SetState(MainState.Playing);
        Time.timeScale = 1f;
    }

    // Example: Pause functionality
    public void PauseGame()
    {
        // currentState = MainState.Paused;
        SetState(MainState.Paused);
        Time.timeScale = 0f; // Freeze time
    }

    public void ResumeGame()
    {
        // currentState = MainState.Playing;
        SetState(MainState.Playing);
        Time.timeScale = 1f; // Resume time
    }

    // Example: Quit functionality
    public void QuitGame()
    {
        SetState(MainState.Quiting);
        Application.Quit();
    }
}