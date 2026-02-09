using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Difficulty
/// </summary>
/// 
namespace Curling.Rules {

    [Serializable]
    public class Difficulty
    {
        public CurlingGameDifficultyType currentMode;

        // public void SetDifficulty(
        //     bool isEasy = false,
        //     bool isMedium = false,
        //     bool isHard = false,
        //     bool isTerror = false,
        //     bool isDefault = false
        // ){

        //     if (isEasy) this.currentMode = CurlingGameDifficultyType.Easy;
        //     else if (isMedium) this.currentMode = CurlingGameDifficultyType.Medium;
        //     else if (isHard) this.currentMode = CurlingGameDifficultyType.Hard;
        //     else if (isTerror) this.currentMode = CurlingGameDifficultyType.Terror;
        //     else if (isDefault) this.currentMode = CurlingGameDifficultyType.Default;
        //     else {
        //         this.currentMode = CurlingGameDifficultyType.Default;
        //     }
        // }
    }
}

 