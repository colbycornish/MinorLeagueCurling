using System.Collections.Generic;
using UnityEngine;
using System;

public class CurlingPlayerManagerV2 : MonoBehaviour
{
    public int totalStonesPerEnd = 10;
    public List<CurlingTeamData> teams = new List<CurlingTeamData>();
    public event Action<List<CurlingTeamData>> OnTeamDataChanged;
    public int currentStoneIndex = 0;
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
            teamName = "Team A",
            teamId = "team_a",
            thrower = new CurlingPlayer { name = "Thrower A", characterId = "char_a", isThrower = true, isSweeper = false },
            sweeperLeft = new CurlingPlayer { name = "Sweeper Left A", characterId = "sweeper_left_a", isThrower = false, isSweeper = true },
            sweeperRight = new CurlingPlayer { name = "Sweeper Right A", characterId = "sweeper_right_a", isThrower = false, isSweeper = true }
        };

        CurlingTeamData teamB = new CurlingTeamData
        {
            teamName = "Team B",
            teamId = "team_b",
            thrower = new CurlingPlayer { name = "Thrower B", characterId = "char_b", isThrower = true, isSweeper = false },
            sweeperLeft = new CurlingPlayer { name = "Sweeper Left B", characterId = "sweeper_left_b", isThrower = false, isSweeper = true },
            sweeperRight = new CurlingPlayer { name = "Sweeper Right B", characterId = "sweeper_right_b", isThrower = false, isSweeper = true }
        };

        SetTeams(teamA, teamB);
    }
}
