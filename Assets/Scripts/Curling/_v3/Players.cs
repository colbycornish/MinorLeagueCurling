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

    public class Players : MonoBehaviour
    {
        // public int totalStonesPerEnd = 10;
        // public List<CurlingTeamData> teams = new List<CurlingTeamData>();
        [Header("Teams and Players")]
        public List<CurlingTeam> teams = new List<CurlingTeam>();
        public CurlingTeam teamHome;
        public CurlingTeam teamAway;

        [Header("Placement Locations")]
        public Transform throwerStartLocation;
        public Transform sweeperLStartLocation;
        public Transform sweeperRStartLocation;
        public List<Transform> idleLocationsTeamHome;
        public List<Transform> idleLocationsTeamAway;

        [Header("Team Members")]
        private GameObject teamHomeThrower;
        private GameObject teamHomeSweeperL;
        private GameObject teamHomeSweeperR;
        private GameObject teamAwayThrower;
        private GameObject teamAwaySweeperL;
        private GameObject teamAwaySweeperR;

        [Header("Current Team")]
        public CurlingTeam currentTeam;
        public event Action<List<CurlingTeam>> OnTeamDataChanged;


        /// <summary>
        /// Setup
        /// </summary>        
        public void Setup(
            CurlingCourseData curlingCourse,
            CurlingTeam teamHome,
            CurlingTeam teamAway
        )
        {
            SetupLocations(curlingCourse);
            SetupTeams(teamHome, teamAway);
            SetupPlayers(teamHome, teamAway, curlingCourse);

            // scoreBug.UpdateTeamInfo(
            //     homeTeamName: "Blue Broom Brushers",
            //     awayTeamName: "Purple Stone Throwers"
            // );
        }

        public void SetupLocations(CurlingCourseData curlingCourse)
        {
            idleLocationsTeamHome = curlingCourse.idleLocationsTeamHome;
            idleLocationsTeamAway = curlingCourse.idleLocationsTeamAway;
            //
            throwerStartLocation = curlingCourse.throwerStartLocation;
            sweeperLStartLocation = curlingCourse.sweeperLStartLocation;
            sweeperRStartLocation = curlingCourse.sweeperRStartLocation;
        }

        // assigns the teams.
        public void SetupTeams(
            CurlingTeam teamHome, 
            CurlingTeam teamAway
        )
        {
            teams.Clear();
            teams.Add(teamHome);
            teams.Add(teamAway);
            this.teamHome = teamHome;
            this.teamAway = teamAway;
            Debug.Log($"Teams set: {teamHome.teamName} vs {teamAway.teamName}");
            OnTeamDataChanged?.Invoke(teams);
        }

        

        public void SetupPlayers(
            CurlingTeam teamHome, 
            CurlingTeam teamAway, 
            CurlingCourseData curlingCourse
        )
        {
            teamHome.thrower = Instantiate(teamHome.thrower, idleLocationsTeamHome[0].position, Quaternion.identity);
            teamHome.sweeperLeft = Instantiate(teamHome.sweeperLeft, idleLocationsTeamHome[1].position, Quaternion.identity);
            teamHome.sweeperRight = Instantiate(teamHome.sweeperRight, idleLocationsTeamHome[2].position, Quaternion.identity);
            
            /// away team
            teamAway.thrower = Instantiate(teamAway.thrower, idleLocationsTeamAway[0].position, Quaternion.identity);
            teamAway.sweeperLeft = Instantiate(teamAway.sweeperLeft, idleLocationsTeamAway[1].position, Quaternion.identity);
            teamAway.sweeperRight = Instantiate(teamAway.sweeperRight, idleLocationsTeamAway[2].position, Quaternion.identity);
            
            PutTeamOnSidelines(
                team: teamHome, 
                idleLocations: idleLocationsTeamHome
            );

            PutTeamOnIce(team: teamAway);            
        }


        /// <summary>
        /// Changing Turns
        /// </summary>
        public void UpdateCurrentTeam()
        {
            if (CurlingManager._instance.turnManager.IsItTheHomeTeamsTurn()){
                Debug.Log("-> Home team is now the current team.");
                currentTeam = teamHome;
            }
            else {
                Debug.Log("-> Away team is now the current team.");
                currentTeam = teamAway;
            }
        }

        public CurlingTeam GetCurrentTeam()
        {
            return currentTeam;
        }

        
        /// <summary>
        /// Game Objects and Updating character positions 
        /// - Moving players into  playing locations or to the bench
        /// - Activing the movement controls for active players
        /// - Deactivating movement controls for non-active players
        /// </summary>


        public void RepositionCharacters()
        {
            Debug.Log("Players: RepositionCharacters()");
            if (CurlingManager._instance.turnManager.IsItTheHomeTeamsTurn()){
                Debug.Log("Players -> Home team to Ice");
                PutTeamOnIce(team: teamHome);
                Debug.Log("Players -> Away team to Sidelines");
                PutTeamOnSidelines(
                    team: teamAway, 
                    idleLocations: idleLocationsTeamAway
                );
            }
            else {
                Debug.Log("Players -> Away team to Ice");
                PutTeamOnIce(team: teamAway);
                Debug.Log("Players -> Home team to Sidelines");
                PutTeamOnSidelines(
                    team: teamHome, 
                    idleLocations: idleLocationsTeamHome
                );
            }
            
            /// if turn == Home
            /// - Move Away players to bench
            /// - deactivate Away players
            /// - Move Home players into position
            /// - activate Home players
            /// 
            /// if turn == Away
            /// - Move Home players to bench
            /// - deactivate Home players
            /// - Move Away players into position
            /// - activate Away players
        }

        public void PutTeamOnSidelines(
            CurlingTeam team, 
            List<Transform> idleLocations
        ){
            team.thrower.transform.position = idleLocations[0].position;
            team.sweeperLeft.transform.position = idleLocations[1].position;
            team.sweeperRight.transform.position = idleLocations[2].position;


            return;
        }

        public void PutTeamOnIce(CurlingTeam team){
            team.thrower.transform.position = throwerStartLocation.position;
            team.sweeperLeft.transform.position = sweeperLStartLocation.position;
            team.sweeperRight.transform.position = sweeperRStartLocation.position;

            // Make Right Sweeper look at Left Sweeper
            team.sweeperRight.transform.LookAt(team.sweeperLeft.transform.position);
            // Make Left Sweeper look at Right Sweeper
            team.sweeperLeft.transform.LookAt(team.sweeperRight.transform.position);
            // Make Thrower look at Target Zone
            team.thrower.transform.LookAt(CurlingManager._instance.scoring.targetZone.transform.position);
            return;
        }




        // TODO: Establish the Curling Player Data structure first.
        // We cannot just bring in one of the NPC as a base (since their nav agent and 
        // action controls conflict with the basis here)
        


        /// <summary>
        /// Helper Functions
        /// </summary>
        /// 

        // Ready?
        public bool IsReady()
        {
            return teams.Count == 2;
        }

        // Reset
        public void Reset()
        {
            teams.Clear();
            idleLocationsTeamHome.Clear();
            idleLocationsTeamAway.Clear();
            throwerStartLocation = null;
            sweeperLStartLocation = null;
            sweeperRStartLocation = null;
        }
    }
}