using UnityEngine;

public class MainManager : MonoBehaviour
{
    // a static private variable to hold the reference to this Manager instance
    public static MainManager _instance;
    // public static AudioManager audio;
    // public static CanvasManager canvas;
    // public statis CameraManager camera;
    // public static GameManager game;
    public static UIManager ui;
    // public static CurlingMatchManager curlingMatch;


    public enum MainState
    {
        Loading,
        MainMenu,
        Playing,
        Paused,
        GameOver
        
    }

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

    public void Play()
    {
        currentState = MainState.Playing;
        Time.timeScale = 1f;
    }

    // Example: Pause functionality
    public void PauseGame()
    {
        currentState = MainState.Paused;
        Time.timeScale = 0f; // Freeze time
    }

    public void ResumeGame()
    {
        currentState = MainState.Playing;
        Time.timeScale = 1f; // Resume time
    }

    // Example: Quit functionality
    public void QuitGame()
    {
        Application.Quit();
    }
}