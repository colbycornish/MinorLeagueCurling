using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// </summary>

namespace CurlingManagersV3
{
    public class Stones : MonoBehaviour
    {
        /// <summary>
        /// Setup the initial values
        /// </summary>

        public void Setup(
            CurlingCourseData courseData,
            CurlingTeam teamHome,
            CurlingTeam teamAway
        )
        {
            Debug.Log("Setting up Stones...");

            // Setup Home Team Stones
            List<GameObject> teamHomeStones = SetupTeamStonesInWaitingArea(
                team: teamHome,
                stoneSpawnLocations: CurlingManager._instance.Parameters.Course.stonesSpawnLocationsTeamHome
            );

            CurlingManager._instance.Parameters.Stones.stonesTeamHome = 
                teamHomeStones.ConvertAll(stone => stone.GetComponent<CurlingStone>());

            Debug.Log($"[Stones] Team Home Stones Count: {teamHomeStones.Count}");


            // Setup Away Team Stones
            List<GameObject> teamAwayStones = SetupTeamStonesInWaitingArea(
                team: teamAway,
                stoneSpawnLocations: CurlingManager._instance.Parameters.Course.stonesSpawnLocationsTeamAway
            );
            
            CurlingManager._instance.Parameters.Stones.stonesTeamAway = 
                teamAwayStones.ConvertAll(stone => stone.GetComponent<CurlingStone>());

            Debug.Log($"[Stones] Team Away Stones Count: {teamAwayStones.Count}");
        }

        public List<GameObject> SetupTeamStonesInWaitingArea(
            CurlingTeam team,
            List<Transform> stoneSpawnLocations
        )
        {
            int numStonesToCreate = stoneSpawnLocations.Count;

            List<GameObject> instantiatedStones = new List<GameObject>();
            for (int i = 0; i < numStonesToCreate; i++)
            {
                // find the spawn points for each team's stones
                Transform spawnPointA = stoneSpawnLocations[i];
                GameObject instantiatedStone = InstatiateStone(
                    stonePrefab: team.defaultStone, 
                    location: spawnPointA, 
                    teamId: team.teamId, 
                    stoneIndex: i
                );
                instantiatedStones.Add(instantiatedStone);
            }

            return instantiatedStones;
        }

         /// Setup a single stone on the ice
        public GameObject InstatiateStone(
            GameObject stonePrefab,
            Transform location,
            string teamId,
            int stoneIndex
        )
        {
            // Instantiate Team A stones
            GameObject stoneInstance = Instantiate(
                stonePrefab, 
                location.position, 
                Quaternion.identity
            );
            CurlingStone curlingStone = stoneInstance.GetComponent<CurlingStone>();

            // toDo: change id to a string
            curlingStone.teamId = teamId; // Team A
            curlingStone.stoneIndex = stoneIndex;
            curlingStone.rb = stoneInstance.GetComponent<Rigidbody>();
            curlingStone.visual = stoneInstance;

            return stoneInstance;

        }

        /// <summary>
        /// Prepare for Next Turn
        /// </summary>
        // TODO: Adjust to look for a selection from the canvas. 
        // These functions would then be a fallback state for if the selecting player
        // times out before selecting a stone.
        public List<CurlingStone> GetStonesForCurrentTeam(){
            
            if (CurlingManager._instance.Parameters.Turn.CurrentTurn == CurlingGameTurnType.Home){
                return CurlingManager._instance.Parameters.Stones.stonesTeamHome;
            }
            else {
                return CurlingManager._instance.Parameters.Stones.stonesTeamAway;
            }
        }

        // Needs to account for the initial state, with a null starting stone.
        public void UpdateCurrentStone(CurlingStone selectedStone)
        {
            CurlingManager._instance.Parameters.Stones.currentStone = selectedStone;
        }


        public void PlaceCurrentStoneInLaunchPosition()
        {
            if (
                CurlingManager._instance.Parameters.Stones.currentStone != null && 
                CurlingManager._instance.Parameters.Course.launchPoint != null
            )
            {
                CurlingManager._instance.Parameters.Stones.currentStone.Parameters.Status.IsThrown = false;
                CurlingManager._instance.Parameters.Stones.currentStone.Parameters.Status.IsInPlay = true;

                CurlingManager._instance.Parameters.Stones.currentStone.transform.position = CurlingManager._instance.Parameters.Course.launchPoint.position;
                CurlingManager._instance.Parameters.Stones.currentStone.rb.linearVelocity = Vector3.zero;
                CurlingManager._instance.Parameters.Stones.currentStone.rb.angularVelocity = Vector3.zero;
            }
            else
            {
                Debug.LogWarning("Cannot place stone in launch position: stone or launchPoint is null.");
            }
        }

        /// <summary>
        /// Check if the current stone is moving
        /// </summary>

        public bool IsCurrentStoneMoving()
        {
            
            CurlingStone cs = CurlingManager._instance.Parameters.Stones.currentStone;
            return cs.IsStoneMoving();
            // Rigidbody rb = cs.rb;
            // float threshold = 0.2f;

            // // Debug.Log($"[Stone Velocity] {rb.linearVelocity.magnitude}");
            // if (rb.linearVelocity == Vector3.zero)
            // {
            //     Debug.Log("Velocity is zero (direct comparison)");
            //     return false;
            //     // Do something when velocity is zero
            // }

            // // Method 2: Checking the magnitude
            // if (rb.linearVelocity.magnitude < threshold)
            // {
            //     Debug.Log("Velocity is near zero (magnitude)");
            //     return false;
            //     // Do something when velocity is near zero
            // }

            // // Method 3: Checking the square of the magnitude (slightly faster than magnitude)
            // if (rb.linearVelocity.sqrMagnitude < threshold * threshold)
            // {
            //     Debug.Log("Velocity is near zero (squared magnitude)");
            //     return false;
            //     // Do something when velocity is near zero
            // }

            // // Method 4: Using IsSleeping() (for more reliable check if object is at rest)
            // if (rb.IsSleeping())
            // {
            //     Debug.Log("Rigidbody is sleeping (at rest)");
            //     return false;
            //     // Do something when the rigidbody is sleeping
            // }

            // return true;
        }

        public bool IsCurrentStoneMovingForward()
        {
            CurlingStone cs = CurlingManager._instance.Parameters.Stones.currentStone;
            Rigidbody rb = cs.rb;
            Vector3 forwardVelocity = Vector3.Project(rb.linearVelocity, transform.forward);
            float threshold = 0.2f;

            // Check if the forward velocity is close to zero
            if (Mathf.Approximately(forwardVelocity.magnitude, 0f) || forwardVelocity.magnitude < threshold)
            {
                // Object is not moving forward (or very slowly)
                // Debug.Log("Object is not moving forward");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Reset various values
        /// </summary>
        
        /// clear the stones
        public void ClearExistingStones()
        {
            foreach (CurlingStone stone in FindObjectsByType<CurlingStone>(FindObjectsSortMode.None))
            {
                Destroy(stone.gameObject);
            }
        }

        public void Reset()
        {
            CurlingManager._instance.Parameters.Stones.stonesTeamHome.Clear();
            CurlingManager._instance.Parameters.Stones.stonesTeamAway.Clear();
            CurlingManager._instance.Parameters.Course.stonesSpawnLocationsTeamHome.Clear();
            CurlingManager._instance.Parameters.Course.stonesSpawnLocationsTeamAway.Clear();
            ClearExistingStones();
        }
    }   
}