using UnityEngine;
using System.Collections.Generic;

/*
Match Structure
    - String: Match ID
    - String: Match Name
    - String: Match Type (e.g., "Friendly", "Tournament")
    - List<GameObject>: Teams
    - Match_Settings
        - Int: Max Games
        - Int: Max Rounds Per Game
        - Int: Max Turns Per Round
        - bool: isPracticeMode
        - bool: isTimedMode
        - float: timeLimit (if timed mode)    
    - Match Score
        - Int: Team 1 Total Score
        - Int: Team 2 Total Score
        - bool: isFinal
        - bool: hasStarted
        - bool: hasEnded
        - bool: hasPaused
        - List<GameObject>: Game Results
            - Game Result Structure
                - String: Game ID
                - Int: Team 1 Score
                - Int: Team 2 Score
                - bool: isFinal
*/

public struct CurlingMatchSettings
{
    //Variable declaration
    //Note: I'm explicitly declaring them as public, but they are public by default. You can use private if you choose.
    [Header("Base Settings")]
    public int gamesPerMatch;
    public int roundsPerGame;
    public int turnsPerTeamPerRound; // There are two teams, so the max is 1
    public int maxStonesPerTeam;

    [Header("Match Type")]
    public string matchType;
    public bool isPracticeMode;
    public bool isTimedMode;
    public float timeLimit;

    //Constructor (not necessary, but helpful)
    public void SetSettings(
        int gamesPerMatch = 1,
        int roundsPerGame = 5,
        int turnsPerTeamPerRound = 1,
        int maxStonesPerTeam = 5,
        string matchType = "Friendly",
        bool isPracticeMode = false,
        bool isTimedMode = false,
        float timeLimit = 0.0f
    )
    {
        this.gamesPerMatch = gamesPerMatch;
        this.roundsPerGame = roundsPerGame;
        this.turnsPerTeamPerRound = turnsPerTeamPerRound;
        this.maxStonesPerTeam = maxStonesPerTeam;
        this.matchType = matchType;
        this.isPracticeMode = isPracticeMode;
        this.isTimedMode = isTimedMode;
        this.timeLimit = timeLimit; // 0 means no time limit
    }
}
