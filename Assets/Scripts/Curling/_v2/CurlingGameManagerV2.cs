using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
/// <summary>
/// This is the high-level game manager for a curling game.
/// It orchestrates the flow of the game, including starting new ends,
/// managing player turns, and handling the end of the game.
/// </summary>


[RequireComponent(typeof(CurlingPlayerManagerV2))]
[RequireComponent(typeof(CurlingStoneManagerV2))]
[RequireComponent(typeof(CurlingStoneAimController))]
[RequireComponent(typeof(CurlingStoneSweepController))]
[RequireComponent(typeof(CurlingEndGameManagerV2))]
public class CurlingGameManagerV2 : MonoBehaviour
{

    // [HideInInspector] 
    public static CurlingGameManagerV2 Instance { get; private set; }
    public CurlingEndGameManagerV2 endManager;
    public CurlingPlayerManagerV2 playerManager;
    public CurlingStoneManagerV2 stoneManager;
    public CurlingStoneAimController aimController;
    public CurlingStoneSweepController sweepController;
    public CurlingGameData gameData;

    [Header("Canvas Objects")]
    public PowerMeterUiV2 powerMeter;    
    public PowerMeterUI powerMeterv2;          // Assign PowerMeterUI script in Inspector
    public GameObject powerMeterPromptUI;

    [Header("[Data] Course")]
    public Camera stoneCamera;
    CurlingCourseData courseData_tmp; // temporary for storage

    [Header("[Data] Player")]
    CurlingTeam teamHome_tmp; // temporary for storage
    CurlingTeam teamAway_tmp; // temporary for storage

    [Header("[Data] Settings")]
    public int currentEnd = 1;
    public int maxEnds = 8;
    public int turnCount = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /// <summary>
    /// Setup data
    /// </summary>

    public void InitSetupFromDemo(
        CurlingCourseData courseData,
        CurlingTeam teamHome,
        CurlingTeam teamAway
    )
    {
        courseData_tmp = courseData;
        teamHome_tmp = teamHome;
        teamAway_tmp = teamAway;
        LoadInitialData();
    }


    public void LoadInitialData()
    {
        // TODO: Adjust to grab information from a singlular storage spot
        // CurlingCourseData courseData = dataManager._instance.curlingGame.courseData;
        // CurlingTeam teamHome = dataManager._instance.curlingGame.teamHome;
        // CurlingTeam teamAway = dataManager._instance.curlingGame.teamAway;

        CurlingCourseData courseData = courseData_tmp;
        CurlingTeam teamHome = teamHome_tmp;
        CurlingTeam teamAway = teamAway_tmp;

        aimController.Setup(courseData);
        playerManager.Setup(
            courseData,
            teamHome,
            teamAway
        );

        endManager.SetTargetZone(courseData.targetZone);

        stoneManager.Setup(
            courseData,
            teamHome,
            teamAway
        );

        StartCurlingGame();
    }

    /// <summary>
    /// Start Game
    /// </summary>

    // Starts the curling game.
    private void StartCurlingGame()
    {
        CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.Loading);
        // Debug.Log($"Starting End {currentEnd}");
        turnCount = 0;
        // Resets the current game layout, and removes the existing stones
        endManager.ResetCurlingGame();

        // Sets up the stone objects for use in the game
        stoneManager.SetupStones();

        //Sets up the players
        playerManager.PreparePlayers();

        // Sets the initial phase of the match
        CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.RoundSplash);
        // OnStoneSelectionConfirmed();
    }


    /// <summary>
    /// Next Turn
    /// </summary>
    public void NextTurn()
    {
        playerManager.NextPlayer();
        turnCount++;
        CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.RoundSplash);
    }


    public void OnStoneSelectionConfirmed()
    {
        stoneManager.PrepareNextStone();
        stoneCamera.GetComponent<CinemachineCamera>().Follow = stoneManager.currentStone.transform;
        aimController.Reset();
        CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.CurlingAimControlsPhase);
    }


    // Called when a stone finishes moving, and is now resting in the target zone.
    public void OnStoneRested(GameObject stone)
    {
        // endManager.AddStone(stone);

        // if (playerManager.AllStonesThrown())
        // {
        //     EndCurrentCurlingGame();
        // }
        // else
        // {
        //     playerManager.NextPlayer();
        //     CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.TeamSplash);
        // }
    }

    /// <summary>
    /// End Game
    /// </summary>
    public void EndCurrentCurlingGame()
    {
        endManager.CalculateScore();
        // playerManager.UpdateScore(endManager.GetScore());

        if (currentEnd < maxEnds)
        {
            currentEnd++;
            StartCurlingGame();
        }
        else
        {
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.FinalResults);
        }
    }
}



// public void InitSetup()
//     {
//         Debug.Log("Initializing Curling Game Setup");
        
//         StartCurlingGame();
//         CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.RoundSplash);
//     }