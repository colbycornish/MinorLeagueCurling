using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// </summary>

namespace CurlingManagersV3
{
    public class GameTurn : MonoBehaviour
    {

        /// <summary>
        /// Set Game Turn
        /// </summary>
        public void SetGameTurn(
            bool isHome = false,
            bool isAway = false
        ){

            if (isHome){
                CurlingManager._instance.Parameters.Turn.CurrentTurn = CurlingGameTurnType.Home;
                Debug.LogWarning("GameTurn: Home Team Turn");
            } 
            else if (isAway){
                CurlingManager._instance.Parameters.Turn.CurrentTurn = CurlingGameTurnType.Away;
                Debug.LogWarning("GameTurn: Away Team Turn");
            }
            else {
                Debug.LogWarning("GameTurn: NextTurn() - currentTurn is None, defaulting to Home");
                CurlingManager._instance.Parameters.Turn.CurrentTurn = CurlingGameTurnType.None;
            }
        }

        public void NextTurn(){
            CurlingManager._instance.Parameters.Turn.CurrentTurnCount++; // Reset current stone at the beginning of the next turn

            // currentTurnCount++;
            if (CurlingManager._instance.Parameters.Turn.CurrentTurn == CurlingGameTurnType.Home){

                // numberOfHomeTeamTurnsCompleted++;
                CurlingManager._instance.Parameters.Turn.NumberOfHomeTeamTurnsCompleted++;
                
                SetGameTurn(isHome: false, isAway: true);
                
            } 
            else if (CurlingManager._instance.Parameters.Turn.CurrentTurn == CurlingGameTurnType.Away){
                
                CurlingManager._instance.Parameters.Turn.NumberOfAwayTeamTurnsCompleted++;
                SetGameTurn(isHome: true, isAway: false);
                
            }
            else {
                Debug.LogWarning("GameTurn: NextTurn() - currentTurn is None, defaulting to Home");
                SetGameTurn(isHome: true, isAway: false);
            }
        }

        public void UpdateCurrentTeam(){
            CurlingManager._instance.Parameters.Teams.currentTeam = 
                CurlingManager._instance.Parameters.Turn.CurrentTurn == CurlingGameTurnType.Home
                    ? CurlingManager._instance.Parameters.Teams.teamHome
                    : CurlingManager._instance.Parameters.Teams.teamAway;
        }



        /// <summary>
        /// Inquire About Current Turn
        /// </summary>
        public bool IsCurrentTurnTheLastTurn(){
            return CurlingManager._instance.Parameters.Turn.CurrentTurnCount + 1 == 
                CurlingManager._instance.Parameters.Turn.MaxTurnCount;
        }

        public bool IsThereAnotherTurnAfterThisOne(){
            return CurlingManager._instance.Parameters.Turn.CurrentTurnCount + 1 < 
            CurlingManager._instance.Parameters.Turn.MaxTurnCount;
        }

        /// <summary>
        /// Reset
        /// </summary>
        public void Reset(){
            SetGameTurn(isHome: true);
            CurlingManager._instance.Parameters.Turn.CurrentTurnCount = 0;
            CurlingManager._instance.Parameters.Turn.NumberOfHomeTeamTurnsCompleted = 0;
            CurlingManager._instance.Parameters.Turn.NumberOfAwayTeamTurnsCompleted = 0;
            CurlingManager._instance.Parameters.Turn.MaxTurnCount = 10;
        }

    }
}