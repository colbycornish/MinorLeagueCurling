using UnityEngine;
using System.Collections.Generic;
using Curling.Rules;



public struct CurlingRules
{
    public Curling.Rules.CurlingRulesGameMode gameMode;
    public Curling.Rules.Opponent opponent;
    public Curling.Rules.Difficulty difficulty;
    public Curling.Rules.Scoring scoring;
    public Curling.Rules.Obstacles obstacles;
    public Curling.Rules.ThrowClock throwClock;

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