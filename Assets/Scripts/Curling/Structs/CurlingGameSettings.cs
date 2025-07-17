using UnityEngine;
using System.Collections.Generic;



public struct CurlingGameSettings {
    //Variable declaration
    [Header("Base Settings")]
    public int roundsPerGame; // A round is composed of one throw per team
    public int turnsPerTeamPerRound;
    public int totalTurnsPerGame;// There are two teams, so the max is 1
    public int maxStonesPerTeam;

    [Header("Game Type")]
    public string gameType;
    public bool isPracticeMode;
    public bool isTimedMode;
    public float timeLimit; // in seconds if timed mode

    //Constructor (not necessary, but helpful)
    public void SetSettings(
        int roundsPerGame = 5,
        int turnsPerTeamPerRound = 1,
        int maxStonesPerTeam = 5,
        bool isPracticeMode = false,
        bool isTimedMode = false,
        float timeLimit = 0.0f // 0 means no time limit
    )
    {
        this.roundsPerGame = roundsPerGame;
        this.turnsPerTeamPerRound = turnsPerTeamPerRound;
        this.maxStonesPerTeam = maxStonesPerTeam;
        this.isPracticeMode = isPracticeMode;
        this.isTimedMode = isTimedMode;
        this.timeLimit = timeLimit;

        // set the total turns per game based on the rounds and turns per team
        int totalTurnsPerRound = turnsPerTeamPerRound * 2;
        this.totalTurnsPerGame = roundsPerGame * totalTurnsPerRound;
    }
}
