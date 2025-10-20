using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// This is the high-level game manager for a curling game.
/// It orchestrates the flow of the game, including starting new ends,
/// managing player turns, and handling the end of the game.
/// </summary>


namespace CurlingManagersV3
{

    public enum CurlingMatchPhase
    {
        /// Should only be used at the start
        None,
        Loading,
        RoundSplash,
        TeamSplash,

        /// Curling loop begins here
        StoneSelection,
        StoneSelectionDetails,
        StoneSelectionConfirm,
        CurlingAimControlsPhase,
        CurlingPowerMeterPhase,
        CurlingStoneSweepingPhase,
        CurlingNoSweepZone,
        PostThrowResult,

        // Displayed if the settings are marked to allow player obstacles
        ObstacleSelection,
        ObstaclePlacement,

        // End Game
        FinalResults,

        // Exit the Curling Game and return to previous location
        ExitCurlingGame
    }

    public class MatchPhaseManager : MonoBehaviour
    {
 
        public static CurlingManagersV3.MatchPhaseManager _instance { get; private set; }
        public CurlingManagersV3.CurlingMatchPhase currentPhase { get; private set; }
        public event Action<CurlingManagersV3.CurlingMatchPhase> OnPhaseChanged;

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }
            _instance = this;
        }

        public void SetPhase(CurlingMatchPhase newPhase)
        {
            Debug.Log($"[MatchPhase] {currentPhase} → {newPhase}");
            if (currentPhase == newPhase) return;

            currentPhase = newPhase;
            Debug.Log($"[MatchPhase] {currentPhase} → {newPhase}");
            OnPhaseChanged?.Invoke(newPhase);
        }

        public void TestNextPhase(){
            SetPhase(currentPhase + 1);
        }
    }
}