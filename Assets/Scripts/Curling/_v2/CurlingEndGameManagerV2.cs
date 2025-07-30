using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary>
/// Manages the end game scoring for a curling match.
/// This class handles:
/// - Adding stones to the end
/// - Calculating the score based on the stones' positions
/// - Resetting the end for a new round
/// </summary>

public class CurlingEndGameManagerV2 : MonoBehaviour
{
    [Header("Game Objects")]
    public GameObject targetZone;
    // TODO: This might not be necessary. We might be able to pull the objects from the 
    // list of stones, instead of adding to / creating a new list.

    [Header("Score")]
    private int[] endScore = new int[2];

    // [Header("Update Functions")]
    public event Action<CurlingGameScore> OnCurlingGameScoreChanged;

    

    /// <summary>
    /// Setup functions
    /// </summary>

    public void SetTargetZone(GameObject zone)
    {
        targetZone = zone;
    }

    /// <summary>
    /// Score Calculation
    /// </summary>

    public void CalculateScore()
    {
        if (targetZone == null) return;
        List<CurlingStone> stonesTeamHome = CurlingGameManagerV2.Instance.stoneManager.stonesTeamHome.Where(a =>
            a.isInPlay == true
        ).ToList();
        List<CurlingStone> stonesTeamAway = CurlingGameManagerV2.Instance.stoneManager.stonesTeamAway.Where(a =>
            a.isInPlay == true
        ).ToList();

        Vector3 targetZoneCenter = targetZone.transform.position;
        stonesTeamHome.Sort((a, b) =>
            Vector3.Distance(targetZoneCenter, a.rb.transform.position)
            .CompareTo(Vector3.Distance(targetZoneCenter, b.rb.transform.position)));

        stonesTeamAway.Sort((a, b) =>
            Vector3.Distance(targetZoneCenter, a.rb.transform.position)
            .CompareTo(Vector3.Distance(targetZoneCenter, b.rb.transform.position)));

        int teamHomeScore = 0;
        int teamAwayScore = 0;

        if (stonesTeamHome.Count > 0 && stonesTeamAway.Count > 0)
        {
            if (IsStoneCloserToTargetThanOtherStone(stonesTeamHome[0], stonesTeamAway[0]) > 0)
            {
                teamHomeScore = CalculatePoints(
                    scoringStones: stonesTeamHome,
                    closestNonScoringStone: stonesTeamAway[0]
                );
                teamAwayScore = 0;
            }
            else if (IsStoneCloserToTargetThanOtherStone(stonesTeamAway[0], stonesTeamHome[0]) > 0)
            {
                teamAwayScore = CalculatePoints(
                    scoringStones: stonesTeamAway,
                    closestNonScoringStone: stonesTeamHome[0]
                );
                teamHomeScore = 0;
            }
        }
        // These just catch if only one team has valid stones
        else if (stonesTeamHome.Count > 0 && stonesTeamAway.Count == 0)
        {
            teamHomeScore = stonesTeamHome.Count;
        }
        else if (stonesTeamAway.Count > 0 && stonesTeamHome.Count == 0)
        {
            teamAwayScore = stonesTeamAway.Count;
        }

        CurlingGameManagerV2.Instance.gameData.score.SetScore(
            teamHomeScore: teamHomeScore,
            teamAwayScore: teamAwayScore,
            isFinal: false
        );

        Debug.Log($"[Scoring] Team Home: {teamHomeScore}, Team Away: {teamAwayScore}");
        OnCurlingGameScoreChanged?.Invoke(CurlingGameManagerV2.Instance.gameData.score);

    }

    /// returns 1 if true
    /// returns -1 if false
    /// returns 0 if equal
    private int IsStoneCloserToTargetThanOtherStone(CurlingStone stoneA, CurlingStone otherStone)
    {
        Vector3 targetZoneCenter = targetZone.transform.position;
        return Vector3.Distance(targetZoneCenter, stoneA.rb.transform.position)
            .CompareTo(Vector3.Distance(targetZoneCenter, otherStone.rb.transform.position));
    }

    private int CalculatePoints(List<CurlingStone> scoringStones, CurlingStone closestNonScoringStone)
    {
        int score = 0;

        foreach (CurlingStone scoringStone in scoringStones)
        {
            if (IsStoneCloserToTargetThanOtherStone(scoringStone, closestNonScoringStone) > 0)
            {
                score += 1;
            }
            else
            {
                return score;
            }
        }

        return score;


    }

    /// <summary>
    /// Helper Functions
    /// </summary>

    public int[] GetScore() => endScore;
    
    public void ResetCurlingGame()
    {
        // stonesThisEnd.Clear();
    }

    // public void AddStone(GameObject stone)
    // {
    //     stonesThisEnd.Add(stone);
    // }
}
