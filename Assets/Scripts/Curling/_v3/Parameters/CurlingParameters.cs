using System;
using UnityEngine;
using System.Collections.Generic;


namespace CurlingManagersV3.Parameters
{

    [Serializable]
    public class CurlingParameters
    {

        [SerializeField]
        private CurlingManagersV3.Parameters.Course _Course;
        public CurlingManagersV3.Parameters.Course Course => _Course;
 
        [SerializeField]
        private CurlingManagersV3.Parameters.Canvas _Canvas;
        public CurlingManagersV3.Parameters.Canvas Canvas => _Canvas;

        [SerializeField]
        private CurlingManagersV3.Parameters.Cinematics _Cinematics;
        public CurlingManagersV3.Parameters.Cinematics Cinematics => _Cinematics;
 
        [SerializeField]
        private CurlingManagersV3.Parameters.AimingParameters _AimingParameters;
        public CurlingManagersV3.Parameters.AimingParameters Aiming => _AimingParameters;

        [SerializeField]
        private CurlingManagersV3.Parameters.Stones _Stones;
        public CurlingManagersV3.Parameters.Stones Stones => _Stones;

        [SerializeField]
        private CurlingManagersV3.Parameters.Status _Status;
        public CurlingManagersV3.Parameters.Status Status => _Status;

        [SerializeField]
        private CurlingManagersV3.Parameters.SweepingParameters _Sweeping;
        public CurlingManagersV3.Parameters.SweepingParameters Sweeping => _Sweeping;

        [SerializeField]
        private CurlingManagersV3.Parameters.Teams _Teams;
        public CurlingManagersV3.Parameters.Teams Teams => _Teams;

        [SerializeField]
        private CurlingManagersV3.Parameters.TurnParameters _Turn;
        public CurlingManagersV3.Parameters.TurnParameters Turn => _Turn;

        [SerializeField]
        private CurlingManagersV3.Parameters.ThrowingParameters _ThrowingParameters;
        public CurlingManagersV3.Parameters.ThrowingParameters Throwing => _ThrowingParameters;

        /// <summary>
        /// Structs
        /// </summary>

        [SerializeField]
        private CurlingRules _Rules;
        public CurlingRules Rules => _Rules;

        [SerializeField]
        public CurlingGameScore CurrentGameScore;



        // [SerializeField]
        // private CurlingManagersV3.Parameters.Match _Match;
        // public CurlingManagersV3.Parameters.Match Match => _Match;

        /// - Match
        ///     - Settings
        ///     - List of Games
        ///     - CurrentGame
        ///         - Score
        ///         - Turn
        /// 
        /// public bool isTeamHomeTurn = false;
        // public bool isTeamAwayTurn = false;

        // public enum CurlingGameTurnType
        // {
        //     Home, // scores cumulate every turn
        //     Away,
        //     None
        // }

        // public CurlingGameTurnType currentTurn;

        // [Header("Helpful References")]
        // public int numberOfHomeTeamTurnsCompleted = 0;
        // public int numberOfAwayTeamTurnsCompleted = 0;
        // public int currentTurnCount = 0;
        // public int maxTurnCount = 10;
        /// 
        ///         - Phase
        /// 
        /// - Course
        /// 
        
        
        

        /// 
        /// - Teams
        ///     - Home
        ///     - Away
        /// 
        ///[Header("Team Members")]
        // private GameObject teamHomeThrower;
        // private GameObject teamHomeSweeperL;
        // private GameObject teamHomeSweeperR;
        // private GameObject teamAwayThrower;
        // private GameObject teamAwaySweeperL;
        // private GameObject teamAwaySweeperR;


        /// 
        /// - Stones
        /// 
        /// [Header("Game Objects")]
        // public GameObject stonePrefab_TeamHome;
        // public GameObject stonePrefab_TeamAway;

        // public List<CurlingStone> stonesTeamHome = new List<CurlingStone>();
        // public List<CurlingStone> stonesTeamAway = new List<CurlingStone>();

        
        // public Transform launchPoint;

        // [Header("Current Stone")]
        // // This is one of the most important bits, and will be referenced by 
        // // the stone throw controller, sweeping controller, etc.
        // public CurlingStone currentStone;
        // public CurlingStone currentStoneId;
        /// 
        /// - Status
        ///     - ReadyToPlay
        ///     - 
        /// 
        /// - Canvas Objects
        /// 
        // public PowerMeterController powerMeterController;
        // public GameObject leftSweeperExhaustionBar;
        // public GameObject rightSweeperExhaustionBar;
        // public ScorebugController scoreBug;

        
        
    }
}


