using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the end game scoring for a curling match.
/// This class handles:
/// - Adding stones to the end
/// - Calculating the score based on the stones' positions
/// - Resetting the end for a new round
/// </summary>

public class CurlingEndGameManagerV2 : MonoBehaviour
{
    public GameObject targetZone;

    // TODO: This might not be necessary. We might be able to pull the objects from the 
    // list of stones, instead of adding to / creating a new list. 
    private List<GameObject> stonesThisEnd = new List<GameObject>();
    private int[] endScore = new int[2];

    public void SetTargetZone(
        GameObject zone
    )
    {
        targetZone = zone;
    }

    public void ResetCurlingGame()
    {
        stonesThisEnd.Clear();
    }

    public void AddStone(GameObject stone)
    {
        stonesThisEnd.Add(stone);
    }

    public void CalculateScore()
    {
        stonesThisEnd.Sort((a, b) =>
            Vector3.Distance(Vector3.zero, a.transform.position)
            .CompareTo(Vector3.Distance(Vector3.zero, b.transform.position)));

        endScore = new int[2];
        if (stonesThisEnd.Count > 0)
        {
            CurlingStone stone = stonesThisEnd[0].GetComponent<CurlingStone>();
            endScore[stone.teamId_i]++;
        }

        Debug.Log($"Scoring End: Team A: {endScore[0]}, Team B: {endScore[1]}");
    }

    public int[] GetScore() => endScore;
}
