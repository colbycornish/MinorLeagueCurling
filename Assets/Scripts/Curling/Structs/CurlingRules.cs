using UnityEngine;
using System.Collections.Generic;

public struct CurlingRules
{
    public CurlingRulesGameMode gameMode;
    public CurlingRulesOpponent opponent;
    public CurlingRulesDifficulty difficulty;
    public CurlingRulesScoring scoring;
    public CurlingRulesObstacles obstacles;
    public CurlingRulesThrowClock throwClock;

    public void SetDefaultRules(){
        gameMode.SetGameMode(isMatch: true);
        opponent.SetOpponentType(isAI: true);
        difficulty.SetDifficulty(isDefault: true);
        scoring.SetScoringMode(isClassic: true);
        obstacles.SetFrequency(isNone: true);
        obstacles.DisableObstaclePlacementByPlayer();
        obstacles.DisableObstaclePlacementByCourse();
        obstacles.EnableObstacles();
        throwClock.DisableThrowClock();
    }


    public void ResetAll(){
        SetDefaultRules();
    }
}


/// <summary>
/// Game Mode
/// </summary>
public struct CurlingRulesGameMode
{
    public enum CurlingRulesGameModeType
    {
        Practice, // scores cumulate every turn
        Match
    }

    public CurlingRulesGameModeType currentType;

    public void SetGameMode(
        bool isPractice = false,
        bool isMatch = false
    ){

        if (isPractice) this.currentType = CurlingRulesGameModeType.Practice;
        else if (isMatch) this.currentType = CurlingRulesGameModeType.Match;
        else {
            this.currentType = CurlingRulesGameModeType.Match;
        }
    }
}

/// <summary>
/// Opponent
/// </summary>
public struct CurlingRulesOpponent
{
    public enum CurlingRulesOpponentType
    {
        None,
        AI,
        Local,
        Online,
        Ghost // attempts to mimic your previous throws path
    }
    public CurlingRulesOpponentType opponentType;

    public void SetOpponentType(
        bool isAI = false,
        bool isGhost = false,
        bool isLocal = false,
        bool isNone = false,
        bool isOnline = false
    ){

        if (isAI) this.opponentType = CurlingRulesOpponentType.AI;
        else if (isGhost) this.opponentType = CurlingRulesOpponentType.Ghost;
        else if (isLocal) this.opponentType = CurlingRulesOpponentType.Local;
        else if (isOnline) this.opponentType = CurlingRulesOpponentType.Online;
        else if (isNone) this.opponentType = CurlingRulesOpponentType.None;
        else {
            this.opponentType = CurlingRulesOpponentType.None;
        }
    }
}

/// <summary>
/// Difficulty
/// </summary>
public struct CurlingRulesDifficulty
{
    public enum CurlingRulesDifficultyType
    {
        Easy,
        Medium,
        Default,
        Hard,
        Terror
    }

    public CurlingRulesDifficultyType currentMode;

    public void SetDifficulty(
        bool isEasy = false,
        bool isMedium = false,
        bool isHard = false,
        bool isTerror = false,
        bool isDefault = false
    ){

        if (isEasy) this.currentMode = CurlingRulesDifficultyType.Easy;
        else if (isMedium) this.currentMode = CurlingRulesDifficultyType.Medium;
        else if (isHard) this.currentMode = CurlingRulesDifficultyType.Hard;
        else if (isTerror) this.currentMode = CurlingRulesDifficultyType.Terror;
        else if (isDefault) this.currentMode = CurlingRulesDifficultyType.Default;
        else {
            this.currentMode = CurlingRulesDifficultyType.Default;
        }
    }
}


/// <summary>
/// Scoring
/// </summary>
public struct CurlingRulesScoring
{
    public enum CurlingRulesScoringMode
    {
        Classic, // after all stones are thrown, only the closest player scores
        RollingTotal, // scores cumulate every turn
        HouseParty, // points are only scored if the stone is within the target zone
        Skins, // Last thrower must score at least two points to win.
        HotShot, // Trick shot points based on the situation
        Brawl // Stones will only be eligable to score if they hit something
    }

    public CurlingRulesScoringMode currentMode;

    public void SetScoringMode(
        bool isClassic = false,
        bool isRollingTotal = false,
        bool isHouseParty = false,
        bool isSkins = false,
        bool isHotShot = false,
        bool isBrawl = false
    ){

        if (isRollingTotal) this.currentMode = CurlingRulesScoringMode.RollingTotal;
        if (isHouseParty) this.currentMode = CurlingRulesScoringMode.HouseParty;
        if (isSkins) this.currentMode = CurlingRulesScoringMode.Skins;
        if (isHotShot) this.currentMode = CurlingRulesScoringMode.HotShot;
        if (isBrawl) this.currentMode = CurlingRulesScoringMode.Brawl;
        else if (isClassic) this.currentMode = CurlingRulesScoringMode.Classic;
        else {
            this.currentMode = CurlingRulesScoringMode.Classic;
        }
    }
}

/// <summary>
/// obstacles
/// </summary>
public struct CurlingRulesObstacles
{
    public enum CurlingRulesObstacleFrequencyType
    {
        None,
        Low,
        Medium,
        High
    }

    public CurlingRulesObstacleFrequencyType frequency;
    public bool enableObstaclePlacementByPlayer;
    public bool enableObstaclePlacementByCourse;
    public bool enableObstacles;

    public void SetFrequency(
        bool isNone = false,
        bool isLow = false,
        bool isMedium = false,
        bool isHigh = false
    ){

        if (isLow) this.frequency = CurlingRulesObstacleFrequencyType.Low;
        else if (isMedium) this.frequency = CurlingRulesObstacleFrequencyType.Medium;
        else if (isHigh) this.frequency = CurlingRulesObstacleFrequencyType.High;
        else if (isNone) this.frequency = CurlingRulesObstacleFrequencyType.None;
        else {
            this.frequency = CurlingRulesObstacleFrequencyType.None;
        }
    }

    ///
    /// Enable or disable obstacles
    /// 
    public void EnableObstaclePlacementByPlayer(){
        enableObstaclePlacementByPlayer = true;
    }

    public void DisableObstaclePlacementByPlayer(){
        enableObstaclePlacementByPlayer = false;
    }

    public void EnableObstaclePlacementByCourse(){
        enableObstaclePlacementByCourse = true;
    }

    public void DisableObstaclePlacementByCourse(){
        enableObstaclePlacementByCourse = false;
    }

    public void EnableObstacles(){
        enableObstacles = true;
        // enableObstaclePlacementByCourse = true;
    }

    public void DisableObstacles(){
        enableObstaclePlacementByPlayer = false;
        enableObstaclePlacementByCourse = false;
        enableObstacles = false;
    }
}

/// <summary>
/// throw clock
/// </summary>
public struct CurlingRulesThrowClock
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

    public void SetTimerLength(int timeLength = 10){
        timeAllowedInSeconds = timeLength;
    }

    public void EnableThrowClock(){
        enabled = true;
        // if (timeAllowedInSeconds == null){
        timeAllowedInSeconds = 30;
        // }
    }

    public void DisableThrowClock(){
        enabled = false;
    }
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