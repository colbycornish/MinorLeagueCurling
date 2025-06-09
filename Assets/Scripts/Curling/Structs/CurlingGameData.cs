using UnityEngine;
using System.Collections.Generic;

/*
Game Structure
    - Game_Score
        - Int: Team 1 Score
        - Int: Team 2 Score
        - bool: isFinal
    - Game_Settings
        - Int: Max Rounds
        - Int: Max Turns Per Round
        - Int: Max Stones Per Team
        - bool: isPracticeMode
        - bool: isTimedMode
        - float: timeLimit (if timed mode)
    - bool: hasStarted
    - bool: hasEnded
    - bool: hasPaused
    - Int: Current Round
    - Int: Max Rounds 
    - Int: Round Count
    - Int: Turn Count
*/

public struct CurlingGameData {
    //Variable declaration
    public string gameId; // Unique identifier for the game
    public bool hasStarted;
    public bool hasEnded;
    public bool isPaused;
    public int roundCurrent;
    public int turnCurrent;

    public CurlingGameScore gameScore;
    public CurlingGameSettings gameSettings;

    //Constructor (not necessary, but helpful)
    public void SetGameData(
        string gameId = "Game_001",
        bool hasStarted = false,
        bool hasEnded = false,
        bool isPaused = false,
        int roundCurrent = 1,
        int turnCurrent = 0
    ) {
        this.gameId = gameId;
        this.hasStarted = hasStarted;
        this.hasEnded = hasEnded;
        this.isPaused = isPaused;
        this.roundCurrent = roundCurrent;
        this.turnCurrent = turnCurrent;

        
    }

    // Round & Turn Management
    public void SetRoundCurrent(int roundCurrent) {
        this.roundCurrent = roundCurrent;
    }
    public void SetTurnCurrent(int turnCurrent) {
        this.turnCurrent = turnCurrent;
    }

    // Score & Settings Management
    public void SetGameScore(CurlingGameScore gameScore) {
        this.gameScore = gameScore;
    }
    public void SetGameSettings(CurlingGameSettings gameSettings) {
        this.gameSettings = gameSettings;
    }

    // public void SetSettings 

    // Game State Management
    public void SetHasStarted(bool hasStarted) {
        this.hasStarted = hasStarted;
    }
    public void SetHasEnded(bool hasEnded) {
        this.hasEnded = hasEnded;
    }
    public void SetIsPaused(bool isPaused) {
        this.isPaused = isPaused;
    }

    // Game Data Management
    public void ResetGame() {
        this.hasStarted = false;
        this.hasEnded = false;
        this.isPaused = false;
        this.roundCurrent = 1;
        this.turnCurrent = 0;
        this.gameScore.setScore(0, 0, false);
    }    
}

public struct CurlingGameScore {
    //Variable declaration
    //Note: I'm explicitly declaring them as public, but they are public by default. You can use private if you choose.
    public int team01Score;
    public string team01Id;
    public int team02Score;
    public string team02Id;
    public bool isFinal;
    
    //Constructor (not necessary, but helpful)
    public void setScore(
        int team01Score = 0,
        int team02Score = 0,
        bool isFinal = false
    ) {
        this.team01Score = team01Score;
        this.team02Score = team02Score;
        this.isFinal = isFinal;
    }
    public void SetTeam01Score(int score) {
        this.team01Score = score;
    }
    public void SetTeam02Score(int score) {
        this.team02Score = score;
    }
    public void SetIsFinal(bool isFinal) {
        this.isFinal = isFinal;
    }
    public void ResetScore() {
        this.team01Score = 0;
        this.team02Score = 0;
        this.isFinal = false;
    }
}

public struct CurlingGameSettings {
    //Variable declaration
    public int roundsPerGame; // A round is composed of one throw per team
    public int turnsPerTeamPerRound; // There are two teams, so the max is 1
    public int maxStonesPerTeam;
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
    ) {
        this.roundsPerGame = roundsPerGame;
        this.turnsPerTeamPerRound = turnsPerTeamPerRound;
        this.maxStonesPerTeam = maxStonesPerTeam;
        this.isPracticeMode = isPracticeMode;
        this.isTimedMode = isTimedMode;
        this.timeLimit = timeLimit;
    }
}
