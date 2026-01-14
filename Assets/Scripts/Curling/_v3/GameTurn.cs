using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// </summary>

namespace CurlingManagersV3
{
    public class GameTurn : MonoBehaviour
    {
        [Header("Helpful References")]
        public int numberOfHomeTeamTurnsCompleted = 0;
        public int numberOfAwayTeamTurnsCompleted = 0;
        public int currentTurnCount = 0;
        public int maxTurnCount = 10;

        /// <summary>
        /// Set Game Turn
        /// </summary>
        public void SetGameTurn(
            bool isHome = false,
            bool isAway = false
        ){

            if (isHome){
                CurlingManager._instance.Parameters.Turn.CurrentTurn = CurlingGameTurnType.Home;
            } 
            else if (isAway){
                CurlingManager._instance.Parameters.Turn.CurrentTurn = CurlingGameTurnType.Away;
            }
            else {
                Debug.LogWarning("GameTurn: NextTurn() - currentTurn is None, defaulting to Home");
                CurlingManager._instance.Parameters.Turn.CurrentTurn = CurlingGameTurnType.None;
            }
        }

        public void NextTurn(){
            currentTurnCount++;
            if (CurlingManager._instance.Parameters.Turn.CurrentTurn == CurlingGameTurnType.Home){
                numberOfHomeTeamTurnsCompleted++;
                
                SetGameTurn(
                    isHome: false,
                    isAway: true
                );
                Debug.LogWarning("GameTurn: Away Team Turn");
            } 
            else if (CurlingManager._instance.Parameters.Turn.CurrentTurn == CurlingGameTurnType.Away){
                numberOfAwayTeamTurnsCompleted++;

                SetGameTurn(isHome: true, isAway: false);
                Debug.LogWarning("GameTurn: Home Team Turn");
            }
            else {
                Debug.LogWarning("GameTurn: NextTurn() - currentTurn is None, defaulting to Home");
                SetGameTurn(isHome: true, isAway: false);
            }
        }


        /// <summary>
        /// Inquire About Current Turn
        /// </summary>
        public bool IsCurrentTurnTheLastTurn(){
            return currentTurnCount == maxTurnCount;
        }

        public bool IsThereAnotherTurnAfterThisOne(){
            return currentTurnCount + 1 <= maxTurnCount;
        }

        /// <summary>
        /// Reset
        /// </summary>
        public void Reset(){
            SetGameTurn(isHome: true);
            numberOfHomeTeamTurnsCompleted = 0;
            numberOfAwayTeamTurnsCompleted = 0;
        }

    }
}