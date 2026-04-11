using UnityEngine;
using System.Collections;
using TMPro;
using CurlingManagersV3;
using System;
using System.Collections.Generic;

namespace CurlingUI.v3 {
    public class Scorebug : MonoBehaviour
    {
        [Header("Team Names")]
        [SerializeField] private TextMeshProUGUI TeamAreaHomeName;
        [SerializeField] private TextMeshProUGUI TeamAreaAwayName;
        

        [Header("Rock Indicators")]
        public GameObject TeamAreaHomeRocks;
        public GameObject TeamAreaAwayRocks;
        public GameObject RockIndicator;
        
        [Header("Throw Clock")]
        public GameObject ThrowClock;

        [Header("Scores")]
        public TextMeshProUGUI HomeTeamScore;
        public TextMeshProUGUI AwayTeamScore;

        [Header("Turn Indicator")]
        public GameObject TurnIndicatorHome;
        public GameObject TurnIndicatorAway;

        

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        // void Start()
        // {
        //     InitializeScorebug();
        // }

        void OnEnable()
        {
            InitializeScorebug();
        }

        void InitializeScorebug()
        {
            BuildTeamRocks();

            UpdateTeamNames();
            UpdateTeamNames();
            UpdateScore();
            // UpdateTeamRockIndicators();

            UpdateTurnIndicator();
        }

        /************************************************************************************************************************/
        public void UpdateScorebug()
        {
            // This will be called by the CurlingGameManager.cs to update the scorebug with the latest info
        }

        /************************************************************************************************************************/
        public void UpdateScore()
        {
            CurlingManager cm = CurlingManager._instance;
            int homeScore = cm.Parameters.CurrentGameScore.teamHomeScore;
            int awayScore = cm.Parameters.CurrentGameScore.teamAwayScore;

            HomeTeamScore.text = homeScore.ToString();
            AwayTeamScore.text = awayScore.ToString();
        }

        /************************************************************************************************************************/
        private void UpdateTeamNames()
        {
            CurlingManager cm = CurlingManager._instance;

            string homeTeamName = cm.Parameters.Teams?.teamHome?.teamName ?? "Purple People Eaters"; // ?? "Purple People Eaters"; // This will be based on the game state, but we'll hardcode it for now
            string awayTeamName = cm.Parameters.Teams?.teamAway?.teamName ?? "Ruby Red Rhino Riders"; //?? "Ruby Red Rhino Riders"; // This will be based on the game state
            TeamAreaHomeName.GetComponent<TMPro.TextMeshProUGUI>().text = $"{homeTeamName}";
            TeamAreaAwayName.GetComponent<TMPro.TextMeshProUGUI>().text = $"{awayTeamName}";
        }

        /************************************************************************************************************************/
        /// <summary>
        /// Rock Indicators 
        /// </summary>

        
        private void BuildTeamRocks()
        {
            foreach (Transform child in TeamAreaHomeRocks.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in TeamAreaAwayRocks.transform)
            {
                Destroy(child.gameObject);
            }

            StartCoroutine(BuildTeamRockIndicators());
        }

        IEnumerator BuildTeamRockIndicators()
        {
            int numberOfRocks = 5; // This will be based on the game state, but we'll hardcode it for now
            for (int i = 0; i < numberOfRocks; i++)
            {
                Instantiate(RockIndicator, TeamAreaHomeRocks.transform);
                Instantiate(RockIndicator, TeamAreaAwayRocks.transform);

                yield return new WaitForSeconds(0.2f); 
            }
            // Update the team names on the scorebug
            UpdateTeamRockIndicators();
        }
 
        

        private void UpdateTeamRockIndicators()
        {
            CurlingManager cm = CurlingManager._instance;
            List<CurlingStone> stonesTeamHome = cm.Parameters.Stones.stonesTeamHome;
            List<CurlingStone> stonesTeamAway = cm.Parameters.Stones.stonesTeamAway;

            UpdateSingleTeamRockIndicators(
                teamAreaRocks: TeamAreaHomeRocks.transform, 
                teamStones: stonesTeamHome, 
                isCurrentActiveTeam: cm.Parameters.Turn.CurrentTurn == CurlingGameTurnType.Home
            );
            UpdateSingleTeamRockIndicators(
                teamAreaRocks: TeamAreaAwayRocks.transform, 
                teamStones:stonesTeamAway, 
                isCurrentActiveTeam: cm.Parameters.Turn.CurrentTurn == CurlingGameTurnType.Away
            );
        }

        private void UpdateSingleTeamRockIndicators(
            Transform teamAreaRocks, 
            List<CurlingStone> teamStones,
            bool isCurrentActiveTeam
        )
        {
            int numRocksThrown = teamStones.FindAll(stone => stone.Parameters.Status.IsThrown).Count; 
            int numRocksScorable = teamStones.FindAll(stone => stone.Parameters.Status.IsInScoringZone).Count;
            int numRockIndicators = teamAreaRocks.childCount; // starts
            Debug.Log($"Updating rock indicators for team. \nNum Rocks Thrown: {numRocksThrown}, \nNum Rocks Scorable: {numRocksScorable}, \nNum Rock Indicators: {numRockIndicators}, \nIs Current Active Team: {isCurrentActiveTeam}\n------");

            for (int i = 0; i < teamAreaRocks.childCount; i++)
            {   
                GameObject rockIndicator = teamAreaRocks.GetChild(i).gameObject;
                ScorebugRockIndicator rockIndicatorScript = rockIndicator.GetComponent<ScorebugRockIndicator>();

                // Adjust Indicator for a thrown rock
                if (i < numRocksThrown)
                {
                    // a thrown rock will either be a bad throw, a good throw, or an active throw
                    if (i < numRocksScorable)
                    {
                        rockIndicatorScript.SetState(ScorebugRockIndicator.RockIndicatorState.GoodThrow);
                    }
                    else
                    {
                        rockIndicatorScript.SetState(ScorebugRockIndicator.RockIndicatorState.BadThrow);
                    }
                }
                // else, if the team is active team and this rock indicator corresponds to the next rock to throw, set it to active
                else if (i == numRocksThrown && isCurrentActiveTeam)
                {
                    rockIndicatorScript.SetState(ScorebugRockIndicator.RockIndicatorState.Active);
                }
                // else the rock remains waiting. 
                else
                {
                    rockIndicatorScript.SetState(ScorebugRockIndicator.RockIndicatorState.Default);
                }
            }
        }

        /************************************************************************************************************************/

        private void UpdateTurnIndicator()
        {
            CurlingManager cm = CurlingManager._instance;
            bool isHomeTeamTurn = cm.Parameters.Turn.CurrentTurn == CurlingGameTurnType.Home;

            TurnIndicatorHome.SetActive(isHomeTeamTurn);
            TurnIndicatorAway.SetActive(!isHomeTeamTurn);
        }
    }
}