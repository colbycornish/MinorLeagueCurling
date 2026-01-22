using System.Collections.Generic;
using UnityEngine;
using System;
using CharacterNPC.v2;
using UnityEngine.AI;

/// <summary>
/// </summary>

namespace CurlingManagersV3
{

    public class Players : MonoBehaviour
    {        
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
            SetupTeams(teamHome, teamAway);
            SetupPlayers(teamHome, teamAway, curlingCourse);

            // scoreBug.UpdateTeamInfo(
            //     homeTeamName: "Blue Broom Brushers",
            //     awayTeamName: "Purple Stone Throwers"
            // );
        }

        // assigns the teams.
        public void SetupTeams(
            CurlingTeam teamHome, 
            CurlingTeam teamAway
        )
        {
            CurlingManager._instance.Parameters.Teams.CurlingTeams.Clear();
            CurlingManager._instance.Parameters.Teams.CurlingTeams.Add(teamHome);
            CurlingManager._instance.Parameters.Teams.CurlingTeams.Add(teamAway);

            CurlingManager._instance.Parameters.Teams.teamHome = teamHome;
            CurlingManager._instance.Parameters.Teams.teamAway = teamAway;

            OnTeamDataChanged?.Invoke(CurlingManager._instance.Parameters.Teams.CurlingTeams);

        }

        

        public void SetupPlayers(
            CurlingTeam teamHome, 
            CurlingTeam teamAway, 
            CurlingCourseData curlingCourse
        )
        {
            CurlingManager._instance.Parameters.Teams.teamHome.thrower = Instantiate(
                CurlingManager._instance.Parameters.Teams.teamHome.thrower, 
                CurlingManager._instance.Parameters.Course.idleLocationsTeamHome[0].position, 
                Quaternion.identity
            );

            CurlingManager._instance.Parameters.Teams.teamHome.sweeperLeft = Instantiate(
                CurlingManager._instance.Parameters.Teams.teamHome.sweeperLeft, 
                CurlingManager._instance.Parameters.Course.idleLocationsTeamHome[1].position, 
                Quaternion.identity
            );
            CurlingManager._instance.Parameters.Teams.teamHome.sweeperRight = Instantiate(
                CurlingManager._instance.Parameters.Teams.teamHome.sweeperRight, 
                CurlingManager._instance.Parameters.Course.idleLocationsTeamHome[2].position, 
                Quaternion.identity
            );

            CurlingManager._instance.Parameters.Teams.teamHome.thrower.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.CurlingTeamPosition = CurlingPlayerPosition.Thrower;
            CurlingManager._instance.Parameters.Teams.teamHome.sweeperLeft.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.CurlingTeamPosition = CurlingPlayerPosition.SweeperLeft;
            CurlingManager._instance.Parameters.Teams.teamHome.sweeperRight.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.CurlingTeamPosition = CurlingPlayerPosition.SweeperRight;

            CurlingManager._instance.Parameters.Teams.teamHome.thrower.GetComponent<CharacterNPC.v2.Character>().Parameters.Status.IsCurling = true;
            CurlingManager._instance.Parameters.Teams.teamHome.sweeperLeft.GetComponent<CharacterNPC.v2.Character>().Parameters.Status.IsCurling = true;
            CurlingManager._instance.Parameters.Teams.teamHome.sweeperRight.GetComponent<CharacterNPC.v2.Character>().Parameters.Status.IsCurling = true;
            
            
            /// away team
            CurlingManager._instance.Parameters.Teams.teamAway.thrower = Instantiate(
                CurlingManager._instance.Parameters.Teams.teamAway.thrower, 
                CurlingManager._instance.Parameters.Course.idleLocationsTeamAway[0].position, 
                Quaternion.identity
            );
            CurlingManager._instance.Parameters.Teams.teamAway.sweeperLeft = Instantiate(
                CurlingManager._instance.Parameters.Teams.teamAway.sweeperLeft, 
                CurlingManager._instance.Parameters.Course.idleLocationsTeamAway[1].position, 
                Quaternion.identity
            );
            CurlingManager._instance.Parameters.Teams.teamAway.sweeperRight = Instantiate(
                CurlingManager._instance.Parameters.Teams.teamAway.sweeperRight, 
                CurlingManager._instance.Parameters.Course.idleLocationsTeamAway[2].position, 
                Quaternion.identity
            );

            CurlingManager._instance.Parameters.Teams.teamAway.thrower.GetComponent<CharacterNPC.v2.Character>().Parameters.Status.IsCurling = true;
            CurlingManager._instance.Parameters.Teams.teamAway.sweeperLeft.GetComponent<CharacterNPC.v2.Character>().Parameters.Status.IsCurling = true;
            CurlingManager._instance.Parameters.Teams.teamAway.sweeperRight.GetComponent<CharacterNPC.v2.Character>().Parameters.Status.IsCurling = true;

            CurlingManager._instance.Parameters.Teams.teamAway.thrower.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.CurlingTeamPosition = CurlingPlayerPosition.Thrower;
            CurlingManager._instance.Parameters.Teams.teamAway.sweeperLeft.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.CurlingTeamPosition = CurlingPlayerPosition.SweeperLeft;
            CurlingManager._instance.Parameters.Teams.teamAway.sweeperRight.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.CurlingTeamPosition = CurlingPlayerPosition.SweeperRight;
            
            PutTeamOnSidelines(
                team: CurlingManager._instance.Parameters.Teams.teamAway, 
                idleLocations: CurlingManager._instance.Parameters.Course.idleLocationsTeamAway
            );

            PutTeamOnIce(team: CurlingManager._instance.Parameters.Teams.teamHome);            
        }


        /// <summary>
        /// Changing Turns
        /// </summary>
        public void UpdateCurrentTeam()
        {
            CurlingManager._instance.Parameters.Teams.currentTeam = 
                CurlingManager._instance.Parameters.Turn.CurrentTurn == CurlingGameTurnType.Home
                    ? CurlingManager._instance.Parameters.Teams.teamHome
                    : CurlingManager._instance.Parameters.Teams.teamAway;
        }

        public CurlingTeam GetCurrentTeam()
        {
            return CurlingManager._instance.Parameters.Teams.currentTeam;
        }

        
        /// <summary>
        /// Game Objects and Updating character positions 
        /// - Moving players into  playing locations or to the bench
        /// - Activing the movement controls for active players
        /// - Deactivating movement controls for non-active players
        /// </summary>


        public void RepositionCharacters()
        {
            PutTeamOnIce(
                team: CurlingManager._instance.Parameters.Turn.CurrentTurn == CurlingGameTurnType.Home 
                    ? CurlingManager._instance.Parameters.Teams.teamHome
                    : CurlingManager._instance.Parameters.Teams.teamAway
            );

            PutTeamOnSidelines(
                team: CurlingManager._instance.Parameters.Turn.CurrentTurn == CurlingGameTurnType.Home 
                    ? CurlingManager._instance.Parameters.Teams.teamAway
                    : CurlingManager._instance.Parameters.Teams.teamHome,
                idleLocations: CurlingManager._instance.Parameters.Turn.CurrentTurn == CurlingGameTurnType.Home 
                    ? CurlingManager._instance.Parameters.Course.idleLocationsTeamAway
                    : CurlingManager._instance.Parameters.Course.idleLocationsTeamHome
            );
        }

        public void PutTeamOnSidelines(
            CurlingTeam team, 
            List<Transform> idleLocations
        ){
            TeleportPlayer(
                playerObject: team.thrower,
                newPosition: idleLocations[0].position
            );
            TeleportPlayer(
                playerObject: team.sweeperLeft,
                newPosition: idleLocations[1].position
            );
            TeleportPlayer(
                playerObject: team.sweeperRight,
                newPosition: idleLocations[2].position
            );
            // team.thrower.transform.position = idleLocations[0].position;
            // team.sweeperLeft.transform.position = idleLocations[1].position;
            // team.sweeperRight.transform.position = idleLocations[2].position;

            team.thrower.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.IsOnIce = false;
            team.sweeperLeft.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.IsOnIce = false;
            team.sweeperRight.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.IsOnIce = false;

            return;
        }

        public void PutTeamOnIce(CurlingTeam team){
            Debug.Log("Putting Team on Ice");
            // Debug.Log($"Thrower before position: {team.thrower.transform.position}");
            // Debug.Log($"Adjusted position: {CurlingManager._instance.Parameters.Course.throwerStartLocation.position}");
            TeleportPlayer(
                playerObject: team.thrower,
                newPosition: CurlingManager._instance.Parameters.Course.throwerStartLocation.position
            );
            
            // team.thrower.transform.position = CurlingManager._instance.Parameters.Course.throwerStartLocation.position;
            // Debug.Log($"Thrower after position: {team.thrower.transform.position}");
            TeleportPlayer(
                playerObject: team.sweeperLeft,
                newPosition: CurlingManager._instance.Parameters.Course.sweeperLStartLocation.position
            );
            TeleportPlayer(
                playerObject: team.sweeperRight,
                newPosition: CurlingManager._instance.Parameters.Course.sweeperRStartLocation.position
            );
            // team.sweeperLeft.transform.position = CurlingManager._instance.Parameters.Course.sweeperLStartLocation.position;
            // team.sweeperRight.transform.position = CurlingManager._instance.Parameters.Course.sweeperRStartLocation.position;
        
            // Make Right Sweeper look at Left Sweeper
            team.sweeperRight.transform.LookAt(team.sweeperLeft.transform.position);
            // Make Left Sweeper look at Right Sweeper
            team.sweeperLeft.transform.LookAt(team.sweeperRight.transform.position);
            // Make Thrower look at Target Zone
            team.thrower.transform.LookAt(
                CurlingManager._instance.Parameters.Course.targetZone.transform.position
            );

            team.thrower.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.IsOnIce = true;
            team.sweeperLeft.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.IsOnIce = true;
            team.sweeperRight.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.IsOnIce = true;

            return;
        }

        // Call this method to teleport the instantiated object
        public void TeleportPlayer(
            GameObject playerObject,
            Vector3 newPosition
        )
        {
            NavMeshAgent nma = playerObject.GetComponent<NavMeshAgent>();
            nma.enabled = false;
            Rigidbody rb = playerObject.GetComponent<Rigidbody>();
            playerObject.transform.position = newPosition;
            if (rb != null)
            {
                Debug.Log($"Teleporting player to {newPosition}");
                rb.position = newPosition; // Instantly changes the position
                // rb.rotation = newRotation; // Instantly changes the rotation

                // Optional: Reset velocity after teleporting
                // rb.linearVelocity = Vector3.zero;
                // rb.angularVelocity = Vector3.zero;
            }
            nma.enabled = true;
        }

        public void PrepareSweepersToTrackActiveStone()
        {
            Debug.Log("Preparing Sweepers to Track Active Stone");
            CurlingTeam activeTeam = 
                CurlingManager._instance.Parameters.Turn.CurrentTurn == CurlingGameTurnType.Home
                    ? CurlingManager._instance.Parameters.Teams.teamHome
                    : CurlingManager._instance.Parameters.Teams.teamAway;

            activeTeam.thrower.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.ActiveStone = 
                CurlingManager._instance.Parameters.Stones.currentStone;
            activeTeam.sweeperLeft.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.ActiveStone = 
                CurlingManager._instance.Parameters.Stones.currentStone;
            activeTeam.sweeperRight.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.ActiveStone = 
                CurlingManager._instance.Parameters.Stones.currentStone;


            activeTeam.thrower.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.TempTargetObject = 
                CurlingManager._instance.Parameters.Stones.currentStone.gameObject;
            activeTeam.sweeperLeft.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.TempTargetObject = 
                CurlingManager._instance.Parameters.Stones.currentStone.gameObject;
            activeTeam.sweeperRight.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.TempTargetObject = 
                CurlingManager._instance.Parameters.Stones.currentStone.gameObject;
        }


        public void UpdateSweeperStoneTracking(
            CurlingStone stone,
            CurlingTeam team
        )
        {
            Debug.Log("Preparing Sweepers to Track Active Stone");
            

            team.thrower.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.ActiveStone = 
                stone;
            team.sweeperLeft.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.ActiveStone = 
                stone;
            team.sweeperRight.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.ActiveStone = 
                stone;

            team.thrower.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.TempTargetObject = 
                stone == null ? null : stone.gameObject;
            team.sweeperLeft.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.TempTargetObject = 
                stone == null ? null : stone.gameObject;
            team.sweeperRight.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.TempTargetObject = 
                stone == null ? null : stone.gameObject;
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
            return CurlingManager._instance.Parameters.Teams.CurlingTeams.Count == 2;
        }

        // Reset
        public void Reset()
        {
            CurlingManager._instance.Parameters.Teams.CurlingTeams.Clear();
            CurlingManager._instance.Parameters.Teams.teamHome = null;
            CurlingManager._instance.Parameters.Teams.teamAway = null;
            CurlingManager._instance.Parameters.Course.idleLocationsTeamHome.Clear();
            CurlingManager._instance.Parameters.Course.idleLocationsTeamAway.Clear();
            CurlingManager._instance.Parameters.Course.throwerStartLocation = null;
            CurlingManager._instance.Parameters.Course.sweeperLStartLocation = null;
            CurlingManager._instance.Parameters.Course.sweeperRStartLocation = null;
        }
    }
}