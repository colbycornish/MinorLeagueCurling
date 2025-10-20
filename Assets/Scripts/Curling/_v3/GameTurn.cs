using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the high-level game manager for a curling game.
/// It orchestrates the flow of the game, including starting new ends,
/// managing player turns, and handling the end of the game.
/// </summary>

namespace CurlingManagersV3
{
    public class GameTurn : MonoBehaviour
    {
        [Header("Turn Settings")]
        public bool isTeamHomeTurn = false;
        public bool isTeamAwayTurn = false;

        public enum CurlingGameTurnType
        {
            Home, // scores cumulate every turn
            Away,
            None
        }

        public CurlingGameTurnType currentTurn;

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
                this.currentTurn = CurlingGameTurnType.Home;
            } 
            else if (isAway){
                this.currentTurn = CurlingGameTurnType.Away;
            }
            else {
                Debug.LogWarning("GameTurn: NextTurn() - currentTurn is None, defaulting to Home");
                this.currentTurn = CurlingGameTurnType.None;
            }
        }

        public void NextTurn(){
            currentTurnCount++;
            if (currentTurn == CurlingGameTurnType.Home){
                numberOfHomeTeamTurnsCompleted++;
                SetGameTurn(isAway: true);
                Debug.LogWarning("GameTurn: Away Team Turn");
            } 
            else if (currentTurn == CurlingGameTurnType.Away){
                numberOfAwayTeamTurnsCompleted++;
                SetGameTurn(isHome: true);
                Debug.LogWarning("GameTurn: Home Team Turn");
            }
            else {
                Debug.LogWarning("GameTurn: NextTurn() - currentTurn is None, defaulting to Home");
                SetGameTurn(isHome: true);
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
        
        public CurlingGameTurnType GetCurrentTurn(){
            return this.currentTurn;
        }

        public bool IsItTheHomeTeamsTurn(){
            return this.currentTurn == CurlingGameTurnType.Home;
        }

        public bool IsItTheAwayTeamsTurn(){
            return this.currentTurn == CurlingGameTurnType.Away;
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