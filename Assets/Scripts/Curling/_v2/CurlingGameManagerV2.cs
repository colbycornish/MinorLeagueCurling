using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
/// <summary>
/// This is the high-level game manager for a curling game.
/// It orchestrates the flow of the game, including starting new ends,
/// managing player turns, and handling the end of the game.
/// </summary>


public class CurlingGameManagerV2 : MonoBehaviour
{
    public static CurlingGameManagerV2 Instance { get; private set; }
    public CurlingEndGameManagerV2 endManager;
    public CurlingPlayerManagerV2 playerManager;
    public CurlingStoneManagerV2 stoneManager;
    public CurlingGameData gameData;
    public Camera stoneCamera;
    CurlingCourseData courseData_tmp; // temporary for storage
    CurlingTeam teamHome_tmp; // temporary for storage
    CurlingTeam teamAway_tmp; // temporary for storage

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

    public void InitSetupFromDemo(
        CurlingCourseData courseData,
        CurlingTeam teamHome,
        CurlingTeam teamAway
    )
    {
        courseData_tmp = courseData;
        teamHome_tmp = teamHome;
        teamAway_tmp = teamAway;

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

    // Starts the curling game.
    private void StartCurlingGame()
    {
        CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.Loading);
        Debug.Log($"Starting End {currentEnd}");
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