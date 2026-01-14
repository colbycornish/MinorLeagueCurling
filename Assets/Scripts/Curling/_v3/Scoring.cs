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
            if (CurlingManager._instance.Parameters.Course.targetZone == null) return;
            Vector3 targetZoneCenter = CurlingManager._instance.Parameters.Course.targetZone.transform.position;

            List<CurlingStone> stonesTeamHome = CurlingManager._instance.Parameters.Stones.stonesTeamHome.Where(a =>
                a.isInPlay == true
            ).ToList();
            List<CurlingStone> stonesTeamAway = CurlingManager._instance.Parameters.Stones.stonesTeamAway.Where(a =>
                a.isInPlay == true
            ).ToList();

            foreach (CurlingStone s in stonesTeamHome)
            {
                s.UpdateDistanceFromTarget(
                    targetZone: CurlingManager._instance.Parameters.Course.targetZone
                );
            }

            foreach (CurlingStone s in stonesTeamAway)
            {
                s.UpdateDistanceFromTarget(
                    targetZone: CurlingManager._instance.Parameters.Course.targetZone
                );
            }

            stonesTeamHome.OrderBy(p => p.distanceFromTarget);
            stonesTeamAway.OrderBy(p => p.distanceFromTarget);

            // stonesTeamHome.Sort((a, b) =>
            //     Vector3.Distance(targetZoneCenter, a.rb.transform.position)
            //     .CompareTo(Vector3.Distance(targetZoneCenter, b.rb.transform.position)));

            // stonesTeamAway.Sort((a, b) => 
            //     Vector3.Distance(targetZoneCenter, a.rb.transform.position)
            //     .CompareTo(Vector3.Distance(targetZoneCenter, b.rb.transform.position)));

            int teamHomeScore = 0;
            int teamAwayScore = 0;

            if (stonesTeamHome.Count > 0 && stonesTeamAway.Count > 0)
            {
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

            // CurlingGameManagerV2.Instance.gameData.score.SetScore(
            //     teamHomeScore: teamHomeScore,
            //     teamAwayScore: teamAwayScore,
            //     isFinal: false
            // );
            CurlingManager._instance.Parameters.CurrentGameScore.SetScore(
                teamHomeScore: teamHomeScore,
                teamAwayScore: teamAwayScore,
                isFinal: false
            );

            Debug.Log($"[Scoring] Team Home: {teamHomeScore}, Team Away: {teamAwayScore}");
            // OnCurlingGameScoreChanged?.Invoke(CurlingGameManagerV2.Instance.gameData.score);

        }

        /// returns 1 if true
        /// returns -1 if false
        /// returns 0 if equal
        private bool IsStoneCloserToTargetThanOtherStone(
            CurlingStone stoneA, 
            CurlingStone otherStone
        )
        {
            // Vector3 targetZoneCenter = CurlingManager._instance.Parameters.Course.targetZone.transform.position;
            // float DistanceOfStoneAToTarget = Vector3.Distance(targetZoneCenter, stoneA.rb.transform.position);
            // float DistanceOfStoneBToTarget = Vector3.Distance(targetZoneCenter, otherStone.rb.transform.position);

            return stoneA.distanceFromTarget > otherStone.distanceFromTarget
                ? false
                : true;
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