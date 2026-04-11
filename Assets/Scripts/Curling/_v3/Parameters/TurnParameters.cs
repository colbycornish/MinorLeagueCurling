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

        [SerializeField]
        private int _CurrentTurnCount = 0;
        public ref int CurrentTurnCount => ref _CurrentTurnCount;    

        [SerializeField]
        private int _NumberOfHomeTeamTurnsCompleted = 0;
        public ref int NumberOfHomeTeamTurnsCompleted => ref _NumberOfHomeTeamTurnsCompleted;

        [SerializeField]
        private int _NumberOfAwayTeamTurnsCompleted = 0;
        public ref int NumberOfAwayTeamTurnsCompleted => ref _NumberOfAwayTeamTurnsCompleted;

        [SerializeField]
        private int _MaxTurnCount = 10;
        public ref int MaxTurnCount => ref _MaxTurnCount;

        public bool IsCurrentTurnTheLastTurn => (_CurrentTurnCount + 1) == _MaxTurnCount;
        
        public bool IsThereAnotherTurnAfterThisOne => _CurrentTurnCount + 1 + 1 <= _MaxTurnCount;

        // // public CurlingGameTurnType currentTurn;

        // [Header("Helpful References")]
        // public int numberOfHomeTeamTurnsCompleted = 0;
        // public int numberOfAwayTeamTurnsCompleted = 0;
        // public int currentTurnCount = 0;
        // public int maxTurnCount = 10;
        
    }
}



// /* TALKING ***********************************************************************************************************************/
