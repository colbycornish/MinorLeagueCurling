using UnityEngine;

/// <summary>
/// This class listens for phase changes from the CurlingMatchPhaseManager 
/// and updates the game state accordingly. It is only resonsible for 
/// INITIATING the BEGINNINGS of each phase, but does not handle exiting 
/// said phases. 
///
/// It should also control the flow of the curling match, including:
/// - managing transitions between phases
/// - team selection
//  - stone selection
//  - throw sequences
//  - end-of-turn logic
//
/// It also interacts with the CurlingGameManager and CurlingPlayerManager 
/// to manage the game state and player turns.
/// </summary>

public class CurlingMatchFlowController : MonoBehaviour
{
    // public CurlingGameManagerV2 gameManager;
    // public CurlingPlayerManagerV2 playerManager;

    
    private void OnEnable()
    {
        if (CurlingMatchPhaseManager.Instance == null) return;
        CurlingMatchPhaseManager.Instance.OnPhaseChanged += HandlePhase;
    }

    private void OnDisable()
    {
        if (CurlingMatchPhaseManager.Instance == null) return;
        CurlingMatchPhaseManager.Instance.OnPhaseChanged -= HandlePhase;
    }

    private void Start()
    {
        // Kick off the match
        CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.Loading);
    }

    private void HandlePhase(CurlingMatchPhase phase)
    {
        // Debug.Log($"Handling phase: {phase}");
        // switch (phase)
        // {
        //     case CurlingMatchPhase.RoundSplash:
        //         Invoke(nameof(ShowTeamSplash), 2f); // Simulate delay
        //         break;

        //     case CurlingMatchPhase.TeamSplash:
        //         Invoke(nameof(BeginStoneSelection), 2f);
        //         break;

        //     case CurlingMatchPhase.StoneSelectionConfirm:
        //         StartThrowSequence();
        //         break;

        //     case CurlingMatchPhase.PostThrowResult:
        //         HandlePostThrow();
        //         break;

        //     case CurlingMatchPhase.ObstaclePlacement:
        //         FinalizeTurn();
        //         break;

        //     case CurlingMatchPhase.FinalResults:
        //         Debug.Log("Match complete.");
        //         break;
        // }
    }

    private void ShowTeamSplash()
    {
        CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.TeamSplash);
    }

    private void BeginStoneSelection()
    {
        CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.StoneSelection);
    }

    public void ConfirmStoneSelection()
    {
        CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.StoneSelectionConfirm);
        // gameManager.stoneManager.PrepareNextStone();
    }

    private void StartThrowSequence()
    {
        CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.CurlingAimControlsPhase);
        // CurlingStone currentStone = gameManager.stoneManager.currentStone;
    }

    private void HandlePostThrow()
    {
        // You could branch here based on game rules
        // if (playerManager.GetCurrentTeam() == 0) // Example condition
        // {
        //     CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.ObstacleSelection);
        // }
        // else
        // {
        //     FinalizeTurn();
        // }
    }

    public void ConfirmObstaclePlacement()
    {
        CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.ObstaclePlacement);
    }

    private void FinalizeTurn()
    {
        // playerManager.NextPlayer();

        // if (playerManager.AllStonesThrown())
        // {
        //     CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.FinalResults);
        // }
        // else
        // {
        //     CurlingMatchPhaseManager.Instance.SetPhase(CurlingMatchPhase.TeamSplash);
        // }
    }
}