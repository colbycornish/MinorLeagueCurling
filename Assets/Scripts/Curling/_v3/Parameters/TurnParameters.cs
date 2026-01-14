using System;
using UnityEngine;

namespace CurlingManagersV3.Parameters
{

    [Serializable]
    public class TurnParameters
    {
        
        [SerializeField]
        private CurlingGameTurnType _CurrentTurn = CurlingGameTurnType.None; // Base launch force (tweak as needed; adjust for distance--may want to bring force down if we shorten the distance)
        public ref CurlingGameTurnType CurrentTurn => ref _CurrentTurn;
 
        // // public CurlingGameTurnType currentTurn;

        // [Header("Helpful References")]
        // public int numberOfHomeTeamTurnsCompleted = 0;
        // public int numberOfAwayTeamTurnsCompleted = 0;
        // public int currentTurnCount = 0;
        // public int maxTurnCount = 10;
        
    }
}



// /* TALKING ***********************************************************************************************************************/
