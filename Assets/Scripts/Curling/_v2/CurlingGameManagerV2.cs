using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;

public class CurlingGameManagerV2 : MonoBehaviour
{
    public CurlingEndGameManagerV2 endManager;
    public CurlingPlayerManagerV2 playerManager;
    public CurlingStoneManagerV2 stoneManager;
    public Camera camera;

    public int currentEnd = 1;
    public int maxEnds = 8;

    private void Start()
    {
        StartCurlingGame();
    }

    // Starts the curling game.
    private void StartCurlingGame()
    {
        Debug.Log($"Starting End {currentEnd}");
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


    public void OnStoneSelectionConfirmed()
    {
        stoneManager.PrepareNextStone();
        camera.GetComponent<CinemachineCamera>().Follow = stoneManager.currentStone.transform;
        CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.AimControls);
    }






    // Called when a stone finishes moving, and is now resting in the target zone.
    public void OnStoneRested(GameObject stone)
    {
        endManager.AddStone(stone);

        if (playerManager.AllStonesThrown())
        {
            EndCurrentCurlingGame();
        }
        else
        {
            playerManager.NextPlayer();
            CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.TeamSplash);
        }
    }

    private void EndCurrentCurlingGame()
    {
        endManager.CalculateScore();
        playerManager.UpdateScore(endManager.GetScore());

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