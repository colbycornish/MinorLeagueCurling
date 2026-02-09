using UnityEngine;
using System.Collections.Generic;
using System;

/// <summary>
/// Game Mode
/// </summary>
namespace Curling.Rules {
    
    [Serializable]
    public class CurlingRulesGameMode
    {
        // public enum CurlingRulesGameModeType
        // {
        //     Practice, // scores cumulate every turn
        //     Match
        // }

        public CurlingGameModeType currentType;

        // public void SetGameMode(
        //     bool isPractice = false,
        //     bool isMatch = false
        // ){

        //     if (isPractice) this.currentType = CurlingGameModeType.Practice;
        //     else if (isMatch) this.currentType = CurlingGameModeType.Match;
        //     else {
        //         this.currentType = CurlingGameModeType.Match;
        //     }
        // }
    }
}
