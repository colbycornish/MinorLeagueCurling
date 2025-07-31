using UnityEngine;
using System.Collections.Generic;

public enum CurlingGameType
{
    /// Should only be used at the start
    Default,
    RollingTotal, // scores cumulate every turn
    WinnerTakeAll, // after all stones are thrown, only the closest player scores
    HouseParty, // points are only scored if the stone is within the target zone
    Skins, // Last thrower must score at least two points to win.
    HotShot, // Trick shot points based on the situation
    Practice // throw as many stones as you can
}

public struct CurlingGameSettings {
    //Variable declaration
    [Header("Base Settings")]
    public int roundsPerGame; // A round is composed of one throw per team
    public int turnsPerTeamPerRound;
    public int totalTurnsPerGame;// There are two teams, so the max is 1
    public int maxStonesPerTeam;

    [Header("Game Type")]
    public CurlingGameType gameType;
    public bool isPracticeMode;
    public bool isTimedMode;
    public float timeLimit; // in seconds if timed mode

    [Header("Additional")]
    public bool enableObstaclePlacementByPlayer;
    public bool enableObstaclePlacementByEnvironment;

    //Constructor (not necessary, but helpful)
    public void SetSettings(
        int roundsPerGame = 5,
        int turnsPerTeamPerRound = 1,
        int maxStonesPerTeam = 5,
        bool isPracticeMode = false,
        bool isTimedMode = false,
        float timeLimit = 0.0f,
        bool enableObstaclePlacementByPlayer = false,
        bool enableObstaclePlacementByEnvironment = false
    )
    {
        this.roundsPerGame = roundsPerGame;
        this.turnsPerTeamPerRound = turnsPerTeamPerRound;
        this.maxStonesPerTeam = maxStonesPerTeam;
        this.isPracticeMode = isPracticeMode;
        this.isTimedMode = isTimedMode;
        this.timeLimit = timeLimit;
        this.enableObstaclePlacementByPlayer = enableObstaclePlacementByPlayer;
        this.enableObstaclePlacementByEnvironment = enableObstaclePlacementByEnvironment;

        // set the total turns per game based on the rounds and turns per team
        int totalTurnsPerRound = turnsPerTeamPerRound * 2;
        this.totalTurnsPerGame = roundsPerGame * totalTurnsPerRound;
    }
}
