using System.Collections.Generic;
using UnityEngine;
using System;

public class CurlingPlayerManagerV2 : MonoBehaviour
{
    public int totalStonesPerEnd = 10;
    public List<CurlingTeamData> teams = new List<CurlingTeamData>();
    public event Action<List<CurlingTeamData>> OnTeamDataChanged;
    private int currentStoneIndex = 0;
    private int currentTeamIndex = 0;
    private int[] matchScore = new int[2];

    public void PreparePlayers()
    {
        currentStoneIndex = 0;
        currentTeamIndex = 0;
    }

    // public bool AllStonesThrown() => currentStoneIndex >= totalStonesPerEnd;

    public void NextPlayer()
    {
        currentStoneIndex++;
        currentTeamIndex = 1 - currentTeamIndex;
    }

    public int GetCurrentTeamIndex() => currentTeamIndex;
    public CurlingTeamData GetCurrentTeam() => teams[currentTeamIndex];


    public void UpdateScore(int[] endScore)
    {
        matchScore[0] += endScore[0];
        matchScore[1] += endScore[1];
        Debug.Log($"Match Score: Team A: {matchScore[0]}, Team B: {matchScore[1]}");
    }

    public void SetTeams(CurlingTeamData team01, CurlingTeamData team02)
    {
        teams.Clear();
        teams.Add(team01);
        teams.Add(team02);
        Debug.Log($"Teams set: {team01.teamName} vs {team02.teamName}");
        OnTeamDataChanged?.Invoke(teams);
    }

    public void SetDemoTeams()
    {
        CurlingTeamData teamA = new CurlingTeamData
        {
            teamName = "Team Blue",
            teamId = "team_blue",
            thrower = new CurlingPlayer { name = "Thrower A", characterId = "char_a", isThrower = true, isSweeper = false },
            sweeperLeft = new CurlingPlayer { name = "Sweeper Left A", characterId = "sweeper_left_a", isThrower = false, isSweeper = true },
            sweeperRight = new CurlingPlayer { name = "Sweeper Right A", characterId = "sweeper_right_a", isThrower = false, isSweeper = true }
        };

        CurlingTeamData teamB = new CurlingTeamData
        {
            teamName = "Team Red",
            teamId = "team_red",
            thrower = new CurlingPlayer { name = "Thrower B", characterId = "char_b", isThrower = true, isSweeper = false },
            sweeperLeft = new CurlingPlayer { name = "Sweeper Left B", characterId = "sweeper_left_b", isThrower = false, isSweeper = true },
            sweeperRight = new CurlingPlayer { name = "Sweeper Right B", characterId = "sweeper_right_b", isThrower = false, isSweeper = true }
        };

        SetTeams(teamA, teamB);
    }
}
