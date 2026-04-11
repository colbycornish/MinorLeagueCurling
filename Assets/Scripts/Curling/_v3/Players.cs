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
        public float sweeperForwardDistanceFromStone = 12f;
        public float sweeperLateralDistanceFromStone = 8f;


        /************************************************************************************************************************/

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

            PutTeamOnSidelines(
                team: CurlingManager._instance.Parameters.Teams.teamHome, 
                idleLocations: CurlingManager._instance.Parameters.Course.idleLocationsTeamHome
            );

            // PutTeamOnIce(
            //     team: CurlingManager._instance.Parameters.Teams.teamHome
            // );            
        }

        // 

        
        /************************************************************************************************************************/

        /// <summary>
        /// Game Objects and Updating character positions 
        /// - Moving players into  playing locations or to the bench
        /// - Activing the movement controls for active players
        /// - Deactivating movement controls for non-active players
        /// </summary>
        public void RepositionCharactersForEpicTeamPose(){
            Debug.Log("Positioning Teams for Epic Posing");
            CurlingTeam teamHome = CurlingManager._instance.Parameters.Teams.teamHome;
            CurlingTeam teamAway = CurlingManager._instance.Parameters.Teams.teamAway; 
            List<Transform> idleLocationsHome = CurlingManager._instance.Parameters.Course.idleLocationsTeamHome;
            List<Transform> idleLocationsAway = CurlingManager._instance.Parameters.Course.idleLocationsTeamAway;

            
            // Home Team
            PutPlayerOnSideline(
                playerObject: teamHome.thrower,
                idleLocation: idleLocationsHome[0],
                lookAtTarget: idleLocationsAway[0].position
            );
            

            PutPlayerOnSideline(
                playerObject: teamHome.sweeperLeft,
                idleLocation: idleLocationsHome[1],
                lookAtTarget: idleLocationsAway[1].position
            );

            PutPlayerOnSideline(
                playerObject: teamHome.sweeperRight,
                idleLocation: idleLocationsHome[2],
                lookAtTarget: idleLocationsAway[2].position//teamAway.sweeperRight.transform.position
            );

            

            // Away Team
            PutPlayerOnSideline(
                playerObject: teamAway.thrower,
                idleLocation: idleLocationsAway[0],
                lookAtTarget: idleLocationsHome[0].position//teamHome.thrower.transform.position
            );

            PutPlayerOnSideline(
                playerObject: teamAway.sweeperLeft,
                idleLocation: idleLocationsAway[1],
                lookAtTarget: idleLocationsHome[1].position//teamHome.sweeperLeft.transform.position
            );

            PutPlayerOnSideline(
                playerObject: teamAway.sweeperRight,
                idleLocation: idleLocationsAway[2],
                lookAtTarget: idleLocationsHome[2].position//teamHome.sweeperRight.transform.position
            );

            TellPlayerToPose(playerObject: teamHome.thrower);
            TellPlayerToPose(playerObject: teamHome.sweeperLeft);
            TellPlayerToPose(playerObject: teamHome.sweeperRight);

            TellPlayerToPose(playerObject: teamAway.thrower);
            TellPlayerToPose(playerObject: teamAway.sweeperLeft);
            TellPlayerToPose(playerObject: teamAway.sweeperRight);

        }


        public void TellPlayerToPose(GameObject playerObject)
        {
            CharacterNPC.v2.Character c = playerObject.GetComponent<CharacterNPC.v2.Character>();
            c.Parameters.Jobs.DesiredAction = ActionType.Pose;
            c.Parameters.Status.IsCurling = false;
        }

        public void TellPlayerToIdle(GameObject playerObject)
        {
            CharacterNPC.v2.Character c = playerObject.GetComponent<CharacterNPC.v2.Character>();
            c.Parameters.Jobs.DesiredAction = ActionType.Idle;
        }

        public void RepositionCharactersForCurling(){
            Debug.Log("Repositioning Characters for Curling");
            TellPlayerToIdle(playerObject: CurlingManager._instance.Parameters.Teams.teamHome.thrower);
            TellPlayerToIdle(playerObject: CurlingManager._instance.Parameters.Teams.teamHome.sweeperLeft);
            TellPlayerToIdle(playerObject: CurlingManager._instance.Parameters.Teams.teamHome.sweeperRight);

            TellPlayerToIdle(playerObject: CurlingManager._instance.Parameters.Teams.teamAway.thrower);
            TellPlayerToIdle(playerObject: CurlingManager._instance.Parameters.Teams.teamAway.sweeperLeft);
            TellPlayerToIdle(playerObject: CurlingManager._instance.Parameters.Teams.teamAway.sweeperRight);

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

            PutPlayerOnSideline(
                playerObject: team.thrower,
                idleLocation: idleLocations[0],
                lookAtTarget: CurlingManager._instance.Parameters.Course.targetZone.transform.position
            );

            PutPlayerOnSideline(
                playerObject: team.sweeperLeft,
                idleLocation: idleLocations[1],
                lookAtTarget: CurlingManager._instance.Parameters.Course.targetZone.transform.position
            );

            PutPlayerOnSideline(
                playerObject: team.sweeperRight,
                idleLocation: idleLocations[2],
                lookAtTarget: CurlingManager._instance.Parameters.Course.targetZone.transform.position
            );

            return;
        }

        private void PutPlayerOnSideline(
            GameObject playerObject, 
            Transform idleLocation,
            Vector3 lookAtTarget
        ){
            TeleportPlayer(
                playerObject: playerObject,
                newPosition: idleLocation.position
            );

            playerObject.transform.LookAt(lookAtTarget);
            playerObject.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.IsOnIce = false;
        }

        public void PutTeamOnIce(CurlingTeam team){
            Debug.Log("Putting Team on Ice");

            PutPlayerOnIce(
                playerObject: team.thrower,
                iceLocation: CurlingManager._instance.Parameters.Course.throwerStartLocation,
                lookAtTarget: CurlingManager._instance.Parameters.Course.targetZone.transform.position
            );

            PutPlayerOnIce(
                playerObject: team.sweeperLeft,
                iceLocation: CurlingManager._instance.Parameters.Course.sweeperLStartLocation,
                lookAtTarget: CurlingManager._instance.Parameters.Course.sweeperRStartLocation.position
            );

            PutPlayerOnIce(
                playerObject: team.sweeperRight,
                iceLocation: CurlingManager._instance.Parameters.Course.sweeperRStartLocation,
                lookAtTarget: CurlingManager._instance.Parameters.Course.sweeperLStartLocation.position
            );
            return;
        }

        private void PutPlayerOnIce(
            GameObject playerObject, 
            Transform iceLocation,
            Vector3 lookAtTarget
        ){
            TeleportPlayer(
                playerObject: playerObject,
                newPosition: iceLocation.position
            );


            playerObject.transform.LookAt(lookAtTarget);
            playerObject.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.IsOnIce = true;
            playerObject.GetComponent<CharacterNPC.v2.Character>().Parameters.Status.IsCurling = true;
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

        /************************************************************************************************************************/

        public void UpdateSweeperStoneTracking(
            CurlingStone stone,
            CurlingTeam team
        )
        {
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
        
        /************************************************************************************************************************/
        /// <summary>
        /// Changing Turns
        /// </summary>

        public CurlingTeam GetCurrentTeam()
        {
            return CurlingManager._instance.Parameters.Teams.currentTeam;
        }


        /************************************************************************************************************************/

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




/// possibly useful code from previous versions:


// InitializePlayerCurlingParameters(
        // //     curlingPlayer: teamHome.thrower, 
        // //     curlingPosition: CurlingPlayerPosition.Thrower,
        // //     initialPosition: CurlingManager._instance.Parameters.Course.idleLocationsTeamHome[0].position
        // // );
        // // InitializePlayerCurlingParameters(
        // //     curlingPlayer: teamHome.sweeperLeft, 
        // //     curlingPosition: CurlingPlayerPosition.SweeperLeft,
        // //     initialPosition: CurlingManager._instance.Parameters.Course.idleLocationsTeamHome[1].position
        // // );
        // // InitializePlayerCurlingParameters(
        // //     curlingPlayer: teamHome.sweeperRight, 
        // //     curlingPosition: CurlingPlayerPosition.SweeperRight,
        // //     initialPosition: CurlingManager._instance.Parameters.Course.idleLocationsTeamHome[2].position
        // // );
        // // InitializePlayerCurlingParameters(
        // //     curlingPlayer: teamAway.thrower, 
        // //     curlingPosition: CurlingPlayerPosition.Thrower,
        // //     initialPosition: CurlingManager._instance.Parameters.Course.idleLocationsTeamAway[0].position
        // // );
        // // InitializePlayerCurlingParameters(
        // //     curlingPlayer: teamAway.sweeperLeft, 
        // //     curlingPosition: CurlingPlayerPosition.SweeperLeft,
        // //     initialPosition: CurlingManager._instance.Parameters.Course.idleLocationsTeamAway[1].position
        // // );
        // // InitializePlayerCurlingParameters(
        // //     curlingPlayer: teamAway.sweeperRight, 
        // //     curlingPosition: CurlingPlayerPosition.SweeperRight,
        // //     initialPosition: CurlingManager._instance.Parameters.Course.idleLocationsTeamAway[2].position
        // // );

        // private void InitializePlayerCurlingParameters(
        //     GameObject curlingPlayer,
        //     CurlingPlayerPosition curlingPosition,
        //     Vector3 initialPosition

        // )
        // {
        //     CurlingManager._instance.Parameters.Teams.teamHome.thrower = Instantiate(
        //         curlingPlayer, 
        //         initialPosition,
        //         Quaternion.identity
        //     );

        //     CharacterNPC.v2.Character c = curlingPlayer.GetComponent<CharacterNPC.v2.Character>();
        //     c.Parameters.Status.IsCurling = true;
        //     c.Parameters.Curling.CurlingTeamPosition = curlingPosition;
        //     c.Parameters.Curling.lateralSpacing = sweeperLateralDistanceFromStone;
        //     c.Parameters.Curling.forwardDistance = sweeperForwardDistanceFromStone;

        //     if (curlingPosition == CurlingPlayerPosition.SweeperLeft || 
        //         curlingPosition == CurlingPlayerPosition.SweeperRight)
        //     {
        //         c.Parameters.Curling.IsAbleToSweep = true;
        //     }
        // }









/// unused code from previous versions:



            // // Make Right Sweeper look at Left Sweeper
            // team.sweeperRight.transform.LookAt(team.sweeperLeft.transform.position);
            // // Make Left Sweeper look at Right Sweeper
            // team.sweeperLeft.transform.LookAt(team.sweeperRight.transform.position);
            // // Make Thrower look at Target Zone
            // team.thrower.transform.LookAt(
            //     CurlingManager._instance.Parameters.Course.targetZone.transform.position
            // );

            // TeleportPlayer(
            //     playerObject: team.thrower,
            //     newPosition: CurlingManager._instance.Parameters.Course.throwerStartLocation.position
            // );
            
            // TeleportPlayer(
            //     playerObject: team.sweeperLeft,
            //     newPosition: CurlingManager._instance.Parameters.Course.sweeperLStartLocation.position
            // );
            // TeleportPlayer(
            //     playerObject: team.sweeperRight,
            //     newPosition: CurlingManager._instance.Parameters.Course.sweeperRStartLocation.position
            // );
            
            // // Make Right Sweeper look at Left Sweeper
            // team.sweeperRight.transform.LookAt(team.sweeperLeft.transform.position);
            // // Make Left Sweeper look at Right Sweeper
            // team.sweeperLeft.transform.LookAt(team.sweeperRight.transform.position);
            // // Make Thrower look at Target Zone
            // team.thrower.transform.LookAt(
            //     CurlingManager._instance.Parameters.Course.targetZone.transform.position
            // );

            // team.thrower.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.IsOnIce = true;
            // team.sweeperLeft.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.IsOnIce = true;
            // team.sweeperRight.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.IsOnIce = true;


        // public void PrepareSweepersToTrackActiveStone()
        // {
        //     Debug.Log("Preparing Sweepers to Track Active Stone");
        //     CurlingTeam activeTeam = 
        //         CurlingManager._instance.Parameters.Turn.CurrentTurn == CurlingGameTurnType.Home
        //             ? CurlingManager._instance.Parameters.Teams.teamHome
        //             : CurlingManager._instance.Parameters.Teams.teamAway;

        //     activeTeam.thrower.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.ActiveStone = 
        //         CurlingManager._instance.Parameters.Stones.currentStone;

        //     activeTeam.sweeperLeft.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.ActiveStone = 
        //         CurlingManager._instance.Parameters.Stones.currentStone;

        //     activeTeam.sweeperRight.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.ActiveStone = 
        //         CurlingManager._instance.Parameters.Stones.currentStone;


        //     activeTeam.thrower.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.TempTargetObject = 
        //         CurlingManager._instance.Parameters.Stones.currentStone.gameObject;

        //     activeTeam.sweeperLeft.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.TempTargetObject = 
        //         CurlingManager._instance.Parameters.Stones.currentStone.gameObject;

        //     activeTeam.sweeperRight.GetComponent<CharacterNPC.v2.Character>().Parameters.Curling.TempTargetObject = 
        //         CurlingManager._instance.Parameters.Stones.currentStone.gameObject;
        // }