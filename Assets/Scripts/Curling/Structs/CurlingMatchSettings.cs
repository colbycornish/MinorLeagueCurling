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


public struct RulesOpponent
{
    public enum CurlingRulesOpponentType
    {
        None,
        AI,
        Local,
        Online,
        Ghost // attempts to mimic your previous throws path
    }
    public CurlingRulesOpponentType opponent;
}

public struct RulesDifficulty
{
    public enum CurlingRulesDifficultyType
    {
        Easy,
        Medium,
        Hard,
        Terror
    }

    public CurlingRulesDifficultyType difficultyRules;
}

/// <summary>
/// Scoring
/// </summary>
public struct RulesMode
{
    public enum CurlingRulesModeType
    {
        Practice, // scores cumulate every turn
        Match
    }

    public CurlingRulesModeType currentMode;
    
    
}


/// <summary>
/// Scoring
/// </summary>
public struct RulesScoring
{
    public enum CurlingRulesScoringType
    {
        RollingTotal, // scores cumulate every turn
        WinnerTakeAll, // after all stones are thrown, only the closest player scores
        HouseParty, // points are only scored if the stone is within the target zone
        Skins, // Last thrower must score at least two points to win.
        HotShot, // Trick shot points based on the situation
        Brawl // Stones will only be eligable to score if they hit something
    }

    public CurlingRulesScoringType currentScoringRules;
    // public bool scoreOn


    public void OnTurnEnd()
    {

    }
}

/// <summary>
/// obstacles
/// </summary>
public struct RulesObstacles
{
    public enum CurlingRulesObstacleFrequencyType
    {
        None,
        Low,
        Medium,
        High
    }

    public CurlingRulesObstacleFrequencyType frequency;
}

/// <summary>
/// throw clock
/// </summary>
public struct RulesThrowClock
{
    public enum CurlingRulesThrowClockType
    {
        None,
        TenSeconds,
        TwentySeconds,
        ThirtySeconds
    }

    public bool enabled;
    public int timeAllowedInSeconds;
}


/// Mode
/// - Practice
/// - Match

/// Throw Clock: Unlimited

/// Obstacles
/// - None
/// - Low
/// - Med
/// - High
/// 
/// Difficulty
/// - easy
/// - med
/// - hard
/// - death
/// 
/// Opponent
/// AI
/// Local
/// Online
/// None (only an option if in practice mode)
/// 
/// NumberOfGames
/// - 1
/// - 3
/// - 5
/// 
/// Scoring
///     RollingTotal, // scores cumulate every turn
///     WinnerTakeAll, // after all stones are thrown, only the closest player scores
///     HouseParty, // points are only scored if the stone is within the target zone
///     Skins, // Last thrower must score at least two points to win.
///     HotShot // Trick shot points based on the situation