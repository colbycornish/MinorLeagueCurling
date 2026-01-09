using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// Scoring
/// </summary>
namespace Curling.Rules {
    public struct Scoring
    {
        public CurlingGameScoreType currentMode;

        public void SetScoringMode(
            bool isClassic = false,
            bool isRollingTotal = false,
            bool isHouseParty = false,
            bool isSkins = false,
            bool isHotShot = false,
            bool isBrawl = false
        ){

            if (isRollingTotal) this.currentMode = CurlingGameScoreType.RollingTotal;
            if (isHouseParty) this.currentMode = CurlingGameScoreType.HouseParty;
            if (isSkins) this.currentMode = CurlingGameScoreType.Skins;
            if (isHotShot) this.currentMode = CurlingGameScoreType.HotShot;
            if (isBrawl) this.currentMode = CurlingGameScoreType.Brawl;
            else if (isClassic) this.currentMode = CurlingGameScoreType.Classic;
            else {
                this.currentMode = CurlingGameScoreType.Classic;
            }
        }
    }
}
