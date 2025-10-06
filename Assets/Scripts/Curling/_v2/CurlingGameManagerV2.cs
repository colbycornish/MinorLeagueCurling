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

    [Header("Canvas Objects")]
    public PowerMeterUiV2 powerMeter;
    public PowerMeterUI powerMeterv2;          // Assign PowerMeterUI script in Inspector
    public GameObject powerMeterPromptUI;

    [Header("[Data] Course")]
    public Camera stoneCamera;
    CurlingCourseData courseData_tmp; // temporary for storage

    [Header("[Data] Exit Information")]
    public string exitScene;
    public string exitSpawnId;

    [Header("[Data] Player")]
    CurlingTeam teamHome_tmp; // temporary for storage
    CurlingTeam teamAway_tmp; // temporary for storage

    [Header("[Data] Settings")]
    public CurlingGameData gameData;
    public int turnsMax = 10;


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
        CurlingTeam teamAway,
        CurlingGameData data,
        string exitSceneName,
        string exitSpawnIdName
    )
    {
        // aimController.AdjustPowers();
        // sweepController.AdjustPowers();
        courseData_tmp = courseData;
        teamHome_tmp = teamHome;
        teamAway_tmp = teamAway;
        gameData = data;
        exitScene = exitSceneName;
        exitSpawnId = exitSpawnIdName;
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
        gameData.turnCurrent = 0;

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
    // private void HandleNextTurn()
    // {
    //     bool gameIsOver = false;
    //     int currentTurnCount = CurlingGameManagerV2.Instance.gameData.currentTurn;
    //     int maxTurnCount = CurlingGameManagerV2.Instance.gameData.turnsMax;
    //     if ((currentTurnCount + 1) == maxTurnCount)
    //     {
    //         CurlingGameManagerV2.Instance.EndCurrentCurlingGame(); // End Curling Game
    //     }
    //     else
    //     {
    //         CurlingGameManagerV2.Instance.NextTurn();
    //         CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.StoneSelection);
    //     }
    // }

    public void NextTurn()
    {
        playerManager.NextPlayer();
        gameData.turnCurrent++;
        // turnCount++;
        CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.StoneSelection);
    }


    public void OnStoneSelectionConfirmed()
    {
        stoneManager.PrepareNextStone();
        stoneCamera.GetComponent<CinemachineCamera>().Follow = stoneManager.currentStone.transform;
        aimController.Reset();
        CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.CurlingAimControlsPhase);
    }
    // Called when a stone finishes moving, and is now resting in the target zone.


    /// <summary>
    /// End Game
    /// </summary>
    public void EndCurrentCurlingGame()
    {
        endManager.CalculateScore();
        // playerManager.UpdateScore(endManager.GetScore());
        CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.FinalResults);
        SaveCurlingGameResults();
    }

    public void SaveCurlingGameResults()
    {
        
    }
    

    public void ExitCurlingGame()
    {
        // Clear Existing Stones
        // Clear Team People
        // Reset All Necessary Things 
        GameManager._instance.TeleportToScene(
            exitScene,
            exitSpawnId
        );
    }
}
