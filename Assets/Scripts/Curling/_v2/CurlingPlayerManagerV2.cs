using System.Collections.Generic;
using UnityEngine;

public class CurlingPlayerManagerV2 : MonoBehaviour
{
    public int totalStonesPerEnd = 10;
    private int currentStoneIndex = 0;
    private int currentTeam = 0;
    private int[] matchScore = new int[2];

    public void PreparePlayers()
    {
        currentStoneIndex = 0;
        currentTeam = 0;
    }

    public bool AllStonesThrown() => currentStoneIndex >= totalStonesPerEnd;

    public void NextPlayer()
    {
        currentStoneIndex++;
        currentTeam = 1 - currentTeam;
    }

    public int GetCurrentTeam() => currentTeam;

    public void UpdateScore(int[] endScore)
    {
        matchScore[0] += endScore[0];
        matchScore[1] += endScore[1];
        Debug.Log($"Match Score: Team A: {matchScore[0]}, Team B: {matchScore[1]}");
    }
}
