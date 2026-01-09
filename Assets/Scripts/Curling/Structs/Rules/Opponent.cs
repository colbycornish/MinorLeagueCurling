using UnityEngine;
using System.Collections.Generic;


/// <summary>
/// Opponent
/// </summary>
namespace Curling.Rules {
    public struct Opponent
    {

        public CurlingOpponentType opponentType;

        public void SetOpponentType(
            bool isAI = false,
            bool isGhost = false,
            bool isLocal = false,
            bool isNone = false,
            bool isOnline = false
        ){

            if (isAI) this.opponentType = CurlingOpponentType.AI;
            else if (isGhost) this.opponentType = CurlingOpponentType.Ghost;
            else if (isLocal) this.opponentType = CurlingOpponentType.Local;
            else if (isOnline) this.opponentType = CurlingOpponentType.Online;
            else if (isNone) this.opponentType = CurlingOpponentType.None;
            else {
                this.opponentType = CurlingOpponentType.None;
            }
        }
    }
}
