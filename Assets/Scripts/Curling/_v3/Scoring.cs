using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using Unity.VisualScripting;

/// <summary>
/// </summary>

namespace CurlingManagersV3
{
    public class Scoring : MonoBehaviour
    {        
        public event Action<CurlingGameScore> OnCurlingGameScoreChanged;

        /// <summary>
        /// Score Calculation
        /// </summary>

        public void CalculateScore()
        {
            CalculateScoreByClassicRules();
        }

        public void CalculateScoreByClassicRules()
        {
                        if (CurlingManager._instance.Parameters.Course.targetZone == null) return;
            Vector3 targetZoneCenter = CurlingManager._instance.Parameters.Course.targetZone.transform.position;

            List<CurlingStone> stonesTeamHome = FilterAndUpdateStoneListForScoring(
                stones: CurlingManager._instance.Parameters.Stones.stonesTeamHome
            );

            List<CurlingStone> stonesTeamAway = FilterAndUpdateStoneListForScoring(
                stones: CurlingManager._instance.Parameters.Stones.stonesTeamAway
            );

            int teamHomeScore = 0;
            int teamAwayScore = 0;

            if (stonesTeamHome.Count > 0 && stonesTeamAway.Count > 0)
            {
                Debug.Log("Scoring: Both teams have 1 or more stones in play");
                if (IsStoneCloserToTargetThanOtherStone(stonesTeamHome[0], stonesTeamAway[0]))
                {
                    Debug.Log("Home Team Stone is Closer");
                    teamHomeScore = CalculatePoints(
                        scoringStones: stonesTeamHome,
                        closestNonScoringStone: stonesTeamAway[0]
                    );
                    teamAwayScore = 0;
                }
                else if (IsStoneCloserToTargetThanOtherStone(stonesTeamAway[0], stonesTeamHome[0]))
                {
                    Debug.Log("Away Team Stone is Closer");
                    teamAwayScore = CalculatePoints(
                        scoringStones: stonesTeamAway,
                        closestNonScoringStone: stonesTeamHome[0]
                    );
                    teamHomeScore = 0;
                }
            }
            // These just catch if only one team has valid stones
            else if (stonesTeamHome.Count > 0 && stonesTeamAway.Count == 0)
            {
                Debug.Log("Scoring Default Stones: Home");
                teamHomeScore = stonesTeamHome.Count;
            }
            else if (stonesTeamAway.Count > 0 && stonesTeamHome.Count == 0)
            {
                Debug.Log("Scoring Default Stones: Away");
                teamAwayScore = stonesTeamAway.Count;
            }

            // Update the Game Score
            CurlingManager._instance.Parameters.CurrentGameScore.SetScore(
                teamHomeScore: teamHomeScore,
                teamAwayScore: teamAwayScore,
                isFinal: false
            );

            Debug.Log($"[Scoring] Team Home: {teamHomeScore}, Team Away: {teamAwayScore}");
            // OnCurlingGameScoreChanged?.Invoke(CurlingGameManagerV2.Instance.gameData.score);
        }


        /// <summary>
        /// Helper Functions
        /// </summary>
        public List<CurlingStone> FilterAndUpdateStoneListForScoring(
            List<CurlingStone> stones
        )
        {
            // filter stones to only those in play
            List<CurlingStone> updatedStones = stones.Where(a =>
                a.Parameters.Status.IsInPlay == true
            ).ToList();

            // Update each stones distance from target
            foreach (CurlingStone s in updatedStones)
            {
                s.UpdateDistanceFromTarget(
                    targetZone: CurlingManager._instance.Parameters.Course.targetZone
                );
            }

            // Order the stones by distance to target (zero is closest)
            updatedStones.OrderBy(p => p.Parameters.Movement.DistanceFromTarget);

            return updatedStones;
        }


        private int CalculatePoints(
            List<CurlingStone> scoringStones, 
            CurlingStone closestNonScoringStone
        )
        {
            int score = 0;

            foreach (CurlingStone scoringStone in scoringStones)
            {
                if (IsStoneCloserToTargetThanOtherStone(scoringStone, closestNonScoringStone))
                {
                    score += 1;
                }
                else
                {
                    return score;
                }
            }

            return score;
        }


        private bool IsStoneCloserToTargetThanOtherStone(
            CurlingStone stoneA, 
            CurlingStone otherStone
        )
        {
            return stoneA.Parameters.Movement.DistanceFromTarget > otherStone.Parameters.Movement.DistanceFromTarget
                ? false
                : true;
        }

        /// <summary>
        /// Update UI
        /// </summary>
        public void UpdateScoreUI(
            int teamHomeScore, 
            int teamAwayScore
        )
        {
            CurlingManager._instance.Parameters.Canvas.scoreBug.UpdateScore(
                homeTeamScore: teamHomeScore,
                awayTeamScore: teamAwayScore
            );
        }


        /// <summary>
        /// Helper Functions
        /// </summary>

        // public int[] GetScore() => endScore;
        
        public void Reset()
        { 
            // stonesThisEnd.Clear();
        }
    }
}



