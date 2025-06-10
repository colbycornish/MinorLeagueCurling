using UnityEngine;

public class MainManager : MonoBehaviour
{
    // a static private variable to hold the reference to this Manager instance
    public static MainManager _instance;
    // public static AudioManager audio;
    // public static CanvasManager canvas;
    // public static UIManager uiManager;
    // public static CurlingMatchManager curlingMatch;


    public enum GameState
    {
        MainMenu,
        Playing,
        Paused,
        GameOver
    }

    public GameState currentState = GameState.MainMenu;

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

    // Example: Pause functionality
    public void PauseGame() {
        currentState = GameState.Paused;
        Time.timeScale = 0f; // Freeze time
    }

    public void ResumeGame() {
        currentState = GameState.Playing;
        Time.timeScale = 1f; // Resume time
    }

    // Example: Quit functionality
    public void QuitGame()
    {
        Application.Quit();
    }

    


}